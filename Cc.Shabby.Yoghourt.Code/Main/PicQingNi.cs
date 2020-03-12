using Cc.Shabby.Yoghourt.Code.Properties;
using Native.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cc.Shabby.Yoghourt.Code.Main
{
    public static class PicQingNi
    {

        //private static PointF[] _point;
        //private static Font _font;
        //private static SolidBrush _brush;
        private static CQLog _log;
        private static CQApi _api;
        private static string _appDir;
        private static bool _enable;

        public static void Init(CQApi api, CQLog log)
        {
            _appDir = api.AppDirectory;
            _log = log;
            _api = api;
            //_brush = new SolidBrush(Color.FromArgb(36, 68, 247));
            //_point = new PointF[] { new PointF(70, 50), new PointF(175, 50), new PointF(70, 160), new PointF(175, 160) };
            if (!Directory.Exists($"{ Directory.GetCurrentDirectory()}\\data\\image\\yoghourt"))
                Directory.CreateDirectory($"{ Directory.GetCurrentDirectory()}\\data\\image\\yoghourt");

            if (!File.Exists($"{api.AppDirectory}HaiPaiQiangDiaoGunShiChaoHeiJian-2_0.ttf"))
            {
                _log.Error("初始化", $"未发现字体文件-{_appDir}HaiPaiQiangDiaoGunShiChaoHeiJian-2_0.ttf");
                return;
            }

            if (!File.Exists($"{_appDir}bg.jpg"))
            {
                _log.Error("初始化", $"未发现背景文件-{_appDir}bg.jpg");
                return;
            }

            _enable = true;
        }

        public static async Task<string> GenerateImage(string text)
        {
            if (!_enable) return null;
            return await Task.Run(() =>
            {
                //try
                //{
                var bmp = new Bitmap($"{ _appDir }bg.jpg");
                var image = Graphics.FromImage(bmp);
                image.TextRenderingHint = TextRenderingHint.AntiAlias;
                _log.Debug("获取资源", "获取bmp图片成功");
                //if (_font == null || _brush == null || _point.Length == 0)
                //    Init(_api, _log);
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile($"{_api.AppDirectory}HaiPaiQiangDiaoGunShiChaoHeiJian-2_0.ttf");
                var font = new Font(pfc.Families[0], 65);
                var brush = new SolidBrush(Color.FromArgb(36, 68, 247));
                var point = new PointF[] { new PointF(70, 50), new PointF(175, 50), new PointF(70, 160), new PointF(175, 160) };
                _log.Debug("资源检查", $"{font.Name}");
                _log.Debug("资源检查", $"{brush.Color}");
                _log.Debug("资源检查", $"{point.Length}");
                for (int i = 0; i < Math.Min(text.Length, point.Length); i++)
                {
                    image.DrawString(Convert.ToString(text[i]), font, brush, point[i]);
                }
                var path = $"yoghourt\\{Guid.NewGuid()}.jpg";
                _log.Debug("输出文件", $"{Directory.GetCurrentDirectory()}\\data\\image\\{path}");
                bmp.Save($"{Directory.GetCurrentDirectory()}\\data\\image\\{path}");
                image.Dispose();
                bmp.Dispose();
                return path;
                //}
                //catch
                //{
                //    _log.Error("生成图片", $"生成图片{text}失败");
                //    return null;
                //}

            });



        }

    }
}
