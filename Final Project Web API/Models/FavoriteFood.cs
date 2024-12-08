namespace Final_Project_Web_API.Models
{
    public class FavoriteFood
    {
        // Ethan Rettinger
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Type { get; set; }  // E.g., Breakfast, Lunch
        public bool IsVegetarian { get; set; }
        public int Rating { get; set; }  // 1 to 5 scale
    }
}
