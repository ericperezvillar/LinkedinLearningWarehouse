using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces
{
    public interface IIntegratorHistoryService
    {
        Task<DateTime> GetLatestProcessDateAsync();

        Task SetNewHistoryProcessAsync(DateTime date);
    }
}
