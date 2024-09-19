using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinLearningWarehouse.DTOs.LearningAsset
{
    public  class AssetPathDto
    {
        public OwnerDto Owner { get; set; }
        public NameDto Name { get; set; }
        public string Urn { get; set; }
        public string Type { get; set; }
    }
}
