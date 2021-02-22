using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskUserRemoval
{
    class Users
    {

        private List<string> collectionCsv = new List<string>();

        public List<User> users { get; set; }

        public List<string> BuildUsers(List<User> usersIn)
        {

            string builder = "";
            foreach(User obj in usersIn)
            {                
                if (obj.name is null)
                {
                    obj.name = "empty";
                }

                if (obj.email is null)
                {
                    obj.email = "empty";
                }

                if (obj.last_login_at == null)
                {
                    obj.last_login_at = new DateTime(10, 01, 01);
                }

                string name = obj.name.Replace(',', ' ');
                string email = obj.email.Replace(',', ' ');


                builder = obj.id + "|" +
                    name + "|" +
                    email + "|" +
                    obj.active + "|" +
                    obj.last_login_at + "|" +
                    obj.suspended;

                collectionCsv.Add(builder);

            }

            

            return collectionCsv;

        }
    }
}
