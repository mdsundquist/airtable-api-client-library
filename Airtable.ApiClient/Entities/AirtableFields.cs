using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Airtable.ApiClient.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Attributes
{
    [JsonArray]
    public class AirtableFieldsDictionary : Dictionary<string, object>//, IEquatable<AirtableFields>
    {
        public AirtableFieldsDictionary()
        {
        }

        public AirtableFieldsDictionary(IDictionary<string, object> dictionary) : base(dictionary)
        {
        }

        public AirtableFieldsDictionary ChangesFrom(AirtableFieldsDictionary fields)
        {
            if (fields == null) return this;

            var diff = new AirtableFieldsDictionary();
            this.Except(fields).ToList().ForEach(kvp => diff.Add(kvp.Key, kvp.Value));
            return diff;
        }

        public AirtableFieldsDictionary ValuesTo<T>(string fieldKey) //where T : IValidatableObject 
            =>
            ValuesTo<T>(new string[] { fieldKey });

        public AirtableFieldsDictionary ValuesTo<T>(string[] fieldKeys) //where T : IValidatableObject
        {
            if (fieldKeys == null) throw new ArgumentNullException(nameof(fieldKeys));

            List<KeyValuePair<string, object>> keyValuePairsToConvert = this
                .Where(f => fieldKeys.Contains(f.Key)
                    && f.Value != null
                    && f.Value.GetType() != typeof(T))
                .ToList();

            if (keyValuePairsToConvert == null) return this;

            var convertedPairs = new List<KeyValuePair<string, object>>();
            foreach (KeyValuePair<string, object> keyValuePair in keyValuePairsToConvert)
            {
                if (keyValuePair.Value != null)
                {
                    T convertedValue = keyValuePair.Value.ToType<T>();
                    if (convertedValue == null)
                    {
                        //throw new ArgumentException($"Field \"{keyValuePair.Key}\" contains one or more values that cannot be converted to type \"{typeof(T)}\"");
                        return null;
                    }
                    convertedPairs.Add(new KeyValuePair<string, object>(keyValuePair.Key, convertedValue));
                }
            }

            var resultFields = new AirtableFieldsDictionary(this);
            convertedPairs.ForEach(p => resultFields[p.Key] = p.Value);

            return resultFields;
        }

        public bool Equals(AirtableFieldsDictionary other) => Count == other?.Count && !this.Except(other).Any();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableFieldsDictionary);
        }

        public override int GetHashCode()
        {
            int hashCode = 29438501;
            return (hashCode * -1521134295) + this.ToString().GetHashCode();
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
