using System;
using System.Collections.Generic;
using System.Text;
using TheXDS.Ganymede.Exceptions;

namespace TheXDS.Ganymede.Resources
{
    public static class Errors
    {
        public static Exception UiHostAccess => new UiHostAccessException();
    }
}
