// Ova datoteka bi se obično nalazila u zasebnom testnom projektu.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class CalculatorTests
{
// GREŠKA: Neadekvatna pokrivenost testovima.
// Ovaj testni paket pokriva samo operaciju "SABIRANJE". Putanje "ODUZIMANJE" i "MNOŽENJE"
//, kao i putanja izuzetka, nisu u potpunosti testirane.
    [TestMethod]
    public void Test_Addition()
    {
        var calculator = new Calculator();
        int result = calculator.Calculate(5, 10, "ADD");
        Assert.AreEqual(15, result);
    }

  // GREŠKA: Nepouzdan test. Ovaj test zavisi od tačnog vremena izvršavanja koda.
  // Može propasti ako se izvršava na granici sekunde, što čini CI izgradnju nepouzdanom.
    [TestMethod]
    public void Test_Flaky_TimeCheck()
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
  // Simuliranje provjere koja bi mogla propasti ako se sekunda prekine između poziva.
        string expectedTimeString = DateTime.Now.ToString("HH:mm:ss");
        Assert.AreEqual(expectedTimeString, timeString);
    }
}
