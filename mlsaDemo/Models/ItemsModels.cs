﻿using System.ComponentModel.DataAnnotations;

namespace mlsaDemo.Models
{
        public class ItemsModel
        {
            [Key]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

    }

