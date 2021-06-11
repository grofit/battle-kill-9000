namespace BK9K.Game.AI.Applicators
{
    public class ApplicatorPriorities
    {
        public static int Unknown = 0;

        public static int Local = 1;
        public static int DependenciesOnLocal = 2;
        public static int DependenciesOnExternal = 3;
    }
}