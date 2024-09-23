using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;
using API_MARVEL.Clases;

namespace API_MARVEL.Services
{
    // Servicio que interactúa con la API de Marvel para obtener personajes
    public class MarvelService
    {
        // Cliente HTTP utilizado para realizar peticiones a la API
        private readonly HttpClient _httpClient;
        // Clave API para autenticarse con la API de Marvel
        private readonly string _apiKey;
        // Hash para la autenticación con la API de Marvel
        private readonly string _apiKeyHash;
        // URL base de la API de Marvel
        private readonly string _apiUrl;
        // Constructor del servicio, inicializa el HttpClient y obtiene las configuraciones necesarias
        public MarvelService()
        {
            _httpClient = new HttpClient();
            _apiKey = ConfigurationManager.AppSettings["MarvelApiKey"];
            _apiKeyHash = ConfigurationManager.AppSettings["MarvelHash"];
            _apiUrl = ConfigurationManager.AppSettings["MarvelApiUrl"];
        }

        // Método asíncrono que obtiene una lista de personajes aleatorios de la API
        // 'count' es el número de personajes aleatorios que se desea obtener
        public async Task<List<Character>> GetRandomCharactersAsync(int count)
        {
            var allCharacters = new List<Character>(); // Lista que almacenará todos los personajes obtenidos
            int maxResults = 200; // Limitar a 200 personajes en total - Para Evitar desbordamiento de memoria

            var offset = 0;
            var limit = 100; // La API permite un máximo de 100 resultados por solicitud

            // Ciclo para obtener los personajes de la API hasta alcanzar el límite deseado
            while (allCharacters.Count < maxResults)
            {
                // Construye la URL de la petición con el offset y limit especificados
                var urlPeticion = $"{_apiUrl}?ts=1&apikey={_apiKey}&hash={_apiKeyHash}&limit={limit}&offset={offset}";

                // Realiza la solicitud GET a la API
                var response = await _httpClient.GetAsync(urlPeticion);
                response.EnsureSuccessStatusCode(); // Verifica que la respuesta sea exitosa

                // Lee el contenido de la respuesta en formato JSON
                var content = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(content); // Parsea el JSON
                var results = (JArray)data["data"]["results"]; // Extrae el array de resultados (personajes)

                // Itera sobre los personajes obtenidos en esta solicitud
                foreach (var character in results)
                {
                    var imageUrl = $"{character["thumbnail"]["path"]}.{character["thumbnail"]["extension"]}";

                    // Omite el personaje si la imagen es "image_not_available.jpg" o "4c002e0305708.gif"
                    if (imageUrl.Contains("image_not_available.jpg") || imageUrl.Contains("4c002e0305708.gif"))
                    {
                        continue; // Pasa al siguiente personaje
                    }

                    // Crea un objeto Character con los datos obtenidos
                    var characterObj = new Character
                    {
                        Name = character["name"].ToString(),
                        Description = character["description"].ToString(),
                        ImageUrl = imageUrl
                    };
                    allCharacters.Add(characterObj); // Añade el personaje a la lista

                    // Si ya se alcanzó el límite de personajes, rompe el ciclo
                    if (allCharacters.Count >= maxResults)
                    {
                        break;
                    }
                }

                // Si el número de resultados es menor al límite, significa que no hay más personajes por obtener
                if (results.Count < limit)
                {
                    break;
                }

                // Incrementa el offset para obtener el siguiente conjunto de personajes
                offset += limit;
            }

            // Mezcla aleatoriamente la lista de personajes y selecciona la cantidad deseada ('count')
            var random = new Random();
            return allCharacters.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}
