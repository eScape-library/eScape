﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Entities
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
