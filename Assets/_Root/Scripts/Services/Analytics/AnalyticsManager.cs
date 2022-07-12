using UnityEngine;
using Services.Analytics.UnityAnalytics;
using Tool;


namespace Services.Analytics
{
    internal class AnalyticsManager : Service <AnalyticsManager>
    {
        private IAnalyticsService[] _services;

        public AnalyticsManager(): base()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsServiceModern()
            };
        }
            
        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendGameControllerStarted() =>
            SendEvent("GameControllerStarted");


        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
