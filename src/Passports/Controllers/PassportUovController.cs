﻿using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;
using System.Diagnostics;
using Newtonsoft.Json;
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
        try {
            var passport = await _unitOfWork.Passports.GetBySeriesNumber(series, number);
            return Ok($"{passport.Series} {passport.Number} {sw.Elapsed}");
        } catch {
            return NotFound("This passport doesn't exist in db");
        }
    }

    [HttpGet("GetActiveUnactiveCount")]
    public async Task<IActionResult> GetActiveUnactiveCount() {
        sw.Restart();
        var countActual = await _unitOfWork.Passports.CountActual(true);
        var countNotActual = await _unitOfWork.Passports.CountActual(false);
        sw.Stop();
        return Ok($"{countActual} active and {countNotActual} unactive passports found in {sw.Elapsed}");
    }

    [HttpGet("GetPassportHistory")]
    public async Task<IActionResult> GetPassportHistory(short series, int number) {
        var passportHistory = await _unitOfWork.Passports.GetPassportHistory(series, number);
        if (passportHistory.Count != 0) {
            return Ok(JsonConvert.SerializeObject(passportHistory));
        } else {
            return NotFound("This passport doesn't exist in db");
        }
    }
}