namespace LabSession1.Services;

public interface IObjectMapperService
{
    TDestination Map<TSource, TDestination>(TSource source);
}