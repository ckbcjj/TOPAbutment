using System.Drawing;

namespace Common.Tool
{
    public class WaterMarkInfo
    {
        private string waterMarkText;
        private string waterMarkFontFamily;
        private int waterMarkFontSize;
        private int waterMarkFontStyle;
        private int waterMarkShadow;
        private string waterMarkColor;
        private int waterMarkAlpha;
        private int waterMarkPostion;
        private int waterMarkRed;
        private int waterMarkGreen;
        private int waterMarkBlue;
        private int waterMarkLeft;
        private int waterMarkTop;

        /// <summary>
        /// 水印文本
        /// </summary>
        public string WaterMarkText
        {
            get
            {
                return waterMarkText;
            }
            set
            {
                waterMarkText = value;
            }
        }

        /// <summary>
        /// 水印字体
        /// </summary>
        public string WaterMarkFontFamily
        {
            get
            {
                return waterMarkFontFamily;
            }
            set
            {
                waterMarkFontFamily = value;
            }
        }

        /// <summary>
        /// 水印字体大小
        /// </summary>
        public int WaterMarkFontSize
        {
            get
            {
                return waterMarkFontSize;
            }
            set
            {
                waterMarkFontSize = value;
            }
        }

        /// <summary>
        /// 水印文本样式
        /// </summary>
        public int WaterMarkFontStyle
        {
            get
            {
                return waterMarkFontStyle;
            }
            set
            {
                waterMarkFontStyle = value;
            }
        }

        /// <summary>
        /// 水印文本是否阴影
        /// </summary>
        public int WaterMarkShadow
        {
            get
            {
                return waterMarkShadow;
            }
            set
            {
                waterMarkShadow = value;
            }
        }

        /// <summary>
        /// 水印文本颜色
        /// </summary>
        public string WaterMarkColor
        {
            get
            {
                return waterMarkColor;
            }
            set
            {
                waterMarkColor = value;
            }
        }

        /// <summary>
        /// 水印文本透明度
        /// </summary>
        public int WaterMarkAlpha
        {
            get
            {
                return waterMarkAlpha;
            }
            set
            {
                waterMarkAlpha = value;
            }
        }

        /// <summary>
        /// 水印文本位置
        /// </summary>
        public int WaterMarkPostion
        {
            get
            {
                return waterMarkPostion;
            }
            set
            {
                waterMarkPostion = value;
            }
        }

        /// <summary>
        /// 三基色Red
        /// </summary>
        public int WaterMarkRed
        {
            get
            {
                return waterMarkRed;
            }
            set
            {
                waterMarkRed = value;
            }
        }

        /// <summary>
        /// 三基色Green
        /// </summary>
        public int WaterMarkGreen
        {
            get
            {
                return waterMarkGreen;
            }
            set
            {
                waterMarkGreen = value;
            }
        }

        /// <summary>
        /// 三基色Blue
        /// </summary>
        public int WaterMarkBlue
        {
            get
            {
                return waterMarkBlue;
            }
            set
            {
                waterMarkBlue = value;
            }
        }

        /// <summary>
        /// 水印文字的左边距
        /// </summary>
        public int WaterMarkLeft
        {
            get
            {
                return waterMarkLeft;
            }
            set
            {
                waterMarkLeft = value;
            }
        }

        /// <summary>
        /// 水印文字的顶边距
        /// </summary>
        public int WaterMarkTop
        {
            get
            {
                return waterMarkTop;
            }
            set
            {
                waterMarkTop = value;
            }
        }

        public static void CreateWaterMark(WaterMarkInfo wmi, string imgPath, string waterImg)
        {
            WaterMark waterMarkHelper = new WaterMark();
            waterMarkHelper.Blue = wmi.WaterMarkBlue;
            waterMarkHelper.FontFamily = wmi.WaterMarkFontFamily;
            waterMarkHelper.FontSize = wmi.WaterMarkFontSize;
            waterMarkHelper.FontStyle = SetFontStyle(wmi.WaterMarkFontStyle);
            waterMarkHelper.Green = wmi.WaterMarkGreen;
            waterMarkHelper.Position = wmi.WaterMarkPostion.ToString();
            waterMarkHelper.Red = wmi.WaterMarkRed;
            waterMarkHelper.Shadow = wmi.WaterMarkShadow == 0 ? false : true;
            waterMarkHelper.Text = wmi.WaterMarkText;
            waterMarkHelper.Alpha = wmi.WaterMarkAlpha;

            waterMarkHelper.BackgroundImage = imgPath;
            string imagePathTemp = imgPath.Insert(imgPath.LastIndexOf('.'), "w");
            waterMarkHelper.ResultImage = imagePathTemp;

            waterMarkHelper.WaterImage = waterImg;

            // waterMarkHelper.CreateText();
            waterMarkHelper.CreateImage();


        }
        public static FontStyle SetFontStyle(int waterMarkFontStyle)
        {
            if (waterMarkFontStyle == 1)
            {
                return FontStyle.Regular;
            }
            if (waterMarkFontStyle == 2)
            {
                return FontStyle.Bold;
            }
            if (waterMarkFontStyle == 3)
            {
                return FontStyle.Italic;
            }
            if (waterMarkFontStyle == 4)
            {
                return FontStyle.Underline;
            }

            return FontStyle.Regular;
        }
    }
}
