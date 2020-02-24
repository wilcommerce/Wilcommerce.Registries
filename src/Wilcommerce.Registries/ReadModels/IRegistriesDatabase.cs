using System.Linq;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.ReadModels
{
    /// <summary>
    /// Defines a readonly access point for the registries entities
    /// </summary>
    public interface IRegistriesDatabase
    {
        /// <summary>
        /// Get the customers
        /// </summary>
        IQueryable<Customer> Customers { get; }

        /// <summary>
        /// Get the shipping addresses
        /// </summary>
        IQueryable<ShippingAddress> ShippingAddresses { get; }

        /// <summary>
        /// Get the billing infos
        /// </summary>
        IQueryable<BillingInfo> BillingInfos { get; }
    }
}
