using System;
using System.IO;
using System.Net;
using System.Text;


namespace ExtendedLibrary
{
    /// <summary>
    /// Provide methods for working with web sites.
    /// </summary>
    public static class ExtendedWebHelpers
    {
        /// <summary>
        /// Get response from site by https protocol (used SSL).
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static WebResponse GetResponse(String url, String login, String password)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
                request.Method = "GET";
                request.UserAgent = "User-Agent=Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.149 Safari/537.36";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip, deflate, sdch");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = false;
                request.PreAuthenticate = true;
                request.CookieContainer = new CookieContainer();
                request.Credentials = new NetworkCredential(login, password);

                return request.GetResponse();
            }
            catch (Exception e)
            {
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
        public static String GetRedirectUrl(String url, String login, String password)
        {
            var responce = (HttpWebResponse)GetResponse(url, login, password);

            if (responce != null)
            {
                return responce.ResponseUri.ToString();
            }

            return String.Empty;
        }

        /// <summary>
        /// Get redirect url by http protocol.
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <returns></returns>
        public static String GetRedirectUrl(String url)
        {
            return GetRedirectUrl(url, String.Empty, String.Empty);
        }

        /// <summary>
        /// Get response from site as HTML content by https protocol (used SSL).
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static String GetResponseHtml(String url, String login, String password)
        {
            var response = (HttpWebResponse)GetResponse(url, login, password);

            if (response != null)
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet != "" ? response.CharacterSet : "windows-1251");
                using (var streamReader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return streamReader.ReadToEnd();
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Get response from site as HTML content by http protocol.
        /// </summary>
        /// <param name="url">Url for response.</param>
        /// <returns></returns>
        public static String GetResponseHtml(String url)
        {
            return GetResponseHtml(url, String.Empty, String.Empty);
        }
    }
}
