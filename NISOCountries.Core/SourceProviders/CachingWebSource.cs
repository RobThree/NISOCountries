using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;
using System.Text;

namespace NISOCountries.Core.SourceProviders
{
    public class CachingWebSource : ISourceProvider
    {
        public RequestCachePolicy CachePolicy { get; set; }

        public ICredentials Credentials { get; set; }

        public IWebProxy Proxy { get; set; }

        public TimeSpan DefaultTTL { get; set; }

        public TimeSpan Timeout { get; set; }

        public Encoding Encoding { get; private set; }

        public static readonly string USERAGENT = string.Format("{0} v{1}", typeof(CachingWebSource).Assembly.GetName().Name, typeof(CachingWebSource).Assembly.GetName().Version.ToString());

        private const string CACHEFILEEXT = ".cache";
        private const string HASHALGO = "MD5";

        public CachingWebSource()
            : this(TimeSpan.FromHours(24)) { }

        public CachingWebSource(TimeSpan defaultTtl)
            : this(defaultTtl, Encoding.UTF8) { }

        public CachingWebSource(TimeSpan defaultTtl, Encoding encoding)
            : this(defaultTtl, encoding, TimeSpan.FromSeconds(30)) { }

        public CachingWebSource(TimeSpan defaultTtl, Encoding encoding, TimeSpan timeOut)
        {
            this.DefaultTTL = defaultTtl;
            this.Encoding = encoding;
            this.Timeout = timeOut;
        }

        public StreamReader GetStreamReader(string uri)
        {
            var tmpfile = this.DownloadFileFromCache(new Uri(uri), GetCacheFile(uri));
            return new StreamReader(tmpfile, this.Encoding);
        }

        private string DownloadFileFromCache(Uri uri, string destinationpath)
        {
            if (IsFileExpired(destinationpath, this.DefaultTTL))
            {
                using (var w = new TimedWebClient(this.Timeout))
                {
                    w.CachePolicy = this.CachePolicy;
                    w.Credentials = this.Credentials;
                    w.Proxy = this.Proxy;
                    w.Headers.Add(HttpRequestHeader.UserAgent, USERAGENT);
                    w.DownloadFile(uri, destinationpath);
                }
            }
            return destinationpath;
        }

        private static bool IsFileExpired(string path, TimeSpan ttl)
        {
            return (!File.Exists(path) || (DateTime.UtcNow - new FileInfo(path).LastWriteTimeUtc) > ttl);
        }


        private static string GetCacheFile(string uri)
        {
            return Path.Combine(Path.GetTempPath(), HashString(uri) + CACHEFILEEXT);
        }

        private static string HashString(string value)
        {
            using (var ha = HashAlgorithm.Create(HASHALGO))
            {
                return string.Join(null, ha.ComputeHash(Encoding.Unicode.GetBytes(value.ToLowerInvariant().ToString()))
                    .Select(v => v.ToString("x2")));
            }
        }

        /// <summary>
        /// Simple "wrapper class" providing timeouts.
        /// </summary>
        private class TimedWebClient : WebClient
        {
            public int Timeout { get; set; }

            public TimedWebClient(TimeSpan timeout)
            {
                this.Timeout = (int)Math.Max(0, timeout.TotalMilliseconds);
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var wr = base.GetWebRequest(address);
                wr.Timeout = this.Timeout;
                return wr;
            }
        }
    }
}
