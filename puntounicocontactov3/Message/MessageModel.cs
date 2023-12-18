using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public class MessageModel
    {
        public List<string>? To { get; set; }
        public string? Subject { get; set; }
        public List<string>? CCP { get; set; }
        public string? BodyMessage { get; set; }
    }
}
