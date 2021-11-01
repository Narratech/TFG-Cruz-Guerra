using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * FORMAT
 * JSON
 * 
 * Array of steps
    * Text, anim, button, etc..
 */

/*
 * Recibe escenario y pilotos
 * 
 * Genera una lista de pasos por evento y piloto
 * 
    * Existe pasos personalizados del piloto para el evento X?
        * Si: Uso esos pasos (Los leo yo)
        * No: Uso la tabla generica de ob -> pasos
 * 
 * Lo exporta a json (o en memoria se juega)
 * 
 */

namespace Arquitecture_Sketch_In_Console
{
    class Script : JsonManager
    {
        private Scene scene_;

        private Pilot captain_, firstOfficer_;

        Script()
        {
            scene_ = null;
            captain_ = null;
            firstOfficer_ = null;
        }

        public int Create(Scene scene, Pilot captain, Pilot firstOfficer)
        {
            scene_ = scene;
            captain_ = captain;
            firstOfficer_ = firstOfficer;

            foreach(Event e in scene.EventsEvents)
            {
                //Mirar s
            }


            return 0;
        }
    }
}
