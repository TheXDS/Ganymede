using System.Drawing;

namespace TheXDS.Ganymede.Models;

[TestFixture]
public class DialogTemplateTests
{
    [Test]
    public void DialogTemplate_DefaultConstructor_HasCorrectDefaultValues()
    {
        var dialogTemplate = new DialogTemplate();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogTemplate.Text, Is.EqualTo(string.Empty));
            Assert.That(dialogTemplate.Color, Is.EqualTo(Color.Gray));
            Assert.That(dialogTemplate.Title, Is.Null);
            Assert.That(dialogTemplate.Icon, Is.Null);
        }
    }

    [Test]
    public void DialogTemplate_ConstructorWithAllProperties_HasCorrectValues()
    {
        var dialogTemplate = new DialogTemplate
        {
            Title = "Test Title",
            Text = "Test Message",
            Icon = "TestIcon",
            Color = Color.Red
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogTemplate.Title, Is.EqualTo("Test Title"));
            Assert.That(dialogTemplate.Text, Is.EqualTo("Test Message"));
            Assert.That(dialogTemplate.Icon, Is.EqualTo("TestIcon"));
            Assert.That(dialogTemplate.Color, Is.EqualTo(Color.Red));
        }
    }

    [Test]
    public void DialogTemplate_Configure_WithNullProperties_OverridesWithNull()
    {
        var dialogTemplate = new DialogTemplate
        {
            Title = null,
            Text = null!,
            Icon = null,
            Color = Color.Empty
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogTemplate.Title, Is.Null);
            Assert.That(dialogTemplate.Text, Is.Null);
            Assert.That(dialogTemplate.Icon, Is.Null);
            Assert.That(dialogTemplate.Color, Is.EqualTo(Color.Empty));
        }
    }

    [Test]
    public void DialogTemplate_Configure_WithEmptyProperties_OverridesWithEmpty()
    {
        var dialogTemplate = new DialogTemplate
        {
            Title = string.Empty,
            Text = string.Empty,
            Icon = string.Empty,
            Color = Color.Empty
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogTemplate.Title, Is.EqualTo(string.Empty));
            Assert.That(dialogTemplate.Text, Is.EqualTo(string.Empty));
            Assert.That(dialogTemplate.Icon, Is.EqualTo(string.Empty));
            Assert.That(dialogTemplate.Color, Is.EqualTo(Color.Empty));
        }
    }

    [Test]
    public void DialogTemplate_Configure_WithNonNullProperties_InitializesProperties()
    {
        var dialogTemplate = new DialogTemplate
        {
            Title = "Template Title",
            Text = "Template Message",
            Icon = "TemplateIcon",
            Color = Color.Red
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogTemplate.Title, Is.EqualTo("Template Title"));
            Assert.That(dialogTemplate.Text, Is.EqualTo("Template Message"));
            Assert.That(dialogTemplate.Icon, Is.EqualTo("TemplateIcon"));
            Assert.That(dialogTemplate.Color, Is.EqualTo(Color.Red));
        }
    }
}