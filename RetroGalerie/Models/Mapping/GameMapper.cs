using RetroGalerie.Data;

namespace RetroGalerie.Models.Mapping
{
    public class GameMapper : BaseMapper<Game, GameViewModel>
    {
        public override GameViewModel ToViewModel(Game entity)
        {
            if (entity == null) return null;

            return new GameViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                YearOfPublication = entity.YearOfPublication,
                Description = entity.Description,
                NumberOfPlayers = entity.NumberOfPlayers,
                Genre = entity.Genre,
                Developer = entity.Developer,
                Publisher = entity.Publisher,
                Region = entity.Region,
                Language = entity.Language,
                CoverImageUrl = entity.CoverImageUrl,
                ConsoleId = entity.ConsoleId,
                ConsoleName = entity.Console?.Name
            };
        }

        public override Game ToEntity(GameViewModel vm)
        {
            if (vm == null) return null;

            return new Game
            {
                Id = vm.Id,
                Title = vm.Title,
                YearOfPublication = vm.YearOfPublication,
                Description = vm.Description,
                NumberOfPlayers = vm.NumberOfPlayers,
                Genre = vm.Genre,
                Developer = vm.Developer,
                Publisher = vm.Publisher,
                Region = vm.Region,
                Language = vm.Language,
                CoverImageUrl = vm.CoverImageUrl,
                ConsoleId = vm.ConsoleId
            };
        }
    }
}
