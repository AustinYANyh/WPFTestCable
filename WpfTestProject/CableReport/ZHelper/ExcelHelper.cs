// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：ExcelHelper.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月08日 10:12
// *   描    述：
// ==============================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HW.JD.CableReport.Model;
using Microsoft.Office.Interop.Excel;
using WpfTestProject.CableReport.Window;
using DataTable = System.Data.DataTable;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace HW.JD.CableReport.ZHelper
{
    public class ExcelHelper:NotifyBase
    {
        private string filePath = "default";
        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                OnPropertyChanged(nameof(FilePath));
            } 
        }

        private List<string> ColumnNames = new List<string>() {"default"};
        private List<string> workSheetNames { get; set; } = new List<string>();

        public List<string> GetColumnNames() => ColumnNames;
        public List<string> GetWorkSheetNames() => workSheetNames;

        private Dictionary<string, List<ExcelData>> data = new Dictionary<string, List<ExcelData>>();

        public Dictionary<string, List<ExcelData>> GetData() => data;

        public bool GetExcelData(bool hasTitle = true)
        {
            data = new Dictionary<string, List<ExcelData>>();
            var openFile = new OpenFileDialog
            {
                Title = @"导入清册",
                Filter = @"Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false
            };
            if (openFile.ShowDialog() == false) return false;
            FilePath = openFile.FileName;

            workSheetNames = new List<string>();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            object oMissing = Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            var dt = new DataTable();
            
            try
            {
                workbook = app.Workbooks.Open(FilePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                Sheets sheets = workbook.Sheets;
                
                for (int i = 1; i <= sheets.Count; i++)
                {
                    List<ExcelData> result = new List<ExcelData>();
                    //将数据读入到DataTable中
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(i); //读取第一张表  
                    if (worksheet == null)
                    {
                        MessageBox.Show("读取Excel时，获取工作簿失败！");
                        continue;
                    }
                    workSheetNames.Add(worksheet.Name);
                    
                    int iRowCount = worksheet.UsedRange.Rows.Count;
                    int iColCount = worksheet.UsedRange.Columns.Count;

                    //生成列头
                    ColumnNames = new List<string>();
                    for (var j = 0; j < iColCount; j++)
                    {
                        var name = "column" + j;
                        if (hasTitle)
                        {
                            var txt = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, j + 1]).Text.ToString();
                            if (!string.IsNullOrWhiteSpace(txt)) name = txt;
                        }

                        while (dt.Columns.Contains(name)) name = name + "_1"; //重复行名称会报错。
                        dt.Columns.Add(new DataColumn(name, typeof(string)));
                        ColumnNames.Add(name);
                    }
                    
                    //生成行数据
                    var rowIdx = hasTitle ? 2 : 1;
                    for (var iRow = rowIdx; iRow <= iRowCount; iRow++)
                    {
                        var dr = dt.NewRow();
                        for (var iCol = 1; iCol <= iColCount; iCol++)
                        {
                            var range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRow, iCol];
                            dr[iCol - 1] = range.Value2 == null ? "" : range.Text.ToString();
                        }

                        dt.Rows.Add(dr);
                    }

                    result.Add(new ExcelData { DataRows = dt.Rows, ColumnNames = ColumnNames, });
                    if (data.ContainsKey(worksheet.Name))
                    {
                        data[worksheet.Name].AddRange(result);
                    }
                    else data.Add(worksheet.Name,result);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开工作簿失败");
                return false;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook?.Close(false, oMissing, oMissing);
                    Marshal.ReleaseComObject(workbook);
                }

                workbook = null;
                app.Workbooks.Close();
                app.Quit();
                Marshal.ReleaseComObject(app);
                app = null;
            }
        }
    }
    
    public class ExcelData
    {
        public DataRowCollection DataRows { get; set; }
        public List<string> ColumnNames { get; set; } 
    }
}