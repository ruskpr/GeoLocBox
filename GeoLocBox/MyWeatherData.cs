using Phidget22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocBox
{
    internal class MyWeatherData
    {
        private TemperatureSensor temp = new TemperatureSensor();
        private HumiditySensor humidity = new HumiditySensor();
        private LightSensor light = new LightSensor();
        private double temperature;
        private double hum;
        private double lum;
        public void RecordData()
        {
            SQLiteDataLayer dl = new SQLiteDataLayer("Data source=../../../Database/groupBoxDb.db");
            humidity.Open(1000);
            temp.Open(1000);
            light.Open(1000);

           
            
            humidity.Close();
            temp.Close();
            light.Close();
            dl.InsertWeatherRecord(temperature,hum,lum);

        }

        public MyWeatherData()
        {
            temp = new TemperatureSensor();
            humidity = new HumiditySensor();
            light = new LightSensor();



            

            temp.TemperatureChange += Temp_TemperatureChange; 

            humidity.HumidityChange += Humidity_HumidityChange; 

            light.IlluminanceChange += Light_IlluminanceChange; 

        }

        private void Light_IlluminanceChange(object sender, Phidget22.Events.LightSensorIlluminanceChangeEventArgs e)
        {
            lum = e.Illuminance;
        }

        private void Humidity_HumidityChange(object sender, Phidget22.Events.HumiditySensorHumidityChangeEventArgs e)
        {
           hum = e.Humidity;
        }

        private void Temp_TemperatureChange(object sender, Phidget22.Events.TemperatureSensorTemperatureChangeEventArgs e)
        {
            temperature = e.Temperature;
        }

       

        //public void SendDataToDB()
        //{
        //    SQLiteDataLayer dl = new SQLiteDataLayer("Data source=../../../Database/groupBoxDb.db");

        //    try
        //    {

        //        dl.InsertWeatherRecord(temperature, hum, lum);
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBox.Show(x.Message);
        //    }
            
        //}
    }
}
