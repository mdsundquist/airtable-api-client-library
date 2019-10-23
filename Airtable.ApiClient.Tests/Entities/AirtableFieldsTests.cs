using Airtable.ApiClient.Attributes;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Airtable.ApiClient.Tests.Entities
{
    public class AirtableFieldsTests : IDisposable
    {
        private MockRepository mockRepository;



        public AirtableFieldsTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private AirtableFieldsDictionary CreateAirtableFields()
        {
            return new AirtableFieldsDictionary();
        }

        #region ChangesFrom() tests
        [Theory] //(DisplayName = "ChangesFrom: null comparison")]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues), 
            MemberType = typeof(AirtableFieldsTestData))]
        public void ChangesFrom_OldObjectIsNull_ReturnsAllPairs(AirtableFieldsDictionary fields)
        {
            // Arrange
            AirtableFieldsDictionary nullFields = null;

            // Act
            AirtableFieldsDictionary result = fields.ChangesFrom(nullFields);

            // Assert
            Assert.True(result.Equals(fields));
        }

        [Theory] //(DisplayName = "ChangesFrom: different anonymous-typed values")]
        [MemberData(nameof(AirtableFieldsTestData.TwoUnequalWithAnonValuesAndDiff),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ChangesFrom_OldObjectWithDifferentAnonValues_ReturnsDifferences(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2, AirtableFieldsDictionary fields3)
        {
            // Act
            AirtableFieldsDictionary result = fields1.ChangesFrom(fields2);

            // Assert
            Assert.True(result.Equals(fields3));
        }

        [Theory] //(DisplayName = "ChangesFrom: different strongly-typed values")]
        [MemberData(nameof(AirtableFieldsTestData.TwoUnequalWithTypedValuesAndDiff),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ChangesFrom_OldObjectWithDifferentTypedValues_ReturnsDifferences(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2, AirtableFieldsDictionary fields3)
        {
            // Act
            AirtableFieldsDictionary result = fields1.ChangesFrom(fields2);

            // Assert
            Assert.True(result.Equals(fields3));
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithAnonValues), MemberType = typeof(AirtableFieldsTestData))]
        public void ChangesFrom_ObjectsHaveTheSameAnonValues_ReturnsEmptyCollection(AirtableFieldsDictionary fields1, AirtableFieldsDictionary fields2)
        {
            // Act
            AirtableFieldsDictionary result = fields1.ChangesFrom(fields2);

            // Assert
            Assert.True(result.Count == 0);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithTypedValues), MemberType = typeof(AirtableFieldsTestData))]
        public void ChangesFrom_ObjectsHaveTheSameTypedValues_ReturnsEmptyCollection(AirtableFieldsDictionary fields1, AirtableFieldsDictionary fields2)
        {
            // Act
            AirtableFieldsDictionary result = fields1.ChangesFrom(fields2);

            // Assert
            Assert.True(result.Count == 0);
        }
        #endregion ChangesFrom() tests

        #region ValuesTo() tests
        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ValuesTo_ValidSingleFieldValuesToAirtableAttachment_SuccessfulConversion(AirtableFieldsDictionary fields)
        {
            // Arrange
            string[] fieldKeys = { "Attachment" };
            Type fieldInitialType = fields["Attachment"].GetType();
            object initialValue = fields["Attachment"];
            // Act
            AirtableFieldsDictionary convertedFields = fields.ValuesTo<AirtableAttachment>(fieldKeys);
            Type fieldPostConversionType = fields["Attachment"].GetType();

            // Assert
            Assert.IsType<AirtableAttachment>(convertedFields["Attachment"]);

            // ensure parameter wasn't unintentionally modified
            Assert.Equal(fieldInitialType, fieldPostConversionType);

            // Check that the object data hasn't changed during conversion
            var convertedValue = (AirtableAttachment)convertedFields["Attachment"];
            Assert.Equal(convertedValue.Id ?? "", fieldInitialType.GetProperty("Id")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Url, (string)fieldInitialType.GetProperty("Url").GetValue(initialValue));
            Assert.Equal(convertedValue.Filename ?? "", fieldInitialType.GetProperty("Filename")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Size ?? 0, fieldInitialType.GetProperty("Size")?.GetValue(initialValue) ?? 0);
            Assert.Equal(convertedValue.Type ?? "", fieldInitialType.GetProperty("Type")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Width ?? 0, fieldInitialType.GetProperty("Width")?.GetValue(initialValue) ?? 0);
            Assert.Equal(convertedValue.Height ?? 0, fieldInitialType.GetProperty("Height")?.GetValue(initialValue) ?? 0);
            Assert.Equal(convertedValue.Height ?? 0, fieldInitialType.GetProperty("Height")?.GetValue(initialValue) ?? 0);
            object initialThumbnails = fieldInitialType.GetProperty("Thumbnails")?.GetValue(initialValue);
            object initialLargeThumbnail = initialThumbnails?.GetType()?.GetProperty("Large")?.GetValue(initialThumbnails);
            Assert.Equal(convertedValue.Thumbnails?.Large?.Url ?? "", initialLargeThumbnail?.GetType()?.GetProperty("Url")?.GetValue(initialLargeThumbnail) ?? "");
            Assert.Equal(convertedValue.Thumbnails?.Large?.Height ?? 0, initialLargeThumbnail?.GetType()?.GetProperty("Height")?.GetValue(initialLargeThumbnail) ?? 0);
            Assert.Equal(convertedValue.Thumbnails?.Large?.Width ?? 0, initialLargeThumbnail?.GetType()?.GetProperty("Width")?.GetValue(initialLargeThumbnail) ?? 0);
            object initialSmallThumbnail = initialThumbnails?.GetType()?.GetProperty("Small")?.GetValue(initialThumbnails);
            Assert.Equal(convertedValue.Thumbnails?.Small?.Url ?? "", initialSmallThumbnail?.GetType()?.GetProperty("Url")?.GetValue(initialSmallThumbnail) ?? "");
            Assert.Equal(convertedValue.Thumbnails?.Small?.Height ?? 0, initialSmallThumbnail?.GetType()?.GetProperty("Height")?.GetValue(initialSmallThumbnail) ?? 0);
            Assert.Equal(convertedValue.Thumbnails?.Small?.Width ?? 0, initialSmallThumbnail?.GetType()?.GetProperty("Width")?.GetValue(initialSmallThumbnail) ?? 0);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ValuesTo_ValidSingleFieldValuesToAirtableBarcode_SuccessfulConversion(AirtableFieldsDictionary fields)
        {
            // Arrange
            string[] fieldKeys = { "Barcode" };
            Type fieldInitialType = fields["Barcode"].GetType();
            object initialValue = fields["Barcode"];

            // Act
            AirtableFieldsDictionary convertedFields = fields.ValuesTo<AirtableBarcode>(fieldKeys);
            Type fieldPostConversionType = fields["Barcode"].GetType();

            // Assert
            Assert.True(convertedFields["Barcode"].GetType() == typeof(AirtableBarcode));

            // ensure parameter wasn't unintentionally modified
            Assert.True(fieldInitialType == fieldPostConversionType);

            // Check that the object data hasn't changed during conversion
            var convertedValue = (AirtableBarcode)convertedFields["Barcode"];
            Assert.Equal(convertedValue.Text ?? "", fieldInitialType.GetProperty("Text")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Type ?? "", fieldInitialType.GetProperty("Type")?.GetValue(initialValue) ?? "");
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ValuesTo_ValidSingleFieldValuesToAirtableCollaborator_SuccessfulConversion(AirtableFieldsDictionary fields)
        {
            // Arrange
            string[] fieldKeys = { "Collaborator" };
            Type fieldInitialType = fields["Collaborator"].GetType();
            object initialValue = fields["Collaborator"];
            // Act
            AirtableFieldsDictionary convertedFields = fields.ValuesTo<AirtableCollaborator>(fieldKeys);
            Type fieldPostConversionType = fields["Collaborator"].GetType();

            // Assert
            Assert.True(convertedFields["Collaborator"].GetType() == typeof(AirtableCollaborator));

            // ensure parameter wasn't unintentionally modified
            Assert.True(fieldInitialType == fieldPostConversionType);

            // Check that the object data hasn't changed during conversion
            var convertedValue = (AirtableCollaborator)convertedFields["Collaborator"];
            Assert.Equal(convertedValue.Id ?? "", fieldInitialType.GetProperty("Id")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Email ?? "", fieldInitialType.GetProperty("Email")?.GetValue(initialValue) ?? "");
            Assert.Equal(convertedValue.Name ?? "", fieldInitialType.GetProperty("Name")?.GetValue(initialValue) ?? "");
        }
        #endregion ValuesTo() tests

        #region Equals() tests
        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void Equals_ObjectsWithAnonValuesAreTheSame_ReturnsTrue(AirtableFieldsDictionary fields1, AirtableFieldsDictionary fields2)
        {
            // Act
            bool result = fields1.Equals(fields2);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithTypedValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void Equals_ObjectsWithTypedValuesAreTheSame_ReturnsTrue(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            bool result = fields1.Equals(fields2);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoUnequalWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void Equals_ObjectsWithAnonValuesAreDifferent_ReturnsFalse(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            bool result = fields1.Equals(fields2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoUnequalWithTypedValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void Equals_ObjectsWithTypedValuesAreDifferent_ReturnsFalse(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            bool result = fields1.Equals(fields2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void EqualsOverride_ObjectsWithAnonValuesAreTheSame_ReturnsTrue(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            bool result = fields1.Equals(fields2 as object);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void EqualsOverride_CompareToDifferentObjectType_ReturnsFalse(AirtableFieldsDictionary fields)
        {
            // Arrange
            object fieldsObject = AirtableFieldsTestData.SingleAnonObjectWithKVPs.First();

            // Act
            bool result = fields.Equals(fieldsObject);

            // Assert
            Assert.False(result);
        }
        #endregion Equals() tests

        #region GetHashCode() tests
        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoEqualWithTypedValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void GetHashCode_GetHashOfTwoEqualObjects_HashCodesAreEqual(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            int hashCode1 = fields1.GetHashCode();
            int hashCode2 = fields2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.TwoUnequalWithTypedValues),
            MemberType = typeof(AirtableFieldsTestData))]
        // In theory, hash codes could be the same for different objects, but it's very unlikely
        public void GetHashCode_GetHashOfTwoUnequalObjects_HashCodesAreDifferent(AirtableFieldsDictionary fields1,
            AirtableFieldsDictionary fields2)
        {
            // Act
            int hashCode1 = fields1.GetHashCode();
            int hashCode2 = fields2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
        #endregion GetHashCode() tests

        #region ToString() tests
        [Theory]
        [MemberData(nameof(AirtableFieldsTestData.SingleWithAnonValues),
            MemberType = typeof(AirtableFieldsTestData))]
        public void ToString_ConvertAirtableFieldsToString_ReturnsJsonSerialization(AirtableFieldsDictionary fields)
        {
            // Act
            string result = fields.ToString();
            string control = JsonConvert.SerializeObject(fields);
            // Assert
            Assert.Equal(control, result);
        }
        #endregion ToString() tests
    }
}
