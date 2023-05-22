namespace Passports.Data; 

public struct ConflictStrings {
    public string Start { get; set; }
    public string End { get; set; }

    public ConflictStrings(string start, string end) {
        Start = start;
        End = end;
    }
}