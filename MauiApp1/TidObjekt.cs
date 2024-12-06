namespace MauiApp1;

public class TidObjekt
{
    private int ugenummer;
    private string name;
    private double timer;

    public TidObjekt(){}
    public TidObjekt(int ugenummer, string name, double timer)
    {
        this.ugenummer = ugenummer;
        this.name = name;
        this.timer = timer;
    }

    public int Ugenummer
    {
        get => ugenummer;
        set => ugenummer = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public double Timer
    {
        get => timer;
        set => timer = value;
    }
}