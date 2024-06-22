using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogTricksApplication.Models
{
    public class Dog
    {
        [Key]
        public int DogId { get; set; }
        public string DogName { get; set; }
        public int DogAge { get; set; }
        public string DogBreed { get; set; }
        public DateTime DogBirthday { get; set; }

        //a dog belongs to one user
        //a user can have many dogs(one for this MVP)
        [ForeignKey("AspNetUsers")]
        public string DogOwner { get; set; }
        public virtual ApplicationUser AspNetUsers { get; set; }

        //a dog can learn many tricks
        public ICollection<DogxTrick> Tricks { get; set; }

    }

    public class DogDto
    {
        public int DogId { get; set; }
        public string DogName { get; set; }
        public int DogAge { get; set; }
        public string DogBreed { get; set; }
        public DateTime DogBirthday { get; set; }

    }
}