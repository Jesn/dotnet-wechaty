namespace Wechaty.PlugIn.Weather
{
    public class OutPutDTO
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// 体表温度
        /// </summary>
        public double FeelsLike { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// H5Url
        /// </summary>
        public string H5Url { get; set; }

    }
}
