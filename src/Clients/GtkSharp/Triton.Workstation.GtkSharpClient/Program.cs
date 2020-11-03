using System;
using System.IO;
using Gtk;

namespace TheXDS.Triton.Workstation.GtkSharpClient
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.Init();

            var app = new Application("TheXDS.Triton.Workstation.GtkSharpClient", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
