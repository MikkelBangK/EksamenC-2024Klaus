using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Business;
using DTO.Model;
using Eksamensprojekt.Data.Model;

namespace MauiApp1;

public partial class OpretMedarbejder : ContentPage
{
    private VirksomhedBLL virksomhedBLL;
    private List<AfdelingDTO> alleAfdelinger;
    public OpretMedarbejder()
    {
        InitializeComponent();
        virksomhedBLL = new VirksomhedBLL();
        alleAfdelinger = virksomhedBLL.GetAfdelinger();
        AfdelingsPicker.ItemsSource = alleAfdelinger;
        BindingContext = this;
    }
    public void AddMedarbejder(object sender, EventArgs eventArgs)
    {
        var medarbejderDto = new MedarbejderDTO()
        {
            Initial = Initial.Text,
            Cpr = Convert.ToInt32(CPR.Text),
            Navn = Navn.Text,
        };
        
        var afdeling = AfdelingsPicker.SelectedItem as AfdelingDTO;
        virksomhedBLL.AddMedarbejder(medarbejderDto, afdeling);
        DisplayAlert("Succes", "Afdeling oprettet", "OK");
    }
}