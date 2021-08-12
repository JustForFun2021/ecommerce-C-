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
    public partial class Shipper
    {

        [DataMember]
        public int ShipperId { get; set; }

        [DataMember]
        public string ShippingTo { get; set; }

        [DataMember]
        public decimal ShippingCost { get; set; }

        [DataMember]
        public int OrderBusinessDays { get; set; }

        [DataMember]
        public int MaxDeliveryDays { get; set; }

        [DataMember]
        public string GetPaidBy { get; set; }

        //[DataMember]
        //public Nullable<int> ProductId { get; set; }

        //[DataMember]
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
    }
}
