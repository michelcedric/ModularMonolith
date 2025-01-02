using My.ModularMonolith.Common.Domain.Entities;

namespace My.ModularMonolith.MyBModule.Domain.Entities;

public class ModelB :  BaseEntity<Guid>
{
    public string Title { get; set; }
    public int Year { get; set; }

    public static ModelB Create(string name, int year)
    {
        return new ModelB(name, year);
    }
    
    private ModelB(string title, int year) : base(Guid.NewGuid())
    {
        Title = title;
        Year = year;
    }
}