// See https://aka.ms/new-console-template for more information
using ConsoleTest;
using System;
using System.Text.RegularExpressions;

//var model = new PayFileModel();
//model.payment_desc = "description";
//model.masterInfo = new CardModel();
//model.masterInfo.card = "card1";
//model.masterInfo.name = "name1";
//model.masterInfo.date = "2023/04/17";
//model.masterInfo.cvc = "123";
//model.masterInfo.remember = true;
//model.masterInfo.imageData = "https://cdn.image.com/1.png";
//model.masterInfo.imageUrl = "https://cdn.image.com/2.png";

//model.visaInfo = new CardModel();
//model.visaInfo.card = "card2";
//model.visaInfo.name = "name2";
//model.visaInfo.date = "2023/04/15";
//model.visaInfo.cvc = "456";
//model.visaInfo.remember = false;
//model.visaInfo.imageData = "https://cdn.image.com/3.png";
//model.visaInfo.imageUrl = "https://cdn.image.com/4.png";

//model.checkInfo = new CheckModel();

//model.checkInfo.imageData = "https://cdn.image.com/5.png";
//model.checkInfo.imageUrl = "https://cdn.image.com/6.png";

//var str = GlobalUtil.SeriliazeToJson(model, camelCase: false);

//var testModel = GlobalUtil.DeserializeJson<PayFileModel>(str);

//var uuid = Guid.NewGuid().ToString("N");

//var arr = new[] { 1,2,3,4,5 };
//arr.Where(r=>r>1).ToList();

//int x = 120;
//char[] chars = x.ToString().TrimEnd('0').ToCharArray();
//Array.Reverse(chars);
//string y = new string(chars);
//Console.WriteLine(Convert.ToInt32(y));


Console.WriteLine(MyAtoi(" ++1"));
Console.WriteLine(MySum(10));

//if(null > 0)
//{
//    Console.WriteLine("true");
//}
//else { Console.WriteLine("false"); }

static int MyAtoi(string s)
{
    int result = 0;
    if(s.Contains("0-") || s.Contains("0+"))
    {
        return result;
    }
    var temp = s.Trim(' ').TrimStart('0');
    if (temp.StartsWith('-'))
    {
        var t = Regex.Split(temp.Substring(1).TrimStart('0'), @"\D")[0];
        if(t.Length > 10 || (t.Length == 10 && t.CompareTo("2147483648") >= 0))
        {
            result = -2147483648;
        }
        else
        {
            int.TryParse(t, out result);
            result = result * (-1);
        }
    }
    else if (temp.StartsWith("+")) {
        var t = Regex.Split(temp.Substring(1).TrimStart('0'), @"\D")[0];
        if (t.Length > 10 || (t.Length == 10 && t.CompareTo("2147483647") >= 0))
        {
            result = 2147483647;
        }
        else
        {
            int.TryParse(t, out result);
        }
    }
    else
    {
        var t = Regex.Split(temp, @"\D")[0];
        if (t.Length > 10 || (t.Length == 10 && t.CompareTo("2147483647") >= 0))
        {
            result = 2147483647;
        }
        else
        {
            int.TryParse(t, out result);
        }
    }   
    return result;
}

static int MySum(int n)
{
    int result = 0;
    for(int i = 0; i < n; i++)
    {
        result += i;
    }
    return result;
}

static int MySum_hotfix(int n)
{
    int result = 0;
    for (int i = 0; i < n; i++)
    {
        result += i;
    }
    return result;
}


