using Phidget22;
using System.Drawing.Text;
using Windows.Devices.Geolocation;
using Phidget22.Events;



namespace GeoLocBox
{
    public partial class Form1 : Form
    {
        DigitalInput btnGreen = null;
        DigitalInput btnRed = null;
        MyGPS myGPS;



        private TemperatureSensor temp = new TemperatureSensor();
        private double temperature;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            myGPS = new MyGPS();

            temp.Attach += Temp_Attach;
            temp.Detach += Temp_Detach;
            temp.TemperatureChange += Temp_TemperatureChange;

            // green
            btnGreen = new DigitalInput();
            btnGreen.Channel = 0;
            btnGreen.HubPort = 0;
            btnGreen.IsHubPortDevice = true;
            //button1.StateChange += Button1_StateChange;
            btnGreen.StateChange += Button1_StateChange;

            btnGreen.Open(1000);

            // red
            btnRed = new DigitalInput();
            btnRed.Channel = 0;
            btnRed.HubPort = 1;
            btnRed.IsHubPortDevice = true;
            btnRed.StateChange += BtnRed_StateChange;
            btnRed.Open(1000);
        }

        private void Temp_TemperatureChange(object sender, TemperatureSensorTemperatureChangeEventArgs e)
        {
            temperature = Convert.ToDouble(e.Temperature.ToString());
        }

        private void Temp_Detach(object sender, DetachEventArgs e)
        {
            
        }

        private void Temp_Attach(object sender, AttachEventArgs e)
        {
            TemperatureSensor attachedDevice = (TemperatureSensor)sender;
        }

        

        private void Gps_Attach(object sender, Phidget22.Events.AttachEventArgs e)
        {
            GPS attached = (GPS)sender;

        }

        private void BtnRed_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            if (this.BackColor == Color.White)
                this.BackColor = Color.Red;
            else
                this.BackColor = Color.White;

            // send coordinates to database on button press
            myGPS.SendDataToDB();

            //SQLiteDataLayer dl = new SQLiteDataLayer("Data source=../../../Database/groupBoxDb.db");
        }

        private void Button1_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
         
            if (temp.Attached)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}