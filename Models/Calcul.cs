namespace CalculateurAge.Models;

// Un calcul effectué : stocké dans l'historique et transmis à ResultatPage.
public record Calcul(
    string Nom,
    DateTime DateNaissance,
    int Age,
    int JoursAvantAnniversaire,
    DateTime Horodatage)
{
    public bool EstMajeur => Age >= 18;
    public string Statut => EstMajeur ? "Majeur" : "Mineur";
    public string Resume => $"{Nom} — {Age} ans ({Statut})";
}
