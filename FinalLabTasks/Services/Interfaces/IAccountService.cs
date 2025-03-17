using FinalLabTask1.Entities;

namespace FinalLabTask1.Services.Interfaces;

public interface IAccountService
{
    public Task<LocalizedAccountDetails> GetAccountDetails(int accountId);
}