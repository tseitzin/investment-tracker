namespace api.Services;

public class KeyVaultService : IKeyVaultService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<KeyVaultService> _logger;

    public KeyVaultService(IConfiguration configuration, ILogger<KeyVaultService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string?> GetSecretAsync(string secretName)
    {
        try
        {
            // First try to get from environment variables
            var envValue = Environment.GetEnvironmentVariable(secretName.ToUpper());
            if (!string.IsNullOrEmpty(envValue))
            {
                return envValue;
            }

            // Special handling for Polygon API key
            if (secretName == "PolygonApiKey")
            {
                var polygonApiKey = Environment.GetEnvironmentVariable("VITE_POLYGON_API_KEY");
                if (!string.IsNullOrEmpty(polygonApiKey))
                {
                    return polygonApiKey;
                }
            }

            // Fallback to configuration if environment variable not found
            return await Task.FromResult(_configuration[secretName]);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving secret {SecretName}", secretName);
            return null;
        }
    }
}