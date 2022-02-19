using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Logic
{
    public class Script : JsonManager
    {
        private Stage scene_;

        private Pilot captain_, firstOfficer_, radio_;
        private Source current_;

        [JsonProperty]
        private string sceneName, captainName, firstOfficerName;

        [JsonProperty]
        private List<Tuple<Source, Step>> steps;

        private int stepsIt;

        public Script()
        {
            current_ = Source.Captain;
            scene_ = null;
            captain_ = null;
            firstOfficer_ = null;
            radio_ = null;
            steps = new List<Tuple<Source, Step>>();

            sceneName = captainName = firstOfficerName = "";

            stepsIt = 0;
        }
        public void addStep(Source source, Step step)
        {
            steps.Add(Tuple.Create<Source, Step>(source, step));
        }
        public void addExamples()
        {
            steps.Add(new Tuple<Source, Step>(current_, new Anim("Point")));
            steps.Add(new Tuple<Source, Step>(current_, new PressButton("Air pump", PressButton.PressType.OffToOn)));
        }

        public void setCaptainName(string pilot)
        {
            captainName = pilot;
        }
        public void setFirstOfficerName(string copilot)
        {
            firstOfficerName = copilot;
        }
        public void setSceneName(string scene)
        {
            sceneName = scene;
        }
        public int Create(Stage scene, Pilot captain, Pilot firstOfficer, Table_CompetencesToOB toOB, Table_OB_Steps oB_Steps, Pilot radio = null, Source starter = Source.Captain)
        {
            scene_ = scene;
            captain_ = captain;
            firstOfficer_ = firstOfficer;
            radio_ = radio;
            current_ = starter;

            sceneName = scene_.Name;
            captainName = captain_.Name;
            firstOfficerName = firstOfficer_.Name;

            Debug.Assert(scene != null && captain != null && firstOfficer != null && toOB != null && oB_Steps != null);

            Queue<Step> steps_captain = new Queue<Step>(), steps_firstOfficer = new Queue<Step>(), steps_radio = new Queue<Step>();

            stepsIt = 0;

            float time = 0.0f;
            float defaultDuration = 1.5f; //in seconds

            foreach (Event e in scene.Events)
            {
                if (e.flightSection >= 0)
                {
                    FlightStageChange fSC = new FlightStageChange();
                    fSC.flightSection = (FlightSections)e.flightSection;
                    fSC.startTime = time;
                    fSC.duration = 1;
                    steps.Add(new Tuple<Source, Step>(Source.FlightStage, fSC));
                }
                else
                {
                    FileInfo f_captain = hasPredefined(e, captain_);
                    FileInfo f_firstOfficer = hasPredefined(e, firstOfficer_);
                    //FileInfo f_radio = hasPredefined(e, radio_);

                    if (current_ == Source.Captain)
                    {
                        if (f_captain != null)
                            fillWithPredefined(e, captain_, f_captain, toOB, ref steps_captain, ref time);
                        else
                            fillWithGeneric(e, captain_, oB_Steps, toOB, ref steps_captain, ref time, defaultDuration);

                        if (f_firstOfficer != null)
                            fillWithPredefined(e, firstOfficer_, f_firstOfficer, toOB, ref steps_firstOfficer, ref time);
                        else
                            fillWithGeneric(e, firstOfficer_, oB_Steps, toOB, ref steps_firstOfficer, ref time, defaultDuration);
                        //if (f_radio != null)
                        //    fillWithPredefined(f_radio, ref steps_radio);
                        //else
                        //    fillWithGeneric(e, ref steps_radio);
                    }
                    else if (current_ == Source.First_Officer)
                    {

                        if (f_firstOfficer != null)
                            fillWithPredefined(e, firstOfficer_, f_firstOfficer, toOB, ref steps_firstOfficer, ref time);
                        else
                            fillWithGeneric(e, firstOfficer_, oB_Steps, toOB, ref steps_firstOfficer, ref time, defaultDuration);
                        if (f_captain != null)
                            fillWithPredefined(e, captain_, f_captain, toOB, ref steps_captain, ref time);
                        else
                            fillWithGeneric(e, captain_, oB_Steps, toOB, ref steps_captain, ref time, defaultDuration);
                        //if (f_radio != null)
                        //    fillWithPredefined(f_radio, ref steps_radio);
                        //else
                        //    fillWithGeneric(e, ref steps_radio);
                    }

                    while (steps_captain.Count > 0 || steps_firstOfficer.Count > 0)
                    {
                        if (current_ == Source.Captain && steps_captain.Count == 0)
                        {  //Si al captain no le quedan steps
                           //Si al first officer si le quedan hay que seguir. Si tampoco le quedan, se pasa de evento
                            if (steps_firstOfficer.Count > 0)
                                current_ = Source.First_Officer;
                            else
                                break;
                        }
                        else if (current_ == Source.First_Officer && steps_firstOfficer.Count == 0)
                        { //Si al first officer no le quedan steps
                          //Si al captain si le quedan hay que seguir. Si tampoco le quedan, se pasa de evento
                            if (steps_captain.Count > 0)
                                current_ = Source.Captain;
                            else
                                break;
                        }

                        Step s = null;

                        switch (current_)
                        {
                            case Source.Captain:
                                s = steps_captain.Dequeue();
                                break;
                            case Source.First_Officer:
                                s = steps_firstOfficer.Dequeue();
                                break;
                            case Source.Radio:
                                break;
                        }

                        if (s is Change)
                            setCurrent(((Change)s).source_);

                        steps.Add(new Tuple<Source, Step>(current_, s));
                    }
                }

            }
            return 0;
        }


        public void setCurrent(Source s)
        {
            current_ = s;
        }


        public void setStarter(Source starter)
        {
            current_ = starter;
        }

        private FileInfo hasPredefined(Event e, Pilot p)
        {
            if (p == null)
                return null;

            string name = p.Name;

            string dir = "Assets/GameAssets/Pilots/" + name + "/StepsPerEvent/";

            if (p.Name == captain_.Name)
                dir += "Pilot";
            else
                dir += "Copilot";

            FileInfo[] files = new DirectoryInfo(dir).GetFiles();
            foreach (FileInfo file in files)
                if (file.Name == e.Name + ".json")
                    return file;

            return null;
        }

        private void fillWithGeneric(Event e, Pilot p, Table_OB_Steps oB_Steps, Table_CompetencesToOB toOB, ref Queue<Step> q, ref float time, float defaultDuration)
        {
            foreach (string OB in e.OBs)
            {
                float hability = p.Competences[toOB.getCompetenceFromOB(OB)];
                bool isGood = hability > e.Difficulty;
                List<Step> steps = oB_Steps.getStepsForOB(OB, isGood);
                if (steps != null)
                    foreach (Step s in steps)
                    {
                        s.OB = OB;
                        s.result = isGood ? Step.Result.Good : Step.Result.Bad;
                        s.startTime = time;
                        s.duration = defaultDuration;
                        time += defaultDuration;
                        q.Enqueue(s);
                    }
            }
        }

        private void fillWithPredefined(Event e, Pilot p, FileInfo f, Table_CompetencesToOB toOB, ref Queue<Step> q, ref float time)
        {
            SpecificStepsForEvent specificStepsForEvent = JsonManager.ImportFromJSON<SpecificStepsForEvent>(f.FullName, true);

            float hability = 0;
            float aux;
            foreach (string OB in e.OBs)
            {
                aux = p.Competences[toOB.getCompetenceFromOB(OB)];
                hability += aux / e.OBs.Count;
            }

            if (hability > e.Difficulty)
                foreach (Step s in specificStepsForEvent.StepsIfGood)
                {
                    q.Enqueue(s);
                    time += s.duration;
                }
            else
                foreach (Step s in specificStepsForEvent.StepsIfBad)
                {
                    q.Enqueue(s);
                    time += s.duration;
                }
        }

        /// <summary>
        /// plays the next event
        /// </summary>
        /// <returns>true if there are more steps to be played, false otherwise</returns>
        public bool Next(out Source source, out Step step)
        {
            source = Source.Captain;
            step = null;
            if (stepsIt >= steps.Count)
                return false;

            source = steps[stepsIt].Item1;
            step = steps[stepsIt].Item2;

            ++stepsIt;

            return stepsIt < steps.Count;
        }

        public int NumberOfSteps() { return steps.Count; }

    }
}