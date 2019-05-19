using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLion.Bot.Extensions
{
    public static class DoubleExtensions
    {
        public static string ReplaceDot(this double d)
        {
            return d.ToString().Replace(",", ".");
        }
    }
}
