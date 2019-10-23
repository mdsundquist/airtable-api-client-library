//using Airtable.ApiClient.Entities;
//using Moq;
//using System;
//using Xunit;

//namespace Airtable.ApiClient.Tests.Entities
//{
//    public class AirtableRecordTests : IDisposable
//    {
//        private MockRepository mockRepository;



//        public AirtableRecordTests()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);


//        }

//        public void Dispose()
//        {
//            this.mockRepository.VerifyAll();
//        }

//        private AirtableRecord CreateAirtableRecord()
//        {
//            return new AirtableRecord();
//        }

//        [Fact]
//        public void Equals_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var airtableRecord = this.CreateAirtableRecord();
//            AirtableRecord record = null;

//            // Act
//            var result = airtableRecord.Equals(
//                record);

//            // Assert
//            Assert.True(false);
//        }

//        [Fact]
//        public void GetHashCode_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var airtableRecord = this.CreateAirtableRecord();

//            // Act
//            var result = airtableRecord.GetHashCode();

//            // Assert
//            Assert.True(false);
//        }

//        [Fact]
//        public void ToString_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var airtableRecord = this.CreateAirtableRecord();

//            // Act
//            var result = airtableRecord.ToString();

//            // Assert
//            Assert.True(false);
//        }
//    }
//}
