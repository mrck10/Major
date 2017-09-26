using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Major
{
    public class EmailFunctions
    {
        public string emailAdress;  // email username
        public string emailPassword; // email password
        public string emailTo; // email target
        public string emailSMTPserver; // email server
        public int emailPort; // email port
        public bool emailSLL = true; // sll is enabled?
        public string emailBody; // email body
        public string emailTitle; // email Title
    }
}
