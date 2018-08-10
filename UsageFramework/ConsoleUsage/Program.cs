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

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
    }
}
