using System;
using System.Linq;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the customer read models
    /// </summary>
    public static class CustomerExtensions
    {
        /// <summary>
        /// Retrieve all the customers which are not deleted
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>A list of customers</returns>
        public static IQueryable<Customer> Active(this IQueryable<Customer> customers)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return from c in customers
                   where !c.Deleted
                   select c;
        }

        /// <summary>
        /// Retrieve all the people customers
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>A list of people</returns>
        public static IQueryable<Person> People(this IQueryable<Customer> customers)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.OfType<Person>();
        }

        /// <summary>
        /// Retrieve all the companies customers
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>A list of companies</returns>
        public static IQueryable<Company> Companies(this IQueryable<Customer> customers)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.OfType<Company>();
        }
    }
}
