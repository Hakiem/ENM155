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

        private double transportEnergyForFossil = 17;
        private double housingEnergyForHeat = 91;
        private double housingEnergyForElectricity = 52;

        private double industryEnergyForHeat = 91;
        private double industryEnergyForElectricity = 49;

        private double electricityHousing;
        private double electricityIndustry;

        private static double transmissionLoss = 0.9;

        // El uppvärmning
        struct Elect_Heat
        {
            public double energyHeat;
            public double electToHeatDistribution;
            public double electToHeatEfficiency;
        };

        // Värmepump till Fjärrvärme
        struct Elect_HeatPump
        {
            public double energyHeat;
            public double pumpToHeatDistribution;
            public double fjarrVarmeToHeatDistribution;
            public double fjarrVarmeToHeatEfficiency;
            public double heatPumpToHeatEfficiency;
        };


        /// <summary>
        /// //////////////////////////// Fossil Fuels related Structures 
        /// </summary>

        // Fossilbränslen till värme
        struct Fossil_Heat
        {
            public double energyHeat;
            public double fossilToHeatDistribution;
            public double fossilToHeatEfficiency;
        };

        // Fossil till fjärrvärme
        struct Fossil_Fjarrvarme
        {
            public double energyHeat;
            public double fjarrVarmeToHeatDistribution;
            public double fossilToFjarrvarmeDistribution;
            public double fjarrvarmeToHeatEfficiency;
            public double fossilToFjarrvarmeEfficiency;
        };

        // Fossil till ström
        struct Fossil_Electricity
        {
            public double fossilToElectDistribution;
            public double fossilToElectEfficiency;
        };

        /// <summary>
        /// //////////////////////////// Bio Fuel Related Structures 
        /// </summary>
        struct BioFuel_Heat
        {
            public double energyHeat;
            public double BioFuelToHeatDistribution;
            public double BioFuelToHeatEfficiency;
        };

        struct BioFuel_Fjarrvarme
        {
            public double energyHeat;
            public double fjarrVarmeToHeatDistribution;
            public double bioFuelToFjarrvarmeDistribution;
            public double fjarrvarmeToHeatEfficiency;
            public double bioFuelToFjarrvarmeEfficiency;
        };

        struct BioFuel_Electricity
        {
            public double bioFuelToElectDistribution;
            public double bioFuelToElectEfficiency;
        };

        /// <summary>
        /// //////////////////////////// Wind Energy Related Structures 
        /// </summary>

        struct WindEnergy_Electricity
        {
            public double windToElectDistribution;
            public double windToElectEfficiency;
        };

        /// <summary>
        /// //////////////////////////// Water Related Structures 
        /// </summary>

        struct WaterEnergy_Electricity
        {
            public double waterToElectDistribution;
            public double waterToElectEfficiency;
        };


        /// <summary>
        /// //////////////////////////// Nuclear Energy Related Structures 
        /// </summary>

        struct NuclearEnergy_Electricity
        {
            public double nuclearToElectDistribution;
            public double nuclearToElectEfficiency;
        };

        /// <summary>
        /// //////////////////////////// Spill Energy Related Structures 
        /// </summary>

        struct SpillEnergy_Heat
        {
            public double energyHeat;
            public double spillToHeatDistribution;
            public double fjarrvarmeToHeatDistribution;
            public double fjarrvarmeToHeatEfficiency;
            public double spillToFjarrvarmeEfficiency;
        };

        struct SpillEnergy_Electricity
        {
            public double spillToElectDistribution;
            public double spillToElectEfficiency;
        };

        #endregion


        public frmStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "KURS ENM155, LP2 : 2017 -> MODELLERING AV SVENSKA ENERGISCENARIER";
            this.MaximizeBox = false;

            InitializeElectricityBaseCalculations();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            //Load all years from now until 2050
            for (int i = 1; i <= 36; i++)
            {
                int formulatedYear = baseYear + i;
                cboTime.Items.Add(formulatedYear.ToString());
            }

            //------------------------------------------FOSSIL FUELS----------------------------------------------------------------------

            // Calculate Energy supplied to housing from fossil fuels
            double k1 = CalculateEnergyFromFossilFuels(
                new Fossil_Heat { energyHeat = housingEnergyForHeat, fossilToHeatDistribution = 0.15, fossilToHeatEfficiency = 0.8}, 
                new Fossil_Fjarrvarme { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, fossilToFjarrvarmeDistribution = 0.1, fjarrvarmeToHeatEfficiency = 0.9, fossilToFjarrvarmeEfficiency = 0.75}, 
                new Fossil_Electricity { fossilToElectDistribution = 0.017, fossilToElectEfficiency = 0.3}, electricityHousing);

            // Calculate Energy supplied to industries from fossil fuels
            double k2 = CalculateEnergyFromFossilFuels(
                new Fossil_Heat { energyHeat = industryEnergyForHeat, fossilToHeatDistribution = 0.56, fossilToHeatEfficiency = 0.9 },
                new Fossil_Fjarrvarme { energyHeat = industryEnergyForHeat, fjarrVarmeToHeatDistribution = 0.04, fossilToFjarrvarmeDistribution = 0.1, fjarrvarmeToHeatEfficiency = 0.9, fossilToFjarrvarmeEfficiency = 0.75 },
                new Fossil_Electricity { fossilToElectDistribution = 0.017, fossilToElectEfficiency = 0.35 }, electricityIndustry);

            // calculate Energy supplied to transport from fossil fuels
            double k3 = GetTransportEnergy(transportEnergyForFossil, 0.94, 0.9, 0.2);

            double k = k1 + k2 + k3;

            //------------------------------------------BIO FUEL----------------------------------------------------------------------

            // Calculate Energy supplied to housing from bio fuels
            double b1 = CalculateEnergyFromBioFuels(
                new BioFuel_Heat { energyHeat = housingEnergyForHeat, BioFuelToHeatDistribution = 0.13, BioFuelToHeatEfficiency = 0.8 },
                new BioFuel_Fjarrvarme { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, bioFuelToFjarrvarmeDistribution = 0.61, fjarrvarmeToHeatEfficiency = 0.9, bioFuelToFjarrvarmeEfficiency = 0.75 },
                new BioFuel_Electricity { bioFuelToElectDistribution = 0.038, bioFuelToElectEfficiency = 0.35 }, electricityHousing);

            // Calculate Energy supplied to industries from bio fuels
            double b2 = CalculateEnergyFromBioFuels(
                new BioFuel_Heat { energyHeat = industryEnergyForHeat, BioFuelToHeatDistribution = 0.4, BioFuelToHeatEfficiency = 0.9 },
                new BioFuel_Fjarrvarme { energyHeat = industryEnergyForHeat, fjarrVarmeToHeatDistribution = 0.04, bioFuelToFjarrvarmeDistribution = 0.61, fjarrvarmeToHeatEfficiency = 0.9, bioFuelToFjarrvarmeEfficiency = 0.75 },
                new BioFuel_Electricity { bioFuelToElectDistribution = 0.038, bioFuelToElectEfficiency = 0.3 }, electricityIndustry);

            // calculate Energy supplied to transport from bio fuels
            double b3 = GetTransportEnergy(transportEnergyForFossil, 0.06, 0.5, 0.2);

            double b = b1 + b2 + b3;

            //------------------------------------------WIND ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from Wind Energy
            double we1 = CalculateEnergyFromWind(new WindEnergy_Electricity { windToElectDistribution = 0.028, windToElectEfficiency = 1 }, electricityHousing);

            // Calculate Energy supplied to industries from Wind Energy
            double we2 = CalculateEnergyFromWind(new WindEnergy_Electricity { windToElectDistribution = 0.028, windToElectEfficiency = 1 }, electricityIndustry);

            double we = we1 + we2;

            //------------------------------------------WATER ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from Water Energy
            double wa1 = CalculateEnergyFromWater(new WaterEnergy_Electricity { waterToElectDistribution = 0.478, waterToElectEfficiency = 1 }, electricityHousing);

            // Calculate Energy supplied to industries from Water Energy
            double wa2 = CalculateEnergyFromWater(new WaterEnergy_Electricity { waterToElectDistribution = 0.478, waterToElectEfficiency = 1 }, electricityIndustry);

            double wa = wa1 + wa2;

            //------------------------------------------NUCLEAR ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from Nuclear Energy
            double ne1 = CalculateEnergyFromNuclearEnergy(new NuclearEnergy_Electricity { nuclearToElectDistribution = 0.397, nuclearToElectEfficiency = 0.34 }, electricityHousing);

            // Calculate Energy supplied to industries from Nucear Energy
            double ne2 = CalculateEnergyFromNuclearEnergy(new NuclearEnergy_Electricity { nuclearToElectDistribution = 0.397, nuclearToElectEfficiency = 0.34 }, electricityIndustry);

            double ne = ne1 + ne2;

            //------------------------------------------SPILL ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from spill
            double sp1 = CalculateEnergyFromSpill(
                new SpillEnergy_Heat { energyHeat = housingEnergyForHeat, spillToHeatDistribution = 0.13, spillToFjarrvarmeEfficiency = 0.8 },
                new SpillEnergy_Electricity { spillToElectDistribution = 0.038, spillToElectEfficiency = 0.35 }, electricityHousing);

            // Calculate Energy supplied to industry from spill
            double sp2 = CalculateEnergyFromSpill(
                new SpillEnergy_Heat { energyHeat = housingEnergyForHeat, spillToHeatDistribution = 0.13, spillToFjarrvarmeEfficiency = 0.8 },
                new SpillEnergy_Electricity { spillToElectDistribution = 0.038, spillToElectEfficiency = 0.35 }, electricityIndustry);

            double sp = sp1 + sp2;
        }

        #region Transport Calculations

        private double GetTransportEnergy(double energyTransport, 
                double fuelDistribution, double fuelEfficiencyProduction, double fuelEfficiencyUsage)
        {
            return ((energyTransport * fuelDistribution) / (fuelEfficiencyUsage * fuelEfficiencyProduction));
        }

        private void CalculateTransportEnergyRelativeBaseYear(int year)
        {
            transportEnergyForFossil = GetTransportEnergy(17.0, 0.84, 0.9, 0.2); 
        }

        #endregion

        #region Housing Calculations

        // Calculate Energy delivered from Wind
        private double CalculateEnergyFromWind(WindEnergy_Electricity we, double electricity)
        {
            return (electricity * (we.windToElectDistribution / we.windToElectEfficiency));
        }

        // Calculate energy delivered from Water 
        private double CalculateEnergyFromWater(WaterEnergy_Electricity we, double electricity)
        {
            return (electricity * (we.waterToElectDistribution / we.waterToElectEfficiency));
        }

        // Calculate energy delivered from Nuclear power plants
        private double CalculateEnergyFromNuclearEnergy(NuclearEnergy_Electricity ne, double electricity)
        {
            return (electricity * (ne.nuclearToElectDistribution / ne.nuclearToElectEfficiency));
        }

        // Calculate energy delivred from Spill sources
        private double CalculateEnergyFromSpill(SpillEnergy_Heat sph, SpillEnergy_Electricity spe, double electricity)
        {
            return (
                (sph.energyHeat * sph.fjarrvarmeToHeatDistribution * sph.spillToHeatDistribution) / 
                (sph.spillToFjarrvarmeEfficiency * sph.fjarrvarmeToHeatEfficiency * transmissionLoss)) +
                    (electricity * (spe.spillToElectDistribution / spe.spillToElectEfficiency));
        }
        #endregion

        #region Industry Calculations

        #endregion

        #region Common Methods

        // Används till böstadsberäkningar
        private double CalculateElectricityForHousingAndIndustry(Elect_Heat eH, Elect_HeatPump eHP, double energyElect)
        {
            
            return (((eH.energyHeat * eH.electToHeatDistribution) / eH.electToHeatEfficiency) 
                + ((eHP.energyHeat * eHP.pumpToHeatDistribution * eHP.fjarrVarmeToHeatDistribution) / 
                    (eHP.fjarrVarmeToHeatEfficiency * eHP.heatPumpToHeatEfficiency * transmissionLoss)) 
                + energyElect) / transmissionLoss;
        }

        // Används till industriberäkningar
        private double CalculateElectricityForHousingAndIndustry(Elect_HeatPump eHP, double energyElect)
        {

            return (((eHP.energyHeat * eHP.pumpToHeatDistribution * eHP.fjarrVarmeToHeatDistribution) /
                    (eHP.fjarrVarmeToHeatEfficiency * eHP.heatPumpToHeatEfficiency * transmissionLoss))
                + energyElect) / transmissionLoss;
        }
  
        private double CalculateEnergyFromFossilFuels(Fossil_Heat fv, Fossil_Fjarrvarme fjv, Fossil_Electricity fe, double electricity)
        {

            return ((fv.energyHeat * fv.fossilToHeatDistribution) / fv.fossilToHeatEfficiency) +
                    ((fjv.energyHeat * fjv.fjarrVarmeToHeatDistribution * fjv.fossilToFjarrvarmeDistribution) / (fjv.fjarrvarmeToHeatEfficiency * fjv.fossilToFjarrvarmeEfficiency * transmissionLoss)) +
                    (electricity * (fe.fossilToElectDistribution / fe.fossilToElectEfficiency));
        }

        private double CalculateEnergyFromBioFuels(BioFuel_Heat bv, BioFuel_Fjarrvarme fjv, BioFuel_Electricity be, double electricity)
        {

            return ((bv.energyHeat * bv.BioFuelToHeatDistribution) / bv.BioFuelToHeatEfficiency) +
                    ((fjv.energyHeat * fjv.fjarrVarmeToHeatDistribution * fjv.bioFuelToFjarrvarmeDistribution) / (fjv.fjarrvarmeToHeatEfficiency * fjv.bioFuelToFjarrvarmeEfficiency * transmissionLoss)) +
                    (electricity * (be.bioFuelToElectDistribution / be.bioFuelToElectEfficiency));
        }

        private void InitializeElectricityBaseCalculations()
        {
            // Calculate EL for Housing
            electricityHousing = CalculateElectricityForHousingAndIndustry(
                new Elect_Heat { energyHeat = housingEnergyForHeat, electToHeatDistribution = 0.21, electToHeatEfficiency = 1 },
                new Elect_HeatPump { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, pumpToHeatDistribution = 0.09, fjarrVarmeToHeatEfficiency = 0.9, heatPumpToHeatEfficiency = 2.5 },
                housingEnergyForElectricity);

            // Calculate EL for industriy
            electricityIndustry = CalculateElectricityForHousingAndIndustry(
                new Elect_HeatPump { energyHeat = 121, fjarrVarmeToHeatDistribution = 0.49, pumpToHeatDistribution = 0.22, fjarrVarmeToHeatEfficiency = 0.9, heatPumpToHeatEfficiency = 2.5 },
                industryEnergyForElectricity);
        }


        #endregion

        void cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTransportEnergyRelativeBaseYear(Convert.ToInt32(cboTime.SelectedItem.ToString()));
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {

        }
    }
}
