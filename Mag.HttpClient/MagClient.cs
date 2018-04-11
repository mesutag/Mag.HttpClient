using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Mag.HttpClient
{
    public class MagClient
    {
        private string _url { get; set; }
        private string _body { get; set; }
        private string _method { get; set; }
        private string _contentType { get; set; }
        public TResponse Request<TResponse>()
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(_url) as HttpWebRequest;
                string sb = _body;
                request.Method = _method;
                request.ContentType = _contentType;
                //if (input.HeaderParameters != null)
                //{
                //    foreach (var headerParameter in input.HeaderParameters)
                //    {
                //        request.Headers.Add(headerParameter.Key + ":" + headerParameter.Value);
                //    }
                //}
                if (!string.IsNullOrEmpty(sb))
                {
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }


                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    if (response != null && response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    }
                    Stream streamData = response.GetResponseStream();
                    StreamReader sr = new StreamReader(streamData);
                    string strsb = sr.ReadToEnd();
                    TResponse objResponse = JsonConvert.DeserializeObject<TResponse>(strsb);

                    return objResponse;

                }
            }
            catch (Exception ex)
            {
                //LogHelper.Log(ex);
                throw new Exception(String.Format("Server error (HTTP {0}).", ex.InnerException));
            }
        }
        public MagClient SetUrl(string url)
        {
            _url = url;
            return this;
        }
        public MagClient SetBody(string body)
        {
            _body = body;
            return this;
        }
        public MagClient SetMethod(string method)
        {
            _method = method;
            return this;
        }
        public MagClient SetContentType(string contetType)
        {
            _contentType = contetType;
            return this;
        }
    }
}
