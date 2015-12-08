﻿using System;
using System.Web.Script.Serialization;

namespace ExtendedLibrary
{
    /// <summary>
    /// Provide methods for working with JavaScript features. 
    /// </summary>
    public static class ExtendedJavascriptHelpers
    {
        /// <summary>
        /// Serialize object to json string.
        /// </summary>
        /// <param name="obj">Object for serializing.</param>
        /// <returns></returns>
        public static String Serialize(Object obj)
        {
            return (new JavaScriptSerializer()).Serialize(obj);
        }

        /// <summary>
        /// Deserialize json string to object.
        /// </summary>
        /// <param name="text">Json string.</param>
        /// <param name="type">Type of deserialize object.</param>
        /// <returns></returns>
        public static Object Deserialize(String text, Type type)
        {
            return (new JavaScriptSerializer()).Deserialize(text, type);
        }

        /// <summary>
        /// Deserialize json string to specific T object.
        /// </summary>
        /// <typeparam name="T">Type of deserialize object.</typeparam>
        /// <param name="text">Json string.</param>
        /// <returns></returns>
        public static T Deserialize<T>(String text) where T : Type
        {
            return (new JavaScriptSerializer()).Deserialize<T>(text);
        }
    }
}
