using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Authenticators;

namespace CadJiraForAll
{
    public class CadJira
    {
        public static string partnummer;
        public static string cadprogram;


        public string UserNameInSystem()
        {
            string Name = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
            return Name;
        }

        public async Task<string> APIRequestAsync()
        {
            string url = "https://jsonplaceholder.typicode.com/posts"; //Get = https://jsonplaceholder.typicode.com/todos/1 // Post = https://jsonplaceholder.typicode.com/posts

            var client = new RestClient(url);

            var request = new RestRequest();
            request.AddParameter("id", "99");

            var restResponse =
                await client.ExecuteGetAsync(request);
            var stringresponse = restResponse.Content;
            return stringresponse;
        }
        public void Formchoice()
        {
            FormChoice formet =  new FormChoice();
            formet.ShowDialog(); //synkront vs Show()
        }


        public string redmAreChosen(string val1, string val2)
        {
            //JSON API query
            return "1";
        }

        public string gcsAreChosen(string val1, string val2)
        {
            //JSON API query
            return "1";
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
    }
    public class RunAll
    {
        public async Task NewMain() //Denne kjører i CAD og tar seg av selve kjøringen.
        {
            CadJira felleskode = new CadJira();
            string testen = felleskode.FunkerDette();


            //felleskode.Formchoice();

            string testename = felleskode.UserNameInSystem();
            var testeAPI = felleskode.APIRequestAsync();
            felleskode.Formchoice();

            MessageBox.Show(testeAPI.Result);
            MessageBox.Show(testename);
            MessageBox.Show(testen);
            //MessageBox.Show(CadJira.partnummer);
        }
    }
}
