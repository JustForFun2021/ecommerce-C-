using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IService
{
    [DataContract(IsReference = true)]
    public partial class WishList
    {

        [DataMember]
        public int WishListId { get; set; }

        //[DataMember]
        //public Nullable<decimal> SumTotalWishList { get; set; }

        //[DataMember]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        //public Nullable<System.DateTime> OrderDate { get; set; }

        [DataMember]
        public Nullable<int> ProductId { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
