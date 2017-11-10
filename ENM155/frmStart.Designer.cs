namespace ENM155
{
    partial class frmStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkBio = new System.Windows.Forms.CheckBox();
            this.chkFossila = new System.Windows.Forms.CheckBox();
            this.chkKarn = new System.Windows.Forms.CheckBox();
            this.chkVatten = new System.Windows.Forms.CheckBox();
            this.chkVind = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboTime = new System.Windows.Forms.ComboBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 735);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkBio);
            this.groupBox2.Controls.Add(this.chkFossila);
            this.groupBox2.Controls.Add(this.chkKarn);
            this.groupBox2.Controls.Add(this.chkVatten);
            this.groupBox2.Controls.Add(this.chkVind);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox2.Size = new System.Drawing.Size(355, 259);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Primär Energi Tillförsel";
            // 
            // chkBio
            // 
            this.chkBio.AutoSize = true;
            this.chkBio.Checked = true;
            this.chkBio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBio.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBio.Location = new System.Drawing.Point(27, 221);
            this.chkBio.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkBio.Name = "chkBio";
            this.chkBio.Size = new System.Drawing.Size(136, 26);
            this.chkBio.TabIndex = 5;
            this.chkBio.Text = "Bio Bränslen";
            this.chkBio.UseVisualStyleBackColor = true;
            this.chkBio.CheckedChanged += new System.EventHandler(this.chkBio_CheckedChanged);
            // 
            // chkFossila
            // 
            this.chkFossila.AutoSize = true;
            this.chkFossila.Checked = true;
            this.chkFossila.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFossila.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFossila.Location = new System.Drawing.Point(27, 182);
            this.chkFossila.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkFossila.Name = "chkFossila";
            this.chkFossila.Size = new System.Drawing.Size(163, 26);
            this.chkFossila.TabIndex = 4;
            this.chkFossila.Text = "Fossila Bränslen";
            this.chkFossila.UseVisualStyleBackColor = true;
            this.chkFossila.CheckedChanged += new System.EventHandler(this.chkFossila_CheckedChanged);
            // 
            // chkKarn
            // 
            this.chkKarn.AutoSize = true;
            this.chkKarn.Checked = true;
            this.chkKarn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKarn.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKarn.Location = new System.Drawing.Point(27, 136);
            this.chkKarn.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkKarn.Name = "chkKarn";
            this.chkKarn.Size = new System.Drawing.Size(72, 26);
            this.chkKarn.TabIndex = 2;
            this.chkKarn.Text = "Kärn";
            this.chkKarn.UseVisualStyleBackColor = true;
            this.chkKarn.CheckedChanged += new System.EventHandler(this.chkKarn_CheckedChanged);
            // 
            // chkVatten
            // 
            this.chkVatten.AutoSize = true;
            this.chkVatten.Checked = true;
            this.chkVatten.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVatten.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVatten.Location = new System.Drawing.Point(27, 98);
            this.chkVatten.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkVatten.Name = "chkVatten";
            this.chkVatten.Size = new System.Drawing.Size(83, 26);
            this.chkVatten.TabIndex = 1;
            this.chkVatten.Text = "Vatten";
            this.chkVatten.UseVisualStyleBackColor = true;
            this.chkVatten.CheckedChanged += new System.EventHandler(this.chkVatten_CheckedChanged);
            // 
            // chkVind
            // 
            this.chkVind.AutoSize = true;
            this.chkVind.Checked = true;
            this.chkVind.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVind.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVind.Location = new System.Drawing.Point(27, 58);
            this.chkVind.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkVind.Name = "chkVind";
            this.chkVind.Size = new System.Drawing.Size(70, 26);
            this.chkVind.TabIndex = 0;
            this.chkVind.Text = "Vind";
            this.chkVind.UseVisualStyleBackColor = true;
            this.chkVind.CheckedChanged += new System.EventHandler(this.chkVind_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboTime);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(5, 274);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox3.Size = new System.Drawing.Size(355, 89);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tidsperiod från 2015";
            // 
            // cboTime
            // 
            this.cboTime.FormattingEnabled = true;
            this.cboTime.Location = new System.Drawing.Point(27, 43);
            this.cboTime.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cboTime.Name = "cboTime";
            this.cboTime.Size = new System.Drawing.Size(291, 31);
            this.cboTime.TabIndex = 0;
            this.cboTime.Text = "2015";
            this.cboTime.SelectedIndexChanged += new System.EventHandler(this.cboTime_SelectedIndexChanged);
            // 
            // chart1
            // 
            this.chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            this.chart1.BorderSkin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.chart1.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            this.chart1.Size = new System.Drawing.Size(861, 731);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Cambria", 11F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeight = 24;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.Salmon;
            this.dataGridView1.Location = new System.Drawing.Point(5, 373);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(355, 357);
            this.dataGridView1.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1250, 745);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(880, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.09782F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.90217F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 366F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(365, 735);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1250, 745);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmStart";
            this.Text = "frmStart";
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkKarn;
        private System.Windows.Forms.CheckBox chkVatten;
        private System.Windows.Forms.CheckBox chkVind;
        private System.Windows.Forms.ComboBox cboTime;
        private System.Windows.Forms.CheckBox chkBio;
        private System.Windows.Forms.CheckBox chkFossila;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}