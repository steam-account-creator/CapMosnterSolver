namespace CapMosnterSolver.Models
{
    public class Configuration
    {
        public string Address { get; set; } = "http://127.0.0.1/";
        public string ApiKey { get; set; } = "1";
        public bool TransferProxy { get; set; } = false;

        public Configuration Clone()
            => new Configuration()
            {
                Address = this.Address,
                ApiKey = this.ApiKey,
                TransferProxy = this.TransferProxy
            };
    }
}
