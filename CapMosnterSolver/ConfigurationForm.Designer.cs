namespace CapMosnterSolver
{
    partial class ConfigurationForm
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
            this.LabAddress = new System.Windows.Forms.Label();
            this.TbAddress = new System.Windows.Forms.TextBox();
            this.LabApiKey = new System.Windows.Forms.Label();
            this.TbApiKey = new System.Windows.Forms.TextBox();
            this.CbTranserProxy = new System.Windows.Forms.CheckBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BsConfig = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BsConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // LabAddress
            // 
            this.LabAddress.AutoSize = true;
            this.LabAddress.Location = new System.Drawing.Point(12, 9);
            this.LabAddress.Name = "LabAddress";
            this.LabAddress.Size = new System.Drawing.Size(45, 13);
            this.LabAddress.TabIndex = 0;
            this.LabAddress.Text = "Address";
            // 
            // TbAddress
            // 
            this.TbAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BsConfig, "Address", true));
            this.TbAddress.Location = new System.Drawing.Point(15, 25);
            this.TbAddress.Name = "TbAddress";
            this.TbAddress.Size = new System.Drawing.Size(236, 20);
            this.TbAddress.TabIndex = 1;
            // 
            // LabApiKey
            // 
            this.LabApiKey.AutoSize = true;
            this.LabApiKey.Location = new System.Drawing.Point(12, 48);
            this.LabApiKey.Name = "LabApiKey";
            this.LabApiKey.Size = new System.Drawing.Size(40, 13);
            this.LabApiKey.TabIndex = 2;
            this.LabApiKey.Text = "ApiKey";
            // 
            // TbApiKey
            // 
            this.TbApiKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BsConfig, "ApiKey", true));
            this.TbApiKey.Location = new System.Drawing.Point(15, 64);
            this.TbApiKey.Name = "TbApiKey";
            this.TbApiKey.Size = new System.Drawing.Size(236, 20);
            this.TbApiKey.TabIndex = 3;
            // 
            // CbTranserProxy
            // 
            this.CbTranserProxy.AutoSize = true;
            this.CbTranserProxy.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BsConfig, "TransferProxy", true));
            this.CbTranserProxy.Location = new System.Drawing.Point(15, 90);
            this.CbTranserProxy.Name = "CbTranserProxy";
            this.CbTranserProxy.Size = new System.Drawing.Size(93, 17);
            this.CbTranserProxy.TabIndex = 4;
            this.CbTranserProxy.Text = "Transfer proxy";
            this.CbTranserProxy.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(12, 113);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(158, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(176, 113);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BsConfig
            // 
            this.BsConfig.DataSource = typeof(CapMosnterSolver.Models.Configuration);
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.BtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(264, 149);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.CbTranserProxy);
            this.Controls.Add(this.TbApiKey);
            this.Controls.Add(this.LabApiKey);
            this.Controls.Add(this.TbAddress);
            this.Controls.Add(this.LabAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.BsConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabAddress;
        private System.Windows.Forms.TextBox TbAddress;
        private System.Windows.Forms.Label LabApiKey;
        private System.Windows.Forms.TextBox TbApiKey;
        private System.Windows.Forms.CheckBox CbTranserProxy;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.BindingSource BsConfig;
    }
}