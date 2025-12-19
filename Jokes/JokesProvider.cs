namespace DukeBot.Jokes
{
    public class JokesProvider
    {
        private List<Joke> jokes = new List<Joke>();
        public JokesProvider()
        {
            string currentQuestion = string.Empty;

            var basePath = AppContext.BaseDirectory;
            var filePath = Path.Combine(basePath, "Jokes", "jokes.txt");

            try
            {

                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (line.StartsWith("Q:"))
                    {
                        currentQuestion = line.Substring(2).Trim();
                    }
                    else if (line.StartsWith("A:") && currentQuestion != string.Empty)
                    {
                        var answer = line.Substring(2).Trim();
                        jokes.Add(new Joke
                        {
                            Question = currentQuestion,
                            Answer = answer
                        });
                        currentQuestion = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to load the jokes!\n" + ex.Message);
            }
        }
        public Joke GetRandomJoke()
        {
            if (jokes.Count == 0) return new Joke
            {
                Question = "Q: Is there a joke here?",
                Answer = "A: None that I can find..."
            };
            var index = Random.Shared.Next(jokes.Count);
            return jokes[index];
        }
    }
}
