using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIClientVelib
{
    public partial class Form1 : Form
    {
        WcfSoapVelib.VelibOperationsClient client;


        string city;
        string station;

        public Form1()
        {
            InitializeComponent();

            client = new WcfSoapVelib.VelibOperationsClient();

            IList<string> contracts = client.getContrats();

            foreach (string contrat in contracts)
            {
                comboBox2.Items.Add(contrat);
            }

            //this.getContrats();

            label7.Text = "Station : ";
            label8.Text = "Nombre de velibs disponibles : ";
        }
        /*
        private async void getContrats()
        {

            Task<string[]> asyncResponse = client.getContrats();
            string[] contrats = await asyncResponse;

            comboBox2.Items.Clear();

            foreach (string contrat in contrats)
            {
                comboBox2.Items.Add(contrat);
            }
        }
        */



        private async void getStations()
        {
            if (city.Length > 0)
            {
                Task<string[]> asyncResponse = client.getStationsAsync(city);
                string[] stations = await asyncResponse;

                listBox2.Items.Clear();
                foreach (string station in stations)
                    listBox2.Items.Add(station);
            }
        }

        private async void afficherVelibsDisposStation()
        {
            Task<int> asyncResponse = client.getAvailableBikesAsync(city, station);
            int availableBikes = await asyncResponse;

            label7.Text = "Station : " + station;
            label8.Text = "Nombre de velibs disponibles : " + availableBikes;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            station = listBox2.GetItemText(listBox2.SelectedItem);
            afficherVelibsDisposStation();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            city = comboBox2.GetItemText(comboBox2.SelectedItem);
            getStations();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
