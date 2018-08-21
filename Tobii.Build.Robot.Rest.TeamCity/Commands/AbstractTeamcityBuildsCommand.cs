using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Route;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public abstract class AbstractTeamcityBuildsCommand : CommandBase
    {
        protected async Task RunViaBuilds(ITeamCity teamCity, Output output, Model.Builds builds)
        {
            var runningBuilds = await teamCity.GetRunningBuilds();
            var runnintAgentsIds = runningBuilds.Build.Select(x => x.Agent.Id).ToHashSet();
            foreach (var build in builds.Build)
            {
                var info = await teamCity.GetBuild(build.Id);
                var agents = await teamCity.GetCompatibleAgents(info.BuildTypeId);
                RequestForBuild(output, info, agents, runnintAgentsIds);
            }
        }

        protected static void RequestForBuild(Output output, Model.Build info, Agents compatibleAgents, HashSet<string> runningAgents)
        {
            var revisions = info.Revisions.Revision.Select(x =>
                string.Format("{0} {1} {2}",
                    x.VcsBranchName,
                    x.Version,
                    x.Href)).ToArray();

            var revisionsJoin = string.Join("; ", revisions);

            var clickables = compatibleAgents.Agent.Select(agent =>
                {
                    return new Clickable("Enqueue on " + agent.Name + (runningAgents.Contains(agent.Id) ? "(busy)" : "")
                        , "", "",
                        "enqueue", info.Revisions.Revision.First().VcsBranchName + " " + info.BuildTypeId + " " + agent.Id);
                })
                .ToArray();
            
            output.Ask(string.Format(
                "Build {0} {1} (queued {2} by {3}){4} with buildtype {5}, at revisions: {6},{7} URL: {8} {9}{10}",
                info.Number,
                info.StatusText,
                info.QueuedDate,
                info?.Triggered?.User?.Username ?? "N/A",
                Environment.NewLine,
                info.BuildTypeId,
                revisionsJoin,
                Environment.NewLine,
                info.Href,
                Environment.NewLine,
                string.IsNullOrEmpty(info.WaitReason) ? "" : "Wait reason: " + info.WaitReason), clickables);
        }

        protected AbstractTeamcityBuildsCommand(CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
        }
    }
}