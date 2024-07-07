using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finapp_backend.Models;

namespace finapp_backend.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUsers users);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteAsync(AppUsers appUser, string symbol);


    }
}