using Phidget22;
namespace GeoLocBox
{
    public partial class Form1 : Form
    {
        DigitalInput btnGreen = null;
        DigitalInput btnRed = null;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;

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

            dl.InsertRecord("this is leila");
            MessageBox.Show("Test");
            MessageBox.Show("TestFORREAL");
        }

        private void Button1_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            //if (this.BackColor == Color.White)
            //{
            //    this.BackColor = Color.Green;
            //}
            //else
            //{
            //    this.BackColor = Color.White;
            //}
        }
    }
}