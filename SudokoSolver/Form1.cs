using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokoSolver
{
    public partial class SudokoSolver : Form
    {
        bool _SolveClean;
        private List<TextBox> TextBoxesList;

        public SudokoSolver()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region Form SetUP

            this.BackColor = Color.CornflowerBlue;
            TextBoxesList = new List<TextBox>();

            this.Height = 500;
            this.Width = 400;

            Label titleLabel = new Label();
            titleLabel.Text = "Sudoku Solver";
            titleLabel.Left = 120;
            titleLabel.Font = new Font("cambria", 16);
            titleLabel.Top = 10;
            titleLabel.AutoSize = true;
            titleLabel.Visible = true;
            titleLabel.BringToFront();
            Controls.Add(titleLabel);

            Label smallTitleLabel = new Label();
            smallTitleLabel.Text = "fill the boxes with numbers and leave " + Environment.NewLine +" the unsolved part empty or with a 0";
            smallTitleLabel.Left = 60;
            smallTitleLabel.Top = 50;
            smallTitleLabel.Font = new Font("cambria", 12);
            smallTitleLabel.AutoSize = true;
            smallTitleLabel.Visible = true;
            smallTitleLabel.BringToFront();
            Controls.Add(smallTitleLabel);

            button1.Top = 400;
            button1.Left = 250;
            button1.Font = new Font("cambria", 15);
            button1.Text = "Solve!";
            button1.AutoSize = true;
            button1.BackColor = Color.Azure;

            int textBoxTop = 110;
            int textBotLeft = 55;
            //int t = 0;
            int l = 0;
            int r = 0;
            for (int i = 1; i < 81 && r < 9; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = $"tb{i}";
                tb.TextAlign = HorizontalAlignment.Center;
                tb.Width = 25;
                tb.Height = 25;
                tb.MaxLength = 1;
                tb.Top = textBoxTop;
                tb.Left = textBotLeft;
                tb.Text = String.Empty;
                TextBoxesList.Add(tb);
                Controls.Add(tb);
                if (l == 2)
                {
                    textBotLeft += 3;
                    l = 0;
                }
                else
                {
                    l++;
                }
                if (i < 9)
                {
                    textBotLeft += 30;
                }
                else
                {
                    textBoxTop += 30;
                    textBotLeft = 55;
                    //t++;
                    i = 0;
                    r++;
                }
            }
            #endregion

        }

        //Calling the api with 30 sec timeout
        private async Task<string> AccessAPI(string data)
        {
            string _url = $"https://localhost:7200/api/Solution/{data}";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.Timeout = TimeSpan.Parse("00:00:30");
           HttpResponseMessage _response = await _client.GetAsync(_url);

           if (_response.IsSuccessStatusCode)
           {
               string _returnedData = await _response.Content.ReadAsStringAsync();

               return _returnedData;
           }

           MessageBox.Show("Time runned out! please check the number you enter are right!");
           return "error";
        }

        //Clean the textboxes
        private void CleanBoxes()
        {
            foreach (var VARIABLE in TextBoxesList)
            {
                VARIABLE.Text = String.Empty;
            }
        }
        
        //Running the solve logic / running CleanBoxes method
        private void button1_Click(object sender, EventArgs e)
        {
            if (_SolveClean)
            {
                CleanBoxes();
                button1.Text = "Solve!";
                _SolveClean = false;
            }
            else
            {
                StringBuilder _builder = new StringBuilder();
                foreach (var VARIABLE in TextBoxesList)
                {
                    int value = 0;
                    bool checkNum = Int32.TryParse(VARIABLE.Text, out value);
                    if (checkNum || string.IsNullOrEmpty(VARIABLE.Text))
                    {
                        _builder.Append(value);
                    }
                    else
                    {
                        MessageBox.Show("Please enter only single numbers!");
                        CleanBoxes();
                        return;
                    }
                }

                SendData(_builder.ToString());
            }

        }

        //Executing the AccessAPI from a new thread
        private void SendData(string data)
        {
            Thread th = new Thread(() =>
            {
                string dataFromApi = AccessAPI(data).Result;
                if (!dataFromApi.Equals("error"))
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        FilldSolution(dataFromApi);
                    }));
                }
            });
            th.Start();
        }

        //Receiving data from thread and filling the textboxes
        private void FilldSolution(string dataFromApi)
        {
            char[] _chars = dataFromApi.ToCharArray();
            int i = 0;
            foreach (var m in _chars)
            {
                TextBoxesList[i].Text = m.ToString();
                i++;
            }
            _SolveClean = true;
            button1.Text = "Clean!";
        }
    }
}
