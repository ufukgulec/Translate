using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Translate
{
    [Obsolete]
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("Under the table is sleeping a cat-Masanın altında uyuyor bir kedi");
            list.Add("Should you come here, please let me know-Gelirseniz buraya, lütfen beni bilgilendirin");
            list.Add("Were you to go to home, you can not see this film-Eğer eve gitseydiniz bu filmi göremezdiniz");
            list.Add("Had you get up early, you would have met my friend-Erken kalkmış olsaydın arkadaşımla karşılaşacaktın");
            list.Add("Keep your friends close, but your enemies closer.-Arkadaşlarını yakın tut ama düşmanlarını daha da yakın.");
            list.Add("I will be back!-Geri döneceğim.");
            list.Add("I will have my vengeance, in this life or the next.-İntikamımı alacağım. Bu hayatta ya da sonraki hayatta.");
            list.Add("Promise me you will survive that. You will never give up. No matter what happens. No matter how hopeless.-Bana hayatta kalacağına dair söz ver. Asla vazgeçmeyeceksin. Ne olursa olsun. Ne kadar umutsuz olursa olsun.");
            list.Add("I don’t understand – Anlamadım\r\n\r\nI’m sorry – Üzgünüm\r\n\r\nSee you – Görüşürüz\r\n\r\nI have no idea – Hiçbir fikrim yok\r\n\r\nAs for me – Bence\r\n\r\nBelieve me – İnan bana\r\n\r\nSee you tomorrow – Yarın görüşürüz\r\n\r\nCall me back – Beni geri ara\r\n\r\nCome with me – Benimle gel\r\n\r\nI’m good – Ben iyiyim\r\n\r\nJoin me – Katıl bana\r\n\r\nI think so – Sabırım\r\n\r\nI would love to – Çok isterim\r\n\r\nI can’t wait – Bekleyemem\r\n\r\nHave a good weekend – İyi hafta sonları\r\n\r\nLet’s do it – Hadi yapalım\r\n\r\nYour turn – Senin sıran");


            foreach (var item in list)
            {
                var text = translater(item, "en", "tr");

                Console.WriteLine(text + "\n");
            }

            var input = translaterV2("Çok teşekkürler", "en");

            Console.WriteLine(input + "\n");

        }


        static string translater(string text, string from, string to)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={HttpUtility.UrlEncode(text)}";

            var webclient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            var result = webclient.DownloadString(url);

            try
            {
                return ParseResult(result);
            }
            catch
            {
                return "error";
            }


        }

        static string translaterV2(string text, string to)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=o&dt=t&q={HttpUtility.UrlEncode(text)}";

            var webclient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            var result = webclient.DownloadString(url);

            try
            {

                return ParseResult(result);

            }
            catch
            {
                return "error";
            }


        }

        private static string ParseResult(string json)
        {
            var jsonData = JArray.Parse(json);

            string translation = "";

            foreach (var translate in jsonData[0])
            {
                translation = translation + translate[0].ToString();
            }

            return translation;
        }

    }

}