using System.Collections.Generic;

namespace BK9K.UAI.Applicators
{
    public interface IApplicatorRegistry
    {
        void AddApplicator(IApplicator applicator);
        void RemoveApplicator(IApplicator applicator);
        IEnumerable<IApplicator> GetApplicators();

        void ApplyAll(IAgent context);
        void ApplyOnlyPriority(IAgent context, int specificPriority);
    }
}