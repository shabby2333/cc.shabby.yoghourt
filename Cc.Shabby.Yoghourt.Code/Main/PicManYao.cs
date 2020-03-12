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
    public static class PicManYao
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
            //_brush = new SolidBrush(Color.FromArgb(252, 136, 185));
            //_point = new PointF[] { new PointF(70, 50), new PointF(175, 50), new PointF(70, 160), new PointF(175, 160) };
            if (!Directory.Exists($"{ Directory.GetCurrentDirectory()}\\data\\image\\yoghourt"))
                Directory.CreateDirectory($"{ Directory.GetCurrentDirectory()}\\data\\image\\yoghourt");
            
            if (!File.Exists($"{api.AppDirectory}HYZhuZiBiXinTiW-2.ttf"))
            {
                _log.Error("初始化", $"未发现字体文件-{_appDir}HYZhuZiBiXinTiW-2.ttf");
                return;
            }

            if (!File.Exists($"{_appDir}btf.jpg"))
            {
                _log.Error("初始化", $"未发现背景文件-{_appDir}btf.jpg");
                return;
            }

            _enable = true;
        }

        public static async Task<string> GenerateImage(string text)
        {
            if (!_enable) return null;
            return await Task.Run(() =>
            {
               // try
                {
                    var bmp = new Bitmap($"{ _appDir }btf.jpg");
                    var image = Graphics.FromImage(bmp);
                    image.TextRenderingHint = TextRenderingHint.AntiAlias;
                    _log.Debug("获取资源", "获取bmp图片成功");
                    //if (_font == null || _brush == null)
                    //   Init(_api, _log);
                    PrivateFontCollection pfc = new PrivateFontCollection();
                    pfc.AddFontFile($"{_api.AppDirectory}HYZhuZiBiXinTiW-2.ttf");
                    var font = new Font(pfc.Families[0], 65);
                    var brush = new SolidBrush(Color.FromArgb(252, 136, 185));

                    _log.Debug("资源检查", $"{font}");
                    _log.Debug("资源检查", $"{brush}");
                    var point = new PointF(300, 260);
                    var size = image.MeasureString(text, font);
                    point.X -= size.Width / 2;
                    point.Y -= size.Height / 2;
                    image.DrawString(text, font, brush, point);

                    var path = $"yoghourt\\{Guid.NewGuid()}.jpg";
                    _log.Debug("输出文件", $"{Directory.GetCurrentDirectory()}\\data\\image\\{path}");
                    bmp.Save($"{Directory.GetCurrentDirectory()}\\data\\image\\{path}");
                    image.Dispose();
                    bmp.Dispose();
                    return path;
                }
               // catch
                //{
                //    _log.Error("生成图片", $"生成图片{text}失败");
               //     return null;
                //}

            });
            
        }

    }
}
