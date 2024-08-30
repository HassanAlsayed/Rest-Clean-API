namespace HR.LeaveManagmant.Application.Exceptions;

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            
        }
    }

