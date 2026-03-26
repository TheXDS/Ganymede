using NUnit.Framework;
using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Tests.Models;

[TestFixture]
public class FileFilterItemTests
{
    [Test]
    public void FileFilterItem_DefaultConstructor_HasCorrectValues()
    {
        var fileFilter = new FileFilterItem("Test", new[] { "*.txt" });
        using (Assert.EnterMultipleScope())
        {
            Assert.That(fileFilter.Name, Is.EqualTo("Test"));
            Assert.That(fileFilter.Extensions, Is.EqualTo(new[] { "*.txt" }));
        }
    }

    [Test]
    public void FileFilterItem_AllFiles_StaticProperty_HasCorrectValues()
    {
        var allFiles = FileFilterItem.AllFiles;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(allFiles.Name, Is.EqualTo("All files"));
            Assert.That(allFiles.Extensions, Is.EqualTo(new[] { "*.*" }));
        }
    }

    [Test]
    public void FileFilterItem_SimpleFactoryMethod_CreatesCorrectFilter()
    {
        var filter = FileFilterItem.Simple("txt");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(filter.Name, Is.EqualTo("TXT file"));
            Assert.That(filter.Extensions, Is.EqualTo(new[] { "*.txt" }));
        }
    }

    [Test]
    public void FileFilterItem_SimpleFactoryMethod_WithDifferentExtension_CreatesCorrectFilter()
    {
        var filter = FileFilterItem.Simple("pdf");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(filter.Name, Is.EqualTo("PDF file"));
            Assert.That(filter.Extensions, Is.EqualTo(new[] { "*.pdf" }));
        }
    }

    [Test]
    public void FileFilterItem_ConstructorWithSingleExtension_CreatesCorrectArray()
    {
        var filter = new FileFilterItem("Test", "*.txt");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(filter.Name, Is.EqualTo("Test"));
            Assert.That(filter.Extensions, Is.EqualTo(new[] { "*.txt" }));
        }
    }

    [Test]
    public void FileFilterItem_ConstructorWithMultipleExtensions_CreatesCorrectArray()
    {
        var filter = new FileFilterItem("Test", new[] { "*.txt", "*.rtf" });
        using (Assert.EnterMultipleScope())
        {
            Assert.That(filter.Name, Is.EqualTo("Test"));
            Assert.That(filter.Extensions, Is.EqualTo(new[] { "*.txt", "*.rtf" }));
        }
    }

    [Test]
    public void FileFilterItem_SimpleFactoryMethod_WithUppercaseExtension_CreatesCorrectFilter()
    {
        var filter = FileFilterItem.Simple("JPG");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(filter.Name, Is.EqualTo("JPG file"));
            Assert.That(filter.Extensions, Is.EqualTo(new[] { "*.JPG" }));
        }
    }
}