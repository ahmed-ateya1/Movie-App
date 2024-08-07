namespace MovieApp.UI.Areas.Admin.Data
{
    public class AdminViewModel
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int FavoriteMoviesCount { get; set; }
    }
}
