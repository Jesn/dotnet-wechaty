namespace Wechaty.Domain.Shared
{
    public class CacheConst
    {
        public static readonly int CacheGlobalExpirationTime = 3600;


        /// <summary>
        ///  Room contact cache key
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public static string CacheKeyRoomMember(string roomId, string contactId) => contactId + "@@@" + roomId;

       
    }
}
