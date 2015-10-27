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
        //��ȡҳ���ļ���Ϣ(ͨ��ҳ���ֱַ�ӻ�ȡ)
        private string GetWebContent(string Url)
        {
            #region
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //����һ��HttpWebRequest����
                request.Timeout = 30000;
                //�������ӳ�ʱʱ��
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
        /// post��ʽ��ȡҳ���ļ�
        /// </summary>
        /// <param name="Url">ҳ���ļ���ַ</param>
        /// <param name="Action">��Ҫ�͵Ĳ���:city=����</param>
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

        #region �ļ�������

        /// <summary>
        /// �����ļ��Ƿ����
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>�Ƿ����</returns>
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
        /// ��ȡ�ļ���
        /// </summary>
        /// <param name="name">�ļ�������</param>
        /// <param name="parentName">·��</param>
        public static void FolderCreate(string name, string parentName)
        {
            DirectoryInfo di = new DirectoryInfo(parentName);
            if (!FolderExists(parentName + "/" + name))
                di.CreateSubdirectory(name);
        }

        /// <summary>
        /// ɾ���ļ���
        /// </summary>
        /// <param name="path"></param>
        public static void FolderDelete(string path)
        {
            Directory.Delete(path);
        }
        /// <summary>
        /// �����ļ���
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
        /// �����ļ��Ƿ����
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>�Ƿ����</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fileUrl">�ļ���ַ</param>
        public static void fileDelete(string fileUrl)
        {
            System.IO.File.Delete(fileUrl);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="content">���ɵ�����</param>
        /// <param name="filename">�ļ�·��</param>
        public static void FileWrite(string content, string filename)
        {
            #region �����ļ�
            Encoding code = Encoding.UTF8;
            StreamWriter sw = null;
            // д�ļ�
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
        /// ��ȡ�ļ�
        /// </summary>
        /// <param name="fileUrl">�ļ�·��</param>
        public static string FileRead(string fileUrl)
        {
            #region �����ļ�
            Encoding code = Encoding.GetEncoding("gb2312");
            // ��ȡģ���ļ� 
            string temp = HttpContext.Current.Server.MapPath(fileUrl);
            StreamReader sr = null;
            string str = "";
            try
            {
                sr = new StreamReader(temp, code);
                str = sr.ReadToEnd(); // ��ȡ�ļ� 
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
        /// �����ļ�
        /// </summary>
        /// <param name="fileUrl">�ļ�·��</param>
        public static void FileCopy(string sourceUrl, string moveToUrl)
        {
            File.Copy(sourceUrl, moveToUrl, true);
        }

        #endregion


        #region ͼƬ��������ͼ��ˮӡ
        /**/
        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·����</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ</param> 
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
                    case "HW"://ָ���߿����ţ����ܱ��Σ� 
                        break;
                    case "W"://ָ�����߰����� 
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://ָ���ߣ�������
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://ָ���߿�ü��������Σ� 
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

            //�½�һ��bmpͼƬ
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //�½�һ������
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(System.Drawing.Color.Transparent);


            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
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
        /// ��ͼƬ����������ˮӡ
        /// </summary>
        /// <param name="Path">ԭ������ͼƬ·��</param>
        /// <param name="Path_sy">���ɵĴ�����ˮӡ��ͼƬ·��</param>
        /// <param name="addText">ˮӡ����������</param>
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
        /// ��ͼƬ������ͼƬˮӡ
        /// </summary>
        /// <param name="Path">ԭ������ͼƬ·��</param>
        /// <param name="Path_syp">���ɵĴ�ͼƬˮӡ��ͼƬ·��</param>
        /// <param name="Path_sypf">ˮӡͼƬ·��</param>
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
        /// C#����ϴ�ͼƬ�Ƿ�ȫ����
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
                    using (StreamReader sr = new StreamReader(strPictureFilePath))    //���ı��ļ���ʽ��ȡͼƬ����
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            str_Temp.Append(line + ",");
                        }
                        //����Ƿ����Σ���ַ���
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

        //�����ж��ļ����͵Ĺؼ�����
        public static string IsAllowedExtension(FileUpload hifile)
        {
            string strReturn = "";
            StringBuilder str_Temp = new StringBuilder();
            System.IO.Stream fs = hifile.PostedFile.InputStream;
            using (StreamReader sr = new StreamReader(fs))    //���ı��ļ���ʽ��ȡͼƬ����
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    str_Temp.Append(line + ",");
                }
                //����Ƿ����Σ���ַ���
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
