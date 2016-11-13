using System.Collections.Generic;

namespace DAEnerys
{
    public class Problem
    {
        public static List<Problem> Problems = new List<Problem>();

        public ProblemTypes Type;
        public string Description;

        public Problem(ProblemTypes type, string description)
        {
            Type = type;
            Description = description;

            Log.WriteLine(Type.ToString() + ": " + description);
            Problems.Add(this);
            Program.main.AddProblem(this);
        }
    }

    public enum ProblemTypes
    {
        ERROR = 1,
        WARNING = 2,
    }
}
