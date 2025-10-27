using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

public class SecurityIssues
{
    // ERROR: SQL Injection vulnerability. User input is concatenated directly into a SQL query.
    public void GetUser(string username)
    {
        string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
        // If 'username' is "' OR 1=1 --", the query will return all users.
        
        // Using SqlCommand like this is still vulnerable.
        var command = new SqlCommand(query);
    }
    
    // ERROR: Hardcoded secret (API Key). This should be loaded from a secure configuration source.
    private readonly string _apiKey = "sk_live_12345ABCDE_very_secret_key";
    public void ConnectToApi()
    {
        Console.WriteLine($"Connecting with key: {_apiKey}");
    }

    // ERROR: Logging sensitive data. The user's password is written to the console/log.
    public void Login(string username, string password)
    {
        Console.WriteLine($"User '{username}' is attempting to log in with password '{password}'.");
        // This password will be visible in any log aggregation system.
    }

    // ERROR: Insecure default permissions. A new user is created as an admin by default.
    public User CreateUser(string username)
    {
        return new User(username, isAdmin: true); // This should be 'false' by default.
    }

    // ERROR: Path Traversal vulnerability. User input is used to construct a file path without sanitization.
    public string ReadUserData(string userFileName)
    {
        // If userFileName is "../../../etc/passwd", this could read sensitive system files.
        string path = Path.Combine("/var/data/users/", userFileName);
        return File.ReadAllText(path);
    }
}

public class User
{
    public string Username { get; }
    public bool IsAdmin { get; }
    public User(string username, bool isAdmin)
    {
        Username = username;
        IsAdmin = isAdmin;
    }
}