using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENM155
{
    public partial class frmStart : Form
    {

        #region Local Varables

        private int baseYear = 2015;

        private int elect_housing;
        private int elect_industry;

        private double transportEnergyFromFossilFuels;
        private double transportEnergyFromBiogas; 


        #endregion


        public frmStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "KURS ENM155, LP2 : 2017 -> MODELLERING AV SVENSKA ENERGISCENARIER";
            this.MaximizeBox = false;
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            //Load all years from now until 2050
            for (int k = 0; k <= 35; k++)
            {
                int formulatedYear = baseYear + k;
                cboTime.Items.Add(formulatedYear.ToString());
            }

            CalculateTransportEnergyRelativeBaseYear(2015);
        }

        #region Transport Calculations

        private double GetTransportEnergy(double overallEnergyUsage, 
                double FinalEnergyUsage, double fuelEfficiencyProduction, double fuelEfficiencyAfterUsage)
        {
            return (overallEnergyUsage * FinalEnergyUsage) / (fuelEfficiencyAfterUsage * fuelEfficiencyProduction);
        }

        private void CalculateTransportEnergyRelativeBaseYear(int year)
        {
            int yearsDifference = (year == baseYear ? 1 : year - baseYear);
            double incrementFactor = (year == baseYear ? 1 : (0.005 * yearsDifference));

            transportEnergyFromFossilFuels =  GetTransportEnergy(17.0, 0.84, 0.9, 0.2) * incrementFactor;
            transportEnergyFromBiogas =  GetTransportEnergy(17.0, 0.16, 0.5, 0.2) * incrementFactor;
        }

        #endregion

        #region Housing Calculations

        #endregion

        #region Industry Calculations

        #endregion

        private void cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTransportEnergyRelativeBaseYear(Convert.ToInt32(cboTime.SelectedItem.ToString()));
            MessageBox.Show((transportEnergyFromFossilFuels + transportEnergyFromBiogas).ToString());
        }
    }
}
