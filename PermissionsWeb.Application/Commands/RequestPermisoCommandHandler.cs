using MediatR;
using PermissionsWeb.Application.Interfaces;
using PermissionsWeb.Domain;
using System.Security;

public class RequestPermisoCommandHandler : IRequestHandler<RequestPermissionCommand, Permiso>
{
    private readonly IPermisoService _permisoService;

    public RequestPermisoCommandHandler(IPermisoService permissionService)
    {
        _permisoService = permissionService;
    }

    public async Task<Permiso> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var oPermiso = new Permiso
        {
            NombreEmpleado = request.Name,
            ApellidoEmpleado = request.Description,
            FechaPermiso = DateTime.UtcNow,
            TipoPermisoId=12
        };

        return await _permisoService.RequestPermisoAsync(oPermiso);
    }
}
