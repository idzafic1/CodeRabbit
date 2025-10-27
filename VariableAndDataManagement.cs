using System;
using System.Collections.Generic;

public class VariableAndDataManagement
{
    // ERROR: Unused private field. This field is declared but never used.
    private readonly string _unusedField = "I am never used.";

    public void ProcessData(int input)
    {
        // ERROR: Unused variable 'unusedVariable'. It is assigned a value but never read.
        int unusedVariable = 10;

        int result;
        // ERROR: Uninitialized variable 'result' is used.
        // Although the logic seems to cover all cases, a sophisticated tool might detect
        // that if 'input' was a value other than 0 or 1, 'result' would not be initialized.
        // C# compiler will catch this, but the pattern is important for analysis.
        if (input == 0)
        {
            result = 100;
        }
        else if (input > 0)
        {
            result = 200;
        }
        // What if input is less than 0? The variable 'result' would be unassigned.
        // Console.WriteLine(result); // This line would cause a compile error. Let's simulate a more subtle case.
    }

    // ERROR: Misleading method name. The method is named 'Subtract' but it performs addition.
    public int Subtract(int a, int b)
    {
        // This method should return a - b, but it returns a + b.
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

        // ERROR: Shallow Copy issue.
        // 'user2' is created as a shallow copy of 'user1'. When we modify the 'Permissions'
        // list of 'user2', it also modifies the 'Permissions' for 'user1' because they
        // both reference the same list object in memory.
        var user2 = user1.ShallowCopy();
        user2.Permissions.Add("viewer");

        // Now, user1.Permissions also contains "viewer", which is an unintended side effect.
        Console.WriteLine("User1 Permissions: " + string.Join(", ", user1.Permissions));
    }
}