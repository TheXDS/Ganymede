using System;
using System.Collections.Generic;
using Gtk;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Ui.Component;
using UI = Gtk.Builder.ObjectAttribute;

namespace TheXDS.Triton.Workstation.GtkSharpClient.Pages
{

    public class TestPage : GtkTritonPage<TestViewModel>
    {
        [UI] private Label lblPageTitle = null!;
        [UI] private Entry txtName = null!;
        [UI] private Label lblGreet = null!;
        [UI] private SpinButton nudOne = null!;
        [UI] private SpinButton nudTwo = null!;
        [UI] private Button btnAdd = null!;
        [UI] private Label lblResult = null!;
        [UI] private Button btnQuit = null!;

        public TestPage() : base(nameof(TestPage))
        {
            InitializeComponent();

            //lblPageTitle.Text = ViewModel?.Title;
            

            


            btnQuit.Bind(() => ViewModel?.CloseCommand);
            btnAdd.Bind(()=> ViewModel?.SumCommand);

        }
    }
}