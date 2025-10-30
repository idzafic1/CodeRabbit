using System;
using System.Collections.Generic;


public class VariableAndDataManagement
{
    // GREŠKA: Ovo polje je deklarisano ali se nikada ne koristi.
    private readonly string _unusedField = "I am never used.";

    public void ProcessData(int input)
    {
        // GREŠKA: Neiskorištena varijabla 'unusedVariable'. Dodijeljena joj je vrijednost ali se nikada ne čita.
        int unusedVariable = 10;

        int result;
        // GREŠKA: Korištena je neinicijalizirana varijabla 'result'.
        // C# kompajler će ovo uhvatiti, ali obrazac je važan za analizu.
        if (input == 0)
        {
            result = 100;
        }
        else if (input > 0)
        {
            result = 200;
        }
        // Šta ako je input manji od 0? Varijabla 'result' bi ostala neinicijalizirana.
        // Console.WriteLine(result); 
    }

    // GREŠKA: Zavaravajući naziv metode. Metoda je imenovana 'Subtract' ali vrši sabiranje.
    public int Subtract(int a, int b)
    {
        // Ova metoda bi trebala vraćati a - b, ali vraća a + b.
        return a + b;
    }

    public class User
    {
        public string Name { get; set; }
        public List<string> Permissions { get; set; }

        public User(string name, List<string> permissions)
        {
            this.Name = name;
            this.Permissions = permissions;
        }

        public User ShallowCopy()
        {
            return (User)this.MemberwiseClone();
        }
    }

    public void ShallowCopyIssue()
    {
        var originalPermissions = new List<string> { "admin", "editor" };
        var user1 = new User("AdminUser", originalPermissions);

        // GREŠKA: Problem s plitkom kopijom (shallow copy).
        // 'user2' je napravljen kao plitka kopija 'user1'. Kada izmijenimo listu 'Permissions'
        // kod 'user2', to također mijenja 'Permissions' kod 'user1' jer oba referenciraju
        // isti objekat liste u memoriji.
        var user2 = user1.ShallowCopy();
        user2.Permissions.Add("viewer");

        // Sada user1.Permissions takođe sadrži "viewer", što je nenamjerna nuspojava.
        Console.WriteLine("User1 Permissions: " + string.Join(", ", user1.Permissions));
    }
}
