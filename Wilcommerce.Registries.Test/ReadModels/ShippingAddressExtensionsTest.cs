using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.ReadModels;
using Xunit;

namespace Wilcommerce.Registries.Test.ReadModels
{
    public class ShippingAddressExtensionsTest
    {
        #region ByCustomer tests
        [Fact]
        public void ByCustomer_Should_Throw_ArgumentNullException_If_ShippingAddresses_Is_Null()
        {
            IQueryable<ShippingAddress> shippingAddresses = null;
            Guid customerId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ShippingAddressExtensions.ByCustomer(shippingAddresses, customerId));
            Assert.Equal(nameof(shippingAddresses), ex.ParamName);
        }

        [Fact]
        public void ByCustomer_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            IQueryable<ShippingAddress> shippingAddresses = new ShippingAddress[0].AsQueryable();
            Guid customerId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ShippingAddressExtensions.ByCustomer(shippingAddresses, customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }
        #endregion
    }
}
