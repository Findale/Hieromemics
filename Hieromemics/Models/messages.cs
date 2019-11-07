using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Hieromemics.Models
{
    public class messages {
        [Key]
        public int messageID {get; set;}

        [ForeignKey("pictures")]
        public int PicID {get; set;}

        public pictures pictures {get; set;}

        [ForeignKey("users")]
        public int UserID {get; set;} //Sender

        public int FriendCode {get; set;} //Receiver

        public users users {get; set;}

        public DateTime timestamp {get; set;} = DateTime.Now;

    }
}