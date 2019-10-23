using Airtable.ApiClient.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using Airtable.ApiClient.Extensions;

namespace Airtable.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var foo = new AirtableRecord(id: "abc123")
            //{
            //    Fields = new AirtableFields
            //    {
            //        { "F1", "Lalalala" },
            //        //{ "F2", new { Subfield1 = "yayaya", Subfield2 = "faff" }},
            //        { "F2", new { Subfield1 = "yayaya", Subfield2 = new { Subfield3 = "deep", Subfield4 = "compare" }}},
            //        { "F3", "3" },
            //        { "F4", 1 }
            //    }
            //};

            //var bar = new AirtableRecord(id: "abc123")
            //{
            //    Fields = new AirtableFields
            //    {
            //        { "F1", "Lalalala" },
            //        { "F2", new { Subfield1 = "yayaya", Subfield2 = "pookie" }},
            //        { "F3", "jack" },
            //        { "F5", 3 }
            //        //{ "F1", "Lalalala" },
            //        //{ "F2", new { Subfield1 = "yayaya", Subfield2 = "faff" }},
            //        //{ "F3", "3" },
            //        //{ "F4", 1 }
            //    }
            //};

            //var baz = new AirtableRecord(id: "abc123")
            //{
            //    Fields = new AirtableFields
            //    {
            //        { "F1", "Lalalala" },
            //        { "F2", new { Subfield1 = "yayaya", Subfield2 = "faff" }},
            //        { "F3", "3" },
            //        { "F4", 1 }
            //    }
            //};

            //Console.WriteLine($"foo == bar: {foo.Equals(bar)}");
            //Console.WriteLine($"foo == baz: {foo.Equals(baz)}");
            //Console.WriteLine($"foo.Fields hash code: {foo.Fields.GetHashCode()}");
            //Console.WriteLine($"bar.Fields hash code: {bar.Fields.GetHashCode()}");
            //Console.WriteLine($"baz.Fields hash code: {baz.Fields.GetHashCode()}");
            //Console.WriteLine($"foo diff bar: {foo.Fields.ChangesFrom(bar.Fields)}");
            //Console.WriteLine($"foo json: {foo.ToString()}");
            //Console.WriteLine($"foo json: {foo}");

            ////string attachmentJson = @"[{""id"":""att6f75cc83f1b648"",""size"":26317,""url"": ""https://www.filepicker.io/api/file/5YTJXioCQG0tYWPw6OPw"",""type"":""image/jpeg"",""filename"":""33823_3_xl.jpg"",""thumbnails"":{""small"":{""url"":""https://www.filepicker.io/api/file/Dy5gioxaShSUvHX0LgIC"",""width"":54,""height"":36},""large"":{""url"":""https://www.filepicker.io/api/file/ueYi00yRiqhuUn420UZA"",""width"":197,""height"":131}}}]";
            //object[] attachmentObj =
            //{
            //    new {
            //        Id = "att6f75cc83f1b648",
            //        Size = 26317,
            //        Url = "https://www.filepicker.io/api/file/5YTJXioCQG0tYWPw6OPw",
            //        Type = "image/jpeg",
            //        Filename = "33823_3_xl.jpg",
            //        Thumbnails = new {
            //            Small = new {
            //                Url = "https://www.filepicker.io/api/file/Dy5gioxaShSUvHX0LgIC",
            //                Width = 54,
            //                Height = 36
            //            },
            //            Large = new {
            //                Url = "https://www.filepicker.io/api/file/ueYi00yRiqhuUn420UZA",
            //                Width = 197,
            //                Height = 131
            //            }
            //        }
            //    }
            //};

            //string attachmentJson = JsonConvert.SerializeObject(attachmentObj);
            //Console.WriteLine($"attachmentJson: {attachmentJson}");

            //AirtableAttachment[] attachment = JsonConvert.DeserializeObject<AirtableAttachment[]>(attachmentJson);

            //var attachmentCast = attachmentObj as AirtableAttachment[];

            //Console.WriteLine($"attachment: {attachment}");

            //var dict = new Dictionary<string, object>();
            //dict.Add("key1", "string1");
            //dict.Add("key2", new { Prop1 = 3 });
            //dict.Add("key3", new object[] { new { id = "1", Val = 2 }, new { id = "foo", Val = 4 } });
            //dict.Add("key4", new object[] { new { foo = 1, Arr2 = 2 }, new { foo = "foo", Arr4 = 4 } });
            //dict.Add("key5", new object[] { new { id = "att1234234", Val = 2 }, new { id = "bar", Val = 5 } });
            //dict.Add("key6", new object[] { new { id = "att99999", Val = 2 }, new { id = "baz", Val = 7 } });
            //dict.Add("key7", new object[] { });

            //var result1 = dict.Where(v => v.Value.GetType().IsArray);

            //var result2 = dict.Where(v => v.Value.GetType() == typeof(object[]));
            //var result2a = result2.Where(v => ((object[])v.Value).Length > 0);
            //var result2b = result2a.Where(v => ((object[])v.Value)[0].GetType().GetProperty("id") != null);
            //var result2c = result2b.Where(v => (((object[])v.Value)[0].GetType().GetProperty("id").GetValue(((object[])v.Value)[0])).GetType() == typeof(string));
            //var result2d = result2c.Where(v => ((string)((object[])v.Value)[0].GetType().GetProperty("id").GetValue(((object[])v.Value)[0])).Length > 3);

            //var result2e = dict.Where(v => ((string)((object[])v.Value)[0].GetType().GetProperty("id").GetValue(((object[])v.Value)[0])).Substring(0, 3) == "att");
            //var result2f = result2e.ToDictionary(x => x.Key, x => x.Value);

            //foreach (var pair in result2f)
            //{
            //    var valJson = JsonConvert.SerializeObject(pair.Value);
            //    dict[pair.Key] = JsonConvert.DeserializeObject<TestType[]>(valJson);
            //}

            object foo = new { Text = "barcode text", Type = "barcode type" };
            AirtableBarcode bar = foo.ToType<AirtableBarcode>();
            Console.WriteLine(bar);
        }

        public class TestType
        {
            public string Id { get; set; }
            public int Val { get; set; }
        }
    }
}
