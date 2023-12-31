﻿using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;

namespace Passports.Data; 

public class UnitOfWorkContextDb :IUnitOfWork {
    private readonly PassportContext _context;
    public IPassportRepository Passports { get; private set; }
    public UnitOfWorkContextDb(PassportContext context, IPassportRepository passports) {
        _context = context;
        Passports = passports;
    }
    public async Task CompleteAsync() {
        await _context.SaveChangesAsync();
    }
}