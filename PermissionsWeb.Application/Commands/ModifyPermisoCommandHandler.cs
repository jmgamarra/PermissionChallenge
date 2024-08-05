using MediatR;
using PermissionsWeb.Application.Interfaces;
using PermissionsWeb.Domain;
using System.Security;

public class ModifyPermisoCommandHandler : IRequestHandler<ModifyPermisoCommand, Permiso>
{
    private readonly IPermisoService _permissionService;

    public ModifyPermisoCommandHandler(IPermisoService permissionService)
    {
        _permissionService = permissionService;
    }

    public async Task<Permiso> Handle(ModifyPermisoCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permiso
        {
            Id = request.Id,
            NombreEmpleado = request.Name,
            ApellidoEmpleado = request.Description,
            FechaPermiso = DateTime.UtcNow,
            TipoPermisoId = request.Id,
        };

        return await _permissionService.ModifyPermisoAsync(permission.Id, permission);
    }
}
