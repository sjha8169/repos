using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string test = "\uD83D\uDCE3";
            //string test = "\uD83D\uDC69‍\uD83D\uDCBB";
            //string test = "\uD83D\uDCE3 Developers \uD83D\uDC69‍\uD83D\uDCBB and designers \uD83D\uDC68‍\uD83C\uDFA8\n\nThis year’s newest emoji are now supported in Twemoji 11.0! \uD83E\uDD73\n\nhttps://t.co/RY3IGtg7bc";

            //var x = HttpUtility.JavaScriptStringEncode(test);
            //byte[] utf8Bytes = Encoding.UTF8.GetBytes(x);
            //String str1 = Encoding.Unicode.GetString(utf8Bytes);
            //String str2 = Encoding.UTF8.GetString(utf8Bytes);
            //Console.WriteLine(str1);

            //var y = HttpUtility.HtmlDecode(x);

            //Console.WriteLine(x);
            //Console.WriteLine(y);
            //var z = HttpUtility.JavaScriptStringEncode("1F4E3");            
            //Console.WriteLine(z);

            //Regex regex = new Regex(@"\\U([0-9A-F]{4})", RegexOptions.IgnoreCase);
            //string result = test;
            //result = regex.Replace(result, match => ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString());

            // string result = System.Uri.EscapeUriString(test);
            //Console.WriteLine(result);


            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(test);
            //var f = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            //Console.WriteLine(f);

            //var x = char.ConvertFromUtf32(0x1F642);

            //var enc = new UTF32Encoding(true, false);
            //var bytes = enc.GetBytes(x);
            //var o = BitConverter.ToString(bytes);
            //Console.WriteLine(o);
            //string comment = "Create a UTF-16 encoded string from a code point.";
            //string comment1b = "Create a code point from a UTF-16 encoded string.";
            //string comment2b = "Create a code point from a surrogate pair at a certain position in a string.";
            //string comment2c = "Create a code point from a high surrogate and a low surrogate code point.";
            char s10 = (char)0xD83D;
            char s12 = (char)0xDC68;
            

            //Console.WriteLine(comment);
           //s1 = Char.ConvertFromUtf32(music);
            //Console.WriteLine(s1);
            ////Console.Write("    2a) 0x{0:X} => ", music);
            //Show(s1);

            var music = Char.ConvertToUtf32(s10, s12);
            string codepointunicode = string.Format("0x{0:X}", music);
            Console.WriteLine(codepointunicode);
            
            Console.ReadLine();
            //https://docs.microsoft.com/en-us/dotnet/api/system.char.convertfromutf32?view=net-5.0
        }


    }
}
