using AutoMapper;

namespace backend.Helpers;

public class PublicDTODomainMapper<TLeftObject, TRightObject> : IAppMapper<TLeftObject, TRightObject>
    where TLeftObject : class where TRightObject : class
{
    private readonly IMapper _mapper;
    
    public PublicDTODomainMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
    public TLeftObject? Map(TRightObject? inObject)
    {
        return _mapper.Map<TLeftObject>(inObject);
    }

    public TRightObject? Map(TLeftObject? inObject)
    {
        return _mapper.Map<TRightObject>(inObject);
    }
}