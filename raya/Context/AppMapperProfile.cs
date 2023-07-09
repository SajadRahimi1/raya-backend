using AutoMapper;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<ClassCategoryDto,ClassCategory>();
        CreateMap<ReserveNurseDto,ReserveNurse>();
        CreateMap<CreateNurseDto,Nurse>();
        CreateMap<UpdateUserDto,User>();
    }
}