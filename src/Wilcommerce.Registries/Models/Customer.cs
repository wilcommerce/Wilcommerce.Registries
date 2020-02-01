using System;
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
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the account information
        /// </summary>
        public virtual AccountInfo Account { get; set; }

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
        #endregion
    }
}
