

using System.Collections.Generic;

namespace Cocktail_Magician_Services.DTO
{
    public class BarDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Information { get; set; }
        public string Picture { get; set; }
        public string MapDirection { get; set; }
        public bool IsDeleted { get; set; }
        public double Rating { get; set; }
        public ICollection<CocktailDTO> CocktailDTOs { get; set; }
        public ICollection<BarReviewDTO> BarReviewDTOs { get; set; }
    }
}
