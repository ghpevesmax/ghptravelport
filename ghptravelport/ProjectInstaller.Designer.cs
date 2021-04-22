
namespace ghptravelport
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.travelportServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.travelportServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // travelportServiceProcessInstaller
            // 
            this.travelportServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.travelportServiceProcessInstaller.Password = null;
            this.travelportServiceProcessInstaller.Username = null;
            // 
            // travelportServiceInstaller
            // 
            this.travelportServiceInstaller.Description = "Service helper to integrate TravelPort data into MiAgenciaGHP";
            this.travelportServiceInstaller.DisplayName = "ghptravelport";
            this.travelportServiceInstaller.ServiceName = "ghptravelport";
            this.travelportServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.travelportServiceProcessInstaller,
            this.travelportServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller travelportServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller travelportServiceInstaller;
    }
}