using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Hieromemics.Models
{
    public class SavedPics {
        [Key]
        public int SavedPicID {get; set;}
        [ForeignKey("users")]
        public int UserID {get; set;}

        public users users {get; set;}
        [ForeignKey("pictures")]
        public int PicID {get; set;}
        
        public pictures pictures {get; set;}
    }
}