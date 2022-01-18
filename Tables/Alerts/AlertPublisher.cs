using Controls.DynamicComponents;
using Microsoft.AspNetCore.Components;
using System.Collections.Immutable;
using System.Drawing;

namespace Controls.Alerts
{
    public class AlertPublisher
    {
        private readonly LinkedList<ComponentMetadata> alerts = new();

        public AlertPublisher(uint maximumAlerts = 1)
        {
            this.maximumAlerts = (int)maximumAlerts;
        }

        private int maximumAlerts;
        public int MaximumAlerts 
        { 
            get => maximumAlerts;
            set
            {
                maximumAlerts = value;
                CheckAlertCount();
            }
        }

        public event EventHandler? OnAlertsChanged;

        public IEnumerable<ComponentMetadata> Alerts => alerts;

        private void CheckAlertCount()
        {
            if (alerts.Count > maximumAlerts)
            {
                int count = alerts.Count - maximumAlerts;
                for (int i = 0; i < count; i++)
                    alerts.RemoveFirst();
            }
        }

        public void AddAlert(ComponentMetadata componentMetadata)
        {
            alerts.AddLast(componentMetadata);
            CheckAlertCount();
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
