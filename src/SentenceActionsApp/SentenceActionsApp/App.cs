using SentenceActionsApp;
while(true) {
    Console.WriteLine("Enter a sentence");
    string input = Console.ReadLine();
    Console.WriteLine("Words Count : " + input.CountWords());
    Console.WriteLine("First Letter To Upper Case : " + input.FirstLettersToUpperCase());
    Console.WriteLine("Sorted : " + input.SortSentence());
    Console.WriteLine();
}