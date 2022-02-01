namespace tfg.Utils
{
    /// <summary>
    /// Represents a tile for pathfinding algorithm
    /// </summary>
    public class Nodo
    {
        public int indiceCola;

        public float startTime;
        public float endTime;

        public Logic.Source source;

        public Logic.Step step;

        public Nodo(Logic.Source source, Logic.Step step)
        {
            this.source = source;
            this.step = step;
            startTime = step.startTime;
            endTime = step.startTime + step.duration;
        }

        public static bool CompareStartTime(Nodo lhs, Nodo rhs)
        {
            return lhs.startTime > rhs.startTime;
        }

        public static bool CompareEndTime(Nodo lhs, Nodo rhs)
        {
            return lhs.endTime > rhs.endTime;
        }
    }
}
