using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Business;
using DTO.Model;

namespace MauiApp1;

public partial class Oversigt : ContentPage
{
    private VirksomhedBLL virksomhedBLL;
    private ObservableCollection<TidObjekt> tidsregistreringer { get; set; }    

    public Oversigt()
    {
        InitializeComponent();
        tidsregistreringer = new ObservableCollection<TidObjekt>();
        virksomhedBLL = new VirksomhedBLL();
        BindingContext = this;
        LoadMedarbejdere();
    }
    public int GetWeekNumber(DateTime date)
    {
        CultureInfo ciCurr = CultureInfo.CurrentCulture;
        int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return weekNum;
    }
    public void LoadMedarbejdere()
    {
        var medarbejdeListe = virksomhedBLL.GetMedarbejdere();

        List<TidsregistreringDTO> list = virksomhedBLL.GetTidsregistreringer();
        foreach (var medarbejder in medarbejdeListe)
        {
            var ugeGruppe = medarbejder.Tidsregistrerings
                .GroupBy(t => GetWeekNumber(t.StartTid))
                .Select(gruppe => new
                {
                    Ugenummer = gruppe.Key,
                    TotaleTimer = gruppe.Sum(t => (t.SlutTid - t.StartTid).TotalHours)
                });
            foreach (var uge in ugeGruppe)
            {
                tidsregistreringer.Add(new TidObjekt()
                {
                    Name = medarbejder.Navn,
                    Ugenummer = uge.Ugenummer,
                    Timer = uge.TotaleTimer
                });
            }
        }

        Tidview.ItemsSource = tidsregistreringer;
    }
}