using System;
using System.Linq;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the shipping address read models
    /// </summary>
    public static class ShippingAddressExtensions
    {
        /// <summary>
        /// Retrieve all shipping addresses by the specified customer id
        /// </summary>
        /// <param name="shippingAddresses"></param>
        /// <param name="customerId">The id of the customer</param>
        /// <returns>A list of shipping addresses</returns>
        public static IQueryable<ShippingAddress> ByCustomer(this IQueryable<ShippingAddress> shippingAddresses, Guid customerId)
        {
            if (shippingAddresses is null)
            {
                throw new ArgumentNullException(nameof(shippingAddresses));
            }

            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            return from a in shippingAddresses
                   where a.Customer.Id == customerId
                   select a;
        }
    }
}
