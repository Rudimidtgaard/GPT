using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Models
{
    public class GPTRequest
    {
        public string Model { get; }
        public Message[] Messages { get; set; }

        public GPTRequest()
        {
            Model = "gpt-3.5-turbo";
            Messages = Array.Empty<Message>();
        }

        public class Message
        {
            public string Role { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
        }
    }
}
