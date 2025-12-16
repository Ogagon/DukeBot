using DukeBot.Events;
using DukeBot.Startup;

var dukeBot = new DiscordBot();
var eventHandler = new DiscordEvents();
dukeBot.RegisterEventHandlers(eventHandler);
await dukeBot.StartAsync();
await Task.Delay(-1);
