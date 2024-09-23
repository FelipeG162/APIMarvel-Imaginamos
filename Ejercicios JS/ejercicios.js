// Función para encontrar el número máximo en un array
// Explicación: Esta función toma un array de números como entrada y recorre cada número para encontrar el más grande.
// Empezamos asumiendo que el primer número es el mayor. Luego, comparamos cada número en el array con el actual "máximo".
// Si encontramos uno mayor, actualizamos el valor de "max". Al final, devolvemos el número máximo.
function findMax(numbers) {
    let max = numbers[0]; // Suponemos que el primer número es el máximo
    for (let i = 1; i < numbers.length; i++) { // Recorremos el array desde el segundo elemento
        if (numbers[i] > max) { // Si encontramos un número mayor
            max = numbers[i]; // Actualizamos el valor de "max"
        }
    }
    return max; // Devolvemos el número más grande
}

// Ejemplo de uso:
console.log(findMax([3, 7, 2, 9, 5])); // Debería devolver 9


// Función para verificar si una palabra es un palíndromo
// Explicación: Un palíndromo es una palabra que se lee igual hacia adelante y hacia atrás (como "radar").
// Esta función limpia la palabra quitando caracteres que no son letras o números, luego compara la palabra original con su versión al revés.
// Si ambas versiones son iguales, la palabra es un palíndromo.
function isPalindrome(word) {
    let cleanedWord = ''; // Creamos una nueva cadena vacía donde almacenaremos solo letras y números
    for (let i = 0; i < word.length; i++) { // Recorremos cada carácter de la palabra
        let char = word[i].toLowerCase(); // Convertimos cada carácter a minúscula para que la comparación no sea sensible a mayúsculas
        if (/[a-z0-9]/.test(char)) { // Solo agregamos letras y números, ignoramos otros caracteres
            cleanedWord += char;
        }
    }

    let reversedWord = ''; // Ahora creamos una nueva cadena para almacenar la versión al revés
    for (let i = cleanedWord.length - 1; i >= 0; i--) { // Recorremos la palabra desde el final hacia el principio
        reversedWord += cleanedWord[i]; // Agregamos cada letra a la nueva cadena
    }

    return cleanedWord === reversedWord; // Devolvemos true si la palabra original es igual a la palabra invertida
}

// Ejemplo de uso:
console.log(isPalindrome("radar")); // Debería devolver true
console.log(isPalindrome("hello")); // Debería devolver false


// Función FizzBuzz
// Explicación: FizzBuzz es un ejercicio clásico. La función recorre los números del 1 al 100.
// Imprime "Fizz" si el número es divisible por 3, "Buzz" si es divisible por 5, y "FizzBuzz" si es divisible por ambos.
// Si no es divisible por ninguno, imprime el número.
function fizzBuzz() {
    for (let i = 1; i <= 100; i++) { // Recorremos los números del 1 al 100
        if (i % 3 === 0 && i % 5 === 0) { // Si el número es divisible por 3 y 5
            console.log("FizzBuzz"); // Imprime "FizzBuzz"
        } else if (i % 3 === 0) { // Si solo es divisible por 3
            console.log("Fizz"); // Imprime "Fizz"
        } else if (i % 5 === 0) { // Si solo es divisible por 5
            console.log("Buzz"); // Imprime "Buzz"
        } else {
            console.log(i); // Si no es divisible por ninguno, imprime el número
        }
    }
}

// Ejemplo de uso:
fizzBuzz(); // Imprime Fizz, Buzz, y FizzBuzz según corresponda


// Función para invertir una cadena
// Explicación: Esta función toma una cadena de texto y devuelve una nueva cadena con los caracteres en orden inverso.
// Recorremos la cadena desde el final hacia el principio y agregamos cada carácter a una nueva cadena.
function reverseString(str) {
    let reversed = ''; // Creamos una nueva cadena vacía donde almacenaremos la cadena invertida
    for (let i = str.length - 1; i >= 0; i--) { // Recorremos la cadena desde el final hasta el principio
        reversed += str[i]; // Agregamos cada carácter a la nueva cadena
    }
    return reversed; // Devolvemos la cadena invertida
}

// Ejemplo de uso:
console.log(reverseString("hello")); // Debería devolver "olleh"


// Función para contar las vocales en una cadena
// Explicación: Esta función cuenta cuántas vocales (a, e, i, o, u) hay en una cadena de texto.
// Recorremos cada carácter de la cadena y comprobamos si es una vocal.
// Si es una vocal, incrementamos un contador.
function countVowels(str) {
    let vowels = 'aeiou'; // Definimos las vocales que queremos contar
    let count = 0; // Inicializamos el contador en 0
    for (let i = 0; i < str.length; i++) { // Recorremos cada carácter de la cadena
        if (vowels.indexOf(str[i].toLowerCase()) !== -1) { // Si el carácter es una vocal
            count++; // Incrementamos el contador
        }
    }
    return count; // Devolvemos el número total de vocales
}

// Ejemplo de uso:
console.log(countVowels("hello world")); // Debería devolver 3
