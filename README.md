## 🟪 RetroGalerie — ASP.NET MVC + Razor (Clean Architecture légère)
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

### Exemple de logique métier affichée dans la vue
- Calcul du total général :  
  `var totalJeux = Model.Consoles.Sum(c => c.GameCount);`
- Affichage dynamique des jeux par console  
- Indicateur visuel d’état (flèche qui pivote, ligne active)  
- Séparation claire entre données, présentation et interactions
- Création des jeux souhaités (à venir)

Ce projet illustre une approche **simple et orientée métier** :  
livrer vite, clarifier le domaine, éviter la complexité inutile, et garder une UI lisible et efficace.
