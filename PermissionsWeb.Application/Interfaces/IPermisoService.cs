using PermissionsWeb.Domain;

namespace PermissionsWeb.Application.Interfaces
{
    public interface IPermisoService
    {
        Task<Permiso> RequestPermisoAsync(Permiso permiso);
        Task<Permiso> ModifyPermisoAsync(int id, Permiso permiso);
        Task<IEnumerable<Permiso>> GetPermisosAsync();
        Task<Permiso> GetPermisoByIdAsync(int id);
    }
}
