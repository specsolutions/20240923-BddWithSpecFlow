namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    public static class AgentServices
    {
        public static bool IsAgentMode =>
            false;
            //TODO: ConfigurationManager.AppSettings["ServerMode"] == "AGENT";

        public static string GetAgent()
        {
            var agent = "Default";
            //TODO
            //var routeData = HttpContext.Current?.Request.RequestContext?.RouteData;
            //if (routeData != null && routeData.Values.TryGetValue("agent", out var agentObj) && agentObj != null)
            //{
            //    agent = agentObj.ToString();
            //}
            return agent;
        }

        public static string GetRole()
        {
            var role = "Default";
            //TODO
            //var routeData = HttpContext.Current?.Request.RequestContext?.RouteData;
            //if (routeData != null && routeData.Values.TryGetValue("role", out var roleObj) && roleObj != null)
            //{
            //    role = roleObj.ToString();
            //}
            return role;
        }
    }
}