using System;
using System.IO;
using System.Net;
using System.Text;


namespace ExtendedLibrary
{
    /// <summary>
    /// Provide methods for working with web sites.
    /// </summary>
    public static class ExtendedHtmlHelpers
    {
        /// <summary>
        /// Get response from site.
        /// </summary>
        /// <param name="url">Url for response.</param>
        /// <param name="host">Host like as "kinopoisk.ru"</param>
        /// <returns></returns>
        public static WebResponse GetResponse(String url, String host)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
                request.Method = "GET";
                request.Host = "www." + host;
                request.UserAgent = "User-Agent=Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.1.7) Gecko/20091221 Firefox/3.5.7 (.NET CLR 3.5.30729)";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip, deflate");
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Referer = "http://" + host;
                request.CookieContainer = new CookieContainer();

                return request.GetResponse();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Get redirect url.
        /// </summary>
        /// <param name="url">Url from get redirect.</param>
        /// <param name="host">Host like as "kinopoisk.ru"</param>
        /// <returns></returns>
        public static String GetRedirectUrl(String url, String host)
        {
            var responce = (HttpWebResponse)GetResponse(url, host);
            if (responce != null)
            {
                return responce.ResponseUri.ToString();
            }
            return String.Empty;
        }

        /// <summary>
        /// Get response from site as HTML content.
        /// </summary>
        /// <param name="url">Url for response.</param>
        /// <param name="host">Host like as "kinopoisk.ru"</param>
        /// <returns></returns>
        public static String GetResponseHtml(String url, String host)
        {
            var response = (HttpWebResponse)GetResponse(url, host);
            var encoding = Encoding.GetEncoding(response.CharacterSet);
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
