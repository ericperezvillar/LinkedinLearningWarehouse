using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public class AssetDiscoverableByDto
    {
        [JsonProperty("accessor")]
        public OwnerDto AccessorsDto { get; set; }
    }
}
