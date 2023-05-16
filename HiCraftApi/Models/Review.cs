﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HiCraftApi.Services.CraftManServices;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace HiCraftApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ClientID { get; set; }
        public Custmer Client { get; set; }

        [ForeignKey("Craftsman")]
        public string CraftsmanId { get; set; }
        public CraftManModel Craftsman { get; set; }
        [Required]
        public string Details  { get; set; }
        [System.ComponentModel.DataAnnotations.Required, Range(0, 5)]
        
        public float RateOFthisWork { get; set; }
    }

}
