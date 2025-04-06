using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Admins.Commandes.LireTousCommandes;

public class LireTousCommandesEndpoint : EndpointWithoutRequest<List<CommandeDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommandeRepository _commandeRepository;

    public LireTousCommandesEndpoint(IMapper mapper, ICommandeRepository commandeRepository)
    {
        _mapper = mapper;
        _commandeRepository = commandeRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("admin/commandes");
        Roles(Domain.Constants.User.Roles.ADMINISTRATOR);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        // AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // On récupère toutes les commandes et on ne garde que les 10 premières.
        var commandes = _commandeRepository.LireTous().Take(10).ToList();
        await SendOkAsync(_mapper.Map<List<CommandeDto>>(commandes), ct);
    }
}