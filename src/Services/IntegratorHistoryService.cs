using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Interfaces;
using LinkedinLearningWarehouse.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LinkedinLearningWarehouse.Services
{
    public class IntegratorHistoryService : IIntegratorHistoryService
    {
        private readonly LinkedinLearningDbContext _dbContext;
        private readonly ILogger _logger;

        public IntegratorHistoryService(LinkedinLearningDbContext dbContext)
        {
            _logger = Log.ForContext<IntegratorHistoryService>();
            _dbContext = dbContext;
        }

        public async Task<DateTime> GetLatestProcessDateAsync()
        {
            try
            {
                var process = await _dbContext.IntegratorHistories.OrderBy(x => x.DateOfProcessing).LastOrDefaultAsync();
                if (process == null)
                    return new DateTime(2024, 1, 1); // Start processing from 1st of January 2024

                return process.DateOfProcessing;
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Integration History data from DB. " + ex.Message);
                return new DateTime(2024, 1, 1); // Start processing from 1st of January 2024
            }


        }

        public async Task SetNewHistoryProcessAsync(DateTime date)
        {
            try
            {
                await _dbContext.IntegratorHistories.AddAsync(new IntegratorHistory() { DateOfProcessing = date });
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("Error setting Integration History data from DB. " + ex.Message);
            }
        }
    }
}
