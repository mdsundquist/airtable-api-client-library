using Airtable.ApiClient.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Airtable.ApiClient.Tests.Entities
{
    public static class AirtableFieldsTestData
    {
        public static IEnumerable<object[]> SingleAnonObjectWithKVPs =>
            new List<object[]>
            {
                new object[]
                {
                    JsonConvert.DeserializeObject<object>(
                    JsonConvert.SerializeObject(
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "String", "string1" },
                        { "Integer", 2 },
                        { "AnonymousObject", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "Attachment", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "Barcode", new { Text = "asdfghjkl", Type = "scan" } },
                        { "Collaborator", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })))
                }
            };

        public static IEnumerable<object[]> SingleWithAnonValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "String", "string1" },
                        { "Integer", 2 },
                        { "AnonymousObject", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "Attachment", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "Barcode", new { Text = "asdfghjkl", Type = "scan" } },
                        { "Collaborator", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> SingleWithTypedValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoEqualWithAnonValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoEqualWithTypedValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoEqualValuesOneTypedAndOneAnon =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "String", "string1" },
                        { "Integer", 2 },
                        { "AnonymousObject", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "Attachment", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "Barcode", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "Collaborator", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "String", "string1" },
                        { "Integer", 2 },
                        { "AnonymousObject", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "Attachment", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "Barcode", new { Text = "asdfghjkl", Type = "scan" } },
                        { "Collaborator", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoUnequalWithTypedValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 3 },
                        { "key3", new { prop1 = "prop1 different", prop2 = 2 } },
                        { "key4", "foo" },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "square" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Biz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoUnequalWithTypedValuesAndDiff =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 3 },
                        { "key3", new { prop1 = "prop1 different", prop2 = 2 } },
                        { "key4", "foo" },
                        { "key5", new AirtableAttachment { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new AirtableAttachmentThumbnails {
                                Large = new AirtableAttachmentThumbnail {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new AirtableAttachmentThumbnail {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "square" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Biz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key6", new AirtableBarcode { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new AirtableCollaborator { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoUnequalWithAnonValues =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 3 },
                        { "key3", new { prop1 = "prop1 different", prop2 = 2 } },
                        { "key4", "foo" },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "square" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Biz" } }
                    })
                }
            };

        public static IEnumerable<object[]> TwoUnequalWithAnonValuesAndDiff =>
            new List<object[]>
            {
                new object[]
                {
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key1", "string1" },
                        { "key2", 3 },
                        { "key3", new { prop1 = "prop1 different", prop2 = 2 } },
                        { "key4", "foo" },
                        { "key5", new { Id = "yyz", Url = "http://test.com",
                            Thumbnails = new {
                                Large = new {
                                    Url = "http://large.com", Height = 100, Width = 100
                                },
                                Small = new {
                                    Url = "http://small.com", Height = 10, Width = 10
                                }
                            }
                        } },
                        { "key6", new { Text = "asdfghjkl", Type = "square" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Biz" } }
                    }),
                    new AirtableFieldsDictionary(new Dictionary<string, object>
                    {
                        { "key2", 2 },
                        { "key3", new { prop1 = "prop1 value", prop2 = 2 } },
                        { "key6", new { Text = "asdfghjkl", Type = "scan" } },
                        { "key7", new { Email = "foo@bar.com", Id = "rtrtr", Name = "Baz" } }
                    })
                }
            };
    }
}
