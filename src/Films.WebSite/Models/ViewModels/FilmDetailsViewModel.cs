namespace Films.WebSite.Models.ViewModels
{
    public class FilmDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }

        public string Poster { get; set; }

        public FilmModel ToModel()
        {
            return new FilmModel
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Director = Director,
                Year = Year,
                Poster = null
            };
        }
    }
}
