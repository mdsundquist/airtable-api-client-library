using Airtable.ApiClient.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airtable.ApiClient.Tests.Extensions
{
    public static class ExtensionsTestData
    {
        public static IEnumerable<object[]> SingleAnonAttachmentObject =>
            new List<object[]>
            {
                new object[]
                {
                    new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        }
                }
            };

        public static IEnumerable<object[]> SingleAnonBarcodeObject =>
            new List<object[]> { new object[] { new { Text = "asdfghjkl", Type = "scan" } } };

        public static IEnumerable<object[]> SingleAnonInvalidBarcodeObject =>
            new List<object[]> { new object[] { new { Text = "", Type = "" } } };


    }
}
