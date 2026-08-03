using System.Linq;

namespace RetroGalerie.IA.Intent
{
    public class IntentExtractor
    {
        public Intent Extract(string question, IntentDictionary dict)
        {
            var intent = new Intent();
            var q = question.ToLower();

            // -------------------------
            // ACTION
            // -------------------------
            if (q.Contains("combien") || q.Contains("nombre"))
                intent.Action = "count";
            else if (q.Contains("quels") || q.Contains("liste") || q.Contains("montre"))
                intent.Action = "list";
            else if (q.Contains("modifie") || q.Contains("change") || q.Contains("ajoute"))
                intent.Action = "update";
            else if (q.Contains("supprime"))
                intent.Action = "delete";
            else
                intent.Action = "search";

            // -------------------------
            // SUBJECT (mot-clé principal)
            // -------------------------

            // 1. Keywords des jeux → on récupère le mot-clé, pas le jeu
            var matchedKeyword = dict.GameKeywords
                .SelectMany(kvp => kvp.Value)
                .Distinct()
                .FirstOrDefault(keyword => q.Contains(keyword));

            if (!string.IsNullOrEmpty(matchedKeyword))
            {
                intent.Subject = matchedKeyword;
            }
            else
            {
                // 2. Plateformes
                var platform = dict.Platforms.FirstOrDefault(p => q.Contains(p));
                if (!string.IsNullOrEmpty(platform))
                {
                    intent.Subject = platform;
                }
                else
                {
                    // 3. Genres
                    var genre = dict.Genres.FirstOrDefault(g => q.Contains(g));
                    if (!string.IsNullOrEmpty(genre))
                    {
                        intent.Subject = genre;
                    }
                }
            }

            // -------------------------
            // FILTERS
            // -------------------------
            intent.Filters = dict.Attributes
                .Where(a => q.Contains(a))
                .ToList();

            return intent;
        }
    }
}
