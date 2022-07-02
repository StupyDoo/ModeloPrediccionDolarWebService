using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{



    public partial class Form1 : Form
    {
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public static string[] valuesIA;
        public static string resultado;
        public Form1()
        {
      
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valuesIA = new string[]
                {
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox6.Text,
                    textBox7.Text,
                    textBox8.Text,
                    textBox9.Text,
                    textBox10.Text,
                    "0",
                };
            InvokeRequestResponseService().Wait();
            textBox11.Text = resultado;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Algo pasó al menos...");
        }

        static async Task InvokeRequestResponseService()
        {


            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Primero", "Segundo", "Tercero", "Cuarto", "Quinto", "Sexto", "S�ptimo", "Octavo", "Noveno", "D�cimo", "Resultado"},
                                Values = new string[,] { 
                                    { 
                                        valuesIA[0].ToString(),
                                        valuesIA[1].ToString(),
                                        valuesIA[2].ToString(),
                                        valuesIA[3].ToString(),
                                        valuesIA[4].ToString(),
                                        valuesIA[5].ToString(),
                                        valuesIA[6].ToString(),
                                        valuesIA[7].ToString(),
                                        valuesIA[8].ToString(),
                                        valuesIA[9].ToString(),
                                        "0"
                                    },  
                                    {
                                        valuesIA[0].ToString(),
                                        valuesIA[1].ToString(),
                                        valuesIA[2].ToString(),
                                        valuesIA[3].ToString(),
                                        valuesIA[4].ToString(),
                                        valuesIA[5].ToString(),
                                        valuesIA[6].ToString(),
                                        valuesIA[7].ToString(),
                                        valuesIA[8].ToString(),
                                        valuesIA[9].ToString(),
                                        "9999"
                                    },  
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }

                };

                

                const string apiKey = "KNlAlubyTKL1BcAZGJREc5mh7BpXhDJ3kN5CLF4cfLETul1zduyXWzZe/KLnooOjNeN/j6basQm287XK4WQgvQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/ff2565f51eff409ab2ae9036e596eb28/services/d8c0569116be4ab1a7456c60af0f7628/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();



                    Console.WriteLine("Result: {0}", result);

                    string FinalString;
                    
                    int Pos1 = result.IndexOf("9999") + 7;
                    int Pos2 = result.IndexOf("]]}}}}") - 1;
                    resultado = result.Substring(Pos1, Pos2 - Pos1);
                    
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }

            
        }

    }



        

}
