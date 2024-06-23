namespace backoffice.Services;

public class ToolService
{
    public List<int> GetNumbers()
    {
        return Enumerable.Range(1, 5).ToList();
    }

   
}