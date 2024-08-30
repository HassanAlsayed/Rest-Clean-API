namespace HR.LeaveManagmant.Application.Exceptions;

public partial class NotFoundException : Exception
{
    public NotFoundException(string name, Object key) : base($"{name} ({key}) was not found")
    {

    }
}
