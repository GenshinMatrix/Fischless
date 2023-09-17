using Octokit;
using System.Diagnostics;

namespace Fischless.Fetch.Lazy;

public static class LazyRepository
{
    public const string DefaultBranch = "main";
    public const string DefaultRepo = "genshin-lazy-rec";
    public const string DefaultPath = "genshin-lazy.yaml";
    public const string DefaultTokenPath = "genshin-lazy.token";

    public static string Token { get; set; } = null!;

    public static async Task<string> GetFile(string token = null!, string repo = null!, string user = null!, string path = null!)
    {
        _ = token ?? Token ?? throw new ArgumentNullException(nameof(token));
        token ??= Token;
        Token ??= token!;
        repo ??= DefaultRepo;
        path ??= DefaultPath;

        GitHubClient client = new(new ProductHeaderValue(repo));

        try
        {
            Credentials tokenAuth = new(token);
            client.Credentials = tokenAuth;

            user ??= (await client.User.Current()).Login;

            IReadOnlyList<RepositoryContent> cs = await client.Repository.Content.GetAllContents(user, repo, path);

            foreach (RepositoryContent c in cs)
            {
                if (c.Name == path)
                {
                    return c.Content;
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return null!;
    }

    public static async Task<bool> UpdateFile(string content, string token = null!, string repo = null!, string user = null!, string path = null!, string branch = null!)
    {
        _ = content ?? throw new ArgumentNullException(nameof(content));
        _ = token ?? Token ?? throw new ArgumentNullException(nameof(token));
        token ??= Token;
        Token ??= token!;
        repo ??= DefaultRepo;
        path ??= DefaultPath;
        branch ??= DefaultBranch;

        GitHubClient client = new(new ProductHeaderValue(repo));

        try
        {
            Credentials tokenAuth = new(token);
            client.Credentials = tokenAuth;

            user ??= (await client.User.Current()).Login;

            IReadOnlyList<RepositoryContent> existingFile = await client.Repository.Content.GetAllContentsByRef(user, repo, path, branch);
            RepositoryContentChangeSet set = await client.Repository.Content.UpdateFile(user, repo, path, new UpdateFileRequest("Updated " + path, content, existingFile.First().Sha, branch));
            return true;
        }
        catch (NotFoundException)
        {
            try
            {
                RepositoryContentChangeSet set = await client.Repository.Content.CreateFile(user, repo, path, new CreateFileRequest("Created " + path, content, branch));
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        return false;
    }

    public static async Task<bool> SetupToken(string path = null!)
    {
        if (string.IsNullOrEmpty(Token))
        {
            if (HasToken(path))
            {
                string token = await GetToken();

                if (!string.IsNullOrEmpty(token) && token.StartsWith("g"))
                {
                    Token = token;
                    Debug.WriteLine("[LazyRepository] Token file detected.");
                    return true;
                }
            }
        }
        return true;
    }

    public static async Task<bool> SaveToken(string token, string path = null!)
    {
        path ??= LazySpecialPathProvider.GetPath(DefaultTokenPath);

        try
        {
            string tokenEncrypted = LazyCrypto.Encrypt(token);
            await File.WriteAllTextAsync(path, tokenEncrypted);
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
        return false;
    }

    public static async Task<string> GetToken(string path = null!)
    {
        path ??= LazySpecialPathProvider.GetPath(DefaultTokenPath);

        try
        {
            if (File.Exists(path))
            {
                string tokenUncrypted = await File.ReadAllTextAsync(path);
                return LazyCrypto.Decrypt(tokenUncrypted);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
        return null!;
    }

    public static bool HasToken(string path = null!)
    {
        return File.Exists(path ?? LazySpecialPathProvider.GetPath(DefaultTokenPath));
    }

    public static void SetupTokenFromCommandLineArgs(string[] args = null!)
    {
        args ??= Environment.GetCommandLineArgs();

        if (args.Length >= 2)
        {
            Debug.WriteLine($"[LazyRepository] GetCommandLineArgs: {string.Join(' ', args)}");

            if (File.Exists(args[1]))
            {
                string token = File.ReadAllText(args[1]);

                if (token.StartsWith("g"))
                {
                    _ = SaveToken(token);
                    Debug.WriteLine($"[LazyRepository] Token '{token}' saved from '{args[1]}'.");
                }
            }
            else
            {
                string token = args[1];

                if (token.StartsWith("g") && !token.StartsWith(LazyProtocol.ProtocolName) && !token.StartsWith(LazyProtocol.GetUrl()))
                {
                    _ = SaveToken(token);
                    Debug.WriteLine($"[LazyRepository] Token '{token}' saved.");
                }
            }
        }
    }
}
