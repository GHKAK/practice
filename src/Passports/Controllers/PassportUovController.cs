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

    [HttpGet("GetPassportBySeriesNumber")]
    public async Task<IActionResult> GetPassportBySeriesNumber(short series, int number) {
        sw.Restart();
        
        var passport = await _unitOfWork.Passports.GetBySeriesNumber(series, number); 
        sw.Stop();
        if (passport != null) {

            return Ok($"{passport.Series} {passport.Number} {sw.Elapsed}");
        }
        return NotFound("This passport doesn't exist in db");
    }
}