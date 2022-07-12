using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;

namespace Services.Analytics.UnityAnalytics
{
    internal class UnityAnalyticsServiceModern : IAnalyticsService
    {
        private Dictionary<string, object> emptyDictionary;

        public UnityAnalyticsServiceModern()
        {


            emptyDictionary = new Dictionary<string, object>()
            {
                { "null", 0 }
            };
        }
        public void SendEvent(string eventName)
        {
            AnalyticsService.Instance.CustomData(eventName, emptyDictionary);
            Debug.Log($"Event {eventName} with empty Dictionary");
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            AnalyticsService.Instance.CustomData(eventName, eventData);


        
    }
}
