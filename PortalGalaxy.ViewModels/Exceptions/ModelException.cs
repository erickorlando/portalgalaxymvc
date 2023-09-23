namespace PortalGalaxy.ViewModels.Exceptions;

public class ModelException : Exception
{
    public string PropertyName { get; set; }

    public ModelException(string propertyName, string message)
        : base(message)
    {
        PropertyName = propertyName;
    }
}