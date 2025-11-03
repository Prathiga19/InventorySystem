**Opgaven:** Lav et simpelt inventory system i Avalonia (C#). Vis ventende og behandlede ordrer i DataGrids. Knap behandler næste ordre fra kø til processed. Vis total revenue. Brug ObservableCollection og binding.

**Min løsning:**
- Alt logik i `MainWindow.axaml.cs` (ordrer, varer, knap).
- UI i `MainWindow.axaml` med binding til VentendeOrdrer, FærdigeOrdrer og TotalPris.
- Test-data: 2 ordrer med hydraulikpumpe, olie og penne.

**PROBLEMER:**
- UI virker ikke! Data vises ikke i tabellerne.
- Appen starter, men knap gør ingenting – binding fejler.
- Røde ??? i XAML, og revenue opdateres ikke.
- Hjælp! Har prøvet Restore NuGet og skift CompiledBindings til false.