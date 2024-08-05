using MediatR;
using PermissionsWeb.Application.Interfaces;
using PermissionsWeb.Domain;
using System.Security;

public class GetPermisosQueryHandler : IRequestHandler<GetPermisosQuery, IEnumerable<Permiso>>
{
    private readonly IPermisoService _permisoService;

    public GetPermisosQueryHandler(IPermisoService permisoService)
    {
        _permisoService = permisoService;
    }

    public async Task<IEnumerable<Permiso>> Handle(GetPermisosQuery request, CancellationToken cancellationToken)
    {
        return await _permisoService.GetPermisosAsync();
    }
}
