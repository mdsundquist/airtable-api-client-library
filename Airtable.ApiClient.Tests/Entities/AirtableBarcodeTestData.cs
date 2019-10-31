using Airtable.ApiClient.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airtable.ApiClient.Tests.Entities
{
    public static class AirtableBarcodeTestData
    {
        public static IEnumerable<object[]> TwoEqualBarcodeObjects =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableBarcode { Text = "asdfghjkl", Type = "scan" },
                    new AirtableBarcode { Text = "asdfghjkl", Type = "scan" }
                }
            };

        public static IEnumerable<object[]> TwoUnequalBarcodeObjects =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableBarcode { Text = "asdfghjkl", Type = "scan" },
                    new AirtableBarcode { Text = "unequal", Type = "foo" }
                }
            };
    }
}
