using Web.Features.Admins.Pages;

namespace Web.Features.Public.Pages.ObtenirPage;
using Domain.Repositories;
using FastEndpoints;
using IMapper = AutoMapper.IMapper;
public class ObtenirPageEndpoint: Endpoint<ObtenirPageRequest, PageDto>
{
    
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public ObtenirPageEndpoint(IMapper mapper, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("page/{slug}");
        AllowAnonymous();
   
    }

    public override async Task HandleAsync(ObtenirPageRequest request, CancellationToken ct)
    {
        var page = _pageRepository.GetPage(request.slug);
        await SendOkAsync(_mapper.Map<PageDto>(page), ct);
        
    }

}