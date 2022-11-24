﻿using System;
using System.Diagnostics;
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
        public static string weight;

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
        }


        public void DeliverAttributes(string partnumber, string CadProgram, string Weight) // string mfg="default"
        {
            partnummer = partnumber;
            cadprogram = CadProgram;
            weight = Weight;
        }

        public static string RigEDMJson()
        {
            string jsonstring = "{" +
                "\"serviceDeskId\": \"4822\"," +
                "\"requestTypeId\": \"14610\"," +
                "\"requestFieldValues\": {" +
                "\"summary\": \"  Partnumber/Name:"+ partnummer +" \"," +
                "\"customfield_16671\": {\"value\": \"No Business Disruption - Workaround Available\"}," +
                "\"customfield_16665\": {\"value\": \"Impacts Me or a Single Person\"}," +
                "\"customfield_10040\": {\"value\": \"Norway\"}," +
                "\"customfield_13564\": [{\"value\": \"Other\"}]," +
                "\"customfield_12699\": {\"value\": \"Rig Systems\"}," +
                //"\"customfield_14114\": {\"value\": \"Norway\"}," +
                "\"customfield_15509\": {\"value\": \"No, update does not need Global ID\"}," +
                //"\"customfield_14471\": {\"value\": \"Purchased\"}," +
                "\"description\": \" Data Collected from: "+cadprogram.ToUpper()+"  ---  Listed weight(in Grams): "+weight+" \"," +
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


        public static async Task RestTwoMain()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://jira.nov.com/rest/") })
            {
                var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("risinggaardsortla:91323212Zenith"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
                var response = await client.PostAsync("servicedeskapi/request", new StringContent(RigEDMJson().ToString(), Encoding.UTF8, "application/json")); //LEGG TIL URI SOM I GET PLUSS JSON/STRINGEN DU VIL POSTE MED REQUESTEN. (https://jira.nov.com/rest/servicedeskapi/request + RigEDMJson() 
                //var response = await client.GetAsync("servicedeskapi/servicedesk/4822/requesttype/14610/field");

                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);
                //Parses out the URL for the ticket on web:
                var jObject = JObject.Parse(responseBody);
                string stringurlfromJson = (string)jObject["_links"]["web"];
                Process.Start(new ProcessStartInfo($"{stringurlfromJson}") { UseShellExecute = true });
            }
        }
    }
    public class RunAll
    {
        public async Task NewMain() //Denne kjører i CAD og tar seg av selve kjøringen.
        {
            //MessageBox.Show("HELLO FROM THE OTHER SIDE AGAIN 7");
            //CadJira felleskode = new CadJira();
            //felleskode.Formchoice();

            //CadJira.Formchoice();
            //MessageBox.Show(CadJira.redm_or_gcs);

            //await CadJira.API_Request(CadJira.redm_or_gcs);

            await CadJira.RestTwoMain();
            //MessageBox.Show(CadJira.partnummer);
            //MessageBox.Show(CadJira.cadprogram);
            //MessageBox.Show(CadJira.weight);

        }
    }
}

