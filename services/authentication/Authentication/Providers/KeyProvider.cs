using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace Authentication.Providers
{
    public class KeyProvider
    {
        private readonly IOptions<AppSettings> _appSettings;

        public KeyProvider(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public string GetKey()
        {
            return _appSettings.Value.Secret;
        }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}