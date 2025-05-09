﻿using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SEDO_Training.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
    
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
