using Identity.Api.Data;
using Identity.Api.Entities;
using Identity.Contracts.Apps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("app-secrets")]
public sealed class AppSecretsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppSecretsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{appId:guid}")]
    public async Task<IEnumerable<AppSecretDto>> GetSecrets(Guid appId)
    {
        return await _db.AppSecrets
            .Where(s => s.AppId == appId)
            .Select(s => new AppSecretDto
            {
                Id = s.Id,
                Provider = s.Provider,
                ClientId = s.ClientId
            })
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<AppSecretDto>> Create(CreateAppSecretRequest dto)
    {
        var secret = new AppSecret
        {
            Id = Guid.NewGuid(),
            AppId = dto.AppId,
            Provider = dto.Provider,
            ClientId = dto.ClientId,
            ClientSecret = dto.ClientSecret
        };

        _db.AppSecrets.Add(secret);
        await _db.SaveChangesAsync();

        return new AppSecretDto
        {
            Id = secret.Id,
            Provider = secret.Provider,
            ClientId = secret.ClientId
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAppSecretRequest dto)
    {
        if (id != dto.Id) return BadRequest();

        var secret = await _db.AppSecrets.FindAsync(id);
        if (secret is null) return NotFound();

        secret.Provider = dto.Provider;
        secret.ClientId = dto.ClientId;
        secret.ClientSecret = dto.ClientSecret;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var secret = await _db.AppSecrets.FindAsync(id);
        if (secret is null) return NotFound();

        _db.AppSecrets.Remove(secret);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}