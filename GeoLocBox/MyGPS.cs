using Phidget22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocBox
{
    public class MyGPS
    {

        private Phidget22.GPS gps;
        //private ErrorEventBox errorBox;
        private double altitude;
        private double longtitude;
        private double latitude;

        public MyGPS()
        {
            gps = new GPS();

            gps.Attach += Gps_Attach;
            gps.Detach += Gps_Detach;
            gps.Error += Gps_Error; ;
            gps.PositionChange += Gps_PositionChange;

            gps.Open();
        }
        
        public void SendDataToDB()
        {
            SQLiteDataLayer dl = new SQLiteDataLayer("Data source=../../../Database/groupBoxDb.db");

            if (gps.Attached)
            {
                dl.InsertLocationRecord(latitude, longtitude, altitude, gps.Time.ToString());
            }
        }

        #region Gps events
        private void Gps_Attach(object sender, Phidget22.Events.AttachEventArgs e)
        {
            GPS attached = (GPS)sender;
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
        #endregion
    }
}
