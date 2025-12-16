using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using DukeBot;

var dukeBot = new DiscordBot();
var eventHandler = new DiscordEvents();
dukeBot.RegisterEventHandlers(eventHandler);
await dukeBot.StartAsync();
await Task.Delay(-1);
