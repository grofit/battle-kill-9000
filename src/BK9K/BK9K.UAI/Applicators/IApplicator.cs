using OpenRpg.Core.Requirements;

namespace BK9K.UAI.Applicators
{
    public interface IApplicator : IHasRequirements
    {
        int Priority { get; }
        bool CanApplyTo(IAgent agent);
        void ApplyTo(IAgent agent);
    }
}