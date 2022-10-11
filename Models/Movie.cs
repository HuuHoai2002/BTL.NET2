namespace BTL.NET2.Models;

// tmdb
public class Movie
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public string? Name { get; set; }
  public string? Overview { get; set; }
  public string? Poster_path { get; set; }
  public string? Backdrop_path { get; set; }
  public string? First_air_date { get; set; }
  public string? Original_language { get; set; }
  public string? Original_name { get; set; }
  public string? Original_title { get; set; }
  public string? Vote_average { get; set; }
  public string? Vote_count { get; set; }
}