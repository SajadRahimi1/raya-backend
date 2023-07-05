using AutoMapper;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<ClassCategoryDto,ClassCategory>();
    }
}