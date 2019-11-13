using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hieromemics.Models
{
    public class templates {
        [Key]
        public int TemplateID {get; set;}
        [ForeignKey("pictures")]
        public int PicID {get; set;}
        
        public pictures pictures {get; set;}
    }
}