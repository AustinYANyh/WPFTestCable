// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：ExportSecond.xaml.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月07日 15:32
// *   描    述：
// ==============================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HW.JD.CableReport.Model;
using HW.JD.CableReport.ZHelper;
using Microsoft.Office.Interop.Excel;
using Microsoft.Practices.Prism;

namespace WpfTestProject.CableReport.Window
{
    public partial class ExportSecond : System.Windows.Window
    {
        public ExcelHelper ExcelHelper { get; set; }= new ExcelHelper();
        public ExportSecond(List<DataGridInfo> infos)
        {
            InitializeComponent();

            SheetData = new ObservableCollection<SheetInfo>
            {
                new SheetInfo() {IsChecked = true, Content = "电力电缆表"},
                new SheetInfo() {IsChecked = false, Content = "电缆标注"}
            };

            DataGridData = new ObservableCollection<DataGridInfo>(infos);
            UpdateExcelColumnInDataGrid();

            DataContext = this;
        }

        private void UpdateExcelColumnInDataGrid()
        {
            foreach (DataGridInfo dataGridInfo in DataGridData)
            {
                dataGridInfo.ExcelIndex = 0;
                dataGridInfo.ExcelColumns.Clear();
                dataGridInfo.ExcelColumns.AddRange(ExcelHelper.GetColumnNames());
                dataGridInfo.UpdateDisPlayCol();
            }
        }

        // private List<string> columns = new List<string>() {"A", "B", "C"};

        public ObservableCollection<SheetInfo> SheetData { get; set; } = new ObservableCollection<SheetInfo>();
        public ObservableCollection<DataGridInfo> DataGridData { get; set; } = new ObservableCollection<DataGridInfo>();

        private List<ExportDataGridInfo> ExportResult = new List<ExportDataGridInfo>();

        public List<ExportDataGridInfo> GetExportResult() => ExportResult;

        private void ExportBtnOnClick(object sender, RoutedEventArgs e)
        {
            if(!ExcelHelper.GetExcelData())return;
            
            SheetData.Clear();
            foreach (string workSheetName in ExcelHelper.GetWorkSheetNames())
            {
                SheetData.Add(new SheetInfo() {IsChecked = false, Content = workSheetName});
            }
            UpdateExcelColumnInDataGrid();
        }

        private void ExportToMainBtnOnClick(object sender, RoutedEventArgs e)
        {
            if (!SheetData.Any(x => x.IsChecked))
            {
                MessageBox.Show("不选工作簿是导不了的");
                return;
            }

            ExportResult = new List<ExportDataGridInfo>();

            //ToDo 起始行开始读取

            foreach (SheetInfo sheetInfo in SheetData)
            {
                if (!sheetInfo.IsChecked) continue;

                Dictionary<string,List<ExcelData>> dictionary = ExcelHelper.GetData();
                List<ExcelData> excelData = dictionary[sheetInfo.Content];
                
                //按行读取
                foreach (ExcelData data in excelData)
                {
                    for (var i = 0; i < data.DataRows.Count; i++)
                    {
                        var dataDataRow = data.DataRows[i];
                        ExportDataGridInfo resultInfo = new ExportDataGridInfo() {Number = (i + 1).ToString()};
                        //按照选择的列读取
                        foreach (DataGridInfo dataGridInfo in DataGridData)
                        {
                            //没有这一列
                            if (!data.ColumnNames.Contains(dataGridInfo.DisplayCol) || dataGridInfo.DataType == InfoType.None)
                            {
                                continue;
                            }

                            string resultStr = Convert.ToString(dataDataRow[dataGridInfo.DisplayCol]);

                            //列数据有误
                            if (string.IsNullOrEmpty(resultStr)) continue;
                            
                            if (dataGridInfo.DataType == InfoType.CableInfo) resultInfo.CableInfo = resultStr;
                            if (dataGridInfo.DataType == InfoType.CableNumber) resultInfo.CableNumber = resultStr;
                            if (dataGridInfo.DataType == InfoType.CableSize) resultInfo.CableSize = resultStr;
                            if (dataGridInfo.DataType == InfoType.StartPointName) resultInfo.StartPointName = resultStr;
                            if (dataGridInfo.DataType == InfoType.EndPointName) resultInfo.EndPointName = resultStr;
                            if (dataGridInfo.DataType == InfoType.StartPointNumber) resultInfo.StartPointNumber = resultStr;
                            if (dataGridInfo.DataType == InfoType.EndPointNumber) resultInfo.EndPointNumber = resultStr;
                            
                            //新增列
                            if (dataGridInfo.DataType == InfoType.NewCol1) resultInfo.NewCol1 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol2) resultInfo.NewCol2 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol3) resultInfo.NewCol3 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol4) resultInfo.NewCol4 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol5) resultInfo.NewCol5 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol6) resultInfo.NewCol6 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol7) resultInfo.NewCol7 = resultStr;
                            if (dataGridInfo.DataType == InfoType.NewCol8) resultInfo.NewCol8 = resultStr;
                        }

                        ExportResult.Add(resultInfo);
                    }
                }
            }
            this.Close();
        }
    }
}