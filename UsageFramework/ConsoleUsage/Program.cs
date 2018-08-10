using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var github = new GithubManager();

                var userInfo = github.GetUserInfo("mehmetozkaya");
                Task.WaitAll(userInfo);

                var issues = github.GetIssues();
                Task.WaitAll(issues);

                var issuesForRepository = github.GetIssuesForRepository("mehmetozkaya", "InterfaceActivityBuilder");
                Task.WaitAll(issuesForRepository);

                var issue = github.CreateIssue();
                Task.WaitAll(issue);

                var updated = github.UpdateIssue(1);
                Task.WaitAll(updated);

                var selectedRepo = github.GetRepository("UsageFra");
                Task.WaitAll(selectedRepo);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
    }
}
