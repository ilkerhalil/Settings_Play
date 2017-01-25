using System;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using RestSharp;

namespace EuroMemberWinService.Aspects {
    public class ReportCronitorAspect:IInterceptor
    {
        private const string Run = "https://cronitor.link/yWG0Vq/run";

        private const string Complete = "https://cronitor.link/yWG0Vq/complete";
        private const string Fail = "https://cronitor.link/yWG0Vq/fail";

        public void Intercept(IInvocation invocation)
        { 
            Get(Run);
            try
            {
                invocation.Proceed();
                Get(Complete);
            }
            catch (Exception e)
            {
                Get(Fail);
            }
        }

        private void Get(string uri)
        {
            var restClient = new RestClient();
            var request =new RestRequest(Run,Method.GET);
            restClient.Get(request);
        }
    }
}
