using Airtable.ApiClient.Entities;
using Airtable.ApiClient.Extensions;
using Moq;
using System;
using Xunit;

namespace Airtable.ApiClient.Tests.Extensions
{
    public sealed class ObjectExtensionsTests : IDisposable
    {
        private readonly MockRepository mockRepository;

        public ObjectExtensionsTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(ExtensionsTestData.SingleAnonAttachmentObject),
            MemberType = typeof(ExtensionsTestData))]
        public void ToType_ValidObjectForConversionToAttachment_ReturnsAttachmentObjectWithCorrectData(object obj)
        {
            // Act
            AirtableAttachment result = obj.ToType<AirtableAttachment>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AirtableAttachment>(result);
            Assert.Equal(obj.GetType().GetProperty("Id")?.GetValue(obj) ?? "", result.Id ?? "");
            Assert.Equal(obj.GetType().GetProperty("Url")?.GetValue(obj) ?? "", result.Url ?? "");
            object objThumbnails = obj.GetType().GetProperty("Thumbnails")?.GetValue(obj);
            object objLargeThumbnail = objThumbnails?.GetType().GetProperty("Large")?.GetValue(objThumbnails);
            Assert.Equal(objLargeThumbnail?.GetType().GetProperty("Url")?.GetValue(objLargeThumbnail) ?? "", result.Thumbnails?.Large.Url);
            Assert.Equal(objLargeThumbnail?.GetType().GetProperty("Height")?.GetValue(objLargeThumbnail) ?? 0, result.Thumbnails?.Large.Height);
            Assert.Equal(objLargeThumbnail?.GetType().GetProperty("Width")?.GetValue(objLargeThumbnail) ?? 0, result.Thumbnails?.Large.Width);
            object objSmallThumbnail = objThumbnails?.GetType().GetProperty("Small")?.GetValue(objThumbnails);
            Assert.Equal(objSmallThumbnail?.GetType().GetProperty("Url")?.GetValue(objSmallThumbnail) ?? "", result.Thumbnails?.Small.Url);
            Assert.Equal(objSmallThumbnail?.GetType().GetProperty("Height")?.GetValue(objSmallThumbnail) ?? 0, result.Thumbnails?.Small.Height);
            Assert.Equal(objSmallThumbnail?.GetType().GetProperty("Width")?.GetValue(objSmallThumbnail) ?? 0, result.Thumbnails?.Small.Width);
        }

        [Theory]
        [MemberData(nameof(ExtensionsTestData.SingleAnonInvalidBarcodeObject),
            MemberType = typeof(ExtensionsTestData))]
        public void ToType_ValidObjectForConversionToBarcode_ReturnsBarcodeObjectWithCorrectData(object obj)
        {
            // Act
            AirtableBarcode result = obj.ToType<AirtableBarcode>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AirtableBarcode>(result);
            Assert.Equal(obj.GetType().GetProperty("Text")?.GetValue(obj) ?? "", result.Text ?? "");
            Assert.Equal(obj.GetType().GetProperty("Type")?.GetValue(obj) ?? "", result.Type ?? "");
        }

        [Theory]
        [MemberData(nameof(ExtensionsTestData.SingleAnonBarcodeObject),
            MemberType = typeof(ExtensionsTestData))]
        public void ToType_InvalidObjectForConversionToAttachment_ReturnsNull(object obj)
        {
            // Act
            AirtableAttachment result = obj.ToType<AirtableAttachment>();

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(ExtensionsTestData.SingleAnonAttachmentObject),
            MemberType = typeof(ExtensionsTestData))]
        public void ToType_InvalidObjectForConversionToBarcode_ReturnsNull(object obj)
        {
            // Act
            AirtableBarcode result = obj.ToType<AirtableBarcode>();

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(ExtensionsTestData.SingleAnonAttachmentObject),
            MemberType = typeof(ExtensionsTestData))]
        public void ToType_InvalidObjectForConversionToCollaborator_ReturnsNull(object obj)
        {
            // Act
            AirtableCollaborator result = obj.ToType<AirtableCollaborator>();

            // Assert
            Assert.Null(result);
        }
    }
}
