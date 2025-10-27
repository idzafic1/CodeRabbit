// This file would typically be in a separate test project.
// Assumes a test framework like MSTest.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class CalculatorTests
{
    // ERROR: Inadequate test coverage.
    // This test suite only covers the "ADD" operation. The "SUBTRACT" and "MULTIPLY"
    // paths, as well as the exception path, are completely untested.
    [TestMethod]
    public void Test_Addition()
    {
        var calculator = new Calculator();
        int result = calculator.Calculate(5, 10, "ADD");
        Assert.AreEqual(15, result);
    }

    // ERROR: Flaky test. This test depends on the exact time the code runs.
    // It might fail if it runs at the boundary of a second, making the CI build unreliable.
    [TestMethod]
    public void Test_Flaky_TimeCheck()
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
        // Simulating a check that might fail if the second ticks over between calls.
        string expectedTimeString = DateTime.Now.ToString("HH:mm:ss");
        Assert.AreEqual(expectedTimeString, timeString);
    }
}