using System;
using System.Linq;
using System.Threading;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public abstract class AbstractTeamcityBuildsCommand : CommandBase
    {
        protected static void Ask(Output output, Model.Build info)
        {
            var revisions = info.Revisions.Revision.Select(x =>
                string.Format("{0} {1} {2}",
                    x.VcsBranchName,
                    x.Version,
                    x.Href)).ToArray();

            var revisionsJoin = string.Join("; ", revisions);

            var clickables = new Clickable[]
            {
                new Clickable("Enqueue again", "", "", "<TODO>", ""),
            };

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