using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.ViewModels.Base;
using MusicPlayerProject.ViewModels.Factories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region private fields
        private readonly IViewModelFactory _viewModelFactory;

        private readonly INavigator _navigator;

        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public double WindowMinimumHeight { get; set; } = 720;
        public double WindowMinimumWidth { get; set; } = 1280;

        #endregion

        public MainWindowViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;

            base.Dispose();
        }
    }
}
