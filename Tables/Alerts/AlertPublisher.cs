﻿using Controls.DynamicComponents;
using Microsoft.AspNetCore.Components;
using System.Collections.Immutable;
using System.Drawing;

namespace Controls.Alerts
{
    public class AlertPublisher
    {
        private readonly LinkedList<ComponentMetadata> alerts = new();
        private readonly uint maximumAlerts;

        public AlertPublisher(uint maximumAlerts = 1)
        {
            this.maximumAlerts = maximumAlerts;
        }

        public event EventHandler? OnAlertsChanged;

        public IEnumerable<ComponentMetadata> Alerts => alerts;

        public void AddAlert(ComponentMetadata componentMetadata)
        {
            if (alerts.Count >= maximumAlerts)
                alerts.RemoveFirst();
            alerts.AddLast(componentMetadata);
            OnAlertsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveAlert(ComponentMetadata componentMetadata)
        {
            alerts.Remove(componentMetadata);
            OnAlertsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void CreateBasicAlert(string text, Color backgroundColor, TimeSpan? autoHideTime = null)
        {
            ComponentMetadata alert = new(typeof(BasicAlert));
            var parameters = new Dictionary<string, object>()
            {
                { "Text", text },
                { "BackgroundColor", backgroundColor },
                { "OnClose", EventCallback.Factory.Create(this, () => RemoveAlert(alert)) },
            };
            if (autoHideTime is not null)
                parameters.Add("AutoHideTime", autoHideTime);
            alert.Parameters = parameters.ToImmutableDictionary();
            AddAlert(alert);
        }
    }
}
