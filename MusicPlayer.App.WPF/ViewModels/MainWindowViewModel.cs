using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Navigators;
using MusicPlayer.Core.Types;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        #region private fields
        private readonly INavigatorService _navigator;
        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public AudioPlayerBarViewModel AudioPlayerBarViewModel { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public double WindowMinimumHeight { get; set; } = 720;
        public double WindowMinimumWidth { get; set; } = 1280;
        #endregion

        public MainWindowViewModel(INavigatorService navigator, IViewModelFactory viewModelFactory, AudioPlayerBarViewModel audioPlayerBarViewModel)
        {
            AudioPlayerBarViewModel = audioPlayerBarViewModel;
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewTypes.Home);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            AudioPlayerBarViewModel.Dispose();
            _navigator.StateChanged -= Navigator_StateChanged;
            base.Dispose();
        }
    }
}
