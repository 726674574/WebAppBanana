using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Text;
using System.Net;

namespace WebHelp
{
    public class FileHelps
    {
        //获取页面文件信息(通过页面地址直接获取)
        private string GetWebContent(string Url)
        {
            #region
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
            }
            catch
            {

            }
            return strResult;
            #endregion
        }

        /// <summary>
        /// post方式获取页面文件
        /// </summary>
        /// <param name="Url">页面文件地址</param>
        /// <param name="Action">需要送的参数:city=福州</param>
        /// <returns></returns>
        private string GetWebContent(string Url, string Action)
        {
            #region
            string strResult = "";
            try
            {
                System.Text.Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                WebRequest req = WebRequest.Create(Url);
                string postData = Action;
                Byte[] bytes = encode.GetBytes(postData);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bytes.Length;
                Stream sendStream = req.GetRequestStream();
                sendStream.Write(bytes, 0, bytes.Length);
                sendStream.Close();
                WebResponse rep = req.GetResponse();
                Stream getStream = rep.GetResponseStream();
                StreamReader sr = new StreamReader(getStream, encode);
                strResult = sr.ReadToEnd();
            }
            catch { }
            return strResult;
            #endregion
        }

        #region 文件操作类

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FolderExists(string path)
        {
            DirectoryInfo mydir = new DirectoryInfo(path);
            if (mydir.Exists)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// 读取文件夹
        /// </summary>
        /// <param name="name">文件夹名称</param>
        /// <param name="parentName">路径</param>
        public static void FolderCreate(string name, string parentName)
        {
            DirectoryInfo di = new DirectoryInfo(parentName);
            if (!FolderExists(parentName + "/" + name))
                di.CreateSubdirectory(name);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void FolderDelete(string path)
        {
            Directory.Delete(path);
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void FolderCopy(string source, string destination)
        {
            String[] files;
            if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                destination += Path.DirectorySeparatorChar;
            if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
            files = Directory.GetFileSystemEntries(source);
            foreach (string element in files)
            {
                if (Directory.Exists(element))
                    FolderCopy(element, destination + Path.GetFileName(element));
                else
                    File.Copy(element, destination + Path.GetFileName(element), true);
            }
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileUrl">文件地址</param>
        public static void fileDelete(string fileUrl)
        {
            System.IO.File.Delete(fileUrl);
        }

        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="content">生成的内容</param>
        /// <param name="filename">文件路径</param>
        public static void FileWrite(string content, string filename)
        {
            #region 生成文件
            Encoding code = Encoding.UTF8;
            StreamWriter sw = null;
            // 写文件
            try
            {
                sw = new StreamWriter(filename, false, code);
                sw.Write(content);
                sw.Flush();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
            finally
            {
                sw.Close();
            }
            #endregion
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileUrl">文件路径</param>
        public static string FileRead(string fileUrl)
        {
            #region 生成文件
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件 
            string temp = HttpContext.Current.Server.MapPath(fileUrl);
            StreamReader sr = null;
            string str = "";
            try
            {
                sr = new StreamReader(temp, code);
                str = sr.ReadToEnd(); // 读取文件 
            }
            catch (Exception exp)
            {
                HttpContext.Current.Response.Write(exp.Message);
                HttpContext.Current.Response.End();
            }
            finally
            {
                sr.Close();
            }
            return str;
            #endregion
        }


        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="fileUrl">文件路径</param>
        public static void FileCopy(string sourceUrl, string moveToUrl)
        {
            File.Copy(sourceUrl, moveToUrl, true);
        }

        #endregion


        #region 图片生成缩略图、水印
        /**/
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param> 
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            #region
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (towidth > ow && toheight > oh)
            {
                towidth = ow;
                toheight = oh;
            }
            else
            {
                switch (mode)
                {
                    case "HW"://指定高宽缩放（可能变形） 
                        break;
                    case "W"://指定宽，高按比例 
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://指定高宽裁减（不变形） 
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);


            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            #endregion
        }

        /**/
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        /// <param name="addText">水印的文字内容</param>
        protected void AddShuiYinWord(string Path, string Path_sy, string addText)
        {
            #region
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 16);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

            g.DrawString(addText, f, b, 15, 15);
            g.Dispose();

            image.Save(Path_sy);
            image.Dispose();
            #endregion
        }

        /**/
        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
        {  
            #region
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            image.Save(Path_syp);
            copyImage.Dispose();
            image.Dispose();
            #endregion
        }

        #endregion

        public static bool CheckPictureSafe(string strPictureFilePath,string x)
        {
            return true;
        }

        /// <summary>
        /// C#检测上传图片是否安全函数
        /// </summary>
        /// <param name="strPictureFilePath"></param>
        public static bool CheckPictureSafe(string strPictureFilePath)  
        {
            bool strReturn = true;
            if (!File.Exists(strPictureFilePath))
            {
                StringBuilder str_Temp = new StringBuilder();
                try
                {
                    using (StreamReader sr = new StreamReader(strPictureFilePath))    //按文本文件方式读取图片内容
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            str_Temp.Append(line + ",");
                        }
                        //检测是否包含危险字符串
                        if (str_Temp == null)
                        {
                            strReturn = false;
                        }
                        else
                        {
                            str_Temp = str_Temp.Replace("'", "''");
                            string DangerString = "script|iframe|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=|include|filesystemobject|shell.application";
                            string[] sArray = DangerString.Split('|');
                            foreach (string i in sArray)
                            {
                                if (str_Temp.ToString().Contains(i))
                                {
                                    strReturn = true;
                                    break;
                                }
                            }
                        }
                        sr.Close();
                    }
                    if (strReturn)
                    {
                        File.Delete(strPictureFilePath);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return strReturn;
        }

        //真正判断文件类型的关键函数
        public static string IsAllowedExtension(FileUpload hifile)
        {
            string strReturn = "";
            StringBuilder str_Temp = new StringBuilder();
            System.IO.Stream fs = hifile.PostedFile.InputStream;
            using (StreamReader sr = new StreamReader(fs))    //按文本文件方式读取图片内容
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    str_Temp.Append(line + ",");
                }
                //检测是否包含危险字符串
                if (str_Temp == null)
                {
                    strReturn = "";
                }
                else
                {
                    str_Temp = str_Temp.Replace("'", "''");
                    string DangerString = "script|iframe|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=|include|filesystemobject|shell.application";
                    string[] sArray = DangerString.Split('|');
                    foreach (string i in sArray)
                    {
                        if (str_Temp.ToString().ToLower().Contains(i))
                        {
                            strReturn = i;
                            break;
                        }
                    }
                }
                sr.Close();
            }
            return strReturn;
        }

        public static int[,] GetImgWandH(string file)
        {
            using (FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/")+file, FileMode.Open, FileAccess.Read))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                int width = image.Width;
                int height = image.Height;

                return new int[,] { { width, height } };
            }
        }
           
    }
}
