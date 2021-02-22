using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskUserRemoval
{
    public class Photo
    {
        public string url { get; set; }
        public double id { get; set; }
        public string file_name { get; set; }
        public string content_url { get; set; }
        public string mapped_content_url { get; set; }
        public string content_type { get; set; }
        public double size { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public bool inline { get; set; }
        public bool deleted { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
    }
}
