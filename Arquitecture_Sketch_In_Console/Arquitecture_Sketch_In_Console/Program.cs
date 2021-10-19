using System;

/*
 
 * Escenario: EXPORT TO FILE

     * Nombre
     * Configuracion 
        - Aeropuerto de salida / llegada
        - Clima
        - Etc
     * Lista de eventos (E1, E2 ... En-1, En) (Ex es una instancia de clase de evento)
 
 * Eventos: EXPORT TO FILE 
 
     * Clase / Tipo de evento (nombre del evento, enum, lo que sea)
     * Conjunto de competencias relevantes para reaccionar lo MEJOR POSIBLE 
     * Dificultad

 * Pilotos: READ Piloto (Crear piloto sobre plantilla de otro piloto); EXPORT TO FILE

    * Nombre y cuestiones estéticas (experiencia, edad, etc)
    * Habilidades por competencia 0f <-> 1f
    * IDEA: el piloto puede tener una tabla especifica que sobrepone la general (como montar un disco sobre otro)
 
 * Guion: READ Escenarios, Eventos, Pilotos; GENERATE Guion; EXPORT TO FILE; EXECUTE FILE

    * Elegir escenario - piloto (de momento no se hace)
    * CREATE -> Copia profunda (Cuando se pueda modificar, de momento no se hace y no se deja modificar)
    * Timeline generado (modificable)
    * Textos, animaciones, etc (modificable)
    * Este guion es lo que se ejecuta

 
 * Tabla general (Competencia - BI): FILE
    
    * Relaciona competencia con lista de BI relacionados 
    * Tipo de reaccion BUENA
    
 * Tabla general (BI - Pasos): FILE
    
    * Lista de pasos (positivo si se tiene [el BI], negativo si NO se tiene [el BI])

 */


namespace Arquitecture_Sketch_In_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
