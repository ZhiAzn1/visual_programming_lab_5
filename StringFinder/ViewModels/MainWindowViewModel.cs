using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using ReactiveUI;
using StringFinder.Models;
using StringFinder.Views;

namespace StringFinder.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            OpenFile = ReactiveCommand.Create(OpenFileImpl);
            SaveFile = ReactiveCommand.Create(SaveFileImpl);
            SetRegex = ReactiveCommand.Create(SetRegexImpl);
        }

        private string? _text = "";
        public string? Text
        {
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); this.ChangeOutput();}
        }

        private string? _result = "";
        public string? Result
        {
            get { return _result;}
            set { this.RaiseAndSetIfChanged(ref _result, value); }
        }

        private string _regex = "";
        public string Regex
        {
            set
            {
                this.RaiseAndSetIfChanged(ref _regex, value);
                this.ChangeOutput();
            }
            get
            {
                return this._regex;
            }
        }   
        public void ChangeOutput()
        {
            var matches = RegexMatcher.GetMatches(Regex, Text);
            String outString = "";
            foreach (string match in matches)
            {
                if (match.Length > 0)
                    outString += match + "\n";
            }

            Result = outString;

        }
        
        ICommand OpenFile { get; }
        async public void OpenFileImpl()
        {
            var taskPath = new OpenFileDialog().ShowAsync((Avalonia.Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);

            string[]? path = await taskPath;
            if (path != null)
                Text = File.ReadAllText(path[0]);
        }
        ICommand SaveFile { get; }
        async public void SaveFileImpl()
        {
            var taskPath = new SaveFileDialog().ShowAsync((Avalonia.Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);

            var path = await taskPath;
            if (path != null)
                File.WriteAllText(path, Result);
        }
        ICommand SetRegex { get; }
        public void SetRegexImpl()
        {
            var dialogWindow = new RegexSetter();
            dialogWindow.ShowDialog((Avalonia.Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
        }
    }
}