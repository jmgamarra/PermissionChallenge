using MediatR;
using PermissionsWeb.Domain;

public class GetPermisoByIdQuery : IRequest<Permiso>
{
    public int Id { get; set; }
}
