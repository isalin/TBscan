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
        private Object scannerLock = new Object();
        bool scanning = false;
        private Regex regex;
        private Match match;

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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TBscanner";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(431, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Enter Mob name";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(224, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Play audio notification when starting scan.";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 67);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(234, 17);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Play audio notification when NPC is located.";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            #if DEBUG
                this.richTextBox1.Location = new System.Drawing.Point(6, 91);
                this.richTextBox1.Name = "richTextBox1";
                this.richTextBox1.ReadOnly = true;
                this.richTextBox1.Size = new System.Drawing.Size(431, 290);
                this.richTextBox1.TabIndex = 5;
                this.richTextBox1.Text = "This is a test";
                this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            #endif
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TBscanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            #if DEBUG
            this.Controls.Add(this.richTextBox1);
            #endif
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "TBscanner";
            this.Size = new System.Drawing.Size(686, 384);
            this.Load += new System.EventHandler(this.TBscanner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

#endregion

		private TextBox textBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button button1;
        private RichTextBox richTextBox1;
        private ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
		
#endregion
		public TBscanner()
		{
			InitializeComponent();
		}

		Label lblStatus;	// The status label that appears in ACT's Plugin tab
		string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\PluginSample.config.xml");
		SettingsSerializer xmlSettings;

#region IActPluginV1 Members
		public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
		{
			lblStatus = pluginStatusText;	// Hand the status label's reference to our local var
			pluginScreenSpace.Controls.Add(this);	// Add this UserControl to the tab ACT provides
			this.Dock = DockStyle.Fill;	// Expand the UserControl to fill the tab's client space
			xmlSettings = new SettingsSerializer(this);	// Create a new settings serializer and pass it this instance
			LoadSettings();

			// Create some sort of parsing event handler.  After the "+=" hit TAB twice and the code will be generated for you.
			ActGlobals.oFormActMain.AfterCombatAction += new CombatActionDelegate(oFormActMain_AfterCombatAction);
            ActGlobals.oFormActMain.OnLogLineRead +=new LogLineEventDelegate(OFormActMain_OnLogLineRead);

            lblStatus.Text = "Plugin Enabled\nNot scanning.";
		}

        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            richTextBox1.Text += "\n" + logInfo.logLine;
            match = regex.Match(logInfo.logLine);
            if (match.Success)
            {
                lock (scannerLock)
                {
                    if (scanning)
                    {
                        stopScanning();

                        if (checkBox2.Checked)
                        {
                            SoundPlayer snd = new SoundPlayer(Properties.Resources.found);
                            snd.Play();
                        }
#if DEBUG
                        MessageBox.Show("Line: " + logInfo.logLine);
#endif
                    }
                }
            }
        }

        public void DeInitPlugin()
		{
			// Unsubscribe from any events you listen to when exiting!
			ActGlobals.oFormActMain.AfterCombatAction -= oFormActMain_AfterCombatAction;
			SaveSettings();
			lblStatus.Text = "Plugin Disabled";
		}
#endregion

		void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo)
		{
            if (actionInfo.ToString().Contains("test"))
            {
               MessageBox.Show("AfterCombat");

            }
            throw new NotImplementedException();
		}

		void LoadSettings()
		{
			xmlSettings.AddControlSetting(textBox1.Name, textBox1);

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
			xWriter.WriteStartElement("Config");	// <Config>
			xWriter.WriteStartElement("SettingsSerializer");	// <Config><SettingsSerializer>
			xmlSettings.ExportToXml(xWriter);	// Fill the SettingsSerializer XML
			xWriter.WriteEndElement();	// </SettingsSerializer>
			xWriter.WriteEndElement();	// </Config>
			xWriter.WriteEndDocument();	// Tie up loose ends (shouldn't be any)
			xWriter.Flush();	// Flush the file buffer to disk
			xWriter.Close();
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                Console.Beep();

            }
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
            if(scanning == false) {
                startScanning();
            } else
            {
                stopScanning();
            }
            
        }

        private void stopScanning()
        {
            ActGlobals.oFormActMain.OnLogLineRead -= OFormActMain_OnLogLineRead;
            scanning = false;
            button1.Text = "Scan";
            lblStatus.Text = "Plugin Enabled\nNot scanning.";
        }

        private void startScanning()
        {
            regex = new Regex("^.+03:Added new combatant " + textBox1.Text.Clone() + ".+");
            scanning = true;

            if (checkBox1.Checked)
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.scanning);
                snd.Play();
            }
            

            button1.Text = "Scannig...";
            ActGlobals.oFormActMain.OnLogLineRead += new LogLineEventDelegate(OFormActMain_OnLogLineRead);
            lblStatus.Text = "Plugin Enabled\nScanning for: " + textBox1.Text.Clone();
        }
    }
}
