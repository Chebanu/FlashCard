using AutoMapper;
using FlashCard.Model.DTO.TranslationDto;

namespace FlashCard.Extentions;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<TranslationRequest, Translation>()
				.ForMember(dest => dest.SourceWordId, opt => opt.MapFrom(src => src.SourceLanguageId))
				.ForMember(dest => dest.TargetWordId, opt => opt.MapFrom(src => src.TargetLanguageId))
				.ForMember(dest => dest.TranslationId, opt => opt.Ignore())
				.ForMember(dest => dest.SourceWord, opt => opt.Ignore())
				.ForMember(dest => dest.TargetWord, opt => opt.Ignore());

		CreateMap<Translation, TranslationResponse>()
				.ForMember(dest => dest.SourceWord, opt => opt.MapFrom(src => src.SourceWord.WordText))
				.ForMember(dest => dest.SourceWordLevelName, opt => opt.MapFrom(src => src.SourceWord.Level.LevelName))
				.ForMember(dest => dest.SourceLanguageName, opt => opt.MapFrom(src => src.SourceWord.Language.LanguageName))
				.ForMember(dest => dest.TargetLanguageName, opt => opt.MapFrom(src => src.TargetWord.Language.LanguageName))
				.ForMember(dest => dest.TargetWord, opt => opt.MapFrom(src => src.TargetWord.WordText))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.SourceWord.ImageUrl != null ? src.SourceWord.ImageUrl : ""));

		CreateMap<TranslationUpdateRequest, Translation>()
			.ForMember(dest => dest.SourceWordId, opt => opt.MapFrom(src => src.SourceWordId))
			.ForMember(dest => dest.TargetWordId, opt => opt.MapFrom(src => src.TargetWordId));


		CreateMap<Translation, Translation>();
		CreateMap<Word, Word>();
	}
}