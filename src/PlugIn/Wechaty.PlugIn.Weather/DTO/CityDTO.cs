namespace Wechaty.PlugIn.Weather
{
    public class CityDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lon { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 时区
        /// </summary>
        public string TZ { get; set; }
        public string UtcOffset { get; set; }

    }
}
