using System.Windows.Forms;

namespace CapMosnterSolver
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        public Models.Configuration ShowForm(Models.Configuration config)
        {
            var _config = config.Clone();
            BsConfig.DataSource = _config;

            if (ShowDialog() != DialogResult.OK)
                return config;

            return _config;
        }

        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
