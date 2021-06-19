using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.ViewModels.Base;

namespace MusicPlayerProject.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewTypes viewType);
    }
}
