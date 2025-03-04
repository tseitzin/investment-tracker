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
            // Use Task.FromResult since we're not doing any actual async work
            return await Task.FromResult(GetSecretValue(secretName));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving secret {SecretName}", secretName);
            return null;
        }
    }

    private string? GetSecretValue(string secretName)
    {
        // First try to get from environment variables
        var envValue = Environment.GetEnvironmentVariable(secretName.ToUpper());
        if (!string.IsNullOrEmpty(envValue))
        {
            return envValue;
        }

        // Fallback to configuration if environment variable not found
        return _configuration[secretName];
    }
}