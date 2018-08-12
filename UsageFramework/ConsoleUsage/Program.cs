using System;

namespace ConsoleUsage
{
    class Program
    {
        static void Main(string[] args)
        {            
            var quartz = new QuartzManager();
            //quartz.RunProgram().GetAwaiter().GetResult();
            quartz.RunGithub().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

            try
            {                
                //var github = new GithubManager();

                //var newRepo = "newGeneratedRepo";
                //var repo = github.CreateRepositoryAsync(newRepo);
                //Task.WaitAll(repo);

                //var firstCommit = github.CreateCommitAsync(newRepo);
                //Task.WaitAll(firstCommit);

                //var lastCommits = github.AddLastCommitAsync(newRepo);
                //Task.WaitAll(lastCommits);



                //var userInfo = github.GetUserInfo("mehmetozkaya");
                //Task.WaitAll(userInfo);

                //var issues = github.GetIssues();
                //Task.WaitAll(issues);

                //var issuesForRepository = github.GetIssuesForRepository("mehmetozkaya", "InterfaceActivityBuilder");
                //Task.WaitAll(issuesForRepository);

                //var issue = github.CreateIssue();
                //Task.WaitAll(issue);

                //var updated = github.UpdateIssue(1);
                //Task.WaitAll(updated);

                //var selectedRepo = github.GetRepository("UsageFra");
                //Task.WaitAll(selectedRepo);

                //var task = github.CommitAsync();
                //Task.WaitAll(task);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
    }
}
