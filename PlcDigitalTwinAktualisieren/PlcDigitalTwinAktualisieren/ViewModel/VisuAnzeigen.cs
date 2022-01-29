using System.Collections.ObjectModel;
using System.ComponentModel;
using PlcDigitalTwinAktualisieren.Model;

namespace PlcDigitalTwinAktualisieren.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{

    public VisuAnzeigen()
    {
        _quellOrdnerProjekte = "-";
        _zielOrdnerProjekte = "-";
        _quellOrdnerTemplate = "-";
        _zielOrdnerTemplate = "-";
        QuellOrdnerProjekte = "-";
        ZielOrdnerProjekte = "-";
        QuellOrdnerTemplate = "-";
        ZielOrdnerTemplate = "-";
    }

    private string _quellOrdnerProjekte;
    public string QuellOrdnerProjekte
    {
        get => _quellOrdnerProjekte;
        set
        {
            _quellOrdnerProjekte = value;
            OnPropertyChanged(nameof(QuellOrdnerProjekte));
        }
    }

    private string _zielOrdnerProjekte;
    public string ZielOrdnerProjekte
    {
        get => _zielOrdnerProjekte;
        set
        {
            _zielOrdnerProjekte = value;
            OnPropertyChanged(nameof(ZielOrdnerProjekte));
        }
    }


    private string _quellOrdnerTemplate;
    public string QuellOrdnerTemplate
    {
        get => _quellOrdnerTemplate;
        set
        {
            _quellOrdnerTemplate = value;
            OnPropertyChanged(nameof(QuellOrdnerTemplate));
        }
    }

    private string _zielOrdnerTemplate;
    public string ZielOrdnerTemplate
    {
        get => _zielOrdnerTemplate;
        set
        {
            _zielOrdnerTemplate = value;
            OnPropertyChanged(nameof(ZielOrdnerTemplate));
        }
    }














    private ObservableCollection<OrdnerDateiInfo> _ordnerDateiInfoDataGrid = new();
    public ObservableCollection<OrdnerDateiInfo> OrdnerDateiInfoDataGrid
    {
        get => _ordnerDateiInfoDataGrid;
        set
        {
            _ordnerDateiInfoDataGrid = value;
            OnPropertyChanged(nameof(OrdnerDateiInfoDataGrid));
        }
    }

    private bool _enableButton;
    public bool EnableButton
    {
        get => _enableButton;
        set
        {
            _enableButton = value;
            OnPropertyChanged(nameof(EnableButton));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}