using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    public static class ExtensionMethod
    {
        public static List<TOut> ReturnType<T, TOut>(this List<T> list)
        {
            return list.Where(x => x.GetType() == typeof(TOut)).OfType<TOut>().ToList<TOut>();
        }
    }
}
