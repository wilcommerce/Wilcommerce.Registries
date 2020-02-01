using System;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public abstract class Customer : IAggregateRoot
    {
        /// <summary>
        /// Get or set the customer's id
        /// </summary>
        public Guid Id { get; protected set; }

        #region Constructor
        /// <summary>
        /// Construct the customer
        /// </summary>
        protected Customer()
        {
            Account = AccountInfo.EmptyAccount();
            this.ShippingAddresses = new HashSet<ShippingAddress>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the account information
        /// </summary>
        public virtual AccountInfo Account { get; set; }

        /// <summary>
        /// Get or set the customer's national identification number
        /// </summary>
        public string NationalIdentificationNumber { get; protected set; }

        /// <summary>
        /// Get or set the customer's shipping addresses
        /// </summary>
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; protected set; }

        /// <summary>
        /// Get or set whether the customer is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get the creation date
        /// </summary>
        public DateTime CreationDate { get; protected set; }

        /// <summary>
        /// Get whether the customer as an account associated
        /// </summary>
        public bool HasAccount => Account != AccountInfo.EmptyAccount();
        #endregion

        #region Behaviors
        /// <summary>
        /// Delete the customer
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("Customer already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the customer
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("Customer is not deleted");
            }

            Deleted = false;
        }

        /// <summary>
        /// Set the account associated to the customer
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="userName">The username</param>
        public virtual void SetAccount(Guid userId, string userName)
        {
            Account = new AccountInfo { UserId = userId, UserName = userName };
        }

        /// <summary>
        /// Remove the account information from the customer
        /// </summary>
        public virtual void RemoveAccount()
        {
            Account = AccountInfo.EmptyAccount();
        }

        /// <summary>
        /// Lock the customer account
        /// </summary>
        public virtual void LockAccount()
        {
            if (!HasAccount)
            {
                throw new InvalidOperationException("Customer hasn't any account associated");
            }

            Account.IsLocked = true;
        }

        /// <summary>
        /// Unlock the customer account
        /// </summary>
        public virtual void UnlockAccount()
        {
            if (!HasAccount)
            {
                throw new InvalidOperationException("Customer hasn't any account associated");
            }
            if (!Account.IsLocked)
            {
                throw new InvalidOperationException("Customer account is not locked");
            }

            Account.IsLocked = false;
        }

        /// <summary>
        /// Set the person's national identification number
        /// </summary>
        /// <param name="nationalIdentificationNumber"></param>
        public virtual void SetNationalIdentificationNumber(string nationalIdentificationNumber)
        {
            NationalIdentificationNumber = nationalIdentificationNumber;
        }

        /// <summary>
        /// Add a shipping address
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="isDefault">Whether is the default shipping address</param>
        public virtual void AddShippingAddress(string address, string city, string postalCode, string province, string country, bool isDefault)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("value cannot be empty", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("value cannot be empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ArgumentException("value cannot be empty", nameof(province));
            }

            if (isDefault && this.ShippingAddresses.Any(a => a.IsDefault))
            {
                ResetDefaultShippingAddress();
            }

            var addressInfo = new PostalAddress
            {
                Address = address,
                City = city,
                Country = country,
                PostalCode = postalCode,
                Province = province
            };

            this.ShippingAddresses.Add(new ShippingAddress
            {
                Id = Guid.NewGuid(),
                AddressInfo = addressInfo,
                IsDefault = isDefault
            });
        }

        /// <summary>
        /// Change the shipping address information
        /// </summary>
        /// <param name="addressId">The address id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="isDefault">Whether is the default shipping address</param>
        public virtual void ChangeShippingAddress(Guid addressId, string address, string city, string postalCode, string province, string country, bool isDefault)
        {
            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("value cannot be empty", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("value cannot be empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ArgumentException("value cannot be empty", nameof(province));
            }

            var shippingAddress = this.ShippingAddresses.SingleOrDefault(s => s.Id == addressId);
            if (shippingAddress == null)
            {
                throw new ArgumentOutOfRangeException(nameof(addressId), "Address not found");
            }

            shippingAddress.AddressInfo = new PostalAddress
            {
                Address = address,
                City = city,
                Country = country,
                PostalCode = postalCode,
                Province = province
            };

            if (isDefault && this.ShippingAddresses.Any(a => a.IsDefault && a.Id != addressId))
            {
                ResetDefaultShippingAddress();
            }

            shippingAddress.IsDefault = isDefault;
        }

        /// <summary>
        /// Remove the shipping address
        /// </summary>
        /// <param name="addressId">The address id</param>
        public virtual void RemoveShippingAddress(Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
            }

            var shippingAddress = this.ShippingAddresses.SingleOrDefault(a => a.Id == addressId);
            if (shippingAddress == null)
            {
                throw new ArgumentOutOfRangeException(nameof(addressId), "Address not found");
            }

            if (shippingAddress.IsDefault)
            {
                throw new InvalidOperationException("Cannot remove the shipping default address");
            }

            this.ShippingAddresses.Remove(shippingAddress);
        }

        /// <summary>
        /// Mark the shipping address as the default address
        /// </summary>
        /// <param name="addressId">The address id</param>
        public virtual void MarkShippingAddressAsDefault(Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
            }

            var shippingAddress = this.ShippingAddresses.SingleOrDefault(a => a.Id == addressId);
            if (shippingAddress == null)
            {
                throw new ArgumentOutOfRangeException(nameof(addressId), "Address not found");
            }

            if (this.ShippingAddresses.Any(a => a.IsDefault && a.Id != addressId))
            {
                ResetDefaultShippingAddress();
            }

            shippingAddress.IsDefault = true;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Reset the default shipping address
        /// </summary>
        protected virtual void ResetDefaultShippingAddress()
        {
            var defaultShippingAddress = this.ShippingAddresses.SingleOrDefault(a => a.IsDefault);
            if (defaultShippingAddress != null)
            {
                defaultShippingAddress.IsDefault = false;
            }
        }
        #endregion
    }
}
