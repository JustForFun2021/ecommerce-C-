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
    public partial class OrderDetail
    {

        [DataMember]
        public int OrderDetailId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public Nullable<int> OrderId { get; set; }

        [DataMember]
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [DataMember]
        public Nullable<int> MessageId { get; set; }

        [DataMember]
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }

        [DataMember]
        public Nullable<int> ProductId { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


    }
}
