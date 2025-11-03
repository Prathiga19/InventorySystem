using Avalonia.Controls;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.ComponentModel; // Til opdatering af pris

namespace InventorySystem;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    // Mine data 
    public ObservableCollection<Order> VentendeOrdrer { get; } = new();
    public ObservableCollection<Order> FærdigeOrdrer { get; } = new();

    private decimal _totalPris = 0;
    public decimal TotalPris
    {
        get => _totalPris;
        set
        {
            _totalPris = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPris)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this; // Så knapper og tekst kan se mine data

        // Jeg laver nogle test-ordrer 
        LavTestData();
    }

    private void LavTestData()
    {
        // Mine varer
        var vare1 = new Vare { Navn = "Hydraulikpumpe", Pris = 20m };
        var vare2 = new Vare { Navn = "Smøreolie", Pris = 2m };
        var vare3 = new Vare { Navn = "Kuglepen", Pris = 1m };

        // Første ordre
        var ordre1 = new Order
        {
            Linjer = new ObservableCollection<OrderLinje>
            {
                new OrderLinje { Vare = vare1, Antal = 4 },
                new OrderLinje { Vare = vare2, Antal = 2.1m },
                new OrderLinje { Vare = vare3, Antal = 100 }
            },
            Tid = DateTime.Now.AddDays(-2)
        };

        // Anden ordre
        var ordre2 = new Order
        {
            Linjer = new ObservableCollection<OrderLinje>
            {
                new OrderLinje { Vare = vare2, Antal = 5m }
            },
            Tid = DateTime.Now
        };

        // Læg ordrer i kø
        VentendeOrdrer.Add(ordre1);
        VentendeOrdrer.Add(ordre2);
    }

    // Knap: Behandle næste ordre
    private void BehandleKnap_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (VentendeOrdrer.Count > 0)
        {
            var næste = VentendeOrdrer[0];
            VentendeOrdrer.RemoveAt(0);
            FærdigeOrdrer.Add(næste);
            TotalPris += næste.BeregnPris(); // Opdater total
        }
    }
}

// Mine egne klasser 
public class Vare
{
    public string Navn { get; set; } = "";
    public decimal Pris { get; set; }
}

public class OrderLinje
{
    public Vare Vare { get; set; } = new();
    public decimal Antal { get; set; } = 1;

    public override string ToString() => $"{Vare.Navn} x {Antal}";
}

public class Order
{
    public ObservableCollection<OrderLinje> Linjer { get; set; } = new();
    public DateTime Tid { get; set; }

    public decimal BeregnPris()
    {
        decimal sum = 0;
        foreach (var linje in Linjer)
            sum += linje.Vare.Pris * linje.Antal;
        return sum;
    }

    public override string ToString() => $"Ordre {Tid:dd-MM HH:mm} - {BeregnPris():F2} kr";
}