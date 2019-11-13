using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Hieromemics.Models
{
    public class pictures {
        [Key]
        public int PicID {get; set;}

        [StringLength(255, MinimumLength = 4)]
        [Required]
        public string StoragePath {get; set;}

        //[InverseProperty("templates")]
        public IList<templates> templates {get; set;}

        //[InverseProperty("SavedPics")]
        public IList<SavedPics> SavedPics {get; set;}

        //[InverseProperty("messages")]
        public IList<messages> messages {get; set;}

    }
}