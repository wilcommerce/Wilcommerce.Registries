namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represents a postal address
    /// </summary>
    public class PostalAddress
    {
        /// <summary>
        /// Get or set the address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Get or set the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Get or set the postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Get or set the province
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Get or set the country
        /// </summary>
        public string Country { get; set; }

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

            var address = obj as PostalAddress;
            if (address == null)
            {
                return false;
            }

            return Address == address.Address && 
                City == address.City &&
                PostalCode == address.PostalCode &&
                Province == address.Province &&
                Country == address.Country;
        }

        /// <summary>
        /// Returns the hash code for the object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Converts the postal address object to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Address}, {City} {PostalCode} ({Province}), {Country}";
        }
        #endregion

        #region Operators
        /// <summary>
        /// Compare the two addresses and return true if they're equal
        /// </summary>
        /// <param name="first">The first address</param>
        /// <param name="second">The second address</param>
        /// <returns>true if the're equal, false otherwise</returns>
        public static bool operator ==(PostalAddress first, PostalAddress second)
        {
            if (ReferenceEquals(first, null))
            {
                return ReferenceEquals(null, second);
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Compare the two addresses and return true if they're not equal
        /// </summary>
        /// <param name="first">The first address</param>
        /// <param name="second">The second address</param>
        /// <returns>true if the accounts are not equal, false otherwise</returns>
        public static bool operator !=(PostalAddress first, PostalAddress second)
        {
            return !(first == second);
        }
        #endregion
    }
}
