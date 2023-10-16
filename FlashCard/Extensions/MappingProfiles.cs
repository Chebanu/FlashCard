﻿using AutoMapper;
using FlashCard.Model.DTO.TranslationDto;
using FlashCard.Model.DTO.WordDto;
using FlashCard.Shared.Enums;

namespace FlashCard.Extentions;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Translation, TranslationResponse>()
			.ForMember(dest => dest.TranslationId, opt => opt.MapFrom(src => src.TranslationId))
			.ForMember(dest => dest.SourceWord, opt => opt.MapFrom(src => src.SourceWord.WordText))
			.ForMember(dest => dest.TargetWord, opt => opt.MapFrom(src => src.TargetWord.WordText))
			.ForMember(dest => dest.SourceLevelName, opt => opt.MapFrom(src => src.SourceWord.Level.LevelName))
			.ForMember(dest => dest.SourceLanguageName, opt => opt.MapFrom(src => src.SourceWord.Language.LanguageName))
			.ForMember(dest => dest.TargetLanguageName, opt => opt.MapFrom(src => src.TargetWord.Language.LanguageName))
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.SourceWord.ImageUrl));

		/*CreateMap<TranslationUpdateRequest, Translation>()
			.ForMember(dest => dest.TranslationId, opt => opt.MapFrom(src => src.TranslationId))
			.ForMember(dest => dest.SourceWordId, opt => opt.Ignore())
			.ForMember(dest => dest.TargetWordId, opt => opt.Ignore());*/
/*
		CreateMap<TranslationResponse, TranslationUpdateRequest>()
			.ForMember(dest => dest.TranslationId, opt => opt.MapFrom(src => src.TranslationId))
			.ForMember(dest => dest.SourceWord.WordText, opt => opt.MapFrom(src => src.SourceWord))
			.ForMember(dest => dest.TargetWord.WordText, opt => opt.MapFrom(src => src.TargetWord))
			.ForMember(dest => dest.SourceWord.Language.LanguageName, opt => opt.MapFrom(src =>
																		src.SourceLanguageName))
			.ForMember(dest => dest.TargetWord.Language.LanguageName, opt => opt.MapFrom(src =>
																		src.TargetLanguageName))
			.ForMember(dest => dest.SourceWord.Level.LevelName, opt => opt.MapFrom(src => src.SourceLevelName))
			.ForMember(dest => dest.SourceWord.ImageUrl, opt => opt.MapFrom(src => src.Image));*/

		CreateMap<WordRequest, Word>()
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.Language, opt => opt.Ignore())
			.ForMember(dest => dest.Level, opt => opt.Ignore())
			.ForMember(dest => dest.WordId, opt => opt.Ignore());

		CreateMap<Word, WordResponse>()
			.ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.WordId))
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.LevelName))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.LanguageName));

		CreateMap<WordUpdateRequest, Word>()
			.ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.WordId))
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.Language, opt => opt.Ignore())
			.ForMember(dest => dest.Level, opt => opt.Ignore());

		CreateMap<WordResponse, WordUpdateRequest>()
			.ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.WordId))
			.ForMember(dest => dest.WordText, opt => opt.MapFrom(src => src.WordText))
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src =>
												Enum.Parse<LanguageOfTheWord>(src.Language)))
			.ForMember(dest => dest.Level, opt => opt.MapFrom(src =>
												Enum.Parse<LevelOfTheWord>(src.Level)))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
	}
}