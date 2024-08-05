using MediatR;
using PermissionsWeb.Domain;

public class RequestPermisoCommandHandler : IRequestHandler<RequestPermisoCommand, Permiso>
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestPermisoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Permiso> Handle(RequestPermisoCommand request, CancellationToken cancellationToken)
    {

        var oPermiso = new Permiso
        {
            NombreEmpleado = request.Nombre,
            ApellidoEmpleado = request.Apellido,
            FechaPermiso = request.Fecha,
            TipoPermisoId = request.TipoId,
        };
        await _unitOfWork.Permissions.AddAsync(oPermiso);
        await _unitOfWork.SaveChangesAsync();
        return oPermiso;
    }
}
