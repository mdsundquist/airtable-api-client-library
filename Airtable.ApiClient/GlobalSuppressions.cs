// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Name is accurate representation of parameter; it could be any object", Scope = "member", Target = "~M:Airtable.ApiClient.Extensions.ObjectExtensions.ToType``1(System.Object)~``0")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Name is accurate representation of parameter; it could be any object", Scope = "member", Target = "~M:Airtable.ApiClient.Extensions.ObjectExtensions.IsValid``1(``0,System.Boolean)~System.Boolean")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by SendAsync() call", Scope = "member", Target = "~M:Airtable.ApiClient.AirtableApiClient.SendAirtableApiRequest(Airtable.ApiClient.Attributes.HttpVerb,System.String,System.Object)~System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}")]
