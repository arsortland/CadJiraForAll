using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace CadJiraForAll
{
    public class CadJira
    {
        public static string partnummer;
        public static string cadprogram;
        public static string weight;
        public static string weightInKilos;
        public static string material;

        public static string redm_or_gcs;
        public static string richtext;
        public static string loginpw;
        public static string username = UserNameInSystem().ToString();

        public static bool loggedIn = false;

        public static string UserNameInSystem()
        {
            string Name = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
            string parsedName = Name.Remove(0, 4);
            return parsedName;
        }

        public static string FromGramToKilos(string aWeight)
        {
            double weighInGrams = Convert.ToDouble(aWeight);
            double weightinKilos = weighInGrams / 1000;
            double weightInKilosRounded = Math.Round(weightinKilos, 3);
            string weightinKiloString = weightInKilosRounded.ToString();
            return weightinKiloString;
        }


        public static void Formchoice()
        {
            FormChoice formet = new FormChoice();
            formet.ShowDialog(); //synkront vs Show()
        }

        public static void FormInput()
        {
            DescUserInput formet = new DescUserInput();
            formet.ShowDialog();
        }

        public static void FormLogin()
        {
            Login formet = new Login();
            formet.ShowDialog();
        }


        public void DeliverAttributes(string partnumber, string CadProgram, string Weight, string Material) // string mfg="default"
        {
            partnummer = partnumber;
            cadprogram = CadProgram;
            weight = Weight;
            material = Material;
            weightInKilos = FromGramToKilos(weight);
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
                "\"description\": \" Data Collected from: " + cadprogram + "  ---  Listed weight: "+ weightInKilos+"kg. ---Material: "+material+"  --- Written by reporter: " + richtext + " \"," +
                "\"customfield_14361\": {\"value\": \"No, Do Not Enable\"}," +
                "\"customfield_13664\": 1" +
                "}" +
                //"" +
                //"" +
                //"" +
                "}";
            //MessageBox.Show(jsonstring);
            return jsonstring;
        }

        public static string GCSJson()
        {
            string json = "{ \"serviceDeskId\": \"11\",\"requestTypeId\": \"1112\", \"requestFieldValues\": { \"customfield_11869\": {\"value\": \"Other\"}, \"description\": \" Data Collected from: "+cadprogram+" --- PartNumber/Name: "+partnummer+"  ---  Listed weight: "+weightInKilos+" kg. --- Material: "+material+"  --- Written by reporter: "+richtext+" \"  } }";
            return json;
        }


        public static async Task API_Request(string choice_made)
        {
            string url = "";
            string jsonstring = "";
            if (choice_made == "redm")
            {
                url = "https://jira.nov.com/rest/";
                jsonstring = RigEDMJson().ToString();
            }
            if (choice_made == "gcs")
            {
                url = "https://support.nov.com/rest/";
                jsonstring = GCSJson().ToString(); 
            }

            using (var client = new HttpClient { BaseAddress = new Uri(url) })
            {
                var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{loginpw}"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);

                    var response = await client.PostAsync("servicedeskapi/request", new StringContent(jsonstring, Encoding.UTF8, "application/json"));
                                                                                                                                                      //var response = await client.GetAsync("servicedeskapi/servicedesk/4822/requesttype/14610/field"); = GET metode
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    //MessageBox.Show(responseBody);

                    //Parses out the URL for the ticket on web:
                    var jObject = JObject.Parse(responseBody);
                    string stringurlfromJson = (string)jObject["_links"]["web"];
                    Process.Start(new ProcessStartInfo($"{stringurlfromJson}") { UseShellExecute = true }); //Opens default browser with the link obtained from JSON.
                    loggedIn = true;
                }
                else
                {
                    MessageBox.Show("Oops! Something didn't work. :(" +
                        "\nError response: " + (int)response.StatusCode + " " + response.StatusCode);
                    //MessageBox.Show(response.StatusCode.ToString());
                }

            }
        }
    }
    public class RunAll
    {
        public void NewMain() //Denne kjører i CAD og tar seg av selve kjøringen.
        {

            if (!CadJira.loggedIn)
            {
                CadJira.FormLogin();
            }

            CadJira.Formchoice();
            //FormInpUt runs the API_Request method.
            CadJira.FormInput();
            
        }
    }
}

