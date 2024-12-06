using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Business;
using DTO.Model;

namespace MauiApp1;

public partial class OpretSag : ContentPage
{

    private VirksomhedBLL _virksomhedBll;
    public OpretSag()
    {
        InitializeComponent();
        _virksomhedBll = new VirksomhedBLL();
        BindingContext = this;
    }
    private void AddSag(object sender, EventArgs e)
    {
        var sagDto = new SagDTO()
        {
            Overskrift = Overskrift.Text,
            Beskrivelse = Beskrivelse.Text
        };
        _virksomhedBll.AddSag(sagDto);
        DisplayAlert("Succes", "Sag oprettet", "OK");
    }
}