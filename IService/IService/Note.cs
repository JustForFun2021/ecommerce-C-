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
    public partial class Note
    {
        [DataMember]
        public int NoteId { get; set; }

        [DataMember]
        public string NoteBody { get; set; }

        [DataMember]
        public Nullable<int> UserId { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public User User { get; set; }


        [DataMember]
        public Nullable<int> OrderId { get; set; }

        [DataMember]
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [DataMember]
        public Nullable<int> ProductId { get; set; }

        [DataMember]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
