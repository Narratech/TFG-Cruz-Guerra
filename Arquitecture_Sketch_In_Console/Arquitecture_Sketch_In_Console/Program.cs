using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
/*

* Escenario: PARSE FROM JSON, EXPORT TO JSON

* Nombre
* Configuracion 
- Aeropuerto de salida / llegada
- Clima
- Etc
* Lista de eventos (E1, E2 ... En-1, En) (Ex es una instancia de clase de evento)

* Eventos: READ FROM JSON, EXPORT TO JSON  

* Clase / Tipo de evento (nombre del evento, enum, lo que sea)
* Conjunto de OB's relevantes para reaccionar lo MEJOR POSIBLE 
* Dificultad (para sacar dificultad por competencia se hace la proporcion de OB's

* Pilotos: READ Piloto (Crear piloto sobre plantilla de otro piloto); EXPORT TO FILE

* Nombre y cuestiones estéticas (experiencia, edad, etc)
* Habilidades por competencia 0f <-> 1f
* El piloto tiene una carpeta donde se guardan los archivos JSON Evento -> pasos
* El piloto acierto o falla el evento entero, no por ob. 
* El piloto acierta si la media de sus habilidades por competencia 
* relacionadas con ese evento sea mayor que la dificultad del evento

* Guion: READ Escenarios, Eventos, Pilotos; GENERATE Guion; EXPORT TO FILE; EXECUTE FILE

* Elegir escenario - piloto (de momento no se hace)
* CREATE -> Copia profunda (Cuando se pueda modificar, de momento no se hace y no se deja modificar)
* Timeline generado (modificable)
* Textos, animaciones, etc (modificable)
* Este guion es lo que se ejecuta

* FOLDER especifica de piloto

* Muchos archivos JSON Evento -> pasos

* Tabla general (OB - Pasos): FILE (las respuestas cutres y sencillas)

* Lista de pasos (positivo si se tiene [el OB], negativo si NO se tiene [el OB])

* JSON competencias - OB (La compañía hace la suya) READ (VER SI LOS PILOTOS SABRÍAN USAR JSON) 
                    * (NOT SURE ABOUT THIS: WRITE desde la aplicacion para meter nuevas competencias)

* Diccionario en memoria OB - Competencias 

*/

/* Arbol de directorios (Se haria en ingles)
 ----AJ-------
 * Pilotos\
    * generalStepsForOB.json (poner un texto por defecto si se meten nuevos OB inesperados)
    * Juanito\
        * Juanito.json
        * StepsPerEvent\
            * E5.json
    * Eva\
        * Eva.json
        * StepsPerEvent\
            * E1.json
            * E23.json

 * Escenarios\
    * escenario1.json
    * escenario2.json
---------------

----Javi-------
 * Eventos\
    * E1.json
    * E2.json

 * Guiones\ (Hablar json o binario)
    * guion1.json
    * guion2.json

 * Competences.json (competencia - ob)
 -------------
 */


namespace Arquitecture_Sketch_In_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Codigo de ejemplo para serializar un piloto
            Dictionary<string, float> a = new Dictionary<string, float>();
            a.Add("Com", .5f);
            a.Add("Plt", .9f);
            Pilot p = new Pilot("Antonio Jesus", "21", "1543", "none", a);
            StreamWriter scen = new StreamWriter("Pilots/PilotTest1.json");
            scen.Write(JsonConvert.SerializeObject(p, Formatting.Indented));
            scen.Close();
            testScene("Test1.json");
            testPilot("Pilots/PilotTest1.json");
            //testTableCO();
        }

        static int testScene(string filename)
        {
            Scene myscene=null;
            try
            {
                StreamReader r = new StreamReader(filename);
                JsonSerializerSettings sett = new JsonSerializerSettings();
                sett.Formatting = Formatting.Indented;
                myscene = JsonConvert.DeserializeObject<Scene>(r.ReadToEnd(),sett);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            Console.WriteLine(myscene.Name);
            Console.WriteLine(myscene.TakeOffAirport);
            Console.WriteLine(myscene.DestinationAirport);
            Console.WriteLine(myscene.Weather);
            List<Event> events = new List<Event>();
            //foreach (string EventName in scene.Events)
            //{
            //    Event my_event;
            //    try
            //    {
            //        my_event = JsonSerializer.Deserialize<Event>(EventName);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //        return -1;
            //    }
            //    events.Add(my_event);

            //}
            //for (int j = 0; j < events.Count; j++)
            //{
            //    Console.WriteLine("---------------------------------------------------");
            //    Console.WriteLine(events[j].TypeOfEvent);
            //    Console.WriteLine(events[j].Difficulty);
            //    foreach (Competences comp in events[j].EventCompetences)
            //    {
            //        Console.WriteLine(comp);
            //    }
            //}
            Console.WriteLine("---------------------------------------------------");
            return 0;
        }

        static int testPilot(string filename)
        {
            Pilot pilot;
            try
            {
                StreamReader read = new StreamReader(filename);
                pilot = JsonConvert.DeserializeObject<Pilot>(read.ReadToEnd());
                read.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            Console.WriteLine(pilot.Name);
            Console.WriteLine(pilot.Age);
            Console.WriteLine(pilot.Experience);
            Console.WriteLine(pilot.ImageRoute);
            foreach (var comp in pilot.Competences)
            {
                Console.WriteLine(comp.Key + ": " + comp.Value);
            }
            return 0;
        }

        static int testTableCO()
        {
            //Table_CompetencesToOB parser_Table = new Table_CompetencesToOB();
            //try
            //{
            //    parser_Table.Parse("TableCompetenceToOB");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    return -1;
            //}

            //foreach(KeyValuePair<Competences, List<OB>> c in parser_Table.Table)
            //{
            //    Console.WriteLine(c.Key);
            //    foreach(OB oB in c.Value)
            //    {
            //        Console.WriteLine(oB);
            //    }
            //    Console.WriteLine("---------------------------------------------------");
            //}
            return 0;
        }
    }
}
