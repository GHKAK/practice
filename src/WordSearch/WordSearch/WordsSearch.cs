using System.Text.RegularExpressions;
using WordSearch;

while(true) {
    Console.WriteLine("Enter a word for search:");
    string word = Console.ReadLine();
    word = word.Trim();
    if(word==String.Empty) {
        continue;
    }
    Console.WriteLine("Enter text:");
    string input = Console.ReadLine();
    Console.WriteLine($"     {input.AllMatchesCount(word)} Matches Is Found.");
}
