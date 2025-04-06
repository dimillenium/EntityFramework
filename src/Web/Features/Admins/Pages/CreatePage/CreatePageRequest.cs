namespace Web.Features.Admins.Pages.CreatePage;

public class CreatePageRequest
{
    public string Slug { get; set; } = default!;
    public string Background { get; set; } = default!;
    public string Section1 { get; set; } = default!;
    public string Section2{ get; set; } = default!;
}