using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EnumToStringListConverter : ValueConverter<ICollection<NurseCategory>, List<string>>
{
    public EnumToStringListConverter(IEnumerable<NurseCategory> mappingValues) : base(t => t.Select(e => e.ToString()).ToList(), s => s.Select(Enum.Parse<NurseCategory>).ToList())
    {
        MappingValues = mappingValues;
    }

    public IEnumerable<NurseCategory> MappingValues { get; }
}
