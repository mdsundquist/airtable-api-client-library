﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Airtable.ApiClient.Extensions
{
    public static class EnumExtensions
    {
        public static bool HasAttribute<T>(this Enum element) where T : Attribute =>
            Attribute.GetCustomAttribute(element.GetType().GetMember(element.ToString()).SingleOrDefault(), typeof(T)) != null;

        public static T GetAttribute<T>(this Enum element) where T : Attribute =>
            (T)Attribute.GetCustomAttribute(element.GetType().GetMember(element.ToString()).SingleOrDefault(), typeof(T));

        public static List<Attribute> GetAttributes(this Enum element) =>
            Attribute.GetCustomAttributes(element.GetType().GetMember(element.ToString()).SingleOrDefault()).ToList();
    }
}
