using System;
using SystemsRx.Events;
using EcsRx.MicroRx.Events;

namespace BK9K.Web.Debug
{
    public class DebugEventSystem : EventSystem, IEventSystem
    {
        public DebugEventSystem(IMessageBroker messageBroker) : base(messageBroker)
        {
            Console.WriteLine("Debugging Event System");
        }
        
        public new void Publish<T>(T message)
        {
            Console.WriteLine($"Event Published For {message.GetType().Name}");
            base.Publish(message);
        }
    }
}