namespace VKInternshipTask.Application.Common.Exceptions
{
    public class ConflictActionException : Exception
    {
        public ConflictActionException(string message)
            : base(message) { }
    }
}
