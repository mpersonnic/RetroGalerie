## 🟪 RetroGalerie — ASP.NET MVC + Razor (Clean Architecture légère) + Chat IA (Ollama)
**Lien :** https://github.com/mpersonnic/RetroGalerie

Application ASP.NET MVC avec Razor Views, orientée **gestion de collections de jeux rétro**.  
Le projet met l’accent sur une architecture claire, une UI dynamique côté serveur et une logique métier explicite.
Il reste à développer la partie "jeux que souhaite voir entrer la collection".

### Points clés
- **ASP.NET MVC + Razor** : rendu serveur, vues fortement typées, logique claire et maintenable  
- **Modèle métier structuré** : Consoles, Jeux, agrégations, totaux, règles simples mais explicites  
- **UI dynamique** : collapses Bootstrap, interactions JS légères, affichage conditionnel  
- **Razor Views propres** : composants réutilisables, code lisible, séparation claire des responsabilités  
- **Clean Architecture légère** : Domain / Services / Controllers / Views  
- **Qualité pragmatique** : code simple, lisible, orienté métier  
- **Exemple concret** :  
  - tableau récapitulatif des consoles  
  - calcul du total de jeux  
  - affichage dynamique des jeux par console  
  - cartes Bootstrap avec images, titres, navigation  
  - gestion d’état (collapse ouvert/fermé) via JS

# RetroGalerie.AI

RetroGalerie.AI est une API IA en **.NET 8 Minimal API** permettant :
- de discuter avec un modèle IA local via **Ollama**
- d’effectuer des recherches intelligentes dans la base RetroGalerie
- de combiner données + IA pour des réponses enrichies

## Fonctionnalités
- Chat IA (modèle local Ollama)
- Recherche intelligente (retrieval EF Core)
- Architecture propre : Domain / Application / Infrastructure / API
- Endpoints Minimal API

## Installation

### 1. Installer Ollama
 - Lancer 'ollama pull llama3' dans un terminal


### 2. Configurer l’API
Modifier `appsettings.json` :

```json
{
  "Ollama": {
    "BaseUrl": "http://localhost:11434",
    "Model": "llama3"
  },
  "ConnectionStrings": {
    "RetroGalerie": "Server=...;Database=RetroGalerie;..."
  }
}
  
Ce projet illustre une approche **simple et orientée métier** :  
livrer vite, clarifier le domaine, éviter la complexité inutile, et garder une UI lisible et efficace.
