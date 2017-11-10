using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
        double electricityKeeper;

        double we = 0;

        private static double transmissionLoss = 0.9;

        IDictionary<int, double[]> growingValues;

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
            public double spillToFjarrvarmeDistribution;
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
            growingValues = new Dictionary<int, double[]>();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            //Load all years from now until 2050
            for (int i = 1; i <= 35; i++)
            {
                int formulatedYear = baseYear + i;
                cboTime.Items.Add(formulatedYear.ToString());
            }

            growingValues = CalculateAllEnergiesPerPeriod();

            LoadGraph();

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
                (sph.energyHeat * sph.fjarrvarmeToHeatDistribution * sph.spillToFjarrvarmeDistribution) /
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
                new Elect_HeatPump { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, pumpToHeatDistribution = 0.09, fjarrVarmeToHeatEfficiency = 0.9, heatPumpToHeatEfficiency = 3 },
                housingEnergyForElectricity);

            electricityKeeper = electricityHousing;

            // Calculate EL for industriy
            electricityIndustry = CalculateElectricityForHousingAndIndustry(
                new Elect_HeatPump { energyHeat = industryEnergyForHeat, fjarrVarmeToHeatDistribution = 0.04, pumpToHeatDistribution = 0.09, fjarrVarmeToHeatEfficiency = 0.9, heatPumpToHeatEfficiency = 3 },
                industryEnergyForElectricity);
        }

        private double[] CalculateAllEnergies(int year)
        {
            //------------------------------------------FOSSIL FUELS----------------------------------------------------------------------

            // Calculate Energy supplied to housing from fossil fuels
            double k1 = CalculateEnergyFromFossilFuels(
                new Fossil_Heat { energyHeat = housingEnergyForHeat, fossilToHeatDistribution = 0.15, fossilToHeatEfficiency = 0.8 },
                new Fossil_Fjarrvarme { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, fossilToFjarrvarmeDistribution = 0.2, fjarrvarmeToHeatEfficiency = 0.9, fossilToFjarrvarmeEfficiency = 0.85 },
                new Fossil_Electricity { fossilToElectDistribution = 0.01, fossilToElectEfficiency = 0.35 }, electricityHousing);

            // Calculate Energy supplied to industries from fossil fuels
            double k2 = CalculateEnergyFromFossilFuels(
                new Fossil_Heat { energyHeat = industryEnergyForHeat, fossilToHeatDistribution = 0.34, fossilToHeatEfficiency = 0.9 },
                new Fossil_Fjarrvarme { energyHeat = industryEnergyForHeat, fjarrVarmeToHeatDistribution = 0.04, fossilToFjarrvarmeDistribution = 0.2, fjarrvarmeToHeatEfficiency = 0.9, fossilToFjarrvarmeEfficiency = 0.85 },
                new Fossil_Electricity { fossilToElectDistribution = 0.01, fossilToElectEfficiency = 0.35 }, electricityIndustry);

            // calculate Energy supplied to transport from fossil fuels
            double k3 = GetTransportEnergy(transportEnergyForFossil, 0.84, 0.9, 0.2);

            double k = k1 + k2 + k3;

            //------------------------------------------BIO FUEL----------------------------------------------------------------------

            // Calculate Energy supplied to housing from bio fuels
            double b1 = CalculateEnergyFromBioFuels(
                new BioFuel_Heat { energyHeat = housingEnergyForHeat, BioFuelToHeatDistribution = 0.15, BioFuelToHeatEfficiency = 0.8 },
                new BioFuel_Fjarrvarme { energyHeat = housingEnergyForHeat, fjarrVarmeToHeatDistribution = 0.49, bioFuelToFjarrvarmeDistribution = 0.63, fjarrvarmeToHeatEfficiency = 0.9, bioFuelToFjarrvarmeEfficiency = 0.9 },
                new BioFuel_Electricity { bioFuelToElectDistribution = 0.04, bioFuelToElectEfficiency = 0.35 }, electricityHousing);

            // Calculate Energy supplied to industries from bio fuels
            double b2 = CalculateEnergyFromBioFuels(
                new BioFuel_Heat { energyHeat = industryEnergyForHeat, BioFuelToHeatDistribution = 0.62, BioFuelToHeatEfficiency = 0.9 },
                new BioFuel_Fjarrvarme { energyHeat = industryEnergyForHeat, fjarrVarmeToHeatDistribution = 0.04, bioFuelToFjarrvarmeDistribution = 0.63, fjarrvarmeToHeatEfficiency = 0.9, bioFuelToFjarrvarmeEfficiency = 0.9 },
                new BioFuel_Electricity { bioFuelToElectDistribution = 0.04, bioFuelToElectEfficiency = 0.35 }, electricityIndustry);

            // calculate Energy supplied to transport from bio fuels
            double b3 = GetTransportEnergy(transportEnergyForFossil, 0.16, 0.5, 0.2);

            double b = b1 + b2 + b3;

            //------------------------------------------WIND ENERGY----------------------------------------------------------------------

            if (year <= 2036) { 

                // Calculate Energy supplied to housing from Wind Energy
                double we1 = CalculateEnergyFromWind(new WindEnergy_Electricity { windToElectDistribution = 0.1, windToElectEfficiency = 1 }, electricityHousing);

                // Calculate Energy supplied to industries from Wind Energy
                double we2 = CalculateEnergyFromWind(new WindEnergy_Electricity { windToElectDistribution = 0.1, windToElectEfficiency = 1 }, electricityIndustry);

                we = we1 + we2;
            }
            //------------------------------------------WATER ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from Water Energy
            double wa1 = CalculateEnergyFromWater(new WaterEnergy_Electricity { waterToElectDistribution = 0.47, waterToElectEfficiency = 1 }, electricityHousing);

            // Calculate Energy supplied to industries from Water Energy
            double wa2 = CalculateEnergyFromWater(new WaterEnergy_Electricity { waterToElectDistribution = 0.47, waterToElectEfficiency = 1 }, electricityIndustry);

            double wa = wa1 + wa2;

            //------------------------------------------NUCLEAR ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from Nuclear Energy
            double ne1 = CalculateEnergyFromNuclearEnergy(new NuclearEnergy_Electricity { nuclearToElectDistribution = 0.34, nuclearToElectEfficiency = 0.34 }, electricityHousing);

            // Calculate Energy supplied to industries from Nucear Energy
            double ne2 = CalculateEnergyFromNuclearEnergy(new NuclearEnergy_Electricity { nuclearToElectDistribution = 0.34, nuclearToElectEfficiency = 0.34 }, electricityIndustry);

            double ne = ne1 + ne2;

            //------------------------------------------SPILL ENERGY----------------------------------------------------------------------

            // Calculate Energy supplied to housing from spill
            double sp1 = CalculateEnergyFromSpill(
                new SpillEnergy_Heat { energyHeat = housingEnergyForHeat, spillToFjarrvarmeDistribution = 0.13, spillToFjarrvarmeEfficiency = 0.8, fjarrvarmeToHeatDistribution = 0.49, fjarrvarmeToHeatEfficiency = 0.9 },
                new SpillEnergy_Electricity { spillToElectDistribution = 0.038, spillToElectEfficiency = 0.35 }, electricityHousing);

            // Calculate Energy supplied to industry from spill
            double sp2 = CalculateEnergyFromSpill(
                new SpillEnergy_Heat { energyHeat = industryEnergyForHeat, spillToFjarrvarmeDistribution = 0.13, spillToFjarrvarmeEfficiency = 0.8, fjarrvarmeToHeatDistribution = 0.04, fjarrvarmeToHeatEfficiency = 0.9 },
                new SpillEnergy_Electricity { spillToElectDistribution = 0.038, spillToElectEfficiency = 0.35 }, electricityIndustry);

            double sp = sp1 + sp2;

            return new double[] { Math.Round(k, 2), Math.Round(b, 2), Math.Round(we, 2), Math.Round(wa, 2), Math.Round(ne, 2), Math.Round(sp, 2) };
        }

        private IDictionary<int, double[]> CalculateAllEnergiesPerPeriod()
        {
            for (int year = 2015; year <= 2050; year++)
            { 
                growingValues.Add(year, CalculateAllEnergies(year));

                transportEnergyForFossil *= 1.005;
                housingEnergyForHeat *= 1.005;
                housingEnergyForElectricity *= 1.005;

                industryEnergyForHeat *= 1.005;
                industryEnergyForElectricity *= 1.005;

                InitializeElectricityBaseCalculations();
            }

            return growingValues;
        }
       
        private void LoadGraph()
        {
            ChartArea chartArea1 = new ChartArea();
            chart1.Series.Clear();
            Legend legend1 = new Legend();

            this.chart1.BackColor = Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            this.chart1.BackGradientStyle = GradientStyle.TopBottom;
            this.chart1.BackSecondaryColor = Color.White;
            this.chart1.BorderlineColor = Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            this.chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            #region ChartArea Properties

            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 38;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.LightStyle = LightStyle.Realistic;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.PointDepth = 200;
            chartArea1.Area3DStyle.Rotation = 9;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.LabelStyle.Font = new Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Cambria", 8.25F, FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(165)))), ((int)(((byte)(191)))), ((int)(((byte)(228)))));
            chartArea1.BackGradientStyle = GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = Color.White;
            chartArea1.BorderColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = ChartDashStyle.Solid;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            #endregion


            // FOSSIL FUELS
            if (chkFossila.Checked)
            {
                Series fossils = new Series();
                fossils.ChartType = SeriesChartType.StepLine;
                fossils.Points.DataBindXY(growingValues.Keys.ToArray<int>(), growingValues.Values.Select(w => w.GetValue(0)).ToArray());
                fossils.Color = Color.Blue;
                fossils.BorderWidth = 2;
                fossils.Name = "Fossila Bränslen";
                chart1.Series.Add(fossils);
            }

            // BIO FUELS
            if (chkBio.Checked)
            {
                Series bio = new Series();
                bio.ChartType = SeriesChartType.Spline;
                bio.Points.DataBindXY(growingValues.Keys.ToArray<int>(), growingValues.Values.Select(w => w.GetValue(1)).ToArray());
                bio.Color = Color.Red;
                bio.BorderWidth = 2;
                bio.Name = "Bio Bränslen";
                chart1.Series.Add(bio);
            }

            // WIND ENERGY
            if (chkVind.Checked)
            {
                Series wind = new Series();
                wind.ChartType = SeriesChartType.Spline;
                wind.Points.DataBindXY(growingValues.Keys.ToArray<int>(), growingValues.Values.Select(w => w.GetValue(2)).ToArray());
                wind.Color = Color.Green;
                wind.BorderWidth = 2;
                wind.Name = "Vindkraft";
                chart1.Series.Add(wind);
            }


            // WATER ENERGY
            if (chkVatten.Checked)
            {
                Series vatten = new Series();
                vatten.ChartType = SeriesChartType.Spline;
                vatten.Points.DataBindXY(growingValues.Keys.ToArray<int>(), growingValues.Values.Select(w => w.GetValue(3)).ToArray());
                vatten.Color = Color.Black;
                vatten.BorderWidth = 2;
                vatten.Name = "Vattenkraft";
                chart1.Series.Add(vatten);
            }

            // NUCLEAR ENERGY
            if (chkKarn.Checked)
            {
                Series nuclear = new Series();
                nuclear.ChartType = SeriesChartType.Spline;
                nuclear.Points.DataBindXY(growingValues.Keys.ToArray<int>(), growingValues.Values.Select(w => w.GetValue(4)).ToArray());
                nuclear.Color = Color.Brown;
                nuclear.BorderWidth = 2;
                nuclear.Name = "Kärnkraft";
                chart1.Series.Add(nuclear);
            }
            
            ReloadDataGridView(2050);
        }

        private void ReloadDataGridView(int endYear)
        {
            DataTable dt = new DataTable();

            var keyValuePair = growingValues.Single(x => x.Key == 2015);
            double[] value = keyValuePair.Value;

            DataRow row = dt.NewRow();

            dt.Columns.Add("År", typeof(int));
            dt.Columns.Add("Fossil", typeof(double));
            dt.Columns.Add("Bio", typeof(double));
            dt.Columns.Add("Vind", typeof(double));
            dt.Columns.Add("Vatten", typeof(double));
            dt.Columns.Add("Kärn", typeof(double));

            dt.Rows.Add(new object[] { 2015, value[0], value[1], value[2], value[3], value[4] });

            keyValuePair = growingValues.Single(x => x.Key == endYear);
            value = keyValuePair.Value;

            dt.Rows.Add(new object[] { endYear, value[0], value[1], value[2], value[3], value[4] });

            var pivotedDataTable = new DataTable(); //the pivoted result
            var firstColumnName = "Energikällor";
            var pivotColumnName = "År";

            pivotedDataTable.Columns.Add(firstColumnName);

            pivotedDataTable.Columns.AddRange(
                dt.Rows.Cast<DataRow>().Select(x => new DataColumn(x[pivotColumnName].ToString())).ToArray());

            for (var index = 1; index < dt.Columns.Count; index++)
            {
                pivotedDataTable.Rows.Add(
                    new List<object> { dt.Columns[index].ColumnName }.Concat(
                        dt.Rows.Cast<DataRow>().Select(x => x[dt.Columns[index].ColumnName])).ToArray());
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.DataSource = pivotedDataTable;
            dataGridView1.EnableHeadersVisualStyles = false;

            DataGridViewCellStyle style;
            style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.BottomCenter;
            style.BackColor = Color.Navy;
            style.Font = new Font("Cambria", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            style.ForeColor = Color.White;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = Color.Navy;
            style.WrapMode = DataGridViewTriState.True;

            foreach (DataGridViewColumn col in dataGridView1.Columns) col.HeaderCell.Style = style;
        }
        #endregion

        void cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            ReloadDataGridView(Convert.ToInt32(cboTime.SelectedItem.ToString()));
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {

        }

        private void chkFossila_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void chkVind_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void chkVatten_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void chkKarn_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void chkBio_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraph();
        }
    }
}
