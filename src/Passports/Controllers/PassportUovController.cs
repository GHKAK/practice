using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;
using System.Diagnostics;
using Passports.Data;
using Passports.Repositories.Interfaces;

namespace Passports.Controllers;

[ApiController]
[Route("[controller]")]
public class PassportUovController : ControllerBase {
    private readonly IUnitOfWork _unitOfWork;
    readonly Stopwatch sw = new();

    public PassportUovController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("MigratePassports")]
    public async Task<IActionResult> MigratePassports() {
        await _unitOfWork.Passports.MigrateFromFile();
        return Ok();
    }
}