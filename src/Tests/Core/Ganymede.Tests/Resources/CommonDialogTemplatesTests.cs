using System.Reflection;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources;

[TestFixture]
public class CommonDialogTemplatesTests
{
    private readonly Type resourceClass = typeof(CommonDialogTemplates);

    [Test]
    public void DialogTemplates_Test()
    {
        foreach (var property in resourceClass.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType.IsAssignableTo(typeof(DialogTemplate))))
        {
            Assert.That(property.GetValue(null), Is.InstanceOf<DialogTemplate>());
        }
    }
}

[TestFixture]
public class CommonWizardStepsTests
{
    [Test]
    public void SimpleTextStep_returns_simple_text_wizard_step()
    {
        var wizardStep = CommonWizardSteps.SimpleTextStep<object>("Test", "NextTest");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(wizardStep, Is.Not.Null);
            Assert.That(wizardStep, Is.InstanceOf<IWizardViewModel<object>>());
            Assert.That(wizardStep.Message, Is.EqualTo("Test"));
            Assert.That(wizardStep.Interactions, Is.Not.Empty);
            Assert.That(wizardStep.Interactions.First().Text, Is.EqualTo("NextTest"));
            Assert.That(wizardStep.Interactions.First().Command, Is.Not.Null);
        }
    }

    [Test]
    public void CancellableOperation_returns_simple_text_wizard_step()
    {
        var wizardStep = CommonWizardSteps.CancellableOperation<object>((p, ct) => Task.CompletedTask);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(wizardStep, Is.Not.Null);
            Assert.That(wizardStep, Is.InstanceOf<IWizardViewModel<object>>());
            Assert.That(wizardStep.Interactions, Is.Not.Empty);
            Assert.That(wizardStep.Interactions.First().Text, Is.Not.Null.And.Not.Empty);
            Assert.That(wizardStep.Interactions.First().Command, Is.Not.Null);
        }
}

    [Test]
    public void FinishPage_returns_simple_text_wizard_step()
    {
        var wizardStep = CommonWizardSteps.FinishPage<object>("Test");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(wizardStep, Is.Not.Null);
            Assert.That(wizardStep, Is.InstanceOf<IWizardViewModel<object>>());
            Assert.That(wizardStep.Message, Is.EqualTo("Test"));
            Assert.That(wizardStep.Interactions, Is.Not.Empty);
            Assert.That(wizardStep.Interactions.First().Text, Is.Not.Null.And.Not.Empty);
            Assert.That(wizardStep.Interactions.First().Command, Is.Not.Null);
        }
    }
}