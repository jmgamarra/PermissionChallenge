using Microsoft.AspNetCore.Mvc;
using PermissionsWeb.Application.DTOs;
using PermissionsWeb.Application.Interfaces;
using PermissionsWeb.Application.ViewModel;
using PermissionsWeb.Domain;
using System.Security;

[ApiController]
[Route("api/[controller]")]
public class PermisosController : ControllerBase
{
    private readonly IPermisoService _permissionService;

    public PermisosController(IPermisoService permisoService)
    {
        _permissionService = permisoService;
    }

    [HttpPost]
    public async Task<IActionResult> RequestPermiso([FromBody] PermisoDTO permisoRequest)
    {
        var oPermission = new Permiso
        {
            NombreEmpleado=permisoRequest.Nombre,
            ApellidoEmpleado=permisoRequest.Apellido,
            FechaPermiso=permisoRequest.Fecha,
            TipoPermisoId=permisoRequest.TipoPermiso
        };
        var createdPermission = await _permissionService.RequestPermisoAsync(oPermission);
        return CreatedAtAction(nameof(GetPermisoById), new { id = createdPermission.Id }, createdPermission);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyPermiso(int id, [FromBody] Permiso permission)
    {
        var updatedPermission = await _permissionService.ModifyPermisoAsync(id, permission);
        return Ok(updatedPermission);
    }

    [HttpGet]
    public async Task<IActionResult> GetPermisos()
    {
        var permissions = await _permissionService.GetPermisosAsync();
        return Ok(permissions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermisoById(int id)
    {
        var permission = await _permissionService.GetPermisoByIdAsync(id);
        if (permission == null)
        {
            return NotFound();
        }

        return Ok(permission);
    }
}
