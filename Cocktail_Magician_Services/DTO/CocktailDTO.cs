using System.Collections.Generic;

namespace Cocktail_Magician_Services.DTO
{
    public class CocktailDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }
        public double Rating { get; set; }
        public ICollection<CocktailReviewDTO> CocktailReviewDTOs { get; set; }
    }
}
