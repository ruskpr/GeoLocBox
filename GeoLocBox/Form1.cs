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
        
        private GPS gps;
        //private ErrorEventBox errorBox;
        private double altitude;
        private double longtitude;
        private double latitude;

        private TemperatureSensor temp = new TemperatureSensor();
        private double temperature;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            gps = new GPS();
          

            gps.Attach += Gps_Attach;
            gps.Detach += Gps_Detach;
            gps.Error += Gps_Error; ;
            gps.PositionChange += Gps_PositionChange;

            temp.Attach += Temp_Attach;
            temp.Detach += Temp_Detach;
            temp.TemperatureChange += Temp_TemperatureChange;

            
            gps.Open();

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

        private void Gps_Error(object sender, Phidget22.Events.ErrorEventArgs e)
        {
            //errorBox.addMessage(e.Description);

        }

        private void Gps_PositionChange(object sender, Phidget22.Events.GPSPositionChangeEventArgs e)
        {
            longtitude = e.Longitude;
            latitude = e.Latitude;
            altitude = e.Altitude;
        }

        private void Gps_Detach(object sender, Phidget22.Events.DetachEventArgs e)
        {
            GPS detached = (GPS)sender;

        }

        private void Gps_Attach(object sender, Phidget22.Events.AttachEventArgs e)
        {
            GPS attached = (GPS)sender;

        }

        private void BtnRed_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            if (this.BackColor == Color.White)
            {
                this.BackColor = Color.Red;
            }
            else
            {
                this.BackColor = Color.White;
            }

            //SQLiteDataLayer dl = new SQLiteDataLayer("Data source = F:/FALL/ITEC 210 - Sytem Analysis/GropLocBox/groupBoxDB.db");
            SQLiteDataLayer dl = new SQLiteDataLayer("Data source=../../../Database/groupBoxDb.db");


            if (gps.Attached)
            {
             dl.InsertRecord(latitude,longtitude,altitude,DateTime.Now.ToString());
                
                //string gpsText = gps.DateAndTime.ToLocalTime().ToString();


            }

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