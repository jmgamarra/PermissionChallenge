using MediatR;
using PermissionsWeb.Domain;

public class GetPermisosQueryHandler : IRequestHandler<GetPermisosQuery, IEnumerable<Permiso>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermisosQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Permiso>> Handle(GetPermisosQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Permissions.GetAllAsync();
    }
}
