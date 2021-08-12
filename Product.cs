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
    public partial class Product
    {

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Nullable<double> Retad { get; set; }

        [DataMember]
        public string ProductCondetion { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public Nullable<int> UnitInStock { get; set; }

        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public string video { get; set; }

        [DataMember]
        public Nullable<int> SubCatgoreyId { get; set; }

        [DataMember]
        public Nullable<float> Discount { get; set; }

        [DataMember]
        [ForeignKey("SubCatgoreyId")]
        public virtual SubCategory SubCatgorey { get; set; }

        [DataMember]
        public Nullable<int> UserId { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [DataMember]
        public Nullable<int> CategoryId { get; set; }

        [DataMember]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [DataMember]
        public Nullable<int> ShipperId { get; set; }

        [DataMember]
        [ForeignKey("ShipperId")]
        public Shipper Shipper { get; set; }

    }
}
