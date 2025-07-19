namespace MenuProducerService.Infrastructure.Security
{
    public interface IAuthClient
    {
        Task<bool> ValidateTokenAsync(string token);
    }
}