using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BK9K.UAI.Tester.Variables.Types;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Tester.Variables.Extensions
{
    public static class DebugExtensions
    {
        private static IDictionary<int, string> GetTypeFieldsDictionary(Type typeObject)
        {
            return typeObject
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .ToDictionary(x => (int)x.GetValue(null), x => x.Name);
        }

        private static IDictionary<int, string> TextLookups = GetTypeFieldsDictionary(typeof(UtilityVariableTypes));
        public static void PrintVariables(this IUtilityVariables utilityVariables)
        {
            foreach (var (key, value) in utilityVariables)
            {
                var keyText = key.RelatedId == 0 ? TextLookups[key.UtilityId] : $"{TextLookups[key.UtilityId]}:{key.RelatedId}";
                Console.WriteLine($"{keyText} = {value}");
            }
        }
    }
}