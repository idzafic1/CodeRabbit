using System;
using System.Data.SqlClient; 
using System.Diagnostics;
using System.IO;

public class SecurityIssues
{
    // GREŠKA: SQL Injection. Unos korisnika se direktno konkatenira u SQL upit.
    public void GetUser(string username)
    {
        string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
        // Ako je 'username' "' OR 1=1 --", upit će vratiti sve korisnike.
        
        // Korištenje SqlCommand na ovaj način i dalje je ranjivo jer je SQL upit unaprijed formatiran.
        var command = new SqlCommand(query); 
    }
    
    // GREŠKA: Hardkodirani tajni podatak (API ključ). Trebalo bi ga učitati iz sigurnog izvora konfiguracije.
    private readonly string _apiKey = "sk_live_12345ABCDE_very_secret_key";
    public void ConnectToApi()
    {
        Console.WriteLine($"Connecting with key: {_apiKey}");
    }

    // GREŠKA: Logiranje osjetljivih podataka. Lozinka korisnika se ispisuje u konzolu/log.
    public void Login(string username, string password)
    {
        Console.WriteLine($"User '{username}' is attempting to log in with password '{password}'.");
        // Ova lozinka će biti vidljiva u bilo kojem sistemu za agregaciju logova.
    }

    // GREŠKA: Nesigurne zadane dozvole. Novi korisnik se po defaultu kreira kao administrator.
    public User CreateUser(string username)
    {
        return new User(username, isAdmin: true); // Ovo bi po defaultu trebalo biti 'false'.
    }

    // GREŠKA: Path Traversal ranjivost. Unos korisnika se koristi za sastavljanje putanje datoteke bez sanitizacije.
    public string ReadUserData(string userFileName)
    {
        // Ako je userFileName "../../../etc/passwd", ovo bi moglo pročitati osjetljive sistemske datoteke.
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
