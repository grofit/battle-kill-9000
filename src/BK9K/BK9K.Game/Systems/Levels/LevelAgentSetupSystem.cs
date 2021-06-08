using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.AI;
using BK9K.Game.Events.Level;
using BK9K.Game.Levels;

namespace BK9K.Game.Systems.Levels
{
    public class LevelAgentSetupSystem : IReactToEventSystem<LevelUnitsSetupCompleteEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }
        public ConsiderationGenerator ConsiderationGenerator { get; }
        public AdviceGenerator AdviceGenerator { get; }
        
        public LevelAgentSetupSystem(Level level, IEventSystem eventSystem, ConsiderationGenerator considerationGenerator, AdviceGenerator adviceGenerator)
        {
            Level = level;
            EventSystem = eventSystem;
            ConsiderationGenerator = considerationGenerator;
            AdviceGenerator = adviceGenerator;
        }

        public void Process(LevelUnitsSetupCompleteEvent eventData)
        {
            ProcessAgentLocalConsiderations();
            ProcessAgentLevelConsiderations();
            ProcessAgentAdvice();
            Level.IsLevelLoading = false;
            EventSystem.Publish(new LevelLoadedEvent());
        }

        public void ProcessAgentLocalConsiderations()
        {
            foreach (var gameUnit in Level.GameUnits)
            {
                ConsiderationGenerator.PopulateLocalConsiderations(gameUnit.Agent);
            }
        }

        public void ProcessAgentLevelConsiderations()
        {
            foreach (var gameUnits in Level.GameUnits)
            {
                ConsiderationGenerator.PopulateExternalConsiderations(gameUnits.Agent, Level);
            }
        }

        public void ProcessAgentAdvice()
        {
            foreach (var gameUnits in Level.GameUnits)
            {
                AdviceGenerator.PopulateAdvice(gameUnits.Agent, Level);
            }
        }
    }
}