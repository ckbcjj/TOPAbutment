using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Common.Tool
{
    /// <summary>
    /// 水印类
    /// </summary>
    public class WaterMark
    {
        private int width;
        private int height;
        private string fontFamily;
        private int fontSize;
        private bool adaptable;
        private FontStyle fontStyle;
        private bool shadow;
        private string backgroundImage;
        private Color bgColor;
        private int left;
        private string resultImage;
        private string text;
        private int top;
        private int alpha;
        private int red;
        private int green;
        private int blue;
        private long quality;
        private string waterImage;
        private string position;

        public WaterMark()
        {
            width = 460;
            height = 30;
            fontFamily = "华文行楷";
            fontSize = 20;
            fontStyle = FontStyle.Regular;
            adaptable = true;
            shadow = false;
            left = 0;
            top = 0;
            alpha = 255;
            red = 0;
            green = 0;
            blue = 0;
            backgroundImage = "";
            waterImage = "";
            quality = 100;
            bgColor = Color.FromArgb(255, 229, 229, 229);
            position = "";
        }

        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily
        {
            get
            {
                return fontFamily;
            }
            set
            {
                fontFamily = value;
            }
        }

        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
            }
        }

        /// <summary>
        /// 文字风格
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                return fontStyle;
            }
            set
            {
                fontStyle = value;
            }
        }

        /// <summary>
        /// 透明度0-255,255表示不透明
        /// </summary>
        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }

        /// <summary>
        /// 水印文字是否使用阴影
        /// </summary>
        public bool Shadow
        {
            get
            {
                return shadow;
            }
            set
            {
                shadow = value;
            }
        }

        /// <summary>
        /// 三基色Red
        /// </summary>
        public int Red
        {
            get
            {
                return red;
            }
            set
            {
                red = value;
            }
        }

        /// <summary>
        /// 三基色Green
        /// </summary>
        public int Green
        {
            get
            {
                return green;
            }
            set
            {
                green = value;
            }
        }

        /// <summary>
        /// 三基色Blue
        /// </summary>
        public int Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = value;
            }
        }

        /// <summary>
        /// 底图
        /// </summary>
        public string BackgroundImage
        {
            get
            {
                return backgroundImage;
            }
            set
            {
                backgroundImage = value;
            }
        }

        /// <summary>
        /// 水印图片
        /// </summary>
        public string WaterImage
        {
            get
            {
                return waterImage;
            }
            set
            {
                waterImage = value;
            }
        }

        /// <summary>
        /// 水印文字的左边距
        /// </summary>
        public int Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        /// <summary>
        /// 水印文字的顶边距
        /// </summary>
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
            }
        }

        /// <summary>
        /// 生成后的图片
        /// </summary>
        public string ResultImage
        {
            get
            {
                return resultImage;
            }
            set
            {
                resultImage = value;
            }
        }

        /// <summary>
        /// 水印文本
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        /// <summary>
        /// 生成图片的宽度
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        /// <summary>
        /// 生成图片的高度
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /// <summary>
        /// 若文字太大，是否根据背景图来调整文字大小，默认为适应
        /// </summary>
        public bool Adaptable
        {
            get
            {
                return adaptable;
            }
            set
            {
                adaptable = value;
            }
        }

        public Color BgColor
        {
            get
            {
                return bgColor;
            }
            set
            {
                bgColor = value;
            }
        }

        /// <summary>
        /// 输出图片质量，质量范围0-100,类型为long
        /// </summary>
        public long Quality
        {
            get
            {
                return quality;
            }
            set
            {
                quality = value;
            }
        }

        /// <summary>
        /// 水印位置
        /// </summary>
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// 立即生成文字水印效果图
        /// </summary>
        /// <returns>生成成功返回true,否则返回false</returns>
        public bool CreateText()
        {
            try
            {
                Bitmap bitmap;
                Graphics g;

                // 使用纯背景色
                if (backgroundImage.Trim() == "")
                {
                    bitmap = new Bitmap(width, height, PixelFormat.Format64bppArgb);
                    g = Graphics.FromImage(bitmap);
                    g.Clear(bgColor);
                }
                else
                {
                    bitmap = new Bitmap(backgroundImage);

                    if (IsPixelFormatIndexed(bitmap.PixelFormat))
                    {
                        Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g2 = Graphics.FromImage(bmp))
                        {
                            g2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g2.SmoothingMode = SmoothingMode.HighQuality;
                            g2.CompositingQuality = CompositingQuality.HighQuality;
                            g2.DrawImage(bitmap, 0, 0);
                        }
                        g = Graphics.FromImage(bmp);
                    }
                    else
                    {
                        g = Graphics.FromImage(bitmap);
                    }
                }

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;

                Font f;
                bool bo;

                try
                {
                    f = new Font(fontFamily, fontSize, fontStyle);
                    bo = true;
                }
                catch (Exception e)
                {
                    f = new Font(fontFamily, fontSize);
                    bo = false;
                }

                SizeF size = g.MeasureString(text, f);

                // 调整文字大小直到能适应图片尺寸
                while (adaptable == true && size.Width > bitmap.Width)
                {
                    fontSize--;
                    if (bo)
                    {
                        f = new Font(fontFamily, fontSize, fontStyle);
                    }
                    else
                    {
                        f = new Font(fontFamily, fontSize);
                    }
                    size = g.MeasureString(text, f);
                }

                Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;

                if (shadow)
                {
                    Brush b2 = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
                    g.DrawString(text, f, b2, left + 2, top + 1);
                }

                SetPosition(bitmap.Width, bitmap.Height, Convert.ToInt32(size.Width), Convert.ToInt32(size.Height));

                g.DrawString(text, f, b, new PointF(left, top), StrFormat);

                System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
                ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)95);

                System.Drawing.Imaging.ImageCodecInfo ici = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];

                bitmap.Save(resultImage, ici, ep);


                bitmap.Dispose();
                g.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 立即生成图片水印效果图
        /// </summary>
        /// <returns>生成成功返回true,否则返回false</returns>
        public bool CreateImage()
        {
            try
            {
                // 获得水印图像
                Image markImg = Image.FromFile(waterImage);

                // 获得底图
                Image img = Image.FromFile(backgroundImage);

                // 创建颜色矩阵
                float[][] ptsArray ={ 
                                            new float[] {1, 0, 0, 0, 0},
                                            new float[] {0, 1, 0, 0, 0},
                                            new float[] {0, 0, 1, 0, 0},
                                            new float[] {0, 0, 0, alpha * (1.0f / 255), 0}, //注意：此处为0.0f为完全透明，1.0f为完全不透明
                                            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
                // 新建一个Image属性
                ImageAttributes imageAttributes = new ImageAttributes();
                // 将颜色矩阵添加到属性
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
                // 生成位图作图区
                Bitmap newBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
                // 设置分辨率
                newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                // 创建Graphics
                Graphics g = Graphics.FromImage(newBitmap);
                // 消除锯齿
                // g.SmoothingMode = SmoothingMode.AntiAlias;

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //  g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量低速度
                ////  g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;//设置取消锯齿
                //  g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality; //高质量低速度复合  
                //  g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//设置高质量插值法 

                // 拷贝原图到作图区
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                // 如果原图过小
                if (markImg.Width > img.Width || markImg.Height > img.Height)
                {
                    System.Drawing.Image.GetThumbnailImageAbort callb = null;
                    // 对水印图片生成缩略图,缩小到原图得1/4
                    System.Drawing.Image newimg = markImg.GetThumbnailImage(img.Width / 4, markImg.Height * img.Width / markImg.Width, callb, new System.IntPtr());

                    SetPosition(newBitmap.Width, newBitmap.Height, newimg.Width, newimg.Height);

                    // 添加水印
                    g.DrawImage(newimg, new Rectangle(left, top, newimg.Width, newimg.Height), 0, 0, newimg.Width, newimg.Height, GraphicsUnit.Pixel, imageAttributes);
                    // 释放缩略图
                    newimg.Dispose();

                    newBitmap.Save(resultImage, ImageFormat.Jpeg);
                }
                //原图足够大
                else
                {
                    SetPosition(newBitmap.Width, newBitmap.Height, markImg.Width, markImg.Height);

                    // 添加水印
                    g.DrawImage(markImg, new Rectangle(left, top, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);

                    newBitmap.Save(resultImage, ImageFormat.Jpeg);
                }

                g.Dispose();
                newBitmap.Dispose();
                img.Dispose();
                markImg.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 定位水印的位置
        /// </summary>
        /// <param name="sWidth">底图的宽</param>
        /// <param name="sHeight">底图的高</param>
        /// <param name="wWidth">水印图片的宽</param>
        /// <param name="wHeight">水印图片的高</param>
        public void SetPosition(int sWidth, int sHeight, int wWidth, int wHeight)
        {
            // 左上
            if (position.Equals("1"))
            {
                left = 0;
                top = 0;
            }
            // 左中
            if (position.Equals("2"))
            {
                left = 0;
                top = (sHeight - wHeight) / 2;
            }
            // 左下
            if (position.Equals("3"))
            {
                left = 0;
                top = sHeight - wHeight;
            }

            // 中上
            if (position.Equals("4"))
            {
                left = (sWidth - wWidth) / 2;
                top = 0;
            }
            // 中中
            if (position.Equals("5"))
            {
                left = (sWidth - wWidth) / 2;
                top = (sHeight - wHeight) / 2;
            }
            // 中下
            if (position.Equals("6"))
            {
                left = (sWidth - wWidth) / 2;
                top = sHeight - wHeight;
            }

            // 右上
            if (position.Equals("7"))
            {
                left = sWidth - wWidth;
                top = 0;
            }
            // 右中
            if (position.Equals("8"))
            {
                left = sWidth - wWidth;
                top = (sHeight - wHeight) / 2;
            }
            // 右下
            if (position.Equals("9"))
            {
                left = sWidth - wWidth;
                top = sHeight - wHeight;
            }
        }

        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare, PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// 无法从带有索引像素格式的图像创建graphics对象
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

        public static bool BingData(WaterMarkInfo wmi)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExcuteDataTable("select * from waterMark where id=1", null, SqlHelper.conStr);
            if (dt.Rows.Count > 0)
            {
                wmi.WaterMarkText = dt.Rows[0]["waterMarkText"].ToString();
                wmi.WaterMarkFontFamily = dt.Rows[0]["waterMarkFontFamily"].ToString();
                wmi.WaterMarkFontSize = Convert.ToInt32(dt.Rows[0]["waterMarkFontSize"]);
                wmi.WaterMarkFontStyle = Convert.ToInt32(dt.Rows[0]["waterMarkFontStyle"]);
                wmi.WaterMarkShadow = Convert.ToInt32(dt.Rows[0]["waterMarkShadow"]);
                wmi.WaterMarkColor = dt.Rows[0]["waterMarkColor"].ToString();
                wmi.WaterMarkAlpha = Convert.ToInt32(dt.Rows[0]["waterMarkAlpha"]);
                wmi.WaterMarkPostion = Convert.ToInt32(dt.Rows[0]["waterMarkPostion"]);
                wmi.WaterMarkRed = Convert.ToInt32(dt.Rows[0]["waterMarkRed"]);
                wmi.WaterMarkGreen = Convert.ToInt32(dt.Rows[0]["waterMarkGreen"]);
                wmi.WaterMarkBlue = Convert.ToInt32(dt.Rows[0]["waterMarkBlue"]);
                wmi.WaterMarkLeft = Convert.ToInt32(dt.Rows[0]["waterMarkLeft"]);
                wmi.WaterMarkTop = Convert.ToInt32(dt.Rows[0]["waterMarkTop"]);
            }
            return true;
        }

    }
}
