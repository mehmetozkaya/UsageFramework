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
                var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
                var user = github.User.Get("mehmetozkaya");
                Task.WaitAll(user);
                var uu = user.Result;
                Console.WriteLine(uu.Followers + " folks love the half ogre!");

                var client = new GitHubClient(new ProductHeaderValue("asdasdasdf"));
                var basicAuth = new Credentials("mehmetozkaya", "Tatg.Wp1"); // NOTE: not real credentials
                client.Credentials = basicAuth;

                var user2 = client.User.Get("shiftkey");
                Task.WaitAll(user2);

                var user3 = client.User.Current();
                Task.WaitAll(user3);

                var apiInfo = client.GetLastApiInfo();
                var rateLimit = apiInfo?.RateLimit;
                var howManyRequestsCanIMakePerHour = rateLimit?.Limit;
                var howManyRequestsDoIHaveLeft = rateLimit?.Remaining;
                var whenDoesTheLimitReset = rateLimit?.Reset; // UTC time

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
    }
}
