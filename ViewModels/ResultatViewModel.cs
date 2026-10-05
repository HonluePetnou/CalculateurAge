using CalculateurAge.Models;

namespace CalculateurAge.ViewModels;

// IQueryAttributable : Shell remet ici les paramètres de navigation,
// à la place des [QueryProperty] + OnAppearing du code-behind.
public class ResultatViewModel : BaseViewModel, IQueryAttributable
{
    private string _message = "";
    private string _statut = "";
    private string _naissance = "";
    private string _anniversaire = "";

    public string Message { get => _message; set => SetField(ref _message, value); }
    public string Statut { get => _statut; set => SetField(ref _statut, value); }
    public string Naissance { get => _naissance; set => SetField(ref _naissance, value); }
    public string Anniversaire { get => _anniversaire; set => SetField(ref _anniversaire, value); }

    public RelayCommand RetourCommand { get; }

    public ResultatViewModel()
    {
        // ".." = revenir à la page précédente.
        RetourCommand = new RelayCommand(async () => await Shell.Current.GoToAsync(".."));
    }

    // Appelée par Shell à l'arrivée sur la page : on reçoit l'objet Calcul lui-même.
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Calcul", out var obj) && obj is Calcul c)
        {
            Message = $"{c.Nom}, vous avez {c.Age} ans";
            Statut = $"Statut : {c.Statut}";
            Naissance = $"Né(e) le {c.DateNaissance:dd/MM/yyyy}";
            Anniversaire = c.JoursAvantAnniversaire == 0
                ? "C'est votre anniversaire aujourd'hui !"
                : $"Prochain anniversaire dans {c.JoursAvantAnniversaire} jour(s)";
        }
    }
}
