using MediatR;
using Nest;
using PermissionsWeb.Domain;
using System.Security;

public class ModifyPermisoCommandHandler : IRequestHandler<ModifyPermisoCommand, Permiso>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticClient _elasticClient;

    public ModifyPermisoCommandHandler(IElasticClient elasticClient, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _elasticClient = elasticClient;
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

        // Actualizar en Elasticsearch
        var response = await _elasticClient.UpdateAsync<Permiso>(request.Id, u => u
            .Index("permissions")
            .Doc(new Permiso
            {
                NombreEmpleado = oPermiso.NombreEmpleado,
                ApellidoEmpleado = oPermiso.ApellidoEmpleado,
                FechaPermiso = oPermiso.FechaPermiso,
                TipoPermisoId=oPermiso.TipoPermisoId,
                Id=oPermiso.Id
            })
        );

        if (!response.IsValid)
        {
            throw new Exception("Updating failed in Elasticsearch.");
        }

        return oPermiso;
    }
}
