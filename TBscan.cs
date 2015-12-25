using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Text.RegularExpressions;
using System.Media;

[assembly: AssemblyTitle("TBscanner")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyCompany("TiaBot")]
[assembly: AssemblyCopyright("Copyright (c) 2015, Tialyth")]
[assembly: AssemblyDescription("A plugin that lets you scan for nearby NPC.")]



namespace TBscan
{

    public class TBscanner : UserControl, IActPluginV1
    {
        //ARR hunt marks
        string[] hunts_arr_srank = { "Agrippa The Mighty", "Bonnacon", "Brontes", "Croakadile", "Croque-Mitaine", "Garlok", "Laideronnette", "Lampalagua", "Mahisha", "Mindflayer", "Minhocao", "Nandi", "Nunyunuwi", "Safat", "Thousand-cast Theda", "Wulgaru", "Zona Seeker" };
        string[] hunts_arr_arank = { "Alectryon", "Cornu", "Dalvag's Final Flame", "Forneus", "Ghede Ti Malice", "Girtab", "Hellsclaw", "Kurrea", "Maahes", "Marberry", "Marraco", "Melt", "Nahn", "Unktehi", "Vogaal Ja", "Sabotender Bailarina", "Zanig'oh" };
        string[] hunts_arr_brank = { "Albin the Ashen", "Barbastelle", "Bloody Mary", "Dark Helmet", "Flame Sergeant Dalvag", "Gatling", "Leech King", "Monarch Ogrefly", "Myradrosh", "Naul", "Ovjang", "Phecda", "Sewer Syrup", "Skogs Fru", "Stinging Sophie", "Vuokho", "White Joker" };

        //HW hunt marks
        string[] hunts_hw_brank = { "Alteci", "Kreutzet", "Gnath Cometdrone", "Thextera", "Pterygotus", "Gigantopithecus", "Scitalis", "The Scarecrow", "Squonk", "Sanu Vali of Dancing Wings", "Lycidas", "Omni" };
        string[] hunts_hw_arank = { "Mirka", "Lyuba", "Pylraster", "Lord of the Wyverns", "Slipkinx Steeljoints", "Stolas", "Bune", "Agathos", "Enkelados", "Sisiutl", "Campacti", "Stench Blossom" };
        string[] hunts_hw_srank = { "Kaiser Behemoth", "Senmurv", "The Pale Rider", "Gandarewa", "Bird of Paradise", "Leucrotta" };

        private Object scannerLock = new Object();
        bool scanning = false;
        private Regex scanner_regex;
        private Match match;

        private GroupBox groupBox1;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckBox checkBox_HW_audio_Arank;
        private CheckBox checkBox_HW_audio_Srank;
        private CheckBox checkBox_HW_audio_Brank;
        private CheckBox checkBox_audio_cancelled;
        private GroupBox groupBox4;
        private CheckBox checkBox_ARR_audio_Brank;
        private CheckBox checkBox_ARR_audio_Arank;
        private CheckBox checkBox_ARR_audio_Srank;

