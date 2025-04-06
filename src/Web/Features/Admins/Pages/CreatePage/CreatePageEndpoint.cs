using Application.Interfaces.Services.Pages;
using Domain.Common;
using Domain.Entities.Pages;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Admins.Pages.CreatePage;

public class CreatePageEndpoint: Endpoint<CreatePageRequest, SucceededOrNotResponse>
{
    private readonly IMapper _mapper;
    private readonly IPageCreationService _pageCreationService;

    public CreatePageEndpoint(IMapper mapper, IPageCreationService pageCreationService)
    {
        _mapper = mapper;
        _pageCreationService = pageCreationService;
    }

    public override void Configure()
    {
        AllowFileUploads();
        DontCatchExceptions();

        Post("/page/create");
        Roles(Domain.Constants.User.Roles.ADMINISTRATOR);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CreatePageRequest req, CancellationToken ct)
    {
        var page = _mapper.Map<Page>(req);
        await _pageCreationService.CreatePage(page);
        await SendOkAsync(new SucceededOrNotResponse(true), ct);
    }
}