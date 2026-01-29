using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Apps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("apps")]
public sealed class AppsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<AppDto>> GetAll()
    {
        return await _db.Apps
            .Select(a => new AppDto
            {
                Id = a.Id,
                Name = a.Name,
                ClientId = a.ClientId,
                Description = a.Description,
                Enabled = a.Enabled
            })
            .ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AppDto>> Get(Guid id)
    {
        var app = await _db.Apps.FindAsync(id);
        if (app is null) return NotFound();

        return new AppDto
        {
            Id = app.Id,
            Name = app.Name,
            ClientId = app.ClientId,
            Description = app.Description,
            Enabled = app.Enabled
        };
    }

    [HttpPost]
    public async Task<ActionResult<AppDto>> Create(CreateAppRequest dto)
    {
        var app = new App
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            ClientId = dto.ClientId,
            Description = dto.Description,
            Enabled = dto.Enabled
        };

        _db.Apps.Add(app);

        // Create secrets if provided
        void AddSecret(string provider, string? clientId, string? clientSecret)
        {
            if (!string.IsNullOrWhiteSpace(clientId) &&
                !string.IsNullOrWhiteSpace(clientSecret))
            {
                _db.AppSecrets.Add(new AppSecret
                {
                    Id = Guid.NewGuid(),
                    AppId = app.Id,
                    Provider = provider,
                    ClientId = clientId,
                    ClientSecret = clientSecret
                });
            }
        }

        AddSecret("Google", dto.GoogleClientId, dto.GoogleClientSecret);
        AddSecret("Apple", dto.AppleClientId, dto.AppleClientSecret);
        AddSecret("Microsoft", dto.MicrosoftClientId, dto.MicrosoftClientSecret);

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = app.Id }, new AppDto
        {
            Id = app.Id,
            Name = app.Name,
            ClientId = app.ClientId,
            Description = app.Description,
            Enabled = app.Enabled
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAppRequest dto)
    {
        if (id != dto.Id) return BadRequest();

        var app = await _db.Apps.FindAsync(id);
        if (app is null) return NotFound();

        app.Name = dto.Name;
        app.ClientId = dto.ClientId;
        app.Description = dto.Description;
        app.Enabled = dto.Enabled;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var app = await _db.Apps.FindAsync(id);
        if (app is null) return NotFound();

        _db.Apps.Remove(app);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}