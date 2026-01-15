using Identity.Data;
using Identity.Domain.Entities;
using Identity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers;

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
        var apps = await _db.Applications
            .Select(a => new ApplicationDto
            {
                Id = a.Id,
                Name = a.Name,
                ClientId = a.ClientId,
                IsActive = a.IsActive
            })
            .ToListAsync();

        return Ok(apps);
    }

    // ------------------------------------------------------------
    // GET /applications/{id}
    // ------------------------------------------------------------
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> Get(Guid id)
    {
        var app = await _db.Applications.FindAsync(id);
        if (app == null)
            return NotFound();

        return Ok(new ApplicationDto
        {
            Id = app.Id,
            Name = app.Name,
            ClientId = app.ClientId,
            IsActive = app.IsActive
        });
    }

    // ------------------------------------------------------------
    // POST /applications
    // ------------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult> Create(CreateApplicationRequest request)
    {
        var app = new Application
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ClientId = request.ClientId,
            IsActive = request.IsActive
        };

        _db.Applications.Add(app);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = app.Id }, null);
    }

    // ------------------------------------------------------------
    // PUT /applications/{id}
    // ------------------------------------------------------------
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateApplicationRequest request)
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

    // ------------------------------------------------------------
    // DELETE /applications/{id}
    // ------------------------------------------------------------
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var app = await _db.Applications.FindAsync(id);
        if (app == null)
            return NotFound();

        _db.Applications.Remove(app);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
