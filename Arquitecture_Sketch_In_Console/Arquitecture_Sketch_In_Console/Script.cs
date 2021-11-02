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

        private Pilot captain_, firstOfficer_,radio_,current_,starter_;

        Script()
        {
            current_ = null;
            starter_ = null;
            scene_ = null;
            captain_ = null;
            radio_ = null;
            firstOfficer_ = null;
        }

        public int Create(Scene scene, Pilot captain, Pilot firstOfficer)
        {
            scene_ = scene;
            captain_ = captain;
            firstOfficer_ = firstOfficer;

            foreach(Event e in scene.Events)
            {
                //Mirar s
            }


            return 0;
        }
        public void setCurrent(Source s)
        {
            //Si ves una forma más eficiente de hacerlo cámbialo
            switch (s)
            {
                case Source.Pilot:
                    current_ = captain_;
                    break;
                case Source.Copilot:
                    current_ = firstOfficer_;
                    break;
                case Source.radio:
                    current_ = radio_;
                    break;
                default:
                    break;
            }
            //otras cosas necesarias para cambiar de current
        }
        public void setStarter(Source starter)
        {
            //lo mismo que en el otro método
            switch (starter)
            {
                case Source.Pilot:
                    starter_ = captain_;
                    break;
                case Source.Copilot:
                    starter_ = firstOfficer_;
                    break;
                case Source.radio:
                    starter_ = radio_;
                    break;
                default:
                    break;
            }

        }
    }
}
