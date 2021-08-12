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
    public partial class Message
    {
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public string NewMessage { get; set; }
        //===========================================
        [DataMember]
        public Nullable<int> UserId { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public virtual User user { get; set; }


    }
}
