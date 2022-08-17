using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokoSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Label l = new Label();
            l.Text = "Hello";
            l.Location = new Point(50, 100);
            l.AutoSize = true;

            l.Text = AccessAPI("008900410023000790710050080000040370380206041047090000070010059031000860096004100").Result;
        }

        private async Task<string> AccessAPI(string data)
        {
            string _url = $"https://localhost:7200/api/Solution/{data}";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
           HttpResponseMessage _response = await _client.GetAsync(_url);

           if (_response.IsSuccessStatusCode)
           {
               string _returnedData = await _response.Content.ReadAsStringAsync();

               return _returnedData;
           }
           return null;
        }
    }
}
