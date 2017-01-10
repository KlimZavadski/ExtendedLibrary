using System;
using System.IO;
using System.Text;

namespace ExtendedLibrary
{
    public static class IOHelper
    {
        #region File

        /// <summary>
        /// Save text to file.
        /// </summary>
        /// <param name="fileName">Output file name.</param>
        /// <param name="text">Text for saving.</param>
        public static void SaveToFile(string fileName, Encoding encoding, string text)
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
        public static void SaveJsonToFile(string fileName, object obj)
        {
            SaveToFile(fileName, Encoding.UTF8, JavascriptHelper.Serialize(obj));
        }

        #endregion

        #region Console

        public delegate bool BoolAction();

        /// <summary>
        /// Show alert in console with question.
        /// </summary>
        /// <param name="message">Question text.</param>
        /// <param name="action">Action that would be executed if answer will 'y'.</param>
        public static void ShowAlert(string message, Action action)
        {
            Console.Write(message);

            if (Console.ReadLine() == "y")
            {
                action();
            }
        }

        /// <summary>
        /// Show alert in console with question.
        /// </summary>
        /// <param name="message">Question text.</param>
        /// <param name="action">Action with return type of bool that would be executed if answer will 'y'.</param>
        /// <returns></returns>
        public static bool ShowAlert(string message, BoolAction action)
        {
            Console.Write(message);

            return Console.ReadLine() == "y" && action();
        }

        #endregion
    }
}
