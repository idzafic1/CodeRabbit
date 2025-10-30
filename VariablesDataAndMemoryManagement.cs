using System;
using System.Collections.Generic;
// komentar

public class MemoryManagementIssues
{
    // ERROR: Ovo polje je deklarisano ali se nikad ne čita.
    private readonly string _unusedField = "I am never used.";

    public void ProcessData(int input)
    {
        // ERROR: Neiskorištena lokalna varijabla 'unusedVariable'. Dodijeljena joj je vrijednost ali se nikad ne čita.
        int unusedVariable = 10;

        int result;
        // ERROR: Korištenje neinicializovane lokalne varijable 'result'.
        // Statički analizator bi trebao otkriti da ako je 'input' negativan, 'result' se koristi bez dodijeljene vrijednosti.
        if (input >= 0)
        {
            result = input * 2;
        }
        // Console.WriteLine(result); // Ovo bi uzrokovalo grešku pri kompajliranju.
    }

    // ERROR: Suvišna alokacija u petlji. Nova lista se kreira u svakoj iteraciji.
    public void InefficientAllocationInLoop()
    {
        int total = 0;
        for (int i = 0; i < 1000; i++)
        {
            // Ovo kreira 1000 lista. Lista bi trebala biti deklarisana izvan petlje.
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
            // Ovo pravi plitku kopiju. Lista 'Tags' nije klonirana.
            return (MutableObject)this.MemberwiseClone();
        }
    }
    
    public void DemonstrateShallowCopyIssue()
    {
        var obj1 = new MutableObject();
        obj1.Tags.Add("original");

        // ERROR: Problem plitke naspram duboke kopije. Izmjena kopije utiče na original.
        var obj2 = obj1.ShallowCopy();
        obj2.Tags.Add("modified");

        // Sada obj1.Tags također sadrži "modified", što je neželjeni sporedni efekat.
        Console.WriteLine(string.Join(", ", obj1.Tags)); // "original, modified"
    }
}

public class EventHolder
{
    // Ovaj događaj može prouzrokovati curenje memorije ako se pretplatnici ne odjave.
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
        // ERROR: Potencijalno curenje memorije. Instanca 'Subscriber' se pretplaćuje na statički događaj.
        // Pošto je događaj statički, on će držati referencu na ovu instancu 'Subscriber' zauvijek,
        // sprečavajući da bude uklonjena od strane sakupljača smeća, osim ako se eksplicitno ne odjavi.
        EventHolder.StaticEvent += OnEvent;
    }

    private void OnEvent(object sender, EventArgs e)
    {
        Console.WriteLine("Event received.");
    }
    
    // Da bi se popravilo ovo, klasa bi trebala implementirati IDisposable i odjaviti se.
    // public void Dispose() { EventHolder.StaticEvent -= OnEvent; }
}
