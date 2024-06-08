using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogTricksApplication.Models
{
    public class DogxTrick
    {
        [Key]
        public int DogTrickId { get; set; }

        public DateTime DogTrickDate { get; set; }

        //many dogs can learn many tricks
        public ICollection<Dog> Dogs { get; set; }

        public ICollection<Trick> Tricks { get; set; }
   
    }
}