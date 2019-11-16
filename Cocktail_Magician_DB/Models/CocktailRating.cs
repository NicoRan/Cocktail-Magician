namespace Cocktail_Magician_DB.Models
{
    public class CocktailRating
    {
        public double Grade { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
