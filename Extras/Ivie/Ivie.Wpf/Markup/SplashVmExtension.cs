using System;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ivie.ViewModels;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ivie.Markup
{
    internal class SplashVmExtension : System.Windows.Markup.MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SplashViewModel() { Message = "Some operation..." };
        }
    }
}
