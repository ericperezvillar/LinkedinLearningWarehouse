using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.Models.LearningAsset
{
    public class AssetAvailableLocale
    {
        public int AssetAvailableLocaleId { get; set; }
        public int AssetId { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
    }
}
