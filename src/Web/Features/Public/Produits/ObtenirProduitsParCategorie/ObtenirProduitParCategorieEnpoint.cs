using Domain.Repositories;
using FastEndpoints;
using IMapper = AutoMapper.IMapper;



namespace Web.Features.Public.Produits.ObtenirProduitsParCategorie
{
    public class ObtenirProduitParCategorieEndpoint : Endpoint<ObtenirProduitParCategorieRequest, List<ProduitDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProduitRepository _produitRepository;

        public ObtenirProduitParCategorieEndpoint(IMapper mapper, IProduitRepository produitRepository)
        {
            _mapper = mapper;
            _produitRepository = produitRepository;
        }

        public override void Configure()
        {
            DontCatchExceptions();
            Get("produits/{categorie}");
            AllowAnonymous();

        }

        public override async Task HandleAsync(ObtenirProduitParCategorieRequest request, CancellationToken ct)
        {
            var produits = _produitRepository.ObtenirParCategorie(request.Categorie);
            await SendOkAsync(_mapper.Map<List<ProduitDto>>(produits), ct);

        }
    }
}