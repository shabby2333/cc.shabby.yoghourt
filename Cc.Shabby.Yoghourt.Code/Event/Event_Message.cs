using Cc.Shabby.Yoghourt.Code.Main;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cc.Shabby.Yoghourt.Code.Event
{
    public class Event_Message : Native.Sdk.Cqp.Interface.IPrivateMessage, Native.Sdk.Cqp.Interface.IGroupMessage
    {
        private readonly Regex _regexQN = new Regex(@"\.酸奶 +(\S{1,4})");
        private readonly Regex _regexMY = new Regex(@"\.小蛮腰 +(\S{1,7})");

        public async void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            var path = await checkAndGenerate(e.Message.Text);
            if (!(path is null))
            {
                e.FromGroup.SendGroupMessage(CQApi.CQCode_Image(path));
            }
        }

        public async void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            var path =  await checkAndGenerate(e.Message.Text);
            if (!(path is null))
            {
                e.FromQQ.SendPrivateMessage(CQApi.CQCode_Image(path));
            }
        }

        private async Task<string> checkAndGenerate(string msg)
        {
            if (_regexQN.IsMatch(msg))
            {
                var text = _regexQN.Match(msg).Groups[1].Value;
                return await PicQingNi.GenerateImage(text);
            }
            if (_regexMY.IsMatch(msg))
            {
                var text = _regexMY.Match(msg).Groups[1].Value;
                return await PicManYao.GenerateImage(text);
            }

            return null;

        }
    }
}
