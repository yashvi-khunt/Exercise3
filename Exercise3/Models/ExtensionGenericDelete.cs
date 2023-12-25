using System;

namespace Exercise3.Models
{
    public static class ExtensionGenericDelete
    {
        public static void MarkDeleted<T>(this T t)
        {
            var type = typeof(T);
           
        }
    }
}