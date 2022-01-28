using System;
using System.IO;
using System.Windows;

namespace PlcDigitalTwinAktualisieren.Model;

internal class JsonConfig
{
    public ProjektVerzeichnisse OrdnerStruktur { get; set; }
    public string QuellOrdnerProjekte { get; set; }
    public string ZielOrdnerProjekte { get; set; }
    public string QuellOrdnerTemplate { get; set; }
    public string ZielOrdnerTemplate { get; set; }
    public string OrdnerIpAdressen { get; set; }

    public JsonConfig(ViewModel.ViewModel viewModel)
    {
        OrdnerStruktur = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjektVerzeichnisse>(File.ReadAllText("_ProjektVerzeichnisse.json"));

        if (OrdnerStruktur?.AlleProjekte[0].Kommentar != "Ordner")
        {
            MessageBox.Show("ProjektVerzeichnis.json ist falsch --> Ordner");
            throw new ArgumentException();
        }

        QuellOrdnerProjekte = OrdnerStruktur.AlleProjekte[0].Quelle;
        ZielOrdnerProjekte = OrdnerStruktur.AlleProjekte[0].Ziel;
        viewModel.ViAnzeige.QuellOrdnerProjekte = QuellOrdnerProjekte;
        viewModel.ViAnzeige.ZielOrdnerProjekte = ZielOrdnerProjekte;

        if (OrdnerStruktur?.AlleProjekte[1].Kommentar != "Template")
        {
            MessageBox.Show("ProjektVerzeichnis.json ist falsch --> Template");
            throw new ArgumentException();
        }

        QuellOrdnerTemplate = OrdnerStruktur.AlleProjekte[1].Quelle;
        ZielOrdnerTemplate = OrdnerStruktur.AlleProjekte[1].Ziel;
        viewModel.ViAnzeige.QuellOrdnerTemplate = QuellOrdnerTemplate;
        viewModel.ViAnzeige.ZielOrdnerTemplate = ZielOrdnerTemplate;

        if (OrdnerStruktur?.AlleProjekte[2].Kommentar != "IpAdressen")
        {
            MessageBox.Show("ProjektVerzeichnis.json ist falsch --> IpAdressen");
            throw new ArgumentException();
        }

        OrdnerIpAdressen = OrdnerStruktur.AlleProjekte[2].Quelle;
    }
}