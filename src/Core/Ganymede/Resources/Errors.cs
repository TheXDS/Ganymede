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
                vm.UiServices?.Properties.Title.OrNull() ??
                vm.GetType().NameOf()));
        }

        public static Exception InvalidViewTypeException(Type type)
        {
            return new InvalidTypeException(St.InvalidViewType, type);
        }
    }
}
