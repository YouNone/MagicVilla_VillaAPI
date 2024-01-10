using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id = 1, Name = "Pool view", Sqft = 100, Occupancy = 4 },
            new VillaDto{Id = 2, Name = "Beach view", Sqft = 200, Occupancy = 3}
        };
    }
}
