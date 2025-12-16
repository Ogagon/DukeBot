using DotNetEnv;
using DukeBot.Events;
using Microsoft.Extensions.Configuration;
using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using System.Threading.Tasks;

namespace DukeBot.Startup
{
    public sealed class DiscordBot
    {
        private readonly GatewayClient _gatewayClient;

        public DiscordBot()
        {
            var token = GetToken();
            _gatewayClient = ConfigureGatewayClient(token);
        }

        public async Task StartAsync() => await _gatewayClient.StartAsync();
        public void RegisterEventHandlers(DiscordEvents handler)
        {
            handler.RegisterCommands();
            _gatewayClient.MessageCreate += handler.OnMessageCreateAsync;
            _gatewayClient.Ready +=  handler.OnReadyAsync;
        }
        private string GetToken()
        {
            Env.Load();
            var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            if (string.IsNullOrEmpty(token))
            {
               throw new InvalidOperationException("Discord token missing.");
            }
            return token;
        }
        private GatewayClient ConfigureGatewayClient(string token)
        {
            GatewayClient dukeBot = new(new BotToken(token), new GatewayClientConfiguration
            {
                Logger = new ConsoleLogger(),
                Intents = GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.MessageContent |GatewayIntents.Guilds
            });
            return dukeBot;
        }
    }
}
