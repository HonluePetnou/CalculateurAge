# Calculateur d'âge — Routing et MVVM (.NET MAUI)

TP1 de l'atelier de développement mobile : une application .NET MAUI qui calcule
l'âge d'une personne à partir de son nom et de sa date de naissance.

L'application est construite en trois temps, visibles dans l'historique Git :

1. **Phase A** — version *code-behind* : la logique manipule directement les contrôles.
2. **Phase B** — seconde page (`ResultatPage`) et navigation Shell avec paramètres dans l'URL.
3. **Phase C** — réécriture en **MVVM** : bindings, `INotifyPropertyChanged`, `ICommand`.

Puis l'**Activité 6** ajoute des fonctionnalités en respectant strictement le MVVM.

## Fonctionnalités ajoutées (Activité 6)

| # | Fonctionnalité | Où |
|---|----------------|----|
| 1 | **Majeur / Mineur** : statut affiché sous le résultat | `Calcul.Statut`, `CalculateurViewModel.Statut` |
| 2 | **Effacer** : remet tous les champs à zéro (actif si nom saisi ou résultat affiché) | `EffacerCommand` |
| 3 | **Refus d'une date future** : message rouge et bouton Calculer grisé | `DateInvalide`, `CalculerCommand` |
| 4 | **Prochain anniversaire** : « Prochain anniversaire dans X jour(s) » ou « Joyeux anniversaire ! » | `ProchainAnniversaire` |
| 5 | **Historique des calculs** (le plus récent en haut) et bouton pour le vider | `Historique`, `ViderHistoriqueCommand` |
| 6 | **Détail via le ViewModel** : l'objet `Calcul` est transmis à `ResultatPage` par la navigation, et non par l'URL | `VoirDetailCommand`, `ResultatViewModel` (`IQueryAttributable`) |

### Respect du MVVM

- Aucun ViewModel ne contient les mots `Label`, `Entry`, `Button` ni `DisplayAlert`.
- Les code-behind se réduisent à `InitializeComponent()` (plus le `BindingContext` pour `MainPage`).
- Aucun `x:Name` ni `Clicked` dans le XAML : tout passe par `{Binding}` et des commandes.
- `RafraichirCommandes()` réévalue le `CanExecute` de toutes les commandes après chaque changement d'état.

## Structure du projet

```
CalculateurAge/
  Models/
    Calcul.cs                  record : Nom, DateNaissance, Age, JoursAvantAnniversaire, Horodatage
  ViewModels/
    BaseViewModel.cs           INotifyPropertyChanged + SetField
    RelayCommand.cs            ICommand + Rafraichir()
    CalculateurViewModel.cs    état et commandes de l'écran principal
    ResultatViewModel.cs       état de la page de détail (IQueryAttributable)
  Views/
    ResultatPage.xaml(.cs)     page de détail
  MainPage.xaml(.cs)           écran principal
  AppShell.xaml(.cs)           Shell + enregistrement de la route ResultatPage
  MauiProgram.cs
  Platforms/  Resources/
```

## Lancement

Prérequis : SDK .NET 9 (épinglé par `global.json`) et workload MAUI (`dotnet workload install maui`).

**Visual Studio 2022** : ouvrir `CalculateurAge.csproj`, choisir un émulateur Android, puis F5.

**Ligne de commande** :

```bash
dotnet build -f net9.0-android
```

```bash
dotnet build -t:Run -f net9.0-android
```

Sous Windows, l'application peut aussi être lancée sans émulateur :

```bash
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

## Historique des commits

Un commit par phase : Étape 0, Phase A, Phase B, Phase C, puis deux commits pour l'Activité 6.
