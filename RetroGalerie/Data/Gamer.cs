using Microsoft.AspNetCore.Identity;

namespace RetroGalerie.Data
{
    /// <summary>
    /// [N Gamers <-> N Games via GameGamer]
    /// </summary>
    public class Gamer : IdentityUser<int>
    {
        public Gamer(): base()
        {
            
        }

        public string? Name { get; set; }
        public string? FisrtName { get; set; }
        public ICollection<GameGamer> GameGamers { get; set; }
    }
}
