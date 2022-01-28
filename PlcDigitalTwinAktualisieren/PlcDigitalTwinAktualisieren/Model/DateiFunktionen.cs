using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace PlcDigitalTwinAktualisieren.Model;

internal class DateiFunktionen
{
    private readonly ViewModel.ViewModel _viewModel;
    private readonly JsonConfig _jsonConfig;

    private const string DotNetOrdner = "net6.0-windows";
    private const string IpBeckhoff = "IpAdressenBeckhoff.json";
    private const string IpSiemens = "IpAdressenSiemens.json";
    public DateiFunktionen(ViewModel.ViewModel viewModel, JsonConfig jsonConfig)
    {
        _viewModel = viewModel;
        _jsonConfig = jsonConfig;
    }
    public void TemplateKopieren()
    {
        OrdnerErstellen(_jsonConfig.ZielOrdnerTemplate);

        _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

        var files = new DirectoryInfo(_jsonConfig.QuellOrdnerTemplate).GetFiles();

        foreach (var datei in files)
        {
            if (datei.Name.Contains("kata", StringComparison.CurrentCultureIgnoreCase)) continue;
            if (datei.Name.Contains("Log.log", StringComparison.CurrentCultureIgnoreCase)) continue;
            datei.CopyTo(Path.Combine(_jsonConfig.ZielOrdnerTemplate, datei.Name));
            _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(datei.Name, false, false, false, false));

            JsonDateienKontrollieren(datei, IpBeckhoff);
            JsonDateienKontrollieren(datei, IpSiemens);
        }
    }

    private void JsonDateienKontrollieren(FileInfo datei, string ipJson)
    {
        if (!string.Equals(datei.Name, ipJson)) return;
        if (!AreFileContentsEqual(datei.FullName, Path.Combine(_jsonConfig.OrdnerIpAdressen, ipJson))) MessageBox.Show(" Dateien sind unterschiedlich: " + ipJson);
    }

    internal void ProjekteAktualisieren()
    {
        _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

        var dateienamenTemplate = DateiNamenExtrahieren($"{_jsonConfig.ZielOrdnerTemplate}");

        foreach (var projekt in _jsonConfig.OrdnerStruktur.AlleProjekte)
        {
            switch (projekt.Kommentar)
            {
                case "Ordner":
                case "Template":
                case "IpAdressen": continue;
                default:
                    _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(projekt.Kommentar, false, false, false, false));
                    EinProjektKopieren(projekt, dateienamenTemplate);
                    break;
            }
        }
    }
    internal void ProjekteKontrollieren()
    {
        _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Clear();

        foreach (var projekt in _jsonConfig.OrdnerStruktur.AlleProjekte)
        {
            switch (projekt.Kommentar)
            {
                case "Ordner":
                case "Template":
                case "IpAdressen": continue;
                default:
                    _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(projekt.Kommentar, false, false, false, false));
                    break;
            }
        }
    }
    private static List<string> DateiNamenExtrahieren(string ordner)
    {
        var dateien = Directory.GetFiles(ordner, "*.*", SearchOption.AllDirectories);
        var anfangsPos = dateien[0].IndexOf(DotNetOrdner, StringComparison.Ordinal);
        var dateiNamen = dateien.Select(dateiname => dateiname[(anfangsPos + 15)..]).ToList();
        return dateiNamen;
    }
    private void EinProjektKopieren(Ordner projekt, List<string> dateienamenTemplate)
    {
        OrdnerNeuErstellen($"{_jsonConfig.ZielOrdnerProjekte}/{projekt.Ziel}");
        OrdnerLoeschen($"{_jsonConfig.ZielOrdnerProjekte}/{projekt.Ziel}/{DotNetOrdner}");

        var dateienProjekt = DateiNamenExtrahieren($"{_jsonConfig.QuellOrdnerProjekte}/{projekt.Quelle}");

        foreach (var dateiProjekt in dateienProjekt)
        {
            var vorhanden = false;
            var dateinameQuelleKomplett = Path.Combine(_jsonConfig.QuellOrdnerProjekte, projekt.Quelle, DotNetOrdner, dateiProjekt);

            foreach (var dateiTemplate in dateienamenTemplate)
            {
                if (!dateiProjekt.Contains(dateiTemplate)) continue;

                var dateinameTemplateKomplett = Path.Combine(_jsonConfig.ZielOrdnerTemplate, dateiProjekt);

                if (AreFileContentsEqual(dateinameQuelleKomplett, dateinameTemplateKomplett))
                {
                    vorhanden = true;
                    _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(dateiProjekt, true, true, false, false));
                }
                else
                {
                    _viewModel.ViAnzeige.OrdnerDateiInfoDataGrid.Add(new OrdnerDateiInfo(dateiProjekt, true, false, false, false));
                }
            }

            if (vorhanden) continue;
            var dateinameZielKomplett = Path.Combine(_jsonConfig.ZielOrdnerProjekte, projekt.Ziel, DotNetOrdner, dateiProjekt);


            var pfad = Path.GetDirectoryName(dateinameZielKomplett);
            if (!Directory.Exists(pfad)) Directory.CreateDirectory(pfad!);

            if (File.Exists(dateinameZielKomplett)) continue;
            File.Copy(dateinameQuelleKomplett, dateinameZielKomplett);
        }
    }
    private static void OrdnerErstellen(string ordner)
    {
        try
        {

            if (Directory.Exists(ordner)) Directory.Delete(ordner, true);
            if (ordner != null) Directory.CreateDirectory(ordner);

        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }
    }
    private static void OrdnerNeuErstellen(string ordner)
    {
        try
        {
            if (ordner == null) return;
            Directory.CreateDirectory(ordner);
        }
        catch (Exception exp)
        {
            MessageBox.Show(exp.ToString());
        }
    }
    private static void OrdnerLoeschen(string pfad)
    {
        if (!Directory.Exists(pfad)) return;
        Directory.Delete(pfad, true);
    }
    public static bool AreFileContentsEqual(string path1, string path2) => File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));
}