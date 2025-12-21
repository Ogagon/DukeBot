using DukeBot.Commands;
using DukeBot.Commands.ActionableCommands;
using DukeBot.Events;
using DukeBot.Features.Event_Scheduling;
using DukeBot.Features.Polling;
using DukeBot.Jokes;
using DukeBot.Quotes;
using DukeBot.Startup;

var quotesProvider = new QuotesProvider();
var jokesProvider = new JokesProvider();
var pollsProvider = new PollsProvider();
var scheduleProvider = new ScheduleEventProvider();

var router = new CommandRouter();

router.Register("quote", new QuotesCommand(quotesProvider));
router.Register("joke", new JokesCommand(jokesProvider));
router.Register("poll", new PollsCommand(pollsProvider));
router.Register("embed", new ScheduleEventCommand(scheduleProvider));
router.Register("ping", new PingCommand());
router.Register("pong", new PongCommand());
router.Register("roll", new RollCommand());
router.Register("hello", new HelloCommand());


var dukeBot = new DiscordBot();
var eventHandler = new DiscordEvents(router);
dukeBot.RegisterEventHandlers(eventHandler);
await dukeBot.StartAsync();
await Task.Delay(-1);
