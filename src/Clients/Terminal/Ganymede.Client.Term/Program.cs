using System;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using System.Threading.Tasks;

namespace TermClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Screen();
            //s.AddPage(new TestViewModel());
            //s.AddPage(new TestViewModel());
            //s.AddPage(new TestViewModel());

            while (true)
            {
                var k = Console.ReadKey(true);
                if (s.Handle(k)) continue;

                Console.Beep();
            }
        }
    }

    interface IInputReader
    {
        ConsoleModifiers Modifiers { get; }
        void Run(ConsoleKeyInfo command, ref bool handled);
    }

    abstract class InputReader : IInputReader
    {
        public Screen Screen { get; }

        public virtual ConsoleModifiers Modifiers => 0;

        public InputReader(Screen screen)
        {
            Screen = screen;
        }

        public abstract void Run(ConsoleKeyInfo command, ref bool handled);
    }

    class TabSwitcher : InputReader
    {
        public override ConsoleModifiers Modifiers => ConsoleModifiers.Alt;

        public override void Run(ConsoleKeyInfo command, ref bool handled)
        {
            if (!"123456789".Contains(command.KeyChar)) return;
            Screen.ActiveTab = int.Parse(command.KeyChar.ToString());
            handled = true;
        }

        public TabSwitcher(Screen screen) : base(screen) { }
    }

    class TabCloser : InputReader
    {
        public TabCloser(Screen screen) : base(screen) { }

        public override void Run(ConsoleKeyInfo command, ref bool handled)
        {
            if (command.Key == ConsoleKey.F12)
            {
                Screen.ClosePage(Screen.ActivePage);
                handled = true;
            }
        }
    }





    class Screen : HostViewModel
    {
        private readonly List<IInputReader> _keyHandlers = new List<IInputReader>();

        private int _activeTab;

        public int ActiveTab
        {
            get => _activeTab + 1;
            set
            {
                if (Pages.Count() < value) return;
                _activeTab = value - 1;
                DrawActiveTab();
            }
        }

        public PageViewModel? ActivePage => _activeTab != -1 ? Pages.ToArray()[_activeTab] : null;

        public Screen() : base(null!)
        {
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.CursorVisible = false;
            _keyHandlers.Add(new TabSwitcher(this));
            _keyHandlers.Add(new TabCloser(this));

            DrawBg();
            DrawTabs();
        }

        public bool Handle(ConsoleKeyInfo input)
        {
            var handled = false;
            foreach (var j in _keyHandlers)
            {
                if (j.Modifiers == input.Modifiers) j.Run(input, ref handled);
                if (handled) break;
            }
            return handled;
        }

        private void DrawBg()
        {
            Console.ResetColor();
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.Write("Tritón demo".PadRight(Console.BufferWidth));
        }
        private void DrawTabs()
        {
            var t = 1;
            Console.CursorTop = 1;
            Console.CursorLeft = 0;
            foreach (var j in Pages.Take(Console.WindowWidth / 4))
            {
                Console.BackgroundColor = j.Host.AccentColor.HasValue ? (ConsoleColor)(Color.To<byte, VGAAttributeByte>(j.Host.AccentColor.Value) & 15) : ConsoleColor.Black;
                Console.Write(t++.ToString().PadLeft(4));
            }
        }
        private void DrawActiveTab()
        {
            var j = ActivePage;
            if (j is null)
            {
                ClearTabView();
                return;
            }
            Console.BackgroundColor = j.Host.AccentColor.HasValue ? (ConsoleColor)(Color.To<byte, VGAAttributeByte>(j.Host.AccentColor.Value) & 15) : ConsoleColor.Black;
            Console.CursorTop = 2;
            Console.CursorLeft = 0;
            Console.Write(j.Host.Title.PadRight(Console.WindowWidth));
        }

        private void ClearTabView()
        {
            Console.CursorTop = 2;
            Console.ResetColor();
            var cb = new string(' ', Console.BufferWidth);
            for (; Console.CursorTop < Console.WindowHeight-1; Console.CursorTop++)
            {
                Console.CursorLeft = 0;
                Console.Write(cb);
            }
        }

        public override async Task AddPage(PageViewModel page)
        {
            await base.AddPage(page);
            DrawTabs();
        }
        public override void ClosePage(PageViewModel page)
        {            
            base.ClosePage(page);
            ActiveTab = Pages.ToList().IndexOf(Pages.LastOrDefault())+1;
            Console.ResetColor();
            Console.CursorTop = 1;
            Console.CursorLeft = ActiveTab * 4;
            Console.Write(new string(' ', 4));
            DrawTabs();
        }
    }



    public class Form
    {
        public List<Widget> Widgets { get; } = new List<Widget>();

        public void Draw()
        {
            foreach (var j in Widgets) j.Draw(Console.BufferWidth - 10);
        }
    }

    public abstract class Widget : NotifyPropertyChanged
    {
        private int _x;
        private int _y;

        /// <summary>
        /// Obtiene o establece el valor X.
        /// </summary>
        /// <value>El valor de X.</value>
        public int X
        {
            get => _x;
            set => Change(ref _x, value);
        }

        /// <summary>
        /// Obtiene o establece el valor Y.
        /// </summary>
        /// <value>El valor de Y.</value>
        public int Y
        {
            get => _y;
            set => Change(ref _y, value);
        }

        public abstract void Draw(int width);
        public abstract void Clear(int width);
    }

    public class Button : Widget
    {
        public string Label { get; set; }

        public override void Clear(int width)
        {
            Console.ResetColor();
            var cb = new string(' ', width);
            for (Console.CursorTop = Y; Console.CursorTop < Y + 3; Console.CursorTop++)
            {
                Console.CursorLeft = X;
                Console.Write(cb);
            }
        }

        public override void Draw(int width)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            var cb = new string(' ', width);
            for (Console.CursorTop = Y; Console.CursorTop < Y + 3; Console.CursorTop++)
            {
                Console.CursorLeft = X;
                Console.Write(cb);
            }
            Console.CursorLeft = X + 1;
            Console.CursorTop = Y + 1;
            Console.Write(Label);
        }
    }
    public class Text : Widget
    {
        public string Label { get; set; }

        public override void Clear(int width)
        {
            Console.ResetColor();
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            Console.Write(new string(' ', Label.Length));
        }

        public override void Draw(int width)
        {
            Console.ResetColor();
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            Console.Write(Label);
        }
    }
    public class NumericInput : Widget
    {
        public int Value { get; set; }
        public override void Draw(int width)
        {
            Console.ResetColor();
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            Console.Write($"+{new string('-', width - 2)}+");
            Console.Write($"|{Value.ToString().PadRight(width-2)}|");
            Console.Write($"+{new string('-', width - 2)}+");
        }

        public override void Clear(int width)
        {
            Console.ResetColor();
            var cb = new string(' ', width);
            for (Console.CursorTop = Y; Console.CursorTop < Y + 3; Console.CursorTop++)
            {
                Console.CursorLeft = X;
                Console.Write(cb);
            }
        }
    }
}
