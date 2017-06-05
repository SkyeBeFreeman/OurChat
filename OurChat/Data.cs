using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurChat {

    public class Content {
        //[PrimaryKey, AutoIncrement]
        public int _Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public DateTime time { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public List<mList> list { get; set; }
        public string username { get; set; }
    }

    public class mList {
        public string name { get; set; }
        public string article { get; set; }
        public string source { get; set; }
        public string info { get; set; }
        public string icon { get; set; }
        public string detailurl { get; set; }
    }

    public class custom {
        public string[] question { get; set; }
        public string[] answer { get; set; }
    }

    public class Result {
        public int code { get; set; }
        public string text { get; set; }
    }
}
