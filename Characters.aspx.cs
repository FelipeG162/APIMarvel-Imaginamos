using API_MARVEL.Clases;
using API_MARVEL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace API_MARVEL
{
    /// <summary>
    /// Página que muestra una lista paginada de personajes de Marvel.
    /// </summary>
    public partial class Characters : System.Web.UI.Page
    {
        // Número de personajes a mostrar por página
        private const int CharactersPerPage = 10;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener la página actual desde el QueryString, si no está presente, se establece en 1
                int pageNumber = int.TryParse(Request.QueryString["page"], out int page) ? page : 1;

                // Verificar si ya tenemos personajes almacenados en la sesión
                if (Session["Characters"] == null)
                {
                    var marvelService = new MarvelService();
                    // Obtener una lista de 50 personajes aleatorios desde la API de Marvel
                    var characters = await marvelService.GetRandomCharactersAsync(50);
                    Session["Characters"] = characters;
                }

                // Obtener la lista de personajes almacenados en la sesión
                var charactersList = (List<Character>)Session["Characters"];
                // Obtener el filtro desde el QueryString
                var filter = Request.QueryString["filter"] ?? string.Empty;
                // Filtrar personajes que contienen el texto del filtro en el nombre o descripción
                var filteredCharacters = charactersList
                    .Where(c => c.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                c.Description.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                // Calcular el número total de personajes y el número total de páginas
                int totalCharacters = filteredCharacters.Count;
                int totalPages = (int)Math.Ceiling((double)totalCharacters / CharactersPerPage);

                // Ajustar el número de página para que esté dentro del rango válido
                if (pageNumber < 1) pageNumber = 1;
                if (pageNumber > totalPages) pageNumber = totalPages;

                // Obtener la lista de personajes para la página actual
                var charactersForPage = filteredCharacters
                    .Skip((pageNumber - 1) * CharactersPerPage)
                    .Take(CharactersPerPage)
                    .ToList();

                // Construir el HTML para mostrar los personajes
                StringBuilder sb = new StringBuilder();
                foreach (var character in charactersForPage)
                {
                    var imageUrl = character.ImageUrl;
                    var name = character.Name;
                    var description = string.IsNullOrEmpty(character.Description) ? "Descripción no disponible" : character.Description;

                    sb.Append($"<div class='character-card'>");
                    sb.Append($"<img src='{imageUrl}' alt='{name}' width='150' height='150' />");
                    sb.Append($"<h3>{name}</h3>");
                    sb.Append($"<p>{description}</p>");
                    sb.Append($"</div>");
                }

                // Establecer el HTML generado en el contenedor de personajes
                charactersGrid.InnerHtml = sb.ToString();

                // Construir los enlaces de paginación
                StringBuilder pagination = new StringBuilder();
                for (int i = 1; i <= totalPages; i++)
                {
                    pagination.Append($"<a href='Characters.aspx?page={i}&filter={HttpUtility.UrlEncode(filter)}' class='pagination-link'>{i}</a>");
                }

                // Establecer el HTML generado en el contenedor de paginación
                paginationContainer.InnerHtml = pagination.ToString();
            }
        }
    }
}
