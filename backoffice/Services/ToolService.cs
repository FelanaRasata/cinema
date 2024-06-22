namespace backoffice.Services;

public class ToolService
{
    public List<int> GetNumbers()
    {
        return Enumerable.Range(1, 5).ToList();
    }

    public List<string> GetCategories()
    {
        string[] array = { "Romance", "Thriller", "Science-Fiction" };
        return new List<string>(array);
    }
}