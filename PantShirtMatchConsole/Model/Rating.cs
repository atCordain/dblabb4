namespace PantShirtMatchConsole
{
    public class Rating
    {
        public int Id { get; set; }
        public virtual Look Look {get;set;}
        public virtual RatingCategory Category { get; set; }
        public int Points { get; set; }
    }
}