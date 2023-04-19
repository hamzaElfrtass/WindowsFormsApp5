using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        localhost.WebService1 proxy= new localhost.WebService1();
        HttpClient client = new HttpClient();   
        public Form1()
        {
            InitializeComponent();
        }
        private void WebServiceSettings()
        {
            client.BaseAddress = new Uri("https://localhost:44332/WebService1.asmx/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string jsoncountries = proxy.COUNTRIES();
            //DataTable dtcountries=JsonConvert.DeserializeObject<DataTable>(jsoncountries);
            //dataGridView1.DataSource= dtcountries;
            WebServiceSettings();
        }
        private DataTable stringsplit(string userjson)
        {
            string[] json=userjson.Split('>');
            string[] finaljson = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finaljson[0]);
            return dt;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            HttpResponseMessage message = client.GetAsync("datatableforusers?id=" + textBoxID.Text + "").Result;
            string userjson=message.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(userjson);
            dataGridView1.DataSource=stringsplit(userjson);
        }
    }
}
