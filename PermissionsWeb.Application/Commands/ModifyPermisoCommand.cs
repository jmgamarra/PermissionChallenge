using MediatR;
using PermissionsWeb.Domain;

public class ModifyPermisoCommand : IRequest<Permiso>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
