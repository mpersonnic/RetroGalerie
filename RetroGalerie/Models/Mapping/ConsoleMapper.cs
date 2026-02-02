using RetroGalerie.Data;
using DataConsole = RetroGalerie.Data.Console;

namespace RetroGalerie.Models.Mapping
{
    public class ConsoleMapper : BaseMapper<DataConsole, ConsoleViewModel>
    {
        public override ConsoleViewModel ToViewModel(DataConsole entity)
        {
            if (entity == null) return null;

            return new ConsoleViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Manufacturer = entity.Manufacturer,
                ReleaseYear = entity.ReleaseYear,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                Games = entity.Games
            };
        }

        public override DataConsole ToEntity(ConsoleViewModel vm)
        {
            if (vm == null) return null;

            return new DataConsole
            {
                Id = vm.Id,
                Name = vm.Name,
                Manufacturer = vm.Manufacturer,
                ReleaseYear = vm.ReleaseYear,
                ImageUrl = vm.ImageUrl,
                Description = vm.Description,
                Games = vm.Games
            };
        }
    }
}
