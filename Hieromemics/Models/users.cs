using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Hieromemics.Models
{
    public class users {
        [Key]
        public int UserID {get; set;}

        public int FriendCode {get; set;}

        //[InverseProperty("SavedPics")]
        public IList<SavedPics> SavedPics {get; set;}

        //[InverseProperty("friendList")]
        public IList<friendList> friendList {get; set;}

        //[InverseProperty("messages")]
        public IList<messages> messages {get; set;}
    }
}