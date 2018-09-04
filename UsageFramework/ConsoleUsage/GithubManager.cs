using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
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
            //_client.Credentials = new Credentials("937023546ae6a29c04972a94f712e00ea32ea120");
        }

        public async Task CommitAsync()
        {
            try
            {
                var owner = "mehmetozkaya";
                var repo = "newGeneratedRepo";
                var branch = "master";

                // create file
                var createChangeSet = await _client.Repository.Content.CreateFile(
                                                owner,
                                                repo,
                                                "code.txt",
                                                new CreateFileRequest("File creation",
                                                                      "Hello World!",
                                                                      branch));

                // update file
                var updateChangeSet = await _client.Repository.Content.UpdateFile(
                                                owner,
                                                repo,
                                                "code.txt",
                                                new UpdateFileRequest("File update",
                                                                      "Hello Universe!",
                                                                      createChangeSet.Content.Sha,
                                                                      branch));

                // delete file
                await _client.Repository.Content.DeleteFile(
                                                owner,
                                                repo,
                                                "code.txt",
                                                new DeleteFileRequest("File deletion",
                                                                      updateChangeSet.Content.Sha,
                                                                      branch));
            }
            catch (Exception exception)
            {
                throw exception;
            }            
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
            var issuesFor = await _client.Issue.GetAllForRepository(userName, repository);
            return issuesFor;
        }

        public async Task<SearchRepositoryResult> GetRepository(string repositoryName)
        {            
            var request = new SearchRepositoriesRequest(repositoryName);
            var result = await _client.Search.SearchRepo(request);            
            return result;
        }

        public async Task<Repository> CreateRepositoryAsync(string repositoryName)
        {
            var isexist = GetRepository(repositoryName);
            Task.WaitAll(isexist);

            if (isexist != null)
                return null;

            var request = new NewRepository(repositoryName);
            var result = await _client.Repository.Create(request);
            return result;
        }

        public async Task CreateCommitAsync(string repositoryName)
        {
            var owner = "mehmetozkaya";
            var repo = repositoryName;
            var branch = "master";

            // create file
            var createChangeSet = await _client.Repository.Content.CreateFile(
                                            owner,
                                            repo,
                                            "path/file.txt",
                                            new CreateFileRequest("File creation",
                                                                  "Hello World!",
                                                                  branch));

        }

        public async Task AddLastCommitAsync(string repositoryName)
        {
            var owner = "mehmetozkaya";
            var headMasterRef = $"heads/master";
            var masterReference = await _client.Git.Reference.Get(owner, repositoryName, headMasterRef);            

            var latestCommit = await _client.Git.Commit.Get(owner, repositoryName, masterReference.Object.Sha);

            var imgBase64 = Convert.ToBase64String(File.ReadAllBytes("MyImage.jpg"));
            var imgBlob = new NewBlob { Encoding = EncodingType.Base64, Content = (imgBase64) };
            var imgBlobRef = await _client.Git.Blob.Create(owner, repositoryName, imgBlob);

            var textBlob = new NewBlob { Encoding = EncodingType.Utf8, Content = "Hellow World!" };
            var textBlobRef = await _client.Git.Blob.Create(owner, repositoryName, textBlob);

            var nt = new NewTree { BaseTree = latestCommit.Tree.Sha };
            nt.Tree.Add(new NewTreeItem { Path = "MyImage.jpg", Mode = "100644", Type = TreeType.Blob, Sha = imgBlobRef.Sha });
            nt.Tree.Add(new NewTreeItem { Path = "HelloW.txt", Mode = "100644", Type = TreeType.Blob, Sha = textBlobRef.Sha });

            var newTree = await _client.Git.Tree.Create(owner, repositoryName, nt);

            // Create Commit
            var newCommit = new NewCommit("Commit test with several files", newTree.Sha, masterReference.Object.Sha);
            var commit = await _client.Git.Commit.Create(owner, repositoryName, newCommit);

            await _client.Git.Reference.Update(owner, repositoryName, headMasterRef, new ReferenceUpdate(commit.Sha));            
        }

        public async Task<Issue> CreateIssue()
        {            
            var createIssue = new NewIssue("this issue generated by api");
            var issue = await _client.Issue.Create("mehmetozkaya", "UsageFramework", createIssue);
            return issue;
        }

        public async Task<Issue> UpdateIssue(int issueId)
        {
            var issue = await _client.Issue.Get("mehmetozkaya", "UsageFramework", issueId);            
            var update = issue.ToUpdate();
            update.Body = "Updated body of issue";
            update.Title = update.Title + " updated";

            var updatedIssue = await _client.Issue.Update("mehmetozkaya", "UsageFramework", issueId, update);
            return updatedIssue;
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
