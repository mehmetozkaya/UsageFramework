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
                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
                var basicAuth = new Credentials("mehmetozkaya", "Tatg.Wp1"); // NOTE: not real credentials
                client.Credentials = basicAuth;

                var user = UserDetailsAsync(client);
                // user.Id
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static async Task<User> UserDetailsAsync(GitHubClient client)
        {
            var user = await client.User.Get("shiftkey");
            var user2 = await client.User.Current();

            return user;

            Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
              user.Name,
              user.PublicRepos,
              user.Url);

            var apiInfo = client.GetLastApiInfo();
            var rateLimit = apiInfo?.RateLimit;
            var howManyRequestsCanIMakePerHour = rateLimit?.Limit;
            var howManyRequestsDoIHaveLeft = rateLimit?.Remaining;
            var whenDoesTheLimitReset = rateLimit?.Reset; // UTC time

        }
    }
}
