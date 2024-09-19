using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces.LearningActivity
{
    public interface IEngagementMetricTypeService
    {
        Task<int> GetMetricTypeIdAsync(string engagementTypeName);
    }
}
