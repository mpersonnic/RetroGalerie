using Microsoft.Extensions.Localization;
using RetroGalerie.Resources;
using System.Reflection;


namespace RetroGalerie.Services
{
    public class SharedLocalizationService
    {
        private readonly IStringLocalizer localizer;
        public SharedLocalizationService(IStringLocalizerFactory factory)
        {
            var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
            localizer = factory.Create(nameof(SharedResources), assemblyName.Name);
        }

        public string Get(string key)
        {
            return localizer[key];
        }
    }
}
