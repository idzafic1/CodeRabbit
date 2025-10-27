using System;
using System.Collections.Generic;

public class MemoryManagementIssues
{
    // ERROR: Unused private field. This field is declared but never read.
    private readonly string _unusedField = "I am never used.";

    public void ProcessData(int input)
    {
        // ERROR: Unused local variable 'unusedVariable'. It is assigned a value but never read.
        int unusedVariable = 10;

        int result;
        // ERROR: Use of uninitialized local variable 'result'. 
        // A static analyzer should detect that if 'input' is negative, 'result' is used without being assigned.
        if (input >= 0)
        {
            result = input * 2;
        }
        // Console.WriteLine(result); // This would cause a compile error. The potential for this is the issue.
    }

    // ERROR: Unnecessary allocation in a loop. A new list is created in every iteration.
    public void InefficientAllocationInLoop()
    {
        int total = 0;
        for (int i = 0; i < 1000; i++)
        {
            // This creates 1000 lists. The list should be declared outside the loop.
            List<int> tempList = new List<int> { i };
            total += tempList.Count;
        }
    }
}

public class DataCopying
{
    public class MutableObject
    {
        public List<string> Tags { get; set; } = new List<string>();
        
        public MutableObject ShallowCopy()
        {
            // This performs a shallow copy. The 'Tags' list is not cloned.
            return (MutableObject)this.MemberwiseClone();
        }
    }
    
    public void DemonstrateShallowCopyIssue()
    {
        var obj1 = new MutableObject();
        obj1.Tags.Add("original");

        // ERROR: Shallow vs. Deep Copy issue. Modifying the copy affects the original.
        var obj2 = obj1.ShallowCopy();
        obj2.Tags.Add("modified");

        // Now obj1.Tags also contains "modified", which is an unintended side effect.
        Console.WriteLine(string.Join(", ", obj1.Tags)); // "original, modified"
    }
}

public class EventHolder
{
    // This event can cause memory leaks if subscribers don't unsubscribe.
    public static event EventHandler<EventArgs> StaticEvent;

    public void TriggerEvent()
    {
        StaticEvent?.Invoke(this, EventArgs.Empty);
    }
}

public class Subscriber
{
    public Subscriber()
    {
        // ERROR: Potential memory leak. The 'Subscriber' instance subscribes to a static event.
        // Because the event is static, it will hold a reference to this 'Subscriber' instance forever,
        // preventing it from being garbage collected, unless it explicitly unsubscribes.
        EventHolder.StaticEvent += OnEvent;
    }

    private void OnEvent(object sender, EventArgs e)
    {
        Console.WriteLine("Event received.");
    }
    
    // To fix this, the class should implement IDisposable and unsubscribe.
    // public void Dispose() { EventHolder.StaticEvent -= OnEvent; }
}