using AutoMapper;

namespace Tests.Common.Mapping;

public class MapperBuilder
{
    private readonly List<Type> _profiles = new();

    public MapperBuilder WithProfile<T>()
    {
        _profiles.Add(typeof(T));
        return this;
    }

    public Mapper Build()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            foreach (var type in _profiles)
                cfg.AddProfile(type);
        });
        return new Mapper(mapperConfiguration);
    }
}