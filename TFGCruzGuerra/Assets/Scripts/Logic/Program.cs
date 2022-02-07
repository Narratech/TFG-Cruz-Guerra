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


namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            testScript();
        }
        static void testTableOBSTEPS(string pilot, string Event)
        {
            Console.WriteLine(Event);
            StreamReader read = new StreamReader("Pilots/" + pilot + "/StepsPerEvent/" + Event);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.TypeNameHandling = TypeNameHandling.Objects;
            Table_OB_Steps steps = JsonConvert.DeserializeObject<Table_OB_Steps>(read.ReadToEnd(), settings);
            List<Step> stepList = null;
            Console.WriteLine("----------------BIEN-----------------------");
            foreach (Step step in stepList)
            {
                //step.Play(null);
            }
            Console.WriteLine("----------------MAL-----------------------");
            stepList = null;
            foreach (Step step in stepList)
            {
                //step.Play(null);
            }
            read.Close();
            Console.WriteLine("\n\n--------------------");

        }

        static int testScene(string filename)
        {
            Stage myscene = JsonManager.ImportFromJSON<Stage>("Scenes/" + filename);
            Console.WriteLine(myscene.Name);
            Console.WriteLine(myscene.TakeOffAirport);
            Console.WriteLine(myscene.DestinationAirport);
            Console.WriteLine(myscene.Weather);
            foreach (Event SceneEvent in myscene.Events)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(SceneEvent.TypeOfEvent);
                Console.WriteLine(SceneEvent.Difficulty);
                foreach (string comp in SceneEvent.OBs)
                {
                    Console.WriteLine(comp);
                }
                Console.WriteLine("---------------------------------------------------");

            }
            return 0;
        }

        static int testPilot(string filename)
        {
            Pilot pilot;
            StreamReader read = new StreamReader("Pilots/" + filename + "/" + filename + ".json");
            try
            {
                pilot = JsonConvert.DeserializeObject<Pilot>(read.ReadToEnd());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            read.Close();
            Console.WriteLine(pilot.Name);
            Console.WriteLine(pilot.Age);
            Console.WriteLine(pilot.Experience);
            Console.WriteLine(pilot.Gender);
            foreach (var comp in pilot.Competences)
            {
                Console.WriteLine(comp.Key + ": " + comp.Value);
            }
            Console.WriteLine("-----------------EVENTOS--------------");
            DirectoryInfo dir = new DirectoryInfo("Pilots/" + filename + "/StepsPerEvent");
            foreach (FileInfo file in dir.GetFiles())
            {
                testTableOBSTEPS(filename, file.Name);
            }
            return 0;

        }

        static void testScript()
        {
            Stage s = JsonManager.ImportFromJSON<Stage>("Stages/Test1");
            Pilot p = JsonManager.ImportFromJSON<Pilot>("Pilots/Javi/Javi");
            Pilot cp = JsonManager.ImportFromJSON<Pilot>("Pilots/Antonio/Antonio");
            Table_CompetencesToOB tcob = JsonManager.ImportFromJSON<Table_CompetencesToOB>("Tables/TableCompetenceToOB");
            Table_OB_Steps obs = JsonManager.ImportFromJSON<Table_OB_Steps>("Tables/TableOBtoSteps", true);

            Script script = new Script();

            script.Create(s, p, cp, tcob, obs, null, Source.Captain);

            script.ExportToJSON("Scripts/Script1", true);
        }
    }
}
