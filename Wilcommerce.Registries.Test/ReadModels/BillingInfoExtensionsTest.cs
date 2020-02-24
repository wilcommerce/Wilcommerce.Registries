using System;
using System.Linq;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.ReadModels;
using Xunit;

namespace Wilcommerce.Registries.Test.ReadModels
{
    public class BillingInfoExtensionsTest
    {
        #region ByCustomer tests
        [Fact]
        public void ByCustomer_Should_Throw_ArgumentNullException_If_BillingInfos_Is_Null()
        {
            IQueryable<BillingInfo> billingInfos = null;
            Guid customerId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => BillingInfoExtensions.ByCustomer(billingInfos, customerId));
            Assert.Equal(nameof(billingInfos), ex.ParamName);
        }

        [Fact]
        public void ByCustomer_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            IQueryable<BillingInfo> billingInfos = new BillingInfo[0].AsQueryable();
            Guid customerId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => BillingInfoExtensions.ByCustomer(billingInfos, customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }
        #endregion
    }
}
