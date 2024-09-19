using LinkedinLearningWarehouse.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces.LearningActivity
{
    public interface IEnterpriseGroupsService
    {
        Task CreateOrUpdateEnterpriseGroupAsync(List<string> enterpriseGroups, int learnerId);
    }
}
