﻿// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：ExportMain.xaml.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月07日 11:16
// *   描    述：
// ==============================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HW.JD.CableReport;
using HW.JD.CableReport.Model;
using HW.JD.CableReport.ZHelper;
using WpfTestProject.CableReport.UserControl;

namespace WpfTestProject.CableReport.Window
{
    public partial class ExportMain : System.Windows.Window
    {
        public ExportMain()
        {
            InitializeComponent();
            
            DataInfos = new ObservableCollection<ExportDataGridInfo>
            {
                new ExportDataGridInfo(){Number = "1",CableNumber = "xx", CableInfo = "123", CableSize = "xx-xx-yy", EndPointNumber = "999", EndPointName = "End", StartPointNumber = "111",StartPointName = "Start"},
                new ExportDataGridInfo(){Number = "2",CableNumber = "YY", CableInfo = "123", CableSize = "xx-xx-yy", EndPointNumber = "999", EndPointName = "End", StartPointNumber = "111",StartPointName = "Start"},
                new ExportDataGridInfo(){Number = "3",CableNumber = "ZZ", CableInfo = "123", CableSize = "xx-xx-yy", EndPointNumber = "999", EndPointName = "End", StartPointNumber = "111",StartPointName = "Start"},
                new ExportDataGridInfo(){Number = "4",CableNumber = "aa", CableInfo = "123", CableSize = "xx-xx-yy", EndPointNumber = "999", EndPointName = "End", StartPointNumber = "111",StartPointName = "Start"},
            };
            this.DataContext = this;
        }

        public ObservableCollection<ExportDataGridInfo> DataInfos { get; set; } = new ObservableCollection<ExportDataGridInfo>();
        
        /// <summary>
        /// 导入清册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportFromExcelBtnOnClick(object sender, RoutedEventArgs e)
        {
            List<DataGridInfo> infos = new List<DataGridInfo>();
            IOrderedEnumerable<DataGridColumn> dataGridColumns = ExportResultDataGrid.Columns.ToList().Where(x=>x.Visibility == Visibility.Visible).OrderBy(x=>x.DisplayIndex);
            foreach (DataGridColumn dataGridColumn in dataGridColumns)
            {
                if (dataGridColumn.Header is string headStr)
                {
                    infos.Add(new DataGridInfo(){TitleName = "起始行",IsNeedRedStar = false,ExcelIndex = 0,DataType = InfoType.None});
                }
                else if (dataGridColumn.Header is TextBox textBox)
                {
                    infos.Add(new DataGridInfo() { TitleName = textBox.Text.Trim(), IsNeedRedStar = false, ExcelIndex = 0 , DataType = GlobalProperty.GetDataType(textBox)});
                }
                else if(dataGridColumn.Header is TextBlockWithRedStar textBlockWithRedStar)
                {
                    infos.Add(new DataGridInfo() { TitleName = textBlockWithRedStar.Header, IsNeedRedStar = true, ExcelIndex = 0 ,DataType = GlobalProperty.GetDataType(textBlockWithRedStar)});
                }
            }
            ExportSecond second = new ExportSecond(infos);
            second.Owner = this;
            second.ShowDialog();
            
            DataInfos.Clear();
            List<ExportDataGridInfo> exportResult = second.GetExportResult();
            exportResult.ForEach(x=>DataInfos.Add(x));
        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddColumnBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // int newColumnIndex = DataInfos.First().NewColumnIndex + 1;
            // DataGridTextColumn column = new DataGridTextColumn(){Header = "新增列",Binding = new Binding($"NewColumnData[{newColumnIndex}]")};
            // ExportResultDataGrid.Columns.Add(column);
            //
            // DataInfos.First().NewColumnIndex += 1;
        }

        private void ExportResultDataGrid_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point Pmouser = e.GetPosition((ItemsControl)sender);

            if (Pmouser.Y <= 30)
            {
                new DataGridHelper(this.ExportResultDataGrid).AddCheckBox();
            }
        }
    }
}