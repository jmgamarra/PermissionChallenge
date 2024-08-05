using MediatR;
using Nest;
using PermissionsWeb.Domain;
using System.Security;

public class RequestPermisoCommandHandler : IRequestHandler<RequestPermisoCommand, Permiso>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticClient _elasticClient;

    public RequestPermisoCommandHandler(IElasticClient elasticClient, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _elasticClient = elasticClient;
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

        // Guardar en Elasticsearch
        var response = await _elasticClient.IndexDocumentAsync(new Permiso
        {
            Id = oPermiso.Id,
            ApellidoEmpleado = oPermiso.ApellidoEmpleado,
            NombreEmpleado = oPermiso.NombreEmpleado,
            FechaPermiso = oPermiso.FechaPermiso,
            TipoPermisoId = oPermiso.TipoPermisoId
        });

        if (!response.IsValid)
        {
            throw new Exception("Saving failed in Elasticsearch.");
        }

        return oPermiso;
    }
}
