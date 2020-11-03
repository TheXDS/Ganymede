using System;
using System.Collections.Generic;
using Gtk;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Ui.Component;
using TheXDS.Triton.Ui.ViewModels;
using TheXDS.Triton.Workstation.GtkSharpClient.Pages;
using UI = Gtk.Builder.ObjectAttribute;

namespace TheXDS.Triton.Workstation.GtkSharpClient
{
    internal class MainWindow : Window
    {
        private HostViewModel _viewModel = new HostViewModel();
        private readonly Dictionary<PageViewModel, int> _openPages = new Dictionary<PageViewModel, int>();
        private readonly IVisualResolver<GtkTritonPage> _resolver;

        [UI] private Notebook _tabRoot = null!;

        public MainWindow() : this(new Builder($"{nameof(MainWindow)}.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject(nameof(MainWindow)).Handle)
        {
            builder.Autoconnect(this);
            var r = new DictionaryVisualResolver<GtkTritonPage>();
            _resolver = new TestFvr(r);
            DeleteEvent += (_, e) => Application.Quit();
            
            _viewModel.PageAdded += AddVisualPage;
            _viewModel.PageClosed += CloseVisualPage;
            
            r.RegisterVisual<TestViewModel, TestPage>();
            
            _viewModel.AddPage(new TestViewModel());
            _viewModel.AddPage(new TestViewModel());
            _viewModel.AddPage(new TestViewModel());
            
        }

        private void CloseVisualPage(object? sender, ValueEventArgs<PageViewModel> e)
        {
            _tabRoot.RemovePage(_openPages[e.Value]);
            _openPages.Remove(e.Value);
        }


        private void AddVisualPage(object? sender, ValueEventArgs<PageViewModel> e)
        {
            var s = _resolver.ResolveVisual(e.Value);
            _openPages.Add(e.Value, _tabRoot.Page = _tabRoot.AppendPage(s, new Label(e.Value.Title)));
            s.Show();
        }
    }

    public class TestFvr : FallbackVisualResolver<GtkTritonPage>
    {
        public TestFvr(IVisualResolver<GtkTritonPage> resolver) : base(resolver) { }

        public override GtkTritonPage ResolveVisual(PageViewModel viewModel)
        {
            var retVal = base.ResolveVisual(viewModel);
            retVal.SetViewModel(viewModel);
            return retVal;
        }

        /// <inheritdoc/>
        protected override GtkTritonPage FallbackResolve(PageViewModel viewModel, Exception ex)
        {
            return new FallbackErrorPage("Error al resolver la página solicitada",$"{ex.GetType().Name}{ex.Message.OrNull(": {0}")}");
        }
    }
}
