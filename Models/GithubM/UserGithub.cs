namespace GithubAPI.Models;
public class UserGitHub
{
    public int Id { get; set; }
    public string Login { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Company { get; set; } = default!;
}