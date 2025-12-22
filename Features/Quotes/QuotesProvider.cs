namespace DukeBot.Features.Quotes
{
    public class QuotesProvider
    {
        private readonly string[] _quotes;

        public QuotesProvider()
        {
            _quotes = LoadQuotes();
        }
        private string[] LoadQuotes()
        {
            // Load all quotes at startup
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Quotes/DukeQuotes.txt");
                if (File.Exists(path))
                    return File.ReadAllLines(path).Select(q => q.Trim()).Where(q => !string.IsNullOrWhiteSpace(q)).ToArray();
                else
                    return Array.Empty<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred while loading in the quotes: " + ex.Message);
                return new[] { "An issue occurred while pulling the quotes." };
            }
        }

        public string GetRandomQuote()
        {
            if (_quotes.Length == 0) return "I like kicking ass and chewing bubble gum! And I am ALL OUT OF bubble gum!";
            var index = Random.Shared.Next(_quotes.Length);
            return _quotes[index];
        }
    }
}
