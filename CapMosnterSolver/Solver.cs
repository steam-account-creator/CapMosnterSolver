using CapMosnterSolver.Models;
using RestSharp;
using SACModuleBase;
using SACModuleBase.Attributes;
using SACModuleBase.Enums;
using SACModuleBase.Enums.Captcha;
using SACModuleBase.Models;
using SACModuleBase.Models.Capcha;
using System;
using System.Linq;
using System.Threading;

namespace CapMosnterSolver
{
    [SACModuleInfo("F0509A4B-7348-4B09-9521-6BB7E2C61ED2", "CapMonsterSolver", "1.0")]
    public class Solver : ISACHandlerCaptcha, ISACHandlerReCaptcha, ISACUserInterface
    {
        public bool ModuleEnabled { get; set; } = true;

        public string ShowButtonCaption => "Settings";

        private RestClient HttpClient;
        private ConfigManager<Configuration> Config;
        private ConfigurationForm Window;

        private static object Sync = new object();

        private ISACLogger Logger;

        public void ModuleInitialize(SACInitialize initialize)
        {
            Logger = initialize.Logger;

            HttpClient = new RestClient();
            HttpClient.AddDefaultParameter("json", 0);

            Config = new ConfigManager<Configuration>(initialize.ConfigurationPath, "config.json", new Configuration(), Sync);
            if (!Config.Load())
            {
                Logger.Warn("Cannot load config. You probably need to configure new one.");
                Config.Save();
            }

            Window = new ConfigurationForm();
        }

        private bool ConfigRefresh()
        {
            var config = Config.Running ?? new Configuration();
            HttpClient.BaseUrl = new Uri(config.Address);
            HttpClient.AddDefaultParameter("key", config.ApiKey);
            return true;
        }

        public CaptchaResponse Solve(CaptchaRequest captcha)
        {
            try
            {
                if (!ConfigRefresh())
                    return new CaptchaResponse(CaptchaStatus.Failed, "Failed to load configuration!");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load configuration!", ex);
                return new CaptchaResponse(CaptchaStatus.Failed, "Failed to load configuration!");
            }

            var requestQueueId = new RestRequest("in.php", Method.GET);
            requestQueueId.AddParameter("soft_id", 2370);
            requestQueueId.AddParameter("body", $"data:image/jpg;base64,{captcha.CaptchaImage}");
            requestQueueId.AddParameter("method", "base64");
            AddProxy(requestQueueId, captcha.Proxy);

            return GetSolution(requestQueueId, false);
        }

        public CaptchaResponse Solve(ReCaptchaRequest captcha)
        {
            try
            {
                if (!ConfigRefresh())
                    return new CaptchaResponse(CaptchaStatus.Failed, "Failed to load configuration!");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load configuration!", ex);
                return new CaptchaResponse(CaptchaStatus.Failed, "Failed to load configuration!");
            }

            var requestQueueId = new RestRequest("in.php", Method.GET);
            requestQueueId.AddParameter("soft_id", 2370);
            requestQueueId.AddParameter("googlekey", captcha.SiteKey);
            requestQueueId.AddParameter("method", "userrecaptcha");
            requestQueueId.AddParameter("pageurl", captcha.Url);
            AddProxy(requestQueueId, captcha.Proxy);

            return GetSolution(requestQueueId, true);
        }

        public void ShowWindow()
        {
            lock (Sync)
            {
                var newConfig = Window.ShowForm(Config.Running ?? new Configuration());
                if (newConfig == null)
                    return; // cancelled

                Config.Running = newConfig;
            }
            Config.Save();
        }

        private void AddProxy(IRestRequest request, Proxy proxy)
        {
            if (proxy == null)
                return;

            var config = Config.Running ?? new Configuration();
            if (!config.TransferProxy)
                return;

            request.AddParameter("proxy", $"{proxy.Host}:{proxy.Port}");
            request.AddParameter("proxytype", (proxy.Type == ProxyType.Unknown) ? "HTTP" : proxy.Type.ToString().ToUpper());
        }

        private CaptchaResponse GetSolution(IRestRequest queueRequest, bool isRecaptcha)
        {
            var responseQueueId = HttpClient.Execute(queueRequest);
            if (!responseQueueId.IsSuccessful)
                return new CaptchaResponse(CaptchaStatus.RetryAvailable, "Failed to request solution!");

            var queueAndStatus = responseQueueId.Content.Split(new[] { '|' }, StringSplitOptions.None);
            var status = queueAndStatus?.FirstOrDefault();
            if (string.IsNullOrEmpty(status))
                status = "Something went wrong!";

            Logger.Debug($"TwoCaptcha/RuCaptcha ID response: {status}\n" +
                $"Plain:\n" +
                $"============\n" +
                $"{responseQueueId.Content}\n" +
                $"============");

            switch (status)
            {
                case "OK":
                    break;
                case "ERROR_NO_SLOT_AVAILABLE":
                    Thread.Sleep(6000);
                    return new CaptchaResponse(CaptchaStatus.RetryAvailable, status);
                case "ERROR_RECAPTCHA_TIMEOUT":
                case "ERROR_RECAPTCHA_TIMEOUT ()":
                    Thread.Sleep(3000);
                    return new CaptchaResponse(CaptchaStatus.RetryAvailable, status);
                default:
                    return new CaptchaResponse(CaptchaStatus.Failed, status);
            }
            var id = queueAndStatus.ElementAtOrDefault(1);
            if (string.IsNullOrEmpty(id))
                return new CaptchaResponse(CaptchaStatus.Failed, "No queue ID is received!");

            Logger.Debug($"TwoCaptcha/RuCaptcha ID: {id}");

            Thread.Sleep(TimeSpan.FromSeconds(20));

            var tryNumber = 0;
            while (true)
            {
                Logger.Debug($"TwoCaptcha/RuCaptcha requesting solution... Try {tryNumber + 1}...");

                var solutionRequest = new RestRequest("res.php", Method.GET);
                solutionRequest.AddParameter("action", "get");
                solutionRequest.AddParameter("id", id);
                var solutionResponse = HttpClient.Execute(solutionRequest);
                if (!solutionResponse.IsSuccessful)
                {
                    Logger.Debug("TwoCaptcha/RuCaptcha requesting solution was failed. Retrying...");
                    // we will retry automatically if there is HTTP error.
                    continue;
                }

                var solutionAndStatus = solutionResponse.Content.Split(new[] { '|' }, StringSplitOptions.None);
                status = solutionAndStatus?.FirstOrDefault();
                if (string.IsNullOrEmpty(status))
                    status = "Something went wrong!";

                Logger.Debug($"TwoCaptcha/RuCaptcha solving status: {status}\n" +
                    $"Plain response:\n" +
                    $"============\n" +
                    $"{solutionResponse.Content}\n" +
                    $"============");

                switch (status)
                {
                    case "OK":
                        {
                            var _solution = new CaptchaResponse(solutionAndStatus.ElementAtOrDefault(1) ?? "", id);
                            Logger.Debug($"TwoCaptcha/RuCaptcha solution: {_solution.Solution}");
                            return _solution;
                        }
                    case "CAPCHA_NOT_READY":
                    case "ERROR_NO_SLOT_AVAILABLE":
                        Thread.Sleep(6000);
                        continue;
                    case "ERROR_RECAPTCHA_TIMEOUT":
                    case "ERROR_RECAPTCHA_TIMEOUT ()":
                        Thread.Sleep(3000);
                        return new CaptchaResponse(CaptchaStatus.RetryAvailable, status);
                    default:
                        return new CaptchaResponse(CaptchaStatus.RetryAvailable, status);
                }
            }
        }
    }
}
