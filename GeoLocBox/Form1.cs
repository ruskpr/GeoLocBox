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
        private HumiditySensor humidity = new HumiditySensor();
        private LightSensor light = new LightSensor();  
        private double temperature;
        private double hum;
        private double lum;


        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            myGPS = new MyGPS();

            temp.Attach += Temp_Attach;
            temp.Detach += Temp_Detach;
            temp.TemperatureChange += Temp_TemperatureChange;

            humidity.HumidityChange += Humidity_HumidityChange;

            light.IlluminanceChange += Light_IlluminanceChange;

            // green
            btnGreen = new DigitalInput();
            btnGreen.Channel = 0;
            btnGreen.HubPort = 0;
            btnGreen.IsHubPortDevice = true;
            //button1.StateChange += Button1_StateChange;
            btnGreen.StateChange += BtnGreen_StateChange;

            btnGreen.Open(1000);

            // red
            btnRed = new DigitalInput();
            btnRed.Channel = 0;
            btnRed.HubPort = 1;
            btnRed.IsHubPortDevice = true;
            btnRed.StateChange += BtnRed_StateChange;
            btnRed.Open(1000);
        }

        private void Light_IlluminanceChange(object sender, LightSensorIlluminanceChangeEventArgs e)
        {
            lum = e.Illuminance;
        }

        private void Humidity_HumidityChange(object sender, HumiditySensorHumidityChangeEventArgs e)
        {
            hum = e.Humidity;

        }

        private void Temp_TemperatureChange(object sender, TemperatureSensorTemperatureChangeEventArgs e)
        {
            temperature = e.Temperature;
        }

        private void Temp_Detach(object sender, DetachEventArgs e)
        {
            
        }

        private void Temp_Attach(object sender, AttachEventArgs e)
        {
            TemperatureSensor attachedDevice = (TemperatureSensor)sender;
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
        private void BtnGreen_StateChange(object sender, DigitalInputStateChangeEventArgs e)
        {
            humidity.Open(1000);
            temp.Open(1000);
            light.Open(1000);

            if (this.BackColor == Color.White)
                this.BackColor = Color.Green;
            else
                this.BackColor = Color.White;
            if (temp.Attached)
            {
                
                MessageBox.Show("temp: " + temperature.ToString());
            }
            if(humidity.Attached)
            {
                MessageBox.Show("humidity: " + hum.ToString());

            }
            if (light.Attached)
            {

                MessageBox.Show("light: " + lum.ToString());
            }
            humidity.Close();
            temp.Close();
            light.Close();
        }

        private void Button1_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
         
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}