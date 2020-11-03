using System;
using System.Collections.Generic;
using Gtk;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Ui.Component;
using TheXDS.Triton.Ui.ViewModels;
using UI = Gtk.Builder.ObjectAttribute;

namespace TheXDS.Triton.Workstation.GtkSharpClient.Pages
{
    public class FallbackErrorPage : GtkTritonPage
    {
        [UI] private Label _lblTitle = null!;
        [UI] private Label _lblMessage = null!;
        
        public FallbackErrorPage(string? title, string? message) : base(nameof(FallbackErrorPage))
        {
            InitializeComponent();
            _lblTitle.Text = title ?? string.Empty;
            _lblMessage.Text = message ?? string.Empty;
            ShowAll();
        }
    }
}