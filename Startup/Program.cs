using DukeBot.Commands;
using DukeBot.Commands.ActionableCommands;
using DukeBot.Events;
using DukeBot.Features.Event_Scheduling;
using DukeBot.Features.Jokes;
using DukeBot.Features.Polling;
using DukeBot.Features.Quotes;
using DukeBot.Startup;

var dukeBot = new DiscordBot();

var quotesProvider = new QuotesProvider();
var jokesProvider = new JokesProvider();
var pollsProvider = new PollsProvider();
var scheduleProvider = new ScheduleEventProvider();

var router = new CommandRouter();
var slashRouter = new SlashCommandRouter();

router.Register("quote", new QuotesCommand(quotesProvider));
router.Register("joke", new JokesCommand(jokesProvider));
router.Register("poll", new PollsCommand(pollsProvider));
router.Register("embed", new ScheduleEventCommand(scheduleProvider, dukeBot.Client));
router.Register("ping", new PingCommand());
router.Register("pong", new PongCommand());
router.Register("roll", new RollCommand());
router.Register("hello", new HelloCommand());

slashRouter.Register("schedule", new ScheduleEventCommand(scheduleProvider, dukeBot.Client));


var eventHandler = new DiscordEvents(router, slashRouter);
dukeBot.RegisterEventHandlers(eventHandler);
await dukeBot.StartAsync();
await eventHandler.RegisterSlashCommandsAsync(dukeBot.Client, dukeBot.Client.Id);
await Task.Delay(-1);

