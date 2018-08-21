using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest.TeamCity
{
    public class TeamCityApi : ITeamCity
    {
        public IRestClient RestClient { get; }

        public TeamCityApi(IRestClient restClient)
        {
            RestClient = restClient;
        }
        
        public async Task<Branches> GetBranchesAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}/branches";
            return await RestClient.Get<Branches>(request);
        }

        public async Task<Builds> GetQueue()
        {
            var request = $"/httpAuth/app/rest/buildQueue";
            return await RestClient.Get<Builds>(request);
        }

        public async Task<Model.Build> GetBuild(string buildId)
        {
            var request = $"/httpAuth/app/rest/builds/id:{buildId}";
            return await RestClient.Get<Model.Build>(request);
        }

        public async Task<Builds> GetBuildsFromProject(string projectId, int count = 50, int start = 0)
        {
            var request = $"/httpAuth/app/rest/builds?locator=affectedProject:{projectId},start:0,count:{count}" +
                          (start > 0 ? $"start:{start}" : "");
            return await RestClient.Get<Builds>(request);
        }

        public async Task<Builds> GetBuildsFromBuildType(string buildTypeId, int count = 50, int start = 0)
        {
            var request = $"/httpAuth/app/rest/builds?buildId={buildTypeId}&locator=count:{count}" +
                          (start > 0 ? $"start:{start}" : "");
            return await RestClient.Get<Builds>(request);
        }

        public async Task<BuildTypes> GetBuildTypes(string projectId)
        {
            var request = $"/httpAuth/app/rest/buildTypes?locator=affectedProject:{projectId}";
            return await RestClient.Get<BuildTypes>(request);
        }

        public async Task<BuildType> GetBuildType(string buildTypeId)
        {
            var request = $"httpAuth/app/rest/buildTypes/id:{buildTypeId}";
            return await RestClient.Get<BuildType>(request);
        }

        public async Task<Project> GetProjectAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}";
            return await RestClient.Get<Project>(request);
        }

        public async Task<Agents> GetAgentsAsync()
        {
            var request = "httpAuth/app/rest/agents";
            return await RestClient.Get<Agents>(request);
        }

        public async Task<Agent> GetAgentAsync(string agentId)
        {
            var request = $"httpAuth/app/rest/agents/id:{agentId}";
            return await RestClient.Get<Agent>(request);
        }

        public async Task<Projects> GetProjectsAsync()
        {
            var request = $"/httpAuth/app/rest/projects";
            return await RestClient.Get<Projects>(request);
        }
       
        public async Task<Agents> GetCompatibleAgents(string buildTypeId)
        {
            var request = $"/httpAuth/app/rest/agents?locator=compatible:(buildType:(id:{buildTypeId}))";
            return await RestClient.Get<Agents>(request);
        }

        public async Task<Builds> GetRunningBuilds()
        {
            var request =
                "httpAuth/app/rest/builds?locator=running:true&fields=build(id,buildType,number,status,agent)";
            return await RestClient.Get<Builds>(request);
        }

        public Task<Model.Build> Enqueue(string branchName, string buildTypeId, string agentId, string comment, bool cleanSource, bool rebuildAllDependencies,
            bool queueTop, string modificationId)
        {
            var request = $"/httpAuth/app/rest/buildQueue";
            var build = new Build
            {
                TriggerOptions = new TriggerOptions()
                {
                    CleanSources = cleanSource,
                    QueueAtTop = queueTop,
                    RebuildAllDependencies = rebuildAllDependencies
                },
                Comment = new Comment(){Text = comment},
                BranchName = branchName,
                BuildType = string.IsNullOrEmpty(buildTypeId) ? null : new Entity() { Id = buildTypeId },
                Agent = string.IsNullOrEmpty(agentId) ? null : new Entity() { Id = agentId },
                LastChanges = string.IsNullOrEmpty(modificationId) ? null : new List<Entity>()
                {
                    new Entity()
                    {
                        Id = modificationId
                    }
                },
            };
            string serialized = Serialize(build);
            return RestClient.Post<Model.Build>(request, serialized);
        }

        private class Utf8StringWriter : StringWriter
        {
            public Utf8StringWriter(StringBuilder sb) : base(sb)
            {
            }

            public override Encoding Encoding => Encoding.UTF8;
        }

        private static string Serialize<T>(T item)
        {
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new Utf8StringWriter(sb))
            {
                serializer.Serialize(stringWriter, item);
            }

            var serialized = sb.ToString();
            return serialized;
        }

        public class TriggerOptions
        {
            [XmlAttribute("cleanSources")]
            public bool CleanSources { get; set; }
            [XmlAttribute("rebuildAllDependencies")]
            public bool RebuildAllDependencies { get; set; }
            [XmlAttribute("queueAtTop")]
            public bool QueueAtTop { get; set; }
        }

        public class Entity
        {
            [XmlAttribute("id")]
            public string Id { get; set; }
        }

        public class Comment
        {
            [XmlElement("text")]
            public string Text { get; set; }
        }

        [XmlInclude(typeof(TriggerOptions))]
        [XmlInclude(typeof(Entity))]
        [XmlInclude(typeof(Comment))]
        [XmlRoot("build")]
        public class Build
        {
            [XmlElement("triggerOptions")]
            public TriggerOptions TriggerOptions { get; set; }

            [XmlElement("buildType")]
            public Entity BuildType { get; set; }

            [XmlArray("lastChanges")]
            [XmlArrayItem("change")]
            public List<Entity> LastChanges { get; set; }

            [XmlElement("agent")]
            public Entity Agent { get; set; }

            [XmlElement("comment")]
            public Comment Comment { get; set; }

            [XmlAttribute("branchName")]
            public string BranchName { get; set; }

        }
    }
}