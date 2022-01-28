using System.Windows;
using PlcDigitalTwinAktualisieren.Model;

namespace PlcDigitalTwinAktualisieren;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    private readonly DateiFunktionen _dateiFunktionen;
    
    public MainWindow()
    {
        var viewModel = new ViewModel.ViewModel();
        var jsonConfig = new JsonConfig(viewModel);
        _dateiFunktionen=new DateiFunktionen(viewModel, jsonConfig);

        InitializeComponent();
        DataContext = viewModel;
     
        DataGrid.ItemsSource = viewModel.ViAnzeige.OrdnerDateiInfoDataGrid;
    }
    internal void TemplateKompieren_Click(object sender, RoutedEventArgs e) => Dispatcher.Invoke(() => { _dateiFunktionen.TemplateKopieren(); });
    internal void ProjekteAktualisieren_Click(object sender, RoutedEventArgs e) => Dispatcher.Invoke(() => { _dateiFunktionen.ProjekteAktualisieren(); });
    internal void TemplateKontrollieren_Click(object sender, RoutedEventArgs e) => Dispatcher.Invoke(() => { _dateiFunktionen.ProjekteKontrollieren(); });
}