using MediatR;
using PermissionsWeb.Domain;

public class RequestPermissionCommand : IRequest<Permiso>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
