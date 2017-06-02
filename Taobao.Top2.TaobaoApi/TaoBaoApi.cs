using Taobao.Top2.TaobaoApi.Top2;

namespace Taobao.Top2.TaobaoApi
{
    public class TaoBaoApi
    {
        public static IRoomOpration CreateRoomOpration()
        {
            return new RoomOpration();
        }
    }
}
