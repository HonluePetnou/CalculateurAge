namespace CalculateurAge.Views;

// Relie le paramètre "nom" de l'URL à la propriété Nom.
[QueryProperty(nameof(Nom), "nom")]
[QueryProperty(nameof(Age), "age")]
public partial class ResultatPage : ContentPage
{
    // Ces propriétés sont remplies par la navigation,
    // APRÈS le constructeur.
    public string Nom { get; set; } = "";
    public string Age { get; set; } = "";

    // Construit l'arbre visuel décrit par le XAML.
    public ResultatPage() => InitializeComponent();

    // Appelé à CHAQUE affichage de la page.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblMessage.Text = $"{Nom}, vous avez {Age} ans";
    }

    // ".." = revenir à la page précédente.
    private async void OnRetourClicked(object? s, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}
