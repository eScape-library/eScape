﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eScape.Core.Helper;

namespace eScape.UseCase.DTOs
{
    public class SubCategoryDTO
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public required string SubCategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? CategoryName { get; set; }
        public string? Slug { get; set; }

    }
}
