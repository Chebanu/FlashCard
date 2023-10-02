using AutoMapper;

namespace FlashCard.Extentions;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Translation, Translation>();
		CreateMap<Word, Word>();
	}
}