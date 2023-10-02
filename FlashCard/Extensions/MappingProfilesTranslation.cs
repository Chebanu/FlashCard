using AutoMapper;

namespace FlashCard.Extentions;

public class MappingProfilesTranslation: Profile
{
    public MappingProfilesTranslation()
    {
        CreateMap<Translation, Translation>();
    }
}

public class MappingProfilesWord : Profile
{
    public MappingProfilesWord()
    {
        CreateMap<Word, Word>();
    }
}
