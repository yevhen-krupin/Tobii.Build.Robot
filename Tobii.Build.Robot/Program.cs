using System;
using System.Threading;
using Telegram.Bot;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Rest;
using Tobii.Build.Robot.Rest.Command;
using Tobii.Build.Robot.Rest.Core;
using Tobii.Build.Robot.Telegram;
using ConfigurationProvider = Tobii.Build.Robot.Telegram.ConfigurationProvider;

namespace Tobii.Build.Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamcityConfig = new Rest.ConfigurationProvider();
            var tc = new TeamCity(teamcityConfig.Host, teamcityConfig.Login, teamcityConfig.Password);
            var gateway = new Gateway(new[] { tc });
            var output = new Output(new IOutputStream [] { new ConsoleStream(), new FileStream("log.txt")});
            var cancellationSource = new CancellationTokenSource();

            output.Write("build bot greetings you");
            output.Write("type exit to leave bot");
           
            var client = new TelegramBotClient(new ConfigurationProvider().ApiKey);
            var commandsExecutor = new CommandsExecutor(new CommandBase[]
            {
                new ExitCommand(cancellationSource),
                new TeamcityGetProjectsCommand(gateway.For<ITeamCity>(), cancellationSource),
                new TeamcityGetBranchesCommand(gateway.For<ITeamCity>(), cancellationSource),
                new TeamcityGetBuildsCommand(gateway.For<ITeamCity>(), cancellationSource),
                new TeamcityGetBuildTypesCommand(gateway.For<ITeamCity>(), cancellationSource),
                new TeamcityGetBuildCommand(gateway.For<ITeamCity>(), cancellationSource)
            });
            var runLooper = new RunLooper(new InputStream(), commandsExecutor, output, cancellationSource);
            using (var botWrapper = new BotWrapper(client, cancellationSource, commandsExecutor, output))
            {
                botWrapper.Start();
                runLooper.Run();
            }
        }
    }
}
