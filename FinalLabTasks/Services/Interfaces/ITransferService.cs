using FinalLabTask1.Entities;

namespace FinalLabTask1.Services.Interfaces;

public interface ITransferService
{
        public Task<bool> Transfer(Account fromAccount, Account toAccount, decimal amount);
}