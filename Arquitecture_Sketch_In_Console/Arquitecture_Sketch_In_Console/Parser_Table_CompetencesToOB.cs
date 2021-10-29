using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
    class Parser_Table_CompetencesToOB : Parser
    {
        //Dictionary of Competence to a list of OB, which they can be either positive (bool as TRUE) or negative (bool as FALSE)
        public Dictionary<Competences, List<Tuple<OB, bool>>> Table { get; private set; }

        public Parser_Table_CompetencesToOB()
        {
            Table = new Dictionary<Competences, List<Tuple<OB, bool>>>();
        }

        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".tabco");

            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(' ');

                Competences c;
                if (!Enum.TryParse<Competences>(line[0], out c))
                    throw new Exception("Competence " + line[0] + " not specified correctly");

                Table[c] = new List<Tuple<OB, bool>>();

                int n = 0;
                if(!int.TryParse(line[1], out n))
                    throw new Exception("Number of OB of " + line[0] + " not specified correctly");

                for(int i = 0; i < n; ++i)
                {
                    if(reader.EndOfStream)
                        throw new Exception("OB(s) missing or N too large from  " + line[0] + " competence");

                    //OB o;
                    //string ob = reader.ReadLine();
                    //if (!Enum.TryParse<OB>(ob, out o))
                    //    throw new Exception("OB " + ob + " doesn't exist");

                    //if(Table[c].Contains(o))
                    //    throw new Exception("OB " + ob + " alredy defined in competence " + line[0]);

                    //Table[c].Add(o);
                }
            }

            reader.Close();
        }
    }
}
