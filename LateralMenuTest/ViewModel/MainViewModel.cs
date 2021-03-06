using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace LateralMenuTest.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly Random _random = new Random();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            AddElementCommand = new RelayCommand(AddElementCommandExecute);
            SubLevelCommand = new RelayCommand(SubLevelCommandExecute);
            Elements = new ObservableCollection<string>
            {
                "a",
                "b",
                "c"
            };
            Header = "First header";
            Header2 = "Second header";
            Header3 = "Third header";
            ButtonText = "Button 2";
        }

        private ICommand _addElementCommand;
        public ICommand AddElementCommand
        {
            get => _addElementCommand;
            set => Set(ref _addElementCommand, value);
        }

        private ICommand _subLevelCommand;
        public ICommand SubLevelCommand
        {
            get => _subLevelCommand;
            set => Set(ref _subLevelCommand, value);
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                Set(ref _header, value);
                RaisePropertyChanged();
            }
        }

        private string _header2;
        public string Header2
        {
            get { return _header2; }
            set
            {
                Set(ref _header2, value);
                RaisePropertyChanged();
            }
        }

        private string _header3;
        public string Header3
        {
            get { return _header3; }
            set
            {
                Set(ref _header3, value);
                RaisePropertyChanged();
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                Set(ref _buttonText, value);
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _elements;
        public ObservableCollection<string> Elements
        {
            get { return _elements; }
            set
            {
                Set(ref _elements, value);
                RaisePropertyChanged();
            }
        }

        public List<int> Items { get; set; } = new List<int>
        {
            2, 5, 9, 8
        };

        private void AddElementCommandExecute()
        {
            Elements.Add(_random.Next().ToString());
        }

        private void SubLevelCommandExecute()
        {
            AddElementCommandExecute();
        }
    }
}