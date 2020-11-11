using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pet_hotel
{
    public class PetOwner {
        // int id: primary key
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string emailAddress { get; set; }
        [JsonIgnore]
        public ICollection<Pet> pets {get; set;}
        [NotMapped]
        public int petCount {
            get{
                return (this.pets == null ? 0 : this.pets.Count);
            }
        }
    }
}



// int petCount: The number of pets owned by this person. 
