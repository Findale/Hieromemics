using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Hieromemics.Models
{

    public class users {
        [Key]
        public int UserID {get; set;}

        public string FriendCode {get; set;} = Guid.NewGuid().ToString("N");

        [Display(Name="User Name")]
        public string userName {get; set;}

        //[InverseProperty("SavedPics")]
        public IList<SavedPics> SavedPics {get; set;}

        //[InverseProperty("friendList")]
        public IList<friendList> friendList {get; set;}

        //[InverseProperty("messages")]
        public IList<messages> messages {get; set;}
    }
}