using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

/*
 * FORMAT
 * 
 * Competence N(OBs)
 * OB1 bool(True/False) (True being positive and False being negative)
 * OB2
 * ...
 * OBN-1
 * OBN
 * Competence M
 * OB1
 * OB2
 * ...
 * OBM-1
 * OBM
 */

namespace Arquitecture_Sketch_In_Console
{
    class Table_CompetencesToOB 
    {
        //Dictionary of Competence to a list of OB, which they can be either positive (bool as TRUE) or negative (bool as FALSE)
        public Dictionary<Competences, List<Tuple<OB, bool>>> Table { get; private set; }

        

       
    }
}
