using System;
using System.Threading.Tasks;
using Quartz;

namespace ConsoleUsage
{
    public class CoommitJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var github = new GithubManager();
            await github.CommitAsync();
            await Console.Out.WriteLineAsync("Commit Success!");
        }
    }
}
