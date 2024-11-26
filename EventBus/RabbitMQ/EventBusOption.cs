using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBusOption
    {
        public string SubscriptionClientName { get; set; }
        public int RetryCount { get; set; } = 10;
    }
}
