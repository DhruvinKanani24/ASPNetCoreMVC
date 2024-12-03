using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfiguration;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration;
            //newBookAlertConfiguration.OnChange(config =>
            //{
            //    _newBookAlertConfiguration = config;
            //});
        }
        public string GetName()
        {
            return _newBookAlertConfiguration.CurrentValue.BooKName;
        }
    }
}
