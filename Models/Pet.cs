using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{

    public enum PetBreedType {}
    public enum PetColorType {}
    public class Pet{
        public int id {get; set;}
        [Required]
        public string name {get; set;}
        [Required]
        public string PetBreed {get; set;}
        [Required]
        public string PetColor {get; set;}
        public DateTime checkedInAt {get; set;}
        public PetOwner owner {get; set;}
        public int petOwnerid {get; set;}




            
    }
}


// int petOwnerid (required):