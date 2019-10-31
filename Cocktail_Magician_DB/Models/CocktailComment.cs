﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class CocktailComment
    {
        public string Comment { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