        #region Designer Created Code (Avoid editing)
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify k
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_targetName = new System.Windows.Forms.TextBox();
            this.checkBox_audio_start = new System.Windows.Forms.CheckBox();
            this.checkBox_audio_located = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox_ARR_audio_Brank = new System.Windows.Forms.CheckBox();
            this.checkBox_ARR_audio_Arank = new System.Windows.Forms.CheckBox();
            this.checkBox_ARR_audio_Srank = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_HW_audio_Arank = new System.Windows.Forms.CheckBox();
            this.checkBox_HW_audio_Srank = new System.Windows.Forms.CheckBox();
            this.checkBox_HW_audio_Brank = new System.Windows.Forms.CheckBox();
            this.checkBox_audio_cancelled = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_targetName
            // 
            this.textBox_targetName.Location = new System.Drawing.Point(6, 20);
            this.textBox_targetName.Name = "textBox_targetName";
            this.textBox_targetName.Size = new System.Drawing.Size(419, 20);
            this.textBox_targetName.TabIndex = 1;
            this.textBox_targetName.Text = "Enter Mob name";
            this.textBox_targetName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox_targetName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_targetName_KeyDown);
            // 
            // checkBox_audio_start
            // 
            this.checkBox_audio_start.AutoSize = true;
            this.checkBox_audio_start.Checked = true;
            this.checkBox_audio_start.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_audio_start.Location = new System.Drawing.Point(6, 19);
            this.checkBox_audio_start.Name = "checkBox_audio_start";
            this.checkBox_audio_start.Size = new System.Drawing.Size(224, 17);
            this.checkBox_audio_start.TabIndex = 2;
            this.checkBox_audio_start.Text = "Play audio notification when starting scan.";
            this.checkBox_audio_start.UseVisualStyleBackColor = true;
            this.checkBox_audio_start.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_audio_located
            // 
            this.checkBox_audio_located.AutoSize = true;
            this.checkBox_audio_located.Checked = true;
            this.checkBox_audio_located.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_audio_located.Location = new System.Drawing.Point(6, 43);
            this.checkBox_audio_located.Name = "checkBox_audio_located";
            this.checkBox_audio_located.Size = new System.Drawing.Size(234, 17);
            this.checkBox_audio_located.TabIndex = 3;
            this.checkBox_audio_located.Text = "Play audio notification when NPC is located.";
            this.checkBox_audio_located.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(431, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_targetName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TBscan";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(78, 17);
            this.toolStripStatusLabel1.Text = "Not scanning";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.checkBox_audio_cancelled);
            this.groupBox2.Controls.Add(this.checkBox_audio_located);
            this.groupBox2.Controls.Add(this.checkBox_audio_start);
            this.groupBox2.Location = new System.Drawing.Point(3, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 232);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Brank);
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Arank);
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Srank);
            this.groupBox4.Location = new System.Drawing.Point(306, 118);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "A Realm Reborn";
            // 
            // checkBox_ARR_audio_Brank
            // 
            this.checkBox_ARR_audio_Brank.AutoSize = true;
            this.checkBox_ARR_audio_Brank.Location = new System.Drawing.Point(6, 68);
            this.checkBox_ARR_audio_Brank.Name = "checkBox_ARR_audio_Brank";
            this.checkBox_ARR_audio_Brank.Size = new System.Drawing.Size(166, 17);
            this.checkBox_ARR_audio_Brank.TabIndex = 2;
            this.checkBox_ARR_audio_Brank.Text = "Notify when B-Rank is nearby";
            this.checkBox_ARR_audio_Brank.UseVisualStyleBackColor = true;
            // 
            // checkBox_ARR_audio_Arank
            // 
            this.checkBox_ARR_audio_Arank.AutoSize = true;
            this.checkBox_ARR_audio_Arank.Checked = true;
            this.checkBox_ARR_audio_Arank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ARR_audio_Arank.Location = new System.Drawing.Point(6, 44);
            this.checkBox_ARR_audio_Arank.Name = "checkBox_ARR_audio_Arank";
            this.checkBox_ARR_audio_Arank.Size = new System.Drawing.Size(166, 17);
            this.checkBox_ARR_audio_Arank.TabIndex = 1;
            this.checkBox_ARR_audio_Arank.Text = "Notify when A-Rank is nearby";
            this.checkBox_ARR_audio_Arank.UseVisualStyleBackColor = true;
            // 
            // checkBox_ARR_audio_Srank
            // 
            this.checkBox_ARR_audio_Srank.AutoSize = true;
            this.checkBox_ARR_audio_Srank.Checked = true;
            this.checkBox_ARR_audio_Srank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ARR_audio_Srank.Location = new System.Drawing.Point(6, 20);
            this.checkBox_ARR_audio_Srank.Name = "checkBox_ARR_audio_Srank";
            this.checkBox_ARR_audio_Srank.Size = new System.Drawing.Size(166, 17);
            this.checkBox_ARR_audio_Srank.TabIndex = 0;
            this.checkBox_ARR_audio_Srank.Text = "Notify when S-Rank is nearby";
            this.checkBox_ARR_audio_Srank.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_HW_audio_Arank);
            this.groupBox3.Controls.Add(this.checkBox_HW_audio_Srank);
            this.groupBox3.Controls.Add(this.checkBox_HW_audio_Brank);
            this.groupBox3.Location = new System.Drawing.Point(306, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 93);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Heavensward";
            // 
            // checkBox_HW_audio_Arank
            // 
            this.checkBox_HW_audio_Arank.AutoSize = true;
            this.checkBox_HW_audio_Arank.Checked = true;
            this.checkBox_HW_audio_Arank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_HW_audio_Arank.Location = new System.Drawing.Point(6, 42);
            this.checkBox_HW_audio_Arank.Name = "checkBox_HW_audio_Arank";
            this.checkBox_HW_audio_Arank.Size = new System.Drawing.Size(161, 17);
            this.checkBox_HW_audio_Arank.TabIndex = 6;
            this.checkBox_HW_audio_Arank.Text = "Notify when A-rank is nearby";
            this.checkBox_HW_audio_Arank.UseVisualStyleBackColor = true;
            // 
            // checkBox_HW_audio_Srank
            // 
            this.checkBox_HW_audio_Srank.AutoSize = true;
            this.checkBox_HW_audio_Srank.Checked = true;
            this.checkBox_HW_audio_Srank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_HW_audio_Srank.Location = new System.Drawing.Point(6, 19);
            this.checkBox_HW_audio_Srank.Name = "checkBox_HW_audio_Srank";
            this.checkBox_HW_audio_Srank.Size = new System.Drawing.Size(161, 17);
            this.checkBox_HW_audio_Srank.TabIndex = 7;
            this.checkBox_HW_audio_Srank.Text = "Notify when S-rank is nearby";
            this.checkBox_HW_audio_Srank.UseVisualStyleBackColor = true;
            // 
            // checkBox_HW_audio_Brank
            // 
            this.checkBox_HW_audio_Brank.AutoSize = true;
            this.checkBox_HW_audio_Brank.Location = new System.Drawing.Point(6, 65);
            this.checkBox_HW_audio_Brank.Name = "checkBox_HW_audio_Brank";
            this.checkBox_HW_audio_Brank.Size = new System.Drawing.Size(161, 17);
            this.checkBox_HW_audio_Brank.TabIndex = 5;
            this.checkBox_HW_audio_Brank.Text = "Notify when B-rank is nearby";
            this.checkBox_HW_audio_Brank.UseVisualStyleBackColor = true;
            // 
            // checkBox_audio_cancelled
            // 
            this.checkBox_audio_cancelled.AutoSize = true;
            this.checkBox_audio_cancelled.Checked = true;
            this.checkBox_audio_cancelled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_audio_cancelled.Location = new System.Drawing.Point(6, 67);
            this.checkBox_audio_cancelled.Name = "checkBox_audio_cancelled";
            this.checkBox_audio_cancelled.Size = new System.Drawing.Size(246, 17);
            this.checkBox_audio_cancelled.TabIndex = 4;
            this.checkBox_audio_cancelled.Text = "Play audio notification when scan is cancelled.";
            this.checkBox_audio_cancelled.UseVisualStyleBackColor = true;
            // 
            // TBscanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "TBscanner";
            this.Size = new System.Drawing.Size(631, 387);
            this.Load += new System.EventHandler(this.TBscanner_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox_targetName_KeyDown(object sender, KeyEventArgs e)
        {
            if (!scanning && e.KeyCode == Keys.Enter)
            {
                startScanning(textBox_targetName.Text.ToString());
            }
        }

        #endregion

        private TextBox textBox_targetName;
        private CheckBox checkBox_audio_start;
        private CheckBox checkBox_audio_located;
        private Button button1;
        private RichTextBox richTextBox1;
        private ContextMenuStrip contextMenuStrip1;

        #endregion
        public TBscanner()
        {
            InitializeComponent();
        }

        Label lblStatus;    // The status label that appears in ACT's Plugin tab
        string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\TBscan.config.xml");
        SettingsSerializer xmlSettings;

        #region IActPluginV1 Members
        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            lblStatus = pluginStatusText;   // Hand the status label's reference to our local var
            pluginScreenSpace.Controls.Add(this);   // Add this UserControl to the tab ACT provides
            this.Dock = DockStyle.Fill; // Expand the UserControl to fill the tab's client space
            xmlSettings = new SettingsSerializer(this); // Create a new settings serializer and pass it this instance
            LoadSettings();

            // Create some sort of parsing event handler.  After the "+=" hit TAB twice and the code will be generated for you.
            ActGlobals.oFormActMain.OnLogLineRead += new LogLineEventDelegate(OFormActMain_OnLogLineRead);

            lblStatus.Text = "Plugin Enabled\nNot scanning.";
        }

        

        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            

            //Scan target
            if (scanning)
            {
                match = scanner_regex.Match(logInfo.logLine);
                if (match.Success)
                {
                    lock (scannerLock)
                    {
                        if (scanning)
                        {
                            stopScanning();

                            if (checkBox_audio_located.Checked)
                            {
                                SoundPlayer snd = new SoundPlayer(Properties.Resources.found);
                                snd.Play();
                            }
#if DEBUG
                            MessageBox.Show("Line: " + logInfo.logLine);
#endif
                        }
                    }
                    return;
                }
            }

            //ARR hunts
            if (checkBox_ARR_audio_Srank.Checked)
            {
                for(int i = 0; i < hunts_arr_srank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_arr_srank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.srank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_ARR_audio_Arank.Checked)
            {
                for (int i = 0; i < hunts_arr_arank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_arr_arank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.arank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_ARR_audio_Brank.Checked)
            {
                for (int i = 0; i < hunts_arr_brank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_arr_brank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.brank);
                        snd.Play();
                        return;
                    }
                }
            }


            //HW hunts
            if (checkBox_HW_audio_Srank.Checked)
            {
                for (int i = 0; i < hunts_hw_srank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_hw_srank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.srank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_HW_audio_Arank.Checked)
            {
                for (int i = 0; i < hunts_hw_arank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_hw_arank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.arank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_HW_audio_Brank.Checked)
            {
                for (int i = 0; i < hunts_hw_brank.Length; i++)
                {
                    match = new Regex("^.+03:Added new combatant " + hunts_hw_brank[i] + ".+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.brank);
                        snd.Play();
                        return;
                    }
                }
            }

        }
        public void DeInitPlugin()
        {
            // Unsubscribe from any events you listen to when exiting!
            ActGlobals.oFormActMain.OnLogLineRead += OFormActMain_OnLogLineRead;

            SaveSettings();
            lblStatus.Text = "Plugin Disabled";
        }
        #endregion

        

        void LoadSettings()
        {
            xmlSettings.AddControlSetting(textBox_targetName.Name, textBox_targetName);

            xmlSettings.AddControlSetting(checkBox_audio_start.Name, checkBox_audio_start);
            xmlSettings.AddControlSetting(checkBox_audio_located.Name, checkBox_audio_located);
            xmlSettings.AddControlSetting(checkBox_audio_cancelled.Name, checkBox_audio_cancelled);

            xmlSettings.AddControlSetting(checkBox_ARR_audio_Brank.Name, checkBox_ARR_audio_Brank);
            xmlSettings.AddControlSetting(checkBox_ARR_audio_Arank.Name, checkBox_ARR_audio_Arank);
            xmlSettings.AddControlSetting(checkBox_ARR_audio_Srank.Name, checkBox_ARR_audio_Srank);

            xmlSettings.AddControlSetting(checkBox_HW_audio_Brank.Name, checkBox_HW_audio_Brank);
            xmlSettings.AddControlSetting(checkBox_HW_audio_Arank.Name, checkBox_HW_audio_Arank);
            xmlSettings.AddControlSetting(checkBox_HW_audio_Srank.Name, checkBox_HW_audio_Srank);


            if (File.Exists(settingsFile))
            {
                FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlTextReader xReader = new XmlTextReader(fs);

                try
                {
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Element)
                        {
                            if (xReader.LocalName == "SettingsSerializer")
                            {
                                xmlSettings.ImportFromXml(xReader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error loading settings: " + ex.Message;
                }
                xReader.Close();
            }
        }
        void SaveSettings()
        {
            FileStream fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 1;
            xWriter.IndentChar = '\t';
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config");    // <Config>
            xWriter.WriteStartElement("SettingsSerializer");    // <Config><SettingsSerializer>
            xmlSettings.ExportToXml(xWriter);   // Fill the SettingsSerializer XML
            xWriter.WriteEndElement();  // </SettingsSerializer>
            xWriter.WriteEndElement();  // </Config>
            xWriter.WriteEndDocument(); // Tie up loose ends (shouldn't be any)
            xWriter.Flush();    // Flush the file buffer to disk
            xWriter.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBscanner_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (scanning == false)
            {
                if (textBox_targetName.Text.ToString().Equals(""))
                {
                    SoundPlayer snd = new SoundPlayer(Properties.Resources.errorPleaseEnterTheNameOfAMob);
                    snd.Play();
                    return;
                }
                startScanning(textBox_targetName.Text.ToString());
            }
            else
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.cancelled);
                snd.Play();
                stopScanning();
            }

        }

        private void stopScanning()
        {
            scanning = false;
            textBox_targetName.ReadOnly = false;

            button1.Text = "Scan";
            lblStatus.Text = "Plugin Enabled\nNot scanning.";
        }

        private void startScanning(string target)
        {
            scanner_regex = new Regex("^.+03:Added new combatant " + target + ".+");
            textBox_targetName.ReadOnly = true;

            if (checkBox_audio_start.Checked)
            {
                if (!scanning)
                {
                    SoundPlayer snd = new SoundPlayer(Properties.Resources.scanning);
                    snd.Play();
                } else
                {
                    SoundPlayer snd = new SoundPlayer(Properties.Resources.targetUpdated);
                    snd.Play();
                }
                

            }

            scanning = true;
            button1.Text = "Scannig...";
            lblStatus.Text = "Plugin Enabled\nScanning for: " + textBox_targetName.Text.Clone();
        }
    }
}
