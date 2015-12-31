using AutoMapper;
using CommonMark;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Enums;
using ITLearning.Shared.Extensions;
using ITLearning.Shared.Formatters;
using System;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
		private static void CreateTasksMappings()
        {
            Mapper.CreateMap<Task, TaskListItemData>()
               .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()))
               .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group.Id))
               .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));

            Mapper.CreateMap<TaskInstance, TaskInstanceData>()
                .ForMember(dest => dest.RepositoryLink, opt => opt.MapFrom(src => UrlFormatter.FormatSourceControlUrl(src.GitRepository.Name)))
                .ForMember(dest => dest.CodeReview, opt => opt.MapFrom(src => src.TaskInstanceReview))
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.GitRepository.Branches))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate != default(DateTime) ? src.StartDate.ToShortDateString() : ""))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.FinishDate != default(DateTime) ? src.FinishDate.ToShortDateString() : ""));

            Mapper.CreateMap<Task, TaskBaseData>()
                .ForMember(dest => dest.RepositoryLink, opt => opt.MapFrom(src => UrlFormatter.FormatSourceControlUrl(src.GitRepository.Name)))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.GitRepository.Branches));

            Mapper.CreateMap<TaskInstanceReview, CodeReviewData>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.TaskInstance.GitRepository.Branches));

            Mapper.CreateMap<TaskBaseData, EditTaskRequestData>()
                .ForMember(dest => dest.SelectedLanguage, opt => opt.MapFrom(src => src.Language.GenerateDisplayData()))
                .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => default(LanguageEnum).GenerateDisplayDataList()))
                .ForMember(dest => dest.SelectedGroup, opt => opt.MapFrom(src => src.Group));

            Mapper.CreateMap<TaskBaseData, TaskData>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => CommonMarkConverter.Convert(src.Description, null)))
                .ForMember(dest => dest.SelectedLanguage, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.UserGroup, opt => opt.MapFrom(src => src.Group));
        }
    }
}