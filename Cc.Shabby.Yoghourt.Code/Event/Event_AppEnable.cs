using Cc.Shabby.Yoghourt.Code.Main;
using Native.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cc.Shabby.Yoghourt.Code.Event
{
    public class Event_AppEnable : Native.Sdk.Cqp.Interface.IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            PicQingNi.Init(e.CQApi, e.CQLog);
            PicManYao.Init(e.CQApi, e.CQLog);
        }
    }
}
