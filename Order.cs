using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IService
{
    [DataContract(IsReference = true)]
    public partial class Order
    {

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [DataMember]
        public Nullable<decimal> SumTotalOreder { get; set; }

        //[DataMember]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy}")]
        //public Nullable<System.DateTime> RequireDate { get; set; }

        //[DataMember]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        //public Nullable<System.DateTime> ShippedDate { get; set; }

        //[DataMember]
        //public string shippingaddresses { get; set; }

        [DataMember]
        public Nullable<int> UserId { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
