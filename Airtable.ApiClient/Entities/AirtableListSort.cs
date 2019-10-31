using System;
using System.Collections.Generic;
using System.Text;

namespace Airtable.ApiClient.Entities
{
    public class AirtableListSort
    {
        public string FieldName { get; }

        public AirtableSortDirection? Direction { get; }

        public AirtableListSort(string fieldName, AirtableSortDirection? direction) : this(fieldName) => Direction = direction;

        public AirtableListSort(string fieldName) => FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));

        public enum AirtableSortDirection
        {
            asc = 0,
            desc = 1
        }
    }
}
