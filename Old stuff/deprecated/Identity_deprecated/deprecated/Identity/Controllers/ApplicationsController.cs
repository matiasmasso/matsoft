using Azure.Core;
using Identity.Api.Infrastructure.Errors;
using Identity.Data;
using Identity.Domain.Entities;
using Identity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ApplicationsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // ------------------------------------------------------------
    // GET /applications
    // ------------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<ApplicationDto>>> GetAll()
    {
        try
        {
            var apps = await _db.Applications
                .Select(a => new ApplicationDto
                {
                    ApplicationId = a.Id,
                    Name = a.Name,
                    ClientId = a.ClientId,
                    IsActive = a.IsActive
                })
                .ToListAsync();

            return Ok(apps);
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }

    }

    // ------------------------------------------------------------
    // GET /applications/{id}
    // ------------------------------------------------------------
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> Get(Guid id)
    {
        try
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null)
                return NotFound();

            return Ok(new ApplicationDto
            {
                ApplicationId = app.Id,
                Name = app.Name,
                ClientId = app.ClientId,
                IsActive = app.IsActive
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }

    }

    // ------------------------------------------------------------
    // POST /applications
    // ------------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult> Update(ApplicationDto value)
    {
        try
        {
            var entity = await _db.Applications.FindAsync(value.ApplicationId);
            if (entity == null)
            {
                entity = new Application();
                entity.Id = value.ApplicationId!.Value;
                _db.Applications.Add(entity);
            }

            entity.Name = value.Name;
            entity.ClientId = value.ClientId;
            entity.IsActive = value.IsActive;
            await _db.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // PUT /applications/{id}
    // ------------------------------------------------------------
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateApplicationRequest request)
    {
        try
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null)
                return NotFound();

            app.Name = request.Name;
            app.ClientId = request.ClientId;
            app.IsActive = request.IsActive;

            await _db.SaveChangesAsync();
            return NoContent();

        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }
    }

    // ------------------------------------------------------------
    // DELETE /applications/{id}
    // ------------------------------------------------------------
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null)
                return NotFound();

            _db.Applications.Remove(app);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorResult.FromException(ex));
        }

    }
}
