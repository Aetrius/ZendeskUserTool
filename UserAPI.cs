using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskUserRemoval
{
    class UserAPI
    {

        public List<User> users { get; set; }

        public string next_page { get; set; }

        public string previous_page { get; set; }

        public double count { get; set; }


    }
}
