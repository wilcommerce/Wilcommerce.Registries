using System;
using System.Linq;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the billing info read models
    /// </summary>
    public static class BillingInfoExtensions
    {
        /// <summary>
        /// Retrieve all billing information by the specified customer id
        /// </summary>
        /// <param name="billingInfos"></param>
        /// <param name="customerId">The id of the customer</param>
        /// <returns>A list of billing info</returns>
        public static IQueryable<BillingInfo> ByCustomer(this IQueryable<BillingInfo> billingInfos, Guid customerId)
        {
            if (billingInfos is null)
            {
                throw new ArgumentNullException(nameof(billingInfos));
            }

            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            return from b in billingInfos
                   where b.Customer.Id == customerId
                   select b;
        }
    }
}
