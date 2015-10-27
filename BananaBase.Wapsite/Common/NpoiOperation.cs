using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Banana.Wapsite
{
    public class NpoiOperation
    {

        /// <summary>
        /// 将Excel中指定的Sheet转换成DataTable
        /// </summary>
        /// <param name="excel"></param>
        /// <param name="index"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static DataTable ConvertExcelSheetToDataTable(string excel, int index, bool header)
        {
            try
            {
                DataTable dt = new DataTable(Path.GetFileNameWithoutExtension(excel) + "_Sheet" + index);

                IWorkbook workbook;

                using (FileStream file = new FileStream(excel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //workbook = new HSSFWorkbook(file);//NPOI 1.0
                    workbook = WorkbookFactory.Create(file);//NPOI 2.0
                }

                ISheet sheet = workbook.GetSheetAt(index);
                //   ISheet sheet = workbook.GetSheet(sheetName);

                var rows = sheet.GetRowEnumerator();

                rows.MoveNext();

                //IRow row = (HSSFRow)rows.Current;//excel2003
                //IRow row = (XSSFRow)rows.Current;//excel2007
                IRow row = (IRow)rows.Current;
                for (int i = 0; i < row.LastCellNum; i++)
                {

                    ICell cell = row.GetCell(i);

                    string columnName = header ? cell.StringCellValue : i.ToString();

                    dt.Columns.Add(columnName, typeof(string));

                }

                if (!header)
                {

                    DataRow first = dt.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {

                        ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            first[i] = string.Empty;
                        }
                        else
                        {

                            if (cell.CellType == CellType.NUMERIC)
                            {
                                cell.SetCellType(CellType.STRING);
                                first[i] = cell.StringCellValue;
                            }
                            else
                            {
                                first[i] = cell.StringCellValue;
                            }
                        }

                    }

                    dt.Rows.Add(first);

                }


                //获取各列的值
                while (rows.MoveNext())
                {

                    //row = (HSSFRow)rows.Current;
                    //row = (XSSFRow)rows.Current;
                    row = (IRow)rows.Current;
                    DataRow dataRow = dt.NewRow();



                    for (int i = 0; i < row.LastCellNum; i++)
                    {

                        ICell cell = row.GetCell(i);
                        if (cell == null)
                        {
                            dataRow[i] = string.Empty;
                        }
                        else
                        {
                            if (cell.CellType == CellType.NUMERIC)
                            {
                                cell.SetCellType(CellType.STRING);
                                dataRow[i] = cell.StringCellValue;
                            }
                            else
                            {
                                dataRow[i] = cell.StringCellValue;
                            }

                        }
                    }

                    dt.Rows.Add(dataRow);

                }
                return dt;
            }

            catch (FileNotFoundException e)
            {
                HttpContext.Current.Response.Write("<script>alert('未找到Excel文件,请重新上传！')</script>");
                return null;

            }
            catch (IOException e)
            {
                HttpContext.Current.Response.Write("<script>alert('读取Excel文件失败,请重新上传再尝试！')</script>");
                return null;
            }
            catch (DuplicateNameException e)
            {
                HttpContext.Current.Response.Write("<script>alert('存在重复的列,请检查！')</script>");
                return null;
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("<script>alert('" + e.Message + "')</script>");

                return null;
            }

        }
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }


    }
}