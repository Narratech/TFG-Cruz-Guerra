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
        static int testScene(string filename)
        {
            Parser_Scene parser_Scene = new Parser_Scene();
            try 
            {
                parser_Scene.Parse(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            Console.WriteLine(parser_Scene.Name);
            Console.WriteLine(parser_Scene.TakeOffAirport);
            Console.WriteLine(parser_Scene.DestinationAirport);
            Console.WriteLine(parser_Scene.Weather);
            Parser_Event[] events = new Parser_Event[parser_Scene.Events.Length];
            foreach (string Event in parser_Scene.Events)
            {
                events[i] = new Parser_Event();
                events[i].Parse(parser_Scene.Events[i]);
              
                for (int j = 0; j < events.Length; j++)
                {
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine(events[j].TypeOfEvent);
                    Console.WriteLine(events[j].Difficulty);
                    foreach (Parser_Event.Competences comp in events[j].EventCompetences)
                    {
                        Console.WriteLine(comp);

                    }
                    Console.WriteLine("---------------------------------------------------");
                }
                foreach (string Event in parser_Scene.Events)
                {
                    Console.WriteLine(Event);
                }
            }
            return 0;     
        }

        static int testPilot(string filename)
        {
            Parser_Pilot parser_Pilot = new Parser_Pilot();
            try
            {
                parser_Pilot.Parse(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            Console.WriteLine(parser_Pilot.Name);
            Console.WriteLine(parser_Pilot.Age);
            Console.WriteLine(parser_Pilot.Experience);
            Console.WriteLine(parser_Pilot.ImageRoute);
            Console.WriteLine(parser_Pilot.BehaviourTable);
            foreach (var comp in parser_Pilot.Competences)
            {
                Console.WriteLine(comp.Key + ": " + comp.Value);
            }
            return 0;
        }

        static void Main(string[] args)
        {
            testScene("Test");
            testPilot("Test");
        }
    }
}
