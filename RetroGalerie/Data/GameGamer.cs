namespace RetroGalerie.Data
{
    /// <summary>
    /// Permet de lier un joueur à des jeux
    /// L'utilisateur peut avoir déjà joué au jeu.    
    /// Les jeux qui sont en relations avec un utilisateur constituent sa collection de jeux
    /// Les jeux qui ne sont pas en relation avec un utilisateur constituent une liste de souhaits
    /// Il peut noter le jeu
    /// [PK = (UserId, GameId)]
    /// </summary>
    public class GameGamer
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Note {  get; set; }

        // Si false, le jeu est dans la liste de souhaits du joueur, sinon il fait partie de sa collection
        public bool Owned { get; set; }

        public Gamer Gamer { get; set; }
        public Game Game { get; set; }

    }
}
