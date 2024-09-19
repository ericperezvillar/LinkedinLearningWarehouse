using LinkedinLearningWarehouse.DTOs.LearningActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces.LearningActivity
{
    public interface ILearnerDetailsService
    {
        Task<int> CreateOrUpdateLearnerDetailAsync(LearnerDetailsDto learnerDto);
    }
}
