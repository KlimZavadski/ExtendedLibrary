using System;
using System.IO;
using System.Text;

namespace ExtendedLibrary
{
    public static class ExtendedIOHelpers
    {
        /// <summary>
        /// Save text to file.
        /// </summary>
        /// <param name="fileName">Output file name.</param>
        /// <param name="text">Text for saving.</param>
        public static void SaveToFile(String fileName, Encoding encoding, String text)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, encoding))
            {
                writer.Write(text);
            }
        }

        /// <summary>
        /// Save object to file as json text.
        /// </summary>
        /// <param name="fileName">Output file name.</param>
        /// <param name="obj">Object for saving.</param>
        public static void SaveAsJsonToFile(String fileName, Object obj)
        {
            SaveToFile(fileName, Encoding.UTF8, ExtendedJavascriptHelpers.Serialize(obj));
        }
    }
}
