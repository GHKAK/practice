using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passports.Models; 

public class AuditPassportEntry {
    public short Series { get; set; }
    public int Number { get; set; }
    public DateOnly ChangeDate { get; set; }
    public bool IsActual { get; set; }

}