using MediatR;
using Nest;
using PermissionsWeb.Domain;

public class GetPermisosQueryHandler : IRequestHandler<GetPermisosQuery, IEnumerable<Permiso>>
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticClient _elasticClient;

    public GetPermisosQueryHandler(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<Permiso>> Handle(GetPermisosQuery request, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<Permiso>(s => s
            .Index("permissions")
            .From(0)
            .Size(1000) // Ajusta el tamaño según tus necesidades
        );

        if (!searchResponse.IsValid)
        {
            throw new Exception("Failed to retrieve documents from Elasticsearch.");
        }

        return searchResponse.Documents;
    }
}
