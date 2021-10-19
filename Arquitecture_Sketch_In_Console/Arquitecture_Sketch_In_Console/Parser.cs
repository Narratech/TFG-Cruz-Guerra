using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    /// <summary>
    /// Interfaz para objetos que saben leerse de archivo
    /// </summary>
    //todo quizá meter un to_string o algo por el estilo para guardarse
    interface Parser
    {
        /// <summary>
        /// Parsea un archivo a la representacion interna del objeto
        /// </summary>
        /// <param name="filename">Archivo donde esta almacenada la informacion del objeto</param>
        public void Parse(string filename);
    }
}
