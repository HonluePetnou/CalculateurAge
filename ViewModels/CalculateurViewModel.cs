using CalculateurAge.Models;

namespace CalculateurAge.ViewModels;

// Contient l'ÉTAT de l'écran et les ACTIONS possibles.
// Aucun contrôle d'interface ni boîte de dialogue ici.
public class CalculateurViewModel : BaseViewModel
{
    // ---------- Champs privés : la vraie donnée ----------
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _statut = "";

    // ---------- Propriétés publiques : ce que le XAML voit ----------
    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value)) RafraichirCommandes(); }
    }

    // Refus d'une date future : DateInvalide dépend de DateNaissance,
    // il faut donc la notifier aussi dans ce setter.
    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
            {
                OnPropertyChanged(nameof(DateInvalide));
                RafraichirCommandes();
            }
        }
    }

    public bool DateInvalide => DateNaissance.Date > DateTime.Today;

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set { if (SetField(ref _resultatVisible, value)) RafraichirCommandes(); }
    }

    // "Majeur" ou "Mineur", affiché sous le résultat.
    public string Statut
    {
        get => _statut;
        set => SetField(ref _statut, value);
    }

    // ---------- Commandes ----------
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        // Grisée si le nom est vide ou si la date est dans le futur.
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom) && !DateInvalide);

        // Disponible dès qu'il y a quelque chose à effacer.
        EffacerCommand = new RelayCommand(
            Effacer,
            () => !string.IsNullOrWhiteSpace(Nom) || ResultatVisible);
    }

    // ---------- Logique métier ----------
    private void Calculer()
    {
        DateTime aujourdhui = DateTime.Today;
        DateTime naissance = DateNaissance.Date;

        int age = aujourdhui.Year - naissance.Year;
        // Si l'anniversaire n'est pas encore passé cette année, on retire une année.
        if (naissance > aujourdhui.AddYears(-age)) age--;

        // Jours avant le prochain anniversaire : 0 si c'est aujourd'hui.
        int jours = naissance.AddYears(age) == aujourdhui
            ? 0
            : (naissance.AddYears(age + 1) - aujourdhui).Days;

        var calcul = new Calcul(Nom.Trim(), naissance, age, jours, DateTime.Now);

        Resultat = $"{calcul.Nom}, vous avez {calcul.Age} ans";
        Statut = calcul.Statut;
        ResultatVisible = true;
        RafraichirCommandes();
    }

    // Remet tous les champs à zéro.
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Statut = "";
        ResultatVisible = false;
        RafraichirCommandes();
    }

    // Après chaque changement d'état, toutes les commandes reposent
    // la question « suis-je exécutable ? ».
    // ?. : les setters peuvent être appelés avant la création des commandes.
    private void RafraichirCommandes()
    {
        CalculerCommand?.Rafraichir();
        EffacerCommand?.Rafraichir();
    }
}
