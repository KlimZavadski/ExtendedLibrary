using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExtendedLibrary
{
    /// <summary>
    /// Provide methods for working with web sites.
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// Get response from site by https protocol (used SSL).
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static WebResponse GetResponse(string url, string login, string password)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);

                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.95 Safari/537.36";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = false;

                request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch, br");
                request.CookieContainer = new CookieContainer();

                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    request.Credentials = new NetworkCredential(login, password);
                    request.PreAuthenticate = true;
                }

                return request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        /// <summary>
        /// Get redirect url by https protocol (used SSL).
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static string GetRedirectUrl(string url, string login, string password)
        {
            return (GetResponse(url, login, password) as HttpWebResponse)?.ResponseUri.ToString();
        }

        /// <summary>
        /// Get redirect url by http protocol.
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <returns></returns>
        public static string GetRedirectUrl(string url)
        {
            return GetRedirectUrl(url, null, null);
        }

        /// <summary>
        /// Get response from site as HTML content by https protocol (used SSL).
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static string GetResponseHtml(string url, string login, string password)
        {
            var response = (HttpWebResponse) GetResponse(url, login, password);

            if (response != null)
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet != string.Empty ? response.CharacterSet : "windows-1251");

                using (var streamReader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return streamReader.ReadToEnd();
                }
            }

            return null;
        }

        /// <summary>
        /// Get response from site as HTML content by http protocol.
        /// </summary>
        /// <param name="url">Url for response.</param>
        /// <returns></returns>
        public static string GetResponseHtml(string url)
        {
            return GetResponseHtml(url, null, null);
        }
    }
}
