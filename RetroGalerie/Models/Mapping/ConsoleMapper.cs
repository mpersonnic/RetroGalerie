using RetroGalerie.Data;
using DataConsole = RetroGalerie.Data.Console;

namespace RetroGalerie.Models.Mapping
{
    public class ConsoleMapper : BaseMapper<DataConsole, ConsoleViewModel>
    {
        public override ConsoleViewModel ToViewModel(DataConsole entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new ConsoleViewModel
            {
                Id = entity.Id,
                Name = entity.Name ?? string.Empty,
                Manufacturer = entity.Manufacturer ?? string.Empty,
                ReleaseYear = entity.ReleaseYear,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                Games = entity.Games ?? new List<Game>()
            };
        }

        public override DataConsole ToEntity(ConsoleViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            return new DataConsole
            {
                Id = vm.Id,
                Name = vm.Name,
                Manufacturer = vm.Manufacturer,
                ReleaseYear = vm.ReleaseYear,
                ImageUrl = vm.ImageUrl ?? string.Empty,
                Description = vm.Description,
                Games = vm.Games ?? new List<Game>()
            };
        }
    }
}
