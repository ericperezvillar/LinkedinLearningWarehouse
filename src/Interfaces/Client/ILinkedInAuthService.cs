using LinkedinLearningWarehouse.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Interfaces.Client
{
    public interface ILinkedInAuthService
    {
        Task<OAuthTokenResponse> GetAccessTokenAsync();
    }
}
