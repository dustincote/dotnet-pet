using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pet_hotel{


// Set up table with the following columns: id (primary key),
// transaction type and the time the action occurred.

    public class Transaction
    {
        public int id { get; set; }
        public string transaction {get; set;}
        public DateTime transactionTime {get; set;}
        

        // public  Transaction (string note){
        //     this.transaction = note;
        // }
    }


}