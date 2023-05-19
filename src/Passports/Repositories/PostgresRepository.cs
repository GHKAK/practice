using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Repositories.Interfaces;
using System.Text;
using Passports.Data;

namespace Passports.Repositories;

public class PostgresRepository : DbRepository, IPassportRepository {
    public PostgresRepository(PassportContext context) : base(context) {
    }
    
}