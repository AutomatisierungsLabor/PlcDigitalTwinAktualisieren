namespace PlcDigitalTwinAktualisieren.Model;

public class OrdnerDateiInfo
{
    public string DateiBezeichnung { get; set; }
    public bool TemplateDateiVorhanden { get; set; }
    public bool TemplateDateiIdentisch { get; set; }
    public bool DeltaDateiVorhanden { get; set; }
    public bool DeltaDateiIdentisch { get; set; }

    public OrdnerDateiInfo(string file,
        bool templateDateiVorhanden, bool templateDateiIdentisch,
        bool deltaDateiVorhanden, bool deltaDateiIdentisch)
    {
        DateiBezeichnung = file;
        TemplateDateiVorhanden = templateDateiVorhanden;
        TemplateDateiIdentisch = templateDateiIdentisch;
        DeltaDateiVorhanden = deltaDateiVorhanden;
        DeltaDateiIdentisch = deltaDateiIdentisch;
    }
}