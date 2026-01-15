namespace Identity.Data
{
    using AutoMapper;
    using Identity.Domain.Entities;
    using Identity.DTO;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<ApplicationRole, RoleDto>();
            CreateMap<CreateRoleRequest, ApplicationRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpperInvariant()));

            CreateMap<UpdateRoleRequest, ApplicationRole>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpperInvariant()));
        }
    }

}
