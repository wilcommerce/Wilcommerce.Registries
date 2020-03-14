using System;
using System.Threading.Tasks;

namespace Wilcommerce.Registries.Services
{
    /// <summary>
    /// Defines a client which exposes methods to communicate with Auth Bounded Context
    /// </summary>
    public interface IAuthClient
    {
        /// <summary>
        /// Register a new account
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The id of the created account</returns>
        Task<Guid> RegisterNewAccount(string userName, string password);

        /// <summary>
        /// Find the account by the username or create a new account if it doesn't exist
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The id of the account found or created</returns>
        Task<Guid> FindOrRegisterAccount(string userName, string password);

        /// <summary>
        /// Disable the account
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns></returns>
        Task DisableAccount(Guid accountId);

        /// <summary>
        /// Enable the account
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns></returns>
        Task EnableAccount(Guid accountId);
    }
}
