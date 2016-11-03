using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismViewModelFirst.Messages
{
    public class ShowSpecificMainContainerViewModelMessage : MessageBase
    {
        public string Content { get; set; }
    }

}
