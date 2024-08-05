using MediatR;
using PermissionsWeb.Domain;

public class ModifyPermisoCommandHandler : IRequestHandler<ModifyPermisoCommand, Permiso>
{
    private readonly IUnitOfWork _unitOfWork;

    public ModifyPermisoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Permiso> Handle(ModifyPermisoCommand request, CancellationToken cancellationToken)
    {

        var oPermiso = await _unitOfWork.Permissions.GetByIdAsync(request.Id);
        if (oPermiso == null)
        {
            throw new Exception($"User with ID {request.Id} not found.");
        }

        oPermiso.NombreEmpleado = request.Nombre;
        oPermiso.ApellidoEmpleado = request.Apellido;
        oPermiso.FechaPermiso = request.Fecha;
        oPermiso.TipoPermisoId = request.TipoId;
       
        await _unitOfWork.Permissions.UpdateAsync(oPermiso);
        await _unitOfWork.SaveChangesAsync();
        return oPermiso;
    }
}
