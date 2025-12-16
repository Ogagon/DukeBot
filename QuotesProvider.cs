using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot
{
    public  class QuotesProvider
    {
        private  readonly string[] _quotes;

        public QuotesProvider()
        {
            // Load all quotes at startup
            var path = Path.Combine(AppContext.BaseDirectory, "DukeQuotes.txt");
            if (File.Exists(path))
                _quotes = File.ReadAllLines(path).Select(q => q.Trim()).Where(q => !string.IsNullOrWhiteSpace(q)).ToArray();
            else
                _quotes = Array.Empty<string>();
        }

        public string GetRandomQuote()
        {
            if (_quotes.Length == 0) return "No quotes available!";
            var index = Random.Shared.Next(_quotes.Length);
            return _quotes[index];
        }
    }
}
