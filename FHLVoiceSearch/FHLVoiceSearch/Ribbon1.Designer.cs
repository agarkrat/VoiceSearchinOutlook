
namespace FHLVoiceSearch
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VoiceSearch = this.Factory.CreateRibbonTab();
            this.TestGroup = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.VoiceSearch.SuspendLayout();
            this.TestGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // VoiceSearch
            // 
            this.VoiceSearch.Groups.Add(this.TestGroup);
            this.VoiceSearch.Label = "VoiceSearch";
            this.VoiceSearch.Name = "VoiceSearch";
            // 
            // TestGroup
            // 
            this.TestGroup.Items.Add(this.button1);
            this.TestGroup.Label = "Test";
            this.TestGroup.Name = "TestGroup";
            // 
            // button1
            // 
            this.button1.Image = global::FHLVoiceSearch.Properties.Resources.Search;
            this.button1.Label = "Search";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.VoiceSearch);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.VoiceSearch.ResumeLayout(false);
            this.VoiceSearch.PerformLayout();
            this.TestGroup.ResumeLayout(false);
            this.TestGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab VoiceSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup TestGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
