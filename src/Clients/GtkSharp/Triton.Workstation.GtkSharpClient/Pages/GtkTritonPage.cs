using System;
using Gtk;
using TheXDS.MCART.Events;
using TheXDS.Triton.Ui.ViewModels;

namespace TheXDS.Triton.Workstation.GtkSharpClient.Pages
{
    public abstract class GtkTritonPage : Bin
    {
        private PageViewModel? viewModel;

        public event EventHandler<ValueEventArgs<PageViewModel?>> ViewModelSet;
        protected Builder Builder { get; }

        public PageViewModel? ViewModel
        {
            get => viewModel;
            private set
            {
                viewModel = value;
                ViewModelSet?.Invoke(this, value);
            }
        }

        private GtkTritonPage(Builder builder, string id) : base(builder.GetObject(id).Handle)
        {
            Builder = builder;
        }

        protected GtkTritonPage(string id) : this(GetBuilder(id), id) { }

        protected void InitializeComponent()
        {
            Builder.Autoconnect(this);
        }

        internal void SetViewModel(PageViewModel vm)
        {
            ViewModel = vm;
        }

        private static Builder GetBuilder(string id)
        {
            return new Builder($"{id}.glade");
        }
    }

    public abstract class GtkTritonPage<TViewModel> : GtkTritonPage where TViewModel: PageViewModel
    {
        protected GtkTritonPage(string id) : base(id)
        {
        }

        public new TViewModel? ViewModel => base.ViewModel as TViewModel;
    }
}