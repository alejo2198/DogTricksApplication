using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogTricksApplication.Models
{
    public class DogTrick
    {
        [Key]
        public int DogTrickId { get; set; }

        public DateTime DogTrickDate { get; set; }

        [ForeignKey("Dogs")]
        public int DogId { get; set; }
        public virtual Dog Dogs { get; set; }

        [ForeignKey("Tricks")]
        public int TrickId { get; set; }
        public virtual Trick Tricks { get; set; }

   
    }
    public class DogxTrickDto
    {
        public int DogTrickId { get; set; }
        public DateTime DogTrickDate { get; set; }
    }
}