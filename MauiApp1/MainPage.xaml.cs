using System.Collections.ObjectModel;
using BusinessLogic.Business;
using DTO.Model;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    private async void NavigateTilOversigt(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new Oversigt());
    }

    private async void NavigateTilOpretMedarbejder(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new OpretMedarbejder());
    }

    private async void NavigateTilOpretAfdeling(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new OpretAfdeling());
    }

    private async void NavigateTilOpretSag(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new OpretSag());
    }
}