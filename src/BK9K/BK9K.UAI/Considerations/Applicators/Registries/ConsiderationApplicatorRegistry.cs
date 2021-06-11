using System.Collections.Generic;
using System.Linq;
using BK9K.UAI.Applicators;

namespace BK9K.UAI.Considerations.Applicators.Registries
{
    public class DefaultConsiderationApplicatorRegistry : DefaultApplicatorRegistry, IConsiderationApplicatorRegistry
    {
        public DefaultConsiderationApplicatorRegistry(IEnumerable<IConsiderationApplicator> applicators = null) : base(applicators)
        {
        }
    }
}