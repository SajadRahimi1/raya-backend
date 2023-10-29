using AutoMapper;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<ClassCategoryDto,ClassCategory>();
        CreateMap<ReserveNurseDto,ReserveNurse>();
        CreateMap<CreateNurseDto,Nurse>();
        CreateMap<UpdateUserDto,User>();
        CreateMap<SendMessageDto,Message>();
        CreateMap<AdminMessageDto,Message>();
        CreateMap<ReserveClassDto,ReserveClass>();
        CreateMap<AdminDto,Admin>();
        CreateMap<NurseDto,Nurse>();
    }
}