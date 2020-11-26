using System;
using TheXDS.Ganymede.Exceptions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.Ganymede.Resources.ErrorStrings;

namespace TheXDS.Ganymede.Resources
{
    public static class Errors
    {
        public static Exception UiHostAccess => new UiHostAccessException();

        public static Exception VisualHostNotFound(PageViewModel vm)
        {
            return new MissingTypeException(string.Format(St.VisualHostNotFound,
                vm.Host?.Title.OrNull() ??
                vm.GetType().NameOf()));
        }
    }
}
