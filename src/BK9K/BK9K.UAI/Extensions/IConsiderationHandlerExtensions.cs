using BK9K.UAI.Considerations;
using BK9K.UAI.Handlers.Considerations;

namespace BK9K.UAI.Extensions
{
    public static class IConsiderationHandlerExtensions
    {
        public static void RemoveConsideration(this IConsiderationHandler handler, IConsideration consideration)
        { handler.RemoveConsideration(consideration); }
    }
}