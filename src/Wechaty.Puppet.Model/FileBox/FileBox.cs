using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wechaty.PuppetModel
{
    // https://github.com/huan/file-box/blob/master/src/file-box.ts

    public class FileBox
    {
        public FileBoxOptionsUrl FromUrl(string url, string name, Dictionary<string, string> headers)
        {
            if (name == "")
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;
                    //建立获取网页标题正则表达式
                    String regex = @"<title>.+</title>";

                    //返回网页标题
                    String title = Regex.Match(response.Content.ReadAsStringAsync().Result, regex).ToString();
                    name = Regex.Replace(title, @"[\""]+", "");
                }
            }
            return new FileBoxOptionsUrl()
            {
                Type = FileBoxType.Url,
                Name = name,
                headers = headers,
                Url = url
            };
        }

        public FileBoxOptionsFile FromFile(string path, string name)
        {

            if (!File.Exists(path))
            {
                // TODO 异常
            }

            return new FileBoxOptionsFile()
            {
                Name = name,
                Path = path,
                Type = FileBoxType.File
            };
        }

        public FileBoxOptionsStream FromStream(Stream stream, string name)
        {
            return new FileBoxOptionsStream()
            {
                Name = name,
                Type = FileBoxType.Stream,
                Stream = stream
            };
        }

        public FileBoxOptionsBuffer FromBuffer(byte buffer, string name)
        {
            return new FileBoxOptionsBuffer()
            {
                Name = name,
                Buffer = buffer,
                Type = FileBoxType.Buffer
            };
        }

        public static FileBoxOptionsBase64 FromBase64(string base64, string name)
        {
            return new FileBoxOptionsBase64()
            {
                Name = name,
                Base64 = base64,
                Type = FileBoxType.Base64
            };
        }

        public FileBoxOptionsQRCode FromQrCode(string qrCode, string name)
        {
            return new FileBoxOptionsQRCode()
            {
                Name = name,
                QrCode = qrCode,
                Type = FileBoxType.QRCode
            };
        }

        /// <summary>
        /// 数据类型的Base64 数据转Base64
        /// </summary>
        /// <param name="dataUrl">data:image/png;base64,${base64Text}</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FileBoxOptionsBase64 FromDataURL(string dataUrl, string name)
        {
            return FromBase64(DataUrlToBase64(dataUrl), name);
        }

        // TODO 该方法放在公共函数里面
        public static string DataUrlToBase64(string dataUrl)
        {
            var dataList = dataUrl.Split(',');
            return dataList[dataList.Length - 1];
        }




    }
}
