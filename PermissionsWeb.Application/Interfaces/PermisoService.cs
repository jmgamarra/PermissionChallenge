using System.Security;
using PermissionsWeb.Domain;
using PermissionsWeb.Application.Interfaces;

public class PermisoService : IPermisoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PermisoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Permiso> RequestPermisoAsync(Permiso permission)
    {
        await _unitOfWork.Permissions.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();
        return permission;
    }

    public async Task<Permiso> ModifyPermisoAsync(int id, Permiso permission)
    {
        var existingPermission = await _unitOfWork.Permissions.GetByIdAsync(id);
        if (existingPermission == null)
        {
            // Handle not found
        }

        existingPermission.NombreEmpleado = permission.NombreEmpleado;
        existingPermission.ApellidoEmpleado = permission.ApellidoEmpleado;
        existingPermission.FechaPermiso = DateTime.UtcNow;

        await _unitOfWork.Permissions.UpdateAsync(existingPermission);
        await _unitOfWork.SaveChangesAsync();
        return existingPermission;
    }

    public async Task<IEnumerable<Permiso>> GetPermisosAsync()
    {
        return await _unitOfWork.Permissions.GetAllAsync();
    }

    public async Task<Permiso> GetPermisoByIdAsync(int id)
    {
        return await _unitOfWork.Permissions.GetByIdAsync(id);
    }
}
