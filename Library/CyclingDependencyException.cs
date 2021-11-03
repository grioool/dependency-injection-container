namespace Library
{
    public class CyclingDependencyException : System.Exception
    {
        public CyclingDependencyException(string message) : base(message)
        {
        }
    }
}