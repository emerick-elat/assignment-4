using EventBus.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interface
{
    public interface IEventBus
    {
        Task PublishAsync(MailEvent mailEvent);
    }
}
