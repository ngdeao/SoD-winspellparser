namespace winparser
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowDetails = new System.Windows.Forms.CheckBox();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.ShowRelated = new System.Windows.Forms.CheckBox();
            this.SearchEffectSlot = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SearchCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SearchLevel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DisplayText = new System.Windows.Forms.RadioButton();
            this.DisplayTable = new System.Windows.Forms.RadioButton();
            this.SearchNotes = new System.Windows.Forms.Label();
            this.SearchEffect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.SearchClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.SearchBrowser = new System.Windows.Forms.WebBrowser();
            this.AutoSearch = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.ResetBtn);
            this.panel1.Controls.Add(this.PrintBtn);
            this.panel1.Controls.Add(this.SearchNotes);
            this.panel1.Controls.Add(this.SearchBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 367);
            this.panel1.TabIndex = 0;
            // 
            // ShowDetails
            // 
            this.ShowDetails.AutoSize = true;
            this.ShowDetails.Location = new System.Drawing.Point(154, 195);
            this.ShowDetails.Name = "ShowDetails";
            this.ShowDetails.Size = new System.Drawing.Size(61, 19);
            this.ShowDetails.TabIndex = 21;
            this.ShowDetails.Text = "Details";
            this.ShowDetails.UseVisualStyleBackColor = true;
            this.ShowDetails.CheckedChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // ResetBtn
            // 
            this.ResetBtn.Enabled = false;
            this.ResetBtn.Location = new System.Drawing.Point(12, 281);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(91, 27);
            this.ResetBtn.TabIndex = 20;
            this.ResetBtn.Text = "Reset";
            this.ResetBtn.UseVisualStyleBackColor = true;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Location = new System.Drawing.Point(143, 281);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(91, 27);
            this.PrintBtn.TabIndex = 19;
            this.PrintBtn.Text = "Print";
            this.PrintBtn.UseVisualStyleBackColor = true;
            this.PrintBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // ShowRelated
            // 
            this.ShowRelated.AutoSize = true;
            this.ShowRelated.Location = new System.Drawing.Point(6, 195);
            this.ShowRelated.Name = "ShowRelated";
            this.ShowRelated.Size = new System.Drawing.Size(126, 19);
            this.ShowRelated.TabIndex = 12;
            this.ShowRelated.Text = "Show related spells";
            this.ShowRelated.UseVisualStyleBackColor = true;
            this.ShowRelated.CheckedChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // SearchEffectSlot
            // 
            this.SearchEffectSlot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchEffectSlot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchEffectSlot.FormattingEnabled = true;
            this.SearchEffectSlot.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.SearchEffectSlot.Location = new System.Drawing.Point(147, 122);
            this.SearchEffectSlot.Name = "SearchEffectSlot";
            this.SearchEffectSlot.Size = new System.Drawing.Size(68, 23);
            this.SearchEffectSlot.TabIndex = 9;
            this.SearchEffectSlot.TextChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(145, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "In Slot";
            // 
            // SearchCategory
            // 
            this.SearchCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchCategory.FormattingEnabled = true;
            this.SearchCategory.Location = new System.Drawing.Point(6, 166);
            this.SearchCategory.Name = "SearchCategory";
            this.SearchCategory.Size = new System.Drawing.Size(209, 23);
            this.SearchCategory.Sorted = true;
            this.SearchCategory.TabIndex = 11;
            this.SearchCategory.TextChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category";
            // 
            // SearchLevel
            // 
            this.SearchLevel.Enabled = false;
            this.SearchLevel.Location = new System.Drawing.Point(148, 78);
            this.SearchLevel.Name = "SearchLevel";
            this.SearchLevel.Size = new System.Drawing.Size(67, 23);
            this.SearchLevel.TabIndex = 5;
            this.SearchLevel.Text = "1-65";
            this.SearchLevel.TextChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Level";
            // 
            // DisplayText
            // 
            this.DisplayText.AutoSize = true;
            this.DisplayText.Checked = true;
            this.DisplayText.Location = new System.Drawing.Point(10, 17);
            this.DisplayText.Name = "DisplayText";
            this.DisplayText.Size = new System.Drawing.Size(60, 19);
            this.DisplayText.TabIndex = 13;
            this.DisplayText.TabStop = true;
            this.DisplayText.Text = "Details";
            this.DisplayText.UseVisualStyleBackColor = true;
            this.DisplayText.Click += new System.EventHandler(this.Initiate_Search);
            // 
            // DisplayTable
            // 
            this.DisplayTable.AutoSize = true;
            this.DisplayTable.Location = new System.Drawing.Point(99, 17);
            this.DisplayTable.Name = "DisplayTable";
            this.DisplayTable.Size = new System.Drawing.Size(54, 19);
            this.DisplayTable.TabIndex = 14;
            this.DisplayTable.Text = "Table";
            this.DisplayTable.UseVisualStyleBackColor = true;
            this.DisplayTable.Click += new System.EventHandler(this.Initiate_Search);
            // 
            // SearchNotes
            // 
            this.SearchNotes.AutoSize = true;
            this.SearchNotes.Location = new System.Drawing.Point(140, 320);
            this.SearchNotes.Margin = new System.Windows.Forms.Padding(0);
            this.SearchNotes.Name = "SearchNotes";
            this.SearchNotes.Size = new System.Drawing.Size(16, 15);
            this.SearchNotes.TabIndex = 16;
            this.SearchNotes.Text = "...";
            // 
            // SearchEffect
            // 
            this.SearchEffect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchEffect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchEffect.FormattingEnabled = true;
            this.SearchEffect.Location = new System.Drawing.Point(6, 122);
            this.SearchEffect.Name = "SearchEffect";
            this.SearchEffect.Size = new System.Drawing.Size(126, 23);
            this.SearchEffect.Sorted = true;
            this.SearchEffect.TabIndex = 7;
            this.SearchEffect.TextChanged += new System.EventHandler(this.Initiate_Search);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Has Effect";
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(12, 314);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(91, 27);
            this.SearchBtn.TabIndex = 15;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // SearchClass
            // 
            this.SearchClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchClass.FormattingEnabled = true;
            this.SearchClass.Location = new System.Drawing.Point(6, 78);
            this.SearchClass.Name = "SearchClass";
            this.SearchClass.Size = new System.Drawing.Size(126, 23);
            this.SearchClass.Sorted = true;
            this.SearchClass.TabIndex = 3;
            this.SearchClass.SelectedValueChanged += new System.EventHandler(this.SearchClass_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Class";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text or ID";
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(6, 34);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(209, 23);
            this.SearchText.TabIndex = 1;
            // 
            // SearchBrowser
            // 
            this.SearchBrowser.AllowWebBrowserDrop = false;
            this.SearchBrowser.CausesValidation = false;
            this.SearchBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBrowser.Location = new System.Drawing.Point(247, 0);
            this.SearchBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.SearchBrowser.Name = "SearchBrowser";
            this.SearchBrowser.Size = new System.Drawing.Size(761, 367);
            this.SearchBrowser.TabIndex = 0;
            this.SearchBrowser.TabStop = false;
            this.SearchBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SearchBrowser_Navigating);
            // 
            // AutoSearch
            // 
            this.AutoSearch.Interval = 500;
            this.AutoSearch.Tick += new System.EventHandler(this.SearchBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DisplayText);
            this.groupBox1.Controls.Add(this.DisplayTable);
            this.groupBox1.Location = new System.Drawing.Point(13, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 45);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display As:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.SearchText);
            this.groupBox2.Controls.Add(this.ShowDetails);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.SearchClass);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ShowRelated);
            this.groupBox2.Controls.Add(this.SearchEffect);
            this.groupBox2.Controls.Add(this.SearchEffectSlot);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.SearchLevel);
            this.groupBox2.Controls.Add(this.SearchCategory);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 224);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // MainForm
            // 
            this.AcceptButton = this.SearchBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 367);
            this.Controls.Add(this.SearchBrowser);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shards of Dalaya Spell Parser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.ComboBox SearchClass;
        private System.Windows.Forms.WebBrowser SearchBrowser;
        private System.Windows.Forms.ComboBox SearchEffect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DisplayText;
        private System.Windows.Forms.RadioButton DisplayTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SearchLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox SearchCategory;
        private System.Windows.Forms.Timer AutoSearch;
        private System.Windows.Forms.ComboBox SearchEffectSlot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ShowRelated;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Label SearchNotes;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.CheckBox ShowDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}

