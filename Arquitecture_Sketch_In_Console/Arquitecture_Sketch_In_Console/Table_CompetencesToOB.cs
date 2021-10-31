using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

/*
 * FORMAT JSON

 * Competence N
     * OB1
     * OB2
     * ...
     * OBN-1
     * OBN
 * Competence N+1
     * OB1
     * OB2
     * ...
     * OBM-1
     * OBM
 */

namespace Arquitecture_Sketch_In_Console
{
    class Table_CompetencesToOB : JsonManager
    {
        //Dictionary of Competence to a list of OB
        [JsonProperty]
        private Dictionary<string, List<string>> Competences;

        [JsonIgnore]
        private Dictionary<string, string> obToComp;

        [JsonIgnore]
        private Dictionary<string, HashSet<string>> compToOBHashed;

        Table_CompetencesToOB()
        {
            Competences = null;
            compToOBHashed = null;
            obToComp = null;
        }

        protected override int Init()
        {
            compToOBHashed = new Dictionary<string, HashSet<string>>();
            obToComp = new Dictionary<string, string>();
            foreach (KeyValuePair<string, List<string>> entry in Competences)
            {
                compToOBHashed.Add(entry.Key, new HashSet<string>(entry.Value));
                foreach (string h in entry.Value)
                {
                    obToComp.Add(h, entry.Key);
                }
            }
            return 0;
        }


        /// <returns>set of all obs of that competence, null if not found</returns>
        public HashSet<string> GetOBsFromCompetence(string competence)
        {
            if (competence == null || competence == "")
                return null;

            HashSet<string> found;
            return compToOBHashed.TryGetValue(competence, out found) ? found : null;
        }
        

        /// <returns>competence of said OB, null OB not found or OB equals "None"</returns>
        public string getCompetenceFromOB(string OB)
        {
            if (OB != null && OB != "" && OB != "None")
            {
                string found;
                return obToComp.TryGetValue(OB, out found) ? found : null;
            }

            return null;
        }
    }
}
