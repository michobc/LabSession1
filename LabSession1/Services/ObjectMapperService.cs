namespace LabSession1.Services;

public class ObjectMapperService : IObjectMapperService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var destination = Activator.CreateInstance<TDestination>();
        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.
                FirstOrDefault(p => p.Name.Equals(sourceProperty.Name, StringComparison.OrdinalIgnoreCase));
   
            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                var value = sourceProperty.GetValue(source);
                if (value != null)
                {
                    if (destinationProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destinationProperty.SetValue(destination, value);
                    }
                    else
                    {
                        try
                        {
                            var convertedValue = Convert.ChangeType(value, destinationProperty.PropertyType);
                            destinationProperty.SetValue(destination, convertedValue);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("error: "+ ex.Message);
                        }
                    }
                }
            }
        }

        return destination;
    }
}
