using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETB310_TennantWebsite.Models
{
    public class ErrorLogItem
    {
        public ErrorLogItem(string label, string description)
        {
            TimeStamp = DateTime.Now;
            Label = label;
            Description = description;
        }
        private ErrorLogItem()
        {
        }
        public string Label { get; }
        public DateTime TimeStamp { get; }
        public string Description { get; }
    }
}