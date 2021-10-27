using System;
using System.Collections.Generic;

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

 
 * Tabla general (Competencia - OB): FILE
    
    * Relaciona competencia con lista de OB relacionados 
    * Tipo de reaccion BUENA
    
 * Tabla general (OB - Pasos): FILE
    
    * Lista de pasos (positivo si se tiene [el OB], negativo si NO se tiene [el OB])

 */


namespace Arquitecture_Sketch_In_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //testScene("Test");
            //testPilot("Test");
            testTableCO();
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
            List<Parser_Event> events = new List<Parser_Event>();
            foreach (string Event in parser_Scene.Events)
            {
                Parser_Event my_event = new Parser_Event();
                events.Add(my_event);
                try
                {
                    my_event.Parse(Event);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }

            }
            for (int j = 0; j < events.Count; j++)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(events[j].TypeOfEvent);
                Console.WriteLine(events[j].Difficulty);
                foreach (Competences comp in events[j].EventCompetences)
                {
                    Console.WriteLine(comp);
                }
            }
            Console.WriteLine("---------------------------------------------------");
            return 0;
        }

        static int testTableCO()
        {
            Parser_Table_CompetencesToOB parser_Table = new Parser_Table_CompetencesToOB();
            try
            {
                parser_Table.Parse("TableCompetenceToOB");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            
            foreach(KeyValuePair<Competences, List<OB>> c in parser_Table.Table)
            {
                Console.WriteLine(c.Key);
                foreach(OB oB in c.Value)
                {
                    Console.WriteLine(oB);
                }
                Console.WriteLine("---------------------------------------------------");
            }
            return 0;
        }
    }
}
