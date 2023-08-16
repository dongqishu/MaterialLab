using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleTest
{
    public class PayFileModel
    {
        public string payment_desc { get; set; } = "";
        public CardModel masterInfo { get; set; }
        public CardModel visaInfo { get; set; }
        public CheckModel checkInfo { get; set; }
    }

    public class CardModel: CheckModel
    {
        public string card { get; set; } = "";
        public string name { get; set; } = "";
        public string date { get; set; } = "";
        public string cvc { get; set; } = "";
        public bool remember { get; set; } = false;
    }

    public class CheckModel
    {
        public string ImageData { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
