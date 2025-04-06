using Web.Features.Admins.Pages;

namespace Web.Features.Public.Pages;
using Domain.Repositories;
using FastEndpoints;
using Web.Dtos;
using IMapper = AutoMapper.IMapper;
public class GetAllPagesEndpoint: EndpointWithoutRequest<List<PageDto>>
{


    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public GetAllPagesEndpoint(IMapper mapper, IPageRepository pagerepository)
    {
        _mapper = mapper;
        _pageRepository = pagerepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        
        Get("Pages");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var pages = _pageRepository.GetAll();
        
        await SendOkAsync(_mapper.Map<List<PageDto>>(pages), ct);
    }

}
