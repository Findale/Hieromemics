using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Hieromemics.Models
{
    public class friendList {
        [Key]
        public int FriendListID {get; set;}

        [ForeignKey("users")]
        public int UserID{get; set;}

        public string FriendCode {get; set;}

        public users users {get; set;}
    }
}
