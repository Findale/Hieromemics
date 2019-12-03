using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Hieromemics.Models {

    public class pendingMatch {
        [Key]

    public int pendingId {get; set;}

    public int lookingId {get; set;}

    public string seekingId {get; set;}
    }

}