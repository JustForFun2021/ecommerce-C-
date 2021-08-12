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
    public partial class Friends
    {
        [DataMember]
        public int FriendsId { get; set; }

        [DataMember]
        public int User1 { get; set; }
        [DataMember]
        public int Friend { get; set; }

    }
}
