using System;
using BK9K.Mechanics.Types.Variables;
using OpenRpg.Core.Variables;

namespace BK9K.Mechanics.Extensions
{
    public static class IVariableExtensions
    {
        public static Guid UniqueId(this IVariables<object> variables)
        {
            if (!variables.HasVariable(CustomVariableTypes.UniqueId))
            { variables[CustomVariableTypes.UniqueId] = Guid.NewGuid(); }

            return (Guid)variables[CustomVariableTypes.UniqueId];
        }

        public static void UniqueId(this IVariables<object> variables, Guid value)
        { variables[CustomVariableTypes.UniqueId] = value; }

        public static string AssetCode(this IVariables<object> variables)
        { return (string)variables[CustomVariableTypes.UniqueId]; }

        public static void AssetCode(this IVariables<object> variables, string value)
        { variables[CustomVariableTypes.UniqueId] = value; }
    }
}