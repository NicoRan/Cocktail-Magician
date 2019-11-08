using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class BarDTOMapper
    {
        public static BarDTO ToDTO(this Bar bar)
        {
            var barDTO = new BarDTO
            {
                Id = bar.BarId,
                Name = bar.Name,
                Information = bar.Information,
                Address = bar.Address,
                Picture = bar.Picture,
                MapDirection = bar.MapDirections,
                IsDeleted = bar.IsDeleted
            };
            return barDTO;
        }

        public static ICollection<BarDTO> ToDTO(this ICollection<Bar> bars)
        {
            var newCollection = bars.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
