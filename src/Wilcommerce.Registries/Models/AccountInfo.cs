using System;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represent the account information
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// Get or set the user id
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// Get or set the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set whether the user is locked
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Create a new empty account value object
        /// </summary>
        /// <returns>The empty account object</returns>
        public static AccountInfo EmptyAccount()
        {
            return new AccountInfo();
        }

        #region Methods
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>true if the specified object is equal to the current, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var account = obj as AccountInfo;
            if (account == null)
            {
                return false;
            }

            return UserId == account.UserId && UserName == account.UserName;
        }

        /// <summary>
        /// Returns the hash code for the object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return $"{UserId}{UserName}".GetHashCode();
        }

        /// <summary>
        /// Converts the account info object to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName;
        }

        #endregion

        #region Operators
        /// <summary>
        /// Compare the two accounts and return true if they're equal
        /// </summary>
        /// <param name="first">The first account</param>
        /// <param name="second">The second account</param>
        /// <returns>true if the're equal, false otherwise</returns>
        public static bool operator==(AccountInfo first, AccountInfo second)
        {
            if (ReferenceEquals(first, null))
            {
                return ReferenceEquals(null, second);
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Compare the two accounts and return true if they're not equal
        /// </summary>
        /// <param name="first">The first account</param>
        /// <param name="second">The second account</param>
        /// <returns>true if the accounts are not equal, false otherwise</returns>
        public static bool operator !=(AccountInfo first, AccountInfo second)
        {
            return !(first == second);
        }
        #endregion
    }
}
