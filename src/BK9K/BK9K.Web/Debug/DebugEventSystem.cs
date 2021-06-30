using System;
using System.Threading.Tasks;
using SystemsRx.Events;
using SystemsRx.Threading;
using EcsRx.MicroRx.Events;
using IMessageBroker = SystemsRx.MicroRx.Events.IMessageBroker;

namespace BK9K.Web.Debug
{
    public class DebugEventSystem : EventSystem, IEventSystem
    {
        public DebugEventSystem(IMessageBroker messageBroker, IThreadHandler threadHandler) : base(messageBroker, threadHandler)
        {
            Console.WriteLine("Debugging Event System");
        }

        public new void Publish<T>(T message)
        {
            Console.WriteLine($"Event Published For {message.GetType().Name}");

                try
                { base.Publish(message); }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }
        
        public new Task PublishAsync<T>(T message)
        {
            Console.WriteLine($"Event Published ASync For {message.GetType().Name}");

            try
            {
                return base.PublishAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Task.FromException(e);
            }
        }
    }
}