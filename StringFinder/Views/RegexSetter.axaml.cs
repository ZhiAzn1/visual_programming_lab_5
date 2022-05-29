using System;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StringFinder.ViewModels;

namespace StringFinder.Views;

public partial class RegexSetter : Window
{
    public RegexSetter()
    {
        InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        this.FindControl<Button>("OK").Click += delegate
        {
            var context = this.Owner.DataContext as MainWindowViewModel;
            var inputField = this.FindControl<TextBox>("Regex");
            try
            {
                Regex rg = new Regex(inputField.Text);
                context.Regex = inputField.Text;
                this.CloseWindow();
            } catch (Exception ex)
            {
                inputField.Text = "No valid regex";
            }
        };
        this.FindControl<Button>("Close").Click += delegate
        {
            CloseWindow();
        };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CloseWindow()
    {
        this.Close();
    }
}