using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces
{
    public interface IQueryParameterBuilderService
    {
        Task<List<string>> BuildQueryString(DateTime dateProcessor);
    }
}
