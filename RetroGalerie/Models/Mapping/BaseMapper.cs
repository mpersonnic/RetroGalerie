using RetroGalerie.Models.Mapping.Interface;

namespace RetroGalerie.Models.Mapping
{
    public abstract class BaseMapper<TEntity, TViewModel> : IMapper<TEntity, TViewModel>
    {
        public abstract TViewModel ToViewModel(TEntity entity);
        public abstract TEntity ToEntity(TViewModel viewModel);
    }
}
