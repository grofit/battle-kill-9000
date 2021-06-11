using System.Collections.Generic;
using BK9K.UAI.Applicators;

namespace BK9K.UAI.Advisors.Applicators.Registries
{
    public class DefaultAdviceApplicatorRegistry : DefaultApplicatorRegistry, IAdviceApplicatorRegistry
    {
        public DefaultAdviceApplicatorRegistry(IEnumerable<IAdviceApplicator> applicators = null) : base(applicators)
        {
        }
    }
}