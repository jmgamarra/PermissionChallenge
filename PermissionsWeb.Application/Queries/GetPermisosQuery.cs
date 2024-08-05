using MediatR;
using PermissionsWeb.Domain;

public class GetPermisosQuery : IRequest<IEnumerable<Permiso>>
{
}
