using System;
using System.Windows.Controls;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Ganymede.Pages;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Client.Pages;

namespace TheXDS.Ganymede.Client.ViewModels
{
    public class MainWindowViewModel : HostViewModel<TabHost>
    {
        public MainWindowViewModel() : base(CreateBuilder())
        {
            AddPage(new TestViewModel());
            AddPage(new TestViewModel());
            AddPage(new TestViewModel());
            
        }

        private static IVisualBuilder<TabHost> CreateBuilder()
        {
            var r = new DictionaryVisualResolver<Page>();
            r.RegisterVisual<TestViewModel, TestPage>();
            return new TabBuilder(new TestFvr(r));
        }
    }

    public class TestFvr : FallbackVisualResolver<Page>
    {
        public TestFvr(IVisualResolver<Page> resolver) : base(resolver)
        {
        }

        /// <inheritdoc/>
        protected override Page FallbackResolve(PageViewModel viewModel, Exception ex)
        {
            return new FallbackErrorPage { Message = $"{ex.GetType().Name}{ex.Message.OrNull(": {0}")}" };
        }
    }
}
