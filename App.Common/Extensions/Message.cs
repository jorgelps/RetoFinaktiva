using System.Collections.Generic;

namespace App.Common.Extensions
{
    public enum TypeMessage
    {
        Error = 0,
        Succes = 1,
        Information = 2,
        Warning = 3
    }

    public class JsonMessages
    {
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public int Code { get; set; }        

        public string Text { get; set; }
        
    }
}
