using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVCConsumWebService
{
    public class Repository
    {
        private string ws1 = "https://localhost:44391/api/";
        public List<contacte> GetAllContactes()
        {
            List<contacte> lc = (List<contacte>)MakeRequest(string.Concat(ws1, "contactesTot/"), null, "GET", "application/json", typeof(List<contacte>));
            return lc;
        }

        public contacte GetContacte(int id)
        {
            contacte c = (contacte)MakeRequest(string.Concat(ws1, "contacteTot/", id), null, "GET", "application/json", typeof(contacte));
            return c;
        }

        public contacte InsertContacte(contacte c2Add)
        {
            contacte c = (contacte)MakeRequest(string.Concat(ws1, "contacte/"), c2Add, "POST", "application/json", typeof(contacte));
            return c;
        }

        public contacte UpdateContacte(contacte c2Upd)
        {
            contacte c = (contacte)MakeRequest(string.Concat(ws1, "contacte/", c2Upd.contacteId), c2Upd, "PUT", "application/json", typeof(contacte));
            return c;
        }

        public void DeleteContacte(int id)
        {
            MakeRequest(string.Concat(ws1, "contacte/", id), null, "DELETE", null, typeof(void));
        }

        public object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        //  requestUrl: Url completa del Web Service, amb l'opció sol·licitada
        //  JSONrequest: objecte que se li passa en el body (només per a POST/PUT)
        //  JSONmethod: "GET"/"POST"/"PUT"/"DELETE"
        //  JSONContentType: "application/json" en els casos que el Web Service torni objectes
        //  JSONRensponseType:  tipus d'objecte que torna el Web Service (typeof(tipus))
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest; //WebRequest WR = WebRequest.Create(requestUrl);   
                string sb = JsonConvert.SerializeObject(JSONRequest);
                request.Method = JSONmethod;  // "GET"/"POST"/"PUT"/"DELETE";  

                if (JSONmethod != "GET")
                {
                    request.ContentType = JSONContentType; // "application/json";   
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                    return objResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
