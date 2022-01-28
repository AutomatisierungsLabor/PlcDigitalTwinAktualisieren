using System.Collections.ObjectModel;

namespace PlcDigitalTwinAktualisieren.Model;

public class ProjektVerzeichnisse
{
    public ObservableCollection<Ordner> AlleProjekte { get; set; } = new();
}
public class Ordner
{
    public Ordner()
    {
        Kommentar = "";
        Quelle = "";
        Ziel = "";
    }
    public string Kommentar { get; set; }
    public string Quelle { get; set; }
    public string Ziel { get; set; }
}