using NUnit.Framework;
using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Tests.Models;

[TestFixture]
public class ProgressReportTests
{
    [Test]
    public void ProgressReport_DefaultConstructor_HasCorrectValues()
    {
        var progressReport = new ProgressReport();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(double.IsNaN(progressReport.Progress), Is.True);
            Assert.That(progressReport.Status, Is.EqualTo(string.Empty));
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithProgressAndStatus_HasCorrectValues()
    {
        var progressReport = new ProgressReport(50.0, "Processing");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(50.0));
            Assert.That(progressReport.Status, Is.EqualTo("Processing"));
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithOnlyStatus_HasCorrectValues()
    {
        var progressReport = new ProgressReport("Processing");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(double.IsNaN(progressReport.Progress), Is.True);
            Assert.That(progressReport.Status, Is.EqualTo("Processing"));
        }
    }

    [Test]
    public void ProgressReport_ImplicitOperator_Double_CreatesCorrectReport()
    {
        ProgressReport progressReport = 75.0;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(75.0));
            Assert.That(progressReport.Status, Is.EqualTo(null));
        }
    }

    [Test]
    public void ProgressReport_ImplicitOperator_String_CreatesCorrectReport()
    {
        ProgressReport progressReport = "Processing";
        using (Assert.EnterMultipleScope())
        {
            Assert.That(double.IsNaN(progressReport.Progress), Is.True);
            Assert.That(progressReport.Status, Is.EqualTo("Processing"));
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithNullStatus_HasCorrectValues()
    {
        var progressReport = new ProgressReport(25.0, null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(25.0));
            Assert.That(progressReport.Status, Is.Null);
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithEmptyStringStatus_HasCorrectValues()
    {
        var progressReport = new ProgressReport(25.0, string.Empty);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(25.0));
            Assert.That(progressReport.Status, Is.EqualTo(string.Empty));
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithZeroProgress_HasCorrectValues()
    {
        var progressReport = new ProgressReport(0.0, "Starting");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(0.0));
            Assert.That(progressReport.Status, Is.EqualTo("Starting"));
        }
    }

    [Test]
    public void ProgressReport_ConstructorWithMaxProgress_HasCorrectValues()
    {
        var progressReport = new ProgressReport(100.0, "Complete");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(progressReport.Progress, Is.EqualTo(100.0));
            Assert.That(progressReport.Status, Is.EqualTo("Complete"));
        }
    }

    [Test]
    public void ProgressReport_WithNaNProgress_HasCorrectValues()
    {
        var progressReport = new ProgressReport(double.NaN, "Indeterminate");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(double.IsNaN(progressReport.Progress), Is.True);
            Assert.That(progressReport.Status, Is.EqualTo("Indeterminate"));
        }
    }
}