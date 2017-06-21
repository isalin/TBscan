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
[assembly: AssemblyVersion("1.0.0.3")]
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
        string[] hunts_arr_brank = { "Albin the Ashen", "Albin The Ashen", "Barbastelle", "Bloody Mary", "Dark Helmet", "Flame Sergeant Dalvag", "Gatling", "Leech King", "Monarch Ogrefly", "Myradrosh", "Naul", "Ovjang", "Phecda", "Sewer Syrup", "Skogs Fru", "Stinging Sophie", "Vuokho", "White Joker" };

        //HW hunt marks
        string[] hunts_hw_brank = { "Alteci", "Kreutzet", "Gnath Cometdrone", "Thextera", "Pterygotus", "Gigantopithecus", "Scitalis", "The Scarecrow", "Squonk", "Sanu Vali Of Dancing Wings", "Lycidas", "Omni" };
        string[] hunts_hw_arank = { "Mirka", "Lyuba", "Pylraster", "Lord of the Wyverns", "Lord Of The Wyverns", "Slipkinx Steeljoints", "Stolas", "Bune", "Agathos", "Enkelados", "Sisiutl", "Campacti", "Stench Blossom" };
        string[] hunts_hw_srank = { "Kaiser Behemoth", "Senmurv", "The Pale Rider", "Gandarewa", "Bird of Paradise", "Leucrotta" };

        //SB hunt marks
        string[] hunts_sb_brank = { "Gwas-y-neidr", "Gwas-y-Neidr", "Gwas-Y-Neidr", "Buccaboo", "Shadow-dweller Yamini", "Shadow-Dweller Yamini", "Ouzelum", "Gauki Strongblade", "Guhuo Niao", "Deidar", "Gyorai Quickstrike", "Kurma", "Aswang", "Manes", "Kiwa" };
        string[] hunts_sb_arank = { "Vochstein", "Aqrabuamelu", "Orcus", "Erle", "Funa Yurei", "Oni Yumemi", "Gajasura", "Angada", "Girimekhala", "Sum", "Mahisha", "Luminare" };
        string[] hunts_sb_srank = { "Okina", "Gamma", "Orghana", "Salt and Light" };

        private Object scannerLock = new Object();
        bool scanning = false;
        private Regex scanner_regex;
        private Match match;

        private GroupBox groupBox1;
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
        private ToolStripStatusLabel toolStripStatusLabel1;
        private StatusStrip statusStrip1;
        private CheckBox checkBox_case_sensitive_search;
        private CheckBox checkBox_only_search_for_npc;
        private GroupBox groupBox_details;
        private RichTextBox textBox_details;
        private CheckBox checkBox_display_infoBox;
        private GroupBox groupBox5;
        private CheckBox checkBox_SB_audio_Brank;
        private CheckBox checkBox_SB_audio_Arank;
        private CheckBox checkBox_SB_audio_Srank;

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_display_infoBox = new System.Windows.Forms.CheckBox();
            this.checkBox_only_search_for_npc = new System.Windows.Forms.CheckBox();
            this.checkBox_case_sensitive_search = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox_ARR_audio_Brank = new System.Windows.Forms.CheckBox();
            this.checkBox_ARR_audio_Arank = new System.Windows.Forms.CheckBox();
            this.checkBox_ARR_audio_Srank = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_HW_audio_Arank = new System.Windows.Forms.CheckBox();
            this.checkBox_HW_audio_Srank = new System.Windows.Forms.CheckBox();
            this.checkBox_HW_audio_Brank = new System.Windows.Forms.CheckBox();
            this.checkBox_audio_cancelled = new System.Windows.Forms.CheckBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox_details = new System.Windows.Forms.GroupBox();
            this.textBox_details = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_SB_audio_Srank = new System.Windows.Forms.CheckBox();
            this.checkBox_SB_audio_Arank = new System.Windows.Forms.CheckBox();
            this.checkBox_SB_audio_Brank = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox_details.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.checkBox_audio_start.Location = new System.Drawing.Point(6, 66);
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
            this.checkBox_audio_located.Location = new System.Drawing.Point(6, 89);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.checkBox_display_infoBox);
            this.groupBox2.Controls.Add(this.checkBox_only_search_for_npc);
            this.groupBox2.Controls.Add(this.checkBox_case_sensitive_search);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.checkBox_audio_cancelled);
            this.groupBox2.Controls.Add(this.checkBox_audio_located);
            this.groupBox2.Controls.Add(this.checkBox_audio_start);
            this.groupBox2.Location = new System.Drawing.Point(3, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 332);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // checkBox_display_infoBox
            // 
            this.checkBox_display_infoBox.AutoSize = true;
            this.checkBox_display_infoBox.Checked = true;
            this.checkBox_display_infoBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_display_infoBox.Location = new System.Drawing.Point(6, 309);
            this.checkBox_display_infoBox.Name = "checkBox_display_infoBox";
            this.checkBox_display_infoBox.Size = new System.Drawing.Size(185, 17);
            this.checkBox_display_infoBox.TabIndex = 13;
            this.checkBox_display_infoBox.Text = "Display target details when found.";
            this.checkBox_display_infoBox.UseVisualStyleBackColor = true;
            this.checkBox_display_infoBox.CheckedChanged += new System.EventHandler(this.checkBox_display_infoBox_CheckedChanged);
            // 
            // checkBox_only_search_for_npc
            // 
            this.checkBox_only_search_for_npc.AutoSize = true;
            this.checkBox_only_search_for_npc.Checked = true;
            this.checkBox_only_search_for_npc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_only_search_for_npc.Location = new System.Drawing.Point(6, 43);
            this.checkBox_only_search_for_npc.Name = "checkBox_only_search_for_npc";
            this.checkBox_only_search_for_npc.Size = new System.Drawing.Size(194, 17);
            this.checkBox_only_search_for_npc.TabIndex = 12;
            this.checkBox_only_search_for_npc.Text = "Only search for NPC (filters players).";
            this.checkBox_only_search_for_npc.UseVisualStyleBackColor = true;
            // 
            // checkBox_case_sensitive_search
            // 
            this.checkBox_case_sensitive_search.AutoSize = true;
            this.checkBox_case_sensitive_search.Location = new System.Drawing.Point(6, 19);
            this.checkBox_case_sensitive_search.Name = "checkBox_case_sensitive_search";
            this.checkBox_case_sensitive_search.Size = new System.Drawing.Size(172, 17);
            this.checkBox_case_sensitive_search.TabIndex = 11;
            this.checkBox_case_sensitive_search.Text = "Make searches case sensitive.";
            this.checkBox_case_sensitive_search.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Brank);
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Arank);
            this.groupBox4.Controls.Add(this.checkBox_ARR_audio_Srank);
            this.groupBox4.Location = new System.Drawing.Point(306, 222);
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
            this.groupBox3.Location = new System.Drawing.Point(306, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
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
            this.checkBox_audio_cancelled.Location = new System.Drawing.Point(6, 112);
            this.checkBox_audio_cancelled.Name = "checkBox_audio_cancelled";
            this.checkBox_audio_cancelled.Size = new System.Drawing.Size(246, 17);
            this.checkBox_audio_cancelled.TabIndex = 4;
            this.checkBox_audio_cancelled.Text = "Play audio notification when scan is cancelled.";
            this.checkBox_audio_cancelled.UseVisualStyleBackColor = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(78, 17);
            this.toolStripStatusLabel1.Text = "Not scanning";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 583);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox_details
            // 
            this.groupBox_details.Controls.Add(this.textBox_details);
            this.groupBox_details.Location = new System.Drawing.Point(3, 403);
            this.groupBox_details.Name = "groupBox_details";
            this.groupBox_details.Size = new System.Drawing.Size(512, 89);
            this.groupBox_details.TabIndex = 9;
            this.groupBox_details.TabStop = false;
            this.groupBox_details.Text = "Target details";
            this.groupBox_details.Visible = false;
            // 
            // textBox_details
            // 
            this.textBox_details.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox_details.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_details.Location = new System.Drawing.Point(3, 16);
            this.textBox_details.Name = "textBox_details";
            this.textBox_details.ReadOnly = true;
            this.textBox_details.Size = new System.Drawing.Size(506, 70);
            this.textBox_details.TabIndex = 0;
            this.textBox_details.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_SB_audio_Brank);
            this.groupBox5.Controls.Add(this.checkBox_SB_audio_Arank);
            this.groupBox5.Controls.Add(this.checkBox_SB_audio_Srank);
            this.groupBox5.Location = new System.Drawing.Point(306, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Stormblood";
            // 
            // checkBox_SB_audio_Srank
            // 
            this.checkBox_SB_audio_Srank.AutoSize = true;
            this.checkBox_SB_audio_Srank.Checked = true;
            this.checkBox_SB_audio_Srank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SB_audio_Srank.Location = new System.Drawing.Point(7, 20);
            this.checkBox_SB_audio_Srank.Name = "checkBox_SB_audio_Srank";
            this.checkBox_SB_audio_Srank.Size = new System.Drawing.Size(164, 17);
            this.checkBox_SB_audio_Srank.TabIndex = 0;
            this.checkBox_SB_audio_Srank.Text = "Notify when S-rank is nearby.";
            this.checkBox_SB_audio_Srank.UseVisualStyleBackColor = true;
            // 
            // checkBox_SB_audio_Arank
            // 
            this.checkBox_SB_audio_Arank.AutoSize = true;
            this.checkBox_SB_audio_Arank.Checked = true;
            this.checkBox_SB_audio_Arank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SB_audio_Arank.Location = new System.Drawing.Point(7, 43);
            this.checkBox_SB_audio_Arank.Name = "checkBox_SB_audio_Arank";
            this.checkBox_SB_audio_Arank.Size = new System.Drawing.Size(164, 17);
            this.checkBox_SB_audio_Arank.TabIndex = 1;
            this.checkBox_SB_audio_Arank.Text = "Notify when A-rank is nearby.";
            this.checkBox_SB_audio_Arank.UseVisualStyleBackColor = true;
            // 
            // checkBox_SB_audio_Brank
            // 
            this.checkBox_SB_audio_Brank.AutoSize = true;
            this.checkBox_SB_audio_Brank.Location = new System.Drawing.Point(7, 66);
            this.checkBox_SB_audio_Brank.Name = "checkBox_SB_audio_Brank";
            this.checkBox_SB_audio_Brank.Size = new System.Drawing.Size(164, 17);
            this.checkBox_SB_audio_Brank.TabIndex = 2;
            this.checkBox_SB_audio_Brank.Text = "Notify when B-rank is nearby.";
            this.checkBox_SB_audio_Brank.UseVisualStyleBackColor = true;
            // 
            // TBscanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_details);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TBscanner";
            this.Size = new System.Drawing.Size(631, 605);
            this.Load += new System.EventHandler(this.TBscanner_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox_details.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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


            if (checkBox_display_infoBox.Checked)
            {
                groupBox_details.Visible = true;
            }
            else
            {
                groupBox_details.Visible = false;
            }
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
            ActGlobals.oFormActMain.OnLogLineRead += new LogLineEventDelegate(scan);

            lblStatus.Text = "Plugin Enabled\nNot scanning.";
        }

        

        private void scan(bool isImport, LogLineEventArgs logInfo)
        {
            if(isImport == true)
            {
                return;
            }

            //Scan target
            if (scanning)
            {
                if(checkBox_case_sensitive_search.Checked)
                {
                    match = scanner_regex.Match(logInfo.logLine);
                } else
                {
                    match = scanner_regex.Match(logInfo.logLine.ToString().ToLower());
                }
                if (match.Success)
                {
                    lock (scannerLock)
                    {
                        if (scanning)
                        {
                            stopScanning();

                            if (checkBox_audio_located.Checked)
                            {
                                updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_arr_srank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_arr_arank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_arr_brank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_hw_srank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_hw_arank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
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
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_hw_brank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.brank);
                        snd.Play();
                        return;
                    }
                }
            }



            //SB hunts
            if (checkBox_SB_audio_Srank.Checked)
            {
                for (int i = 0; i < hunts_hw_srank.Length; i++)
                {
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_sb_srank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.srank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_SB_audio_Arank.Checked)
            {
                for (int i = 0; i < hunts_hw_arank.Length; i++)
                {
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_sb_arank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.arank);
                        snd.Play();
                        return;
                    }
                }
            }

            if (checkBox_SB_audio_Brank.Checked)
            {
                for (int i = 0; i < hunts_hw_brank.Length; i++)
                {
                    match = new Regex(@"\[.+\] 03:Added new combatant " + hunts_sb_brank[i] + "\\.  Job: 0 Level:.+").Match(logInfo.logLine);
                    if (match.Success)
                    {
                        updateInfoBox(logInfo.logLine);
                        SoundPlayer snd = new SoundPlayer(Properties.Resources.brank);
                        snd.Play();
                        return;
                    }
                }
            }

        }

        private void updateInfoBox(string logLine)
        {
            if (checkBox_display_infoBox.Checked) //Only update if details is enabled
            {
                //Parse line
                Regex regex = new Regex(@"\[.+\] 03:Added new combatant (?<name>.+ ?.+)\.  Job: (?<class>\d+) Level: (?<level>\d+) Max HP: (?<HP>\d+) Max MP: (?<MP>\d+).+");
                Match match = regex.Match(logLine);
                if (match.Success)
                {
                    String details = "";
                    details += "Name:\t" + match.Groups["name"].Value + "\n";
                    if (match.Groups["class"].Value.Equals("0"))
                    {
                        details += "Type:\tNPC\n";
                    } else
                    {
                        details += "Type:\tPlayer\n";
                    }
                    details += "Level:\t" + match.Groups["level"].Value + "\n";
                    details += "HP:\t" + match.Groups["HP"].Value + "\n";
                    details += "MP:\t" + match.Groups["MP"].Value;

                    //Update the details textbox
                    textBox_details.Text = details;
                }

                
            }
        }

        public void DeInitPlugin()
        {
            // Unsubscribe from any events you listen to when exiting!
            ActGlobals.oFormActMain.OnLogLineRead += scan;

            SaveSettings();
            lblStatus.Text = "Plugin Disabled";
        }
        #endregion

        

        void LoadSettings()
        {
            xmlSettings.AddControlSetting(textBox_targetName.Name, textBox_targetName);

            xmlSettings.AddControlSetting(checkBox_case_sensitive_search.Name, checkBox_case_sensitive_search);
            xmlSettings.AddControlSetting(checkBox_only_search_for_npc.Name, checkBox_only_search_for_npc);
            xmlSettings.AddControlSetting(checkBox_audio_start.Name, checkBox_audio_start);
            xmlSettings.AddControlSetting(checkBox_audio_located.Name, checkBox_audio_located);
            xmlSettings.AddControlSetting(checkBox_audio_cancelled.Name, checkBox_audio_cancelled);

            xmlSettings.AddControlSetting(checkBox_display_infoBox.Name, checkBox_display_infoBox);

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
            checkBox_only_search_for_npc.Enabled = true;
            checkBox_case_sensitive_search.Enabled = true;


            button1.Text = "Scan";
            toolStripStatusLabel1.Text = "Not scanning";
            lblStatus.Text = "Plugin Enabled\nNot scanning.";
        }

        private void startScanning(string target)
        {
            String regexString;
            String regexSuffix;

            checkBox_only_search_for_npc.Enabled = false;
            checkBox_case_sensitive_search.Enabled = false;

            if (checkBox_only_search_for_npc.Checked)
            {
                regexSuffix = @"\.  Job: 0 Level: .+";
            } else
            {
                regexSuffix = ".+";
            }
            if (checkBox_case_sensitive_search.Checked)
            {
                regexString = @"\[.+\] 03:Added new combatant " + target + regexSuffix;
            }
            else
            {
                regexString = (@"\[.+\] 03:Added new combatant " + target + regexSuffix).ToLower();
            }
            scanner_regex = new Regex(regexString);
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
            button1.Text = "Scanning...";
            toolStripStatusLabel1.Text = "Scanning...";
            lblStatus.Text = "Plugin Enabled\nScanning for: " + textBox_targetName.Text.Clone();
        }

        private void checkBox_display_infoBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_display_infoBox.Checked)
            {
                groupBox_details.Visible = true;
            }
            else
            {
                groupBox_details.Visible = false;
            }
        }
    }
}
