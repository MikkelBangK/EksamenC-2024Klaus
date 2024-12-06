using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Business;
using DTO.Model;

namespace MauiApp1;

public partial class OpretAfdeling : ContentPage
{
    private VirksomhedBLL virksomhedBLL;
    public OpretAfdeling()
    {
        InitializeComponent();
        virksomhedBLL = new VirksomhedBLL();
        BindingContext = this;
    }


    private void AddAfdeling(object sender, EventArgs e)
    {
        var afdelingDto = new AfdelingDTO()
        {
            Navn = Navn.Text,
            Nummer = Convert.ToInt32(Nummer.Text)
        };
        virksomhedBLL.AddAfdeling(afdelingDto);
        DisplayAlert("Succes", "Medarbejder oprettet", "OK");

    }
}