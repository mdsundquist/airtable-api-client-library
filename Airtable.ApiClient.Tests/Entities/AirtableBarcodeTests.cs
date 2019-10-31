using Airtable.ApiClient.Entities;
using Moq;
using System;
using Xunit;

namespace Airtable.ApiClient.Tests.Entities
{
    public class AirtableBarcodeTests : IDisposable
    {
        private MockRepository mockRepository;



        public AirtableBarcodeTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private AirtableBarcode CreateAirtableBarcode()
        {
            return new AirtableBarcode();
        }

        [Theory]
        [MemberData(nameof(AirtableBarcodeTestData.TwoEqualBarcodeObjects),
                MemberType = typeof(AirtableBarcodeTestData))]
        public void Equals_TwoEqualAnonBarcodeObject_ReturnsTrue(AirtableBarcode barcode1, AirtableBarcode barcode2)
        {
            // Act
            var result = barcode1?.Equals(barcode2);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AirtableBarcodeTestData.TwoUnequalBarcodeObjects),
        MemberType = typeof(AirtableBarcodeTestData))]
        public void Equals_TwoUnequalAnonBarcodeObject_ReturnsFalse(AirtableBarcode barcode1, AirtableBarcode barcode2)
        {
            // Act
            var result = barcode1?.Equals(barcode2);

            // Assert
            Assert.False(result);
        }

        //[Fact]
        //public void Equals_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var airtableBarcode = this.CreateAirtableBarcode();
        //    object obj = null;

        //    // Act
        //    var result = airtableBarcode.Equals(
        //        obj);

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public void GetHashCode_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var airtableBarcode = this.CreateAirtableBarcode();

        //    // Act
        //    var result = airtableBarcode.GetHashCode();

        //    // Assert
        //    Assert.True(false);
        //}
    }
}
