using CalculateurAge.Views;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Gestionnaire appelé au clic du bouton Calculer.
    // sender = le contrôle cliqué ; e = données de l'événement.
    private async void OnCalculerClicked(object? sender, EventArgs e)
    {
        // Validation : on refuse un nom vide.
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlert("Erreur", "Entrez un nom", "OK");
            return;   // on sort sans rien calculer
        }

        DateTime d = pickerDate.Date;
        int age = DateTime.Today.Year - d.Year;
        // Si l'anniversaire n'est pas encore passé cette année,
        // on retire une année.
        if (d.Date > DateTime.Today.AddYears(-age)) age--;

        // Le résultat part vers ResultatPage dans l'URL de navigation.
        await Shell.Current.GoToAsync(
            $"{nameof(ResultatPage)}?nom={entryNom.Text}&age={age}");
    }
}
