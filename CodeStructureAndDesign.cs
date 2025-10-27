// To test nullability, this file should be compiled with <Nullable>enable</Nullable> in the .csproj file.

using System;
using System.Net.Http;

// ERROR (Linter): Unused 'using' statements. These should be removed.
using System.Collections.Generic;
using Microsoft.VisualBasic;

#nullable enable

public class DesignIssues
{
    // ERROR: Violation of "Don't Repeat Yourself" (DRY). The logic for calculating total price is duplicated.
    public decimal CalculateStandardUserPrice(decimal price)
    {
        decimal tax = price * 0.20m; // Duplicated logic
        return price + tax;
    }

    public decimal CalculatePremiumUserPrice(decimal price)
    {
        decimal tax = price * 0.20m; // Duplicated logic
        decimal discount = price * 0.10m;
        return price + tax - discount;
    }
    
    // ERROR: Misleading method name. The name suggests it gets a user, but it also modifies state.
    public string GetUserAndLogAccess(int userId)
    {
        Console.WriteLine($"Logging access for user {userId}"); // This is a side effect.
        return $"User_{userId}";
    }
}

// ERROR: Violation of Single Responsibility Principle (SRP). This class does too much.
public class UserProcessor
{
    public void ProcessUser(string username)
    {
        // 1. Validation logic
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username is required.");
        }
        
        // 2. Database logic
        Console.WriteLine("Saving user to the database...");

        // 3. HTTP client logic
        var client = new HttpClient();
        client.PostAsync("http://api.example.com/notify", new StringContent("user processed"));
    }
}

// ERROR: Illogical inheritance hierarchy. A 'Report' is not a type of 'User'.
public class User { public string? Name { get; set; } }
public class Report : User { public string? ReportContent { get; set; } }

public class NullabilityIssues
{
    // ERROR: Potential null dereference. The 'name' parameter is nullable but dereferenced without a check.
    // A good analyzer will flag this if nullable reference types are enabled.
    public int GetNameLength(string? name)
    {
        return name.Length; // Warning: Dereference of a possibly null reference.
    }

    public void Process(string? message)
    {
        // This is the correct way to handle it.
        if (message != null)
        {
            Console.WriteLine(message.ToUpper());
        }
    }
}

// --- LINTING VIOLATIONS ADDED HERE ---

public class Linting{
        // ERROR (Linter): Naming convention violation. Should be 'testVariable'.
        // ERROR (Linter): Inconsistent indentation.
        const int testVari_able = 13;
        
        // ERROR (Linter): Inconsistent spacing before method definition.
        public double loopWhileTest()
        {
            // ERROR (Linter): Naming convention violation. Should be 'aRngStupidVar' or similar.
            const int aRNGsTuPIdVar = 13;
            double result = 13;
            // ERROR (Linter): Naming convention violation. Should be 'testVer'.
            int teS_tVer = 13;
            
            // ERROR (Linter): Extreme indentation.
                    //while(true)
            {
                            Console.WriteLine("I don't know what I am doing");
                            // ERROR (Linter): Extreme indentation.
                                                  return 0;
            
}}


    }

// ERROR (Linter): Naming convention violation. Class name 'linting_Helper' should be 'LintingHelper'.
public static class linting_Helper
    {
        public static void HelperMethod()
        {
            const int aOvr = 13;
            Console.WriteLine("This is a helper method.");
            // ERROR (Linter): Missing space before opening brace '{'.
            for (int i = 0; i < 10; i++){
                Console.WriteLine("This is the uglieset code I have ever written.");
            }
            // ERROR (Linter): Unused local variable 'UnusedVariable'.
            // ERROR (Linter): Multiple statements on the same line.
            int UnusedVariable = 5;
            int result = 0;
            // ERROR (Linter): Nested 'if' statements without braces are discouraged.
            // ERROR (Linter): Inconsistent indentation.
            if (true)
                             if (false)
                Console.WriteLine("Nested if just because");

        }
    }

// ERROR (Linter): Empty class definition.
// ERROR (Linter): Inconsistent indentation and spacing.
                public static class lintingRemover{


}