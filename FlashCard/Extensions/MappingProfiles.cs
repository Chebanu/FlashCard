using AutoMapper;
using FlashCard.Model.DTO.TranslationDto;
using FlashCard.Model.DTO.WordDto;

namespace FlashCard.Extentions;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<TranslationRequest, Translation>()
			.ForMember(dest => dest.SourceWord, opt => opt.MapFrom(src => src.SourceWord))
			.ForMember(dest => dest.TargetWord, opt => opt.MapFrom(src => src.TargetWord))
			.ForMember(dest => dest.TranslationId, opt => opt.Ignore());

		CreateMap<Translation, TranslationResponse>()
			.ForMember(dest => dest.SourceWord, opt => opt.MapFrom(src => src.SourceWord.WordText))
			.ForMember(dest => dest.TargetWord, opt => opt.MapFrom(src => src.TargetWord.WordText))
			.ForMember(dest => dest.SourceTheme, opt => opt.MapFrom(src => src.SourceWord.Theme))
			.ForMember(dest => dest.TargetTheme, opt => opt.MapFrom(src => src.TargetWord.Theme))
			.ForMember(dest => dest.SourceWordLevelName, opt => opt.MapFrom(src => src.SourceWord.Level.LevelName))
			.ForMember(dest => dest.SourceLanguageName, opt => opt.MapFrom(src => src.SourceWord.Language.LanguageName))
			.ForMember(dest => dest.TargetLanguageName, opt => opt.MapFrom(src => src.TargetWord.Language.LanguageName))
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.SourceWord.ImageUrl));

		CreateMap<TranslationUpdateRequest, Translation>()
			.ForMember(dest => dest.SourceWordId, opt => opt.MapFrom(src => src.SourceWordId))
			.ForMember(dest => dest.TargetWordId, opt => opt.MapFrom(src => src.TargetWordId));


		CreateMap<WordRequest, Word>()
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.Language, opt => opt.Ignore())
			.ForMember(dest => dest.Level, opt => opt.Ignore())
			.ForMember(dest => dest.WordId, opt => opt.Ignore());

		CreateMap<Word, WordResponse>()
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.LevelName))
			.ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme.ThemeName))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.LanguageName))
			.ReverseMap();

		CreateMap<WordUpdateRequest, Word>()
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.LevelId, opt => opt.MapFrom(src => src.LevelId))
			.ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId));
	}
}