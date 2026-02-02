namespace RetroGalerie.Models.Mapping.Interface
{
    public interface IMapper<TEntity, TViewModel>
    {
        TViewModel ToViewModel(TEntity entity);
        TEntity ToEntity(TViewModel viewModel);
    }
}
