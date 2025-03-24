using System.Formats.Asn1;
using AutoMapper;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.DB;
using TaskAlbayan.DB.Models;

namespace TaskAlbayan.Common.Mappers
{
    public class TaskAlbayanAutoMapperProfile : Profile
    {
        public TaskAlbayanAutoMapperProfile()
        {
            CreateMap<CreateUpdateClientDto , Client>();
            CreateMap<Client , ClientDto>();


            CreateMap<CreateUpdateTaskDto , Task>();
            CreateMap<Task , TaskDto>();

              CreateMap<CreateUpdateVisitDto , Visit>();
            CreateMap<Visit , VisitDto>();

            CreateMap<CreateUpdateVisitTaskDto , VisitTask>();
            CreateMap<VisitTask , VisitTaskDto>();




            CreateMap<CreateUpdateUserDto , User>();
            CreateMap<User , UserDto>();
        }
    }
}