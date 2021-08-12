using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace IService
{
    [DataContract(IsReference = true)]
    public partial class SubCategory
    {

        [DataMember]
        public int SubCategoryId { get; set; }

        [DataMember]
        public string SubCategoryName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
