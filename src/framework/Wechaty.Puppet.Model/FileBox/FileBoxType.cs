
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wechaty.PuppetModel
{
    public enum FileBoxType
    {
        Unknown = 0,

        /**
         * Serializable by toJSON()
         */
        Base64 = 1,
        Url = 2,
        QRCode = 3,

        /**
         * Not serializable by toJSON()
         * Need to convert to FileBoxType.Base64 before call toJSON()
         */
        Buffer = 4,
        File = 5,
        Stream = 6,
    }

    public class FileBoxOptionsCommon
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }

    public class FileBoxOptionsFile: FileBoxOptionsCommon
    {
        [JsonProperty("type", Required = Required.Always)]
        public FileBoxType Type { get; set; } = FileBoxType.File;

        [JsonProperty("path", Required = Required.Always)]
        public string Path { get; set; }
    }

    public class FileBoxOptionsUrl : FileBoxOptionsCommon
    {
        [JsonProperty("url", Required = Required.Always)]
        public FileBoxType Type { get; set; } = FileBoxType.Url;
        public string Url { get; set; }

        // TODO http.Header
        public Dictionary<string,string>  headers { get; set; }

    }

    public class FileBoxOptionsBuffer : FileBoxOptionsCommon
    {
        public FileBoxType Type { get; set; } = FileBoxType.Buffer;
        public byte Buffer { get; set; }
    }

    public class FileBoxOptionsStream : FileBoxOptionsCommon
    {
        public FileBoxType Type { get; set; } = FileBoxType.Stream;
        public Stream Stream { get; set; }
    }

    public class FileBoxOptionsQRCode : FileBoxOptionsCommon
    {
        public FileBoxType Type { get; set; } = FileBoxType.QRCode;
        public string QrCode { get; set; }
    }

    public class FileBoxOptionsBase64 : FileBoxOptionsCommon
    {
        public FileBoxType Type { get; set; } = FileBoxType.Base64;
        public string Base64 { get; set; }
    }







}
