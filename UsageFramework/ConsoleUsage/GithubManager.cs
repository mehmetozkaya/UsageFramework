using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUsage
{
    internal class GithubManager
    {
        private readonly GitHubClient _client;

        public GithubManager()
        {
            _client = new GitHubClient(new ProductHeaderValue("asdasdasdf"));
            var basicAuth = new Credentials("mehmetozkaya", "Tatg.Wp1");
            _client.Credentials = basicAuth;
        }

        
        public async Task<User> GetUserInfo(string userName)
        {
            var user = await _client.User.Get(userName);
            return user;
        }

        public async Task<IReadOnlyList<Issue>> GetIssues()
        {
            var issues = await _client.Issue.GetAllForCurrent();
            return issues;
        }

        public async Task<IReadOnlyList<Issue>> GetIssuesForRepository(string userName, string repository)
        {
            var issuesForOctokit = await _client.Issue.GetAllForRepository(userName, repository);
            return issuesForOctokit;
        }

        public void CreateIssue()
        {

        }


        public void ApiInformation()
        {
            var apiInfo = _client.GetLastApiInfo();
            var rateLimit = apiInfo?.RateLimit;
            var howManyRequestsCanIMakePerHour = rateLimit?.Limit;
            var howManyRequestsDoIHaveLeft = rateLimit?.Remaining;
            var whenDoesTheLimitReset = rateLimit?.Reset; // UTC time
        }

        public Task<User> CurrentUser
        {
            get
            {
                return _client.User.Current();
            }
        }
    }
}
