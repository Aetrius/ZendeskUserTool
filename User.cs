using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskUserRemoval
{
    public class User
    {
        public object id { get; set; }
        public string url { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string time_zone { get; set; }
        public string iana_time_zone { get; set; }
        public string phone { get; set; }
        public bool? shared_phone_number { get; set; }
        public Photo photo { get; set; }
        public double locale_id { get; set; }
        public string locale { get; set; }
        public object organization_id { get; set; }
        public string role { get; set; }
        public bool verified { get; set; }
        public object external_id { get; set; }
        public List<string> tags { get; set; }
        public string alias { get; set; }
        public bool? active { get; set; }
        public bool shared { get; set; }
        public bool shared_agent { get; set; }
        public DateTime? last_login_at { get; set; }
        public bool? two_factor_auth_enabled { get; set; }
        public string signature { get; set; }
        public string details { get; set; }
        public string notes { get; set; }
        public object role_type { get; set; }
        public object custom_role_id { get; set; }
        public bool moderator { get; set; }
        public string ticket_restriction { get; set; }
        public bool only_private_comments { get; set; }
        public bool restricted_agent { get; set; }
        public bool? suspended { get; set; }
        public bool chat_only { get; set; }
        public double? default_group_id { get; set; }
        public bool report_csv { get; set; }
        public UserFields user_fields { get; set; }

    }
}
