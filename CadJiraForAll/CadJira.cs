﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace CadJiraForAll
{
    public class CadJira
    {
        public static string partnummer;
        public static string cadprogram;
        public static string redm_or_gcs;


        //public string UserNameInSystem()
        //{
        //    string Name = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
        //    return Name;
        //}


        public static void Formchoice()
        {
            FormChoice formet = new FormChoice();
            formet.ShowDialog(); //synkront vs Show()
        }

        public static async Task API_Request(string choice_made)
        {

            string url="";
            string json="";
            if (choice_made == "redm")
            {
                url = "https://jira.nov.com/rest/servicedeskapi/request";
                json = RigEDMJson();
            }
            if (choice_made == "gcs")
            {
                url = "https://support.nov.com/rest/servicedeskapi/request";
                json = GCSJson();
            }
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddStringBody(json, DataFormat.Json);

            client.Authenticator = new HttpBasicAuthenticator("risinggaardsortla", "91323212Zenith");

            var restResponse = await client.ExecutePostAsync(request);
            //MessageBox.Show("ETTER RESTRESPONSE"); //FJERNES//FJERNES - DENNE KJØRER IKKE! SE LINJE 61!

            if (restResponse.IsSuccessful)
            {
                MessageBox.Show("Ticket created");
                //Console.WriteLine(JsonConvert.DeserializeObject(restResponse.Content)); //DENNE MÅ VEKK PÅ ET TIDSPUNKT -  FINNE URL FRA DENNE.
            }
            //MessageBox.Show(restResponse.Content.ToString() );  ///HVA BLIR FAKTISK SENDT.
            //MessageBox.Show(restResponse.StatusCode.ToString());
            //MessageBox.Show(restResponse.ErrorException.ToString());

        }


        public void DeliverAttributes(string partnumber, string CadProgram) // string mfg="default"
        {
            partnummer = partnumber;
            cadprogram = CadProgram;
            /*Felleskoden mottar data gjennom denne metoden.
             Hentes fra AD.
            Brukeren får et valg om REDM og GCS&ET via Windows Form.
            Basert på dette valget =
            Så opprettes det et JSON objekt med dataene fra CAD og AD.
            Disse dataene som blir benyttet til å gjøre et API kall til JIRA.
            Ta vare på respons fra API kallet som bruker til å åpne riktig URL.
            Når kallet er gjort blir et åpnet en nettleser med ticketen som nå er opprettet.
            */
            //kort sagt: Samle data, ta valg, bruke dataene og presentere resultat.
        }
        public string FunkerDette() //ja det gjør det
        {
            return $"{partnummer} og {cadprogram}";
        }

        public static string RigEDMJson()
        {
            string jsonstring = "{" +
                "\"serviceDeskId\": \"4822\"," +
                "\"requestTypeId\": \"14610\"," +
                "\"requestFieldValues\": {" +
                "\"summary\": \" "+ partnummer +"\"," +
                "\"customfield_16671\": {\"value\": \"No Business Disruption - Workaround Available\"}," +
                "\"customfield_16665\": {\"value\": \"Impacts Me or a Single Person\"}," +
                "\"customfield_10040\": {\"value\": \"Norway\"}," +
                "\"customfield_13564\": [{\"value\": \"Other\"}]," +
                "\"customfield_12699\": {\"value\": \"Rig Systems\"}," +
                //"\"customfield_14114\": {\"value\": \"Norway\"}," +
                "\"customfield_15509\": {\"value\": \"No, update does not need Global ID\"}," +
                //"\"customfield_14471\": {\"value\": \"Purchased\"}," +
                "\"description\": \"This is a General description sent through JIRA API, Please Ignore this ticket.\"," +
                "\"customfield_14361\": {\"value\": \"No, Do Not Enable\"}," +
                "\"customfield_13664\": 1" +
                "}" +
                //"" +
                //"" +
                //"" +
                "}";
            //Console.WriteLine(jsonstring);
            return jsonstring;
        }

        public static string GCSJson()
        {
            string json = "{ \"serviceDeskId\": \"11\",\"requestTypeId\": \"1112\", \"requestFieldValues\": { \"customfield_11869\": {\"value\": \"Other\"}, \"description\": \"Greetings from CADJIRA API TEST\"  } }";
            return json;
        }

        //static readonly HttpClient client = new HttpClient();

        public static async Task NewTwoMain()
        {
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/66");
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();

                MessageBox.Show(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
        public static async Task RestTwoMain()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://jira.nov.com/rest/") })
            {
                var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("risinggaardsortla:91323212Zenith"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
                //POST//var response = await client.PostAsync(requestUri, new StringContent());
                var response = await client.GetAsync("servicedeskapi/servicedesk/4822/requesttype/14610/field");

                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);
            }
        }
    }
    public class RunAll
    {
        public async Task NewMain() //Denne kjører i CAD og tar seg av selve kjøringen.
        {
            MessageBox.Show("HELLO FROM THE OTHER SIDE AGAIN 7");
            //CadJira felleskode = new CadJira();
            //felleskode.Formchoice();

            //CadJira.Formchoice();
            //MessageBox.Show(CadJira.redm_or_gcs);

            //await CadJira.API_Request(CadJira.redm_or_gcs);

            await CadJira.RestTwoMain();

            //LAGE REQUEST I JIRADDIN??
            //HVORDAN FÅ KJØRT DENNE HER...?
        }
    }
}


//Lage en HTTP request, men RestSharp er bar een wrapper da? mmmmm