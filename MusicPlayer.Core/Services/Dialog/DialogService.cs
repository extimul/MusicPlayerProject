using System;
using System.Collections.Generic;
using System.Windows;

namespace MusicPlayer.Core.Services.Dialog
{
    public sealed class DialogService : IDialogService
    {
        public IDictionary<Type, Type> Mappings { get; }

        public DialogService()
        {
            Mappings = new Dictionary<Type, Type>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            void handler(object sender, DialogCloseRequestEventArgs e)
            {
                viewModel.CloseRequested -= handler;

                if (e.ResultValue.HasValue)
                {
                    if (e.ResultValue.Value)
                    {
                        dialog.DialogResult = e.ResultValue;
                    }
                    else
                    {
                        dialog.Close();
                    }
                }
            }

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = Application.Current.MainWindow;

            return dialog.ShowDialog();
        }
    }
}
