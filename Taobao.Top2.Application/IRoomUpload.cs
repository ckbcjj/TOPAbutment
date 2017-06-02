using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.Application
{
    public interface IRoomUpload
    {
        void Upload(TaobaoHotel taobao,int flag);
        void SynRoomTypeStatus();
    }
}
