
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "StringContent doesn't require disposal", Scope = "member", Target = "~M:Airtable.ApiClient.AirtableClient.Create(System.String,System.Collections.Generic.List{Airtable.ApiClient.Entities.AirtableRecord})~System.Threading.Tasks.Task{System.Collections.Generic.List{Airtable.ApiClient.Entities.AirtableRecord}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "StringContent doesn't require disposal", Scope = "member", Target = "~M:Airtable.ApiClient.AirtableClient.Create(System.String,System.Collections.Generic.IEnumerable{Airtable.ApiClient.Entities.AirtableRecord})~System.Threading.Tasks.Task{System.Collections.Generic.List{Airtable.ApiClient.Entities.AirtableRecord}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "StringContent doesn't require disposal", Scope = "member", Target = "~M:Airtable.ApiClient.AirtableClient.Put(System.String,System.Collections.Generic.IEnumerable{Airtable.ApiClient.Entities.AirtableRecord})~System.Threading.Tasks.Task{System.Collections.Generic.List{Airtable.ApiClient.Entities.AirtableRecord}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("General", "RCS1118:Mark local variable as const.", Justification = "Using a const would cause an overflow at compile time", Scope = "member", Target = "~M:Airtable.ApiClient.Entities.AirtableFields.GetHashCode~System.Int32")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "JsonException is specific enough for our purposes; we want to catch any error arising from the JSON conversion process", Scope = "member", Target = "~M:Airtable.ApiClient.Extensions.ObjectExtensions.ToType``1(System.Object)~``0")]

