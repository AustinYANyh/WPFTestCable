// ============================================================================================
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using HW.JD.CableReport;
using HW.JD.CableReport.Model;
using HW.JD.CableReport.ZHelper;
using WpfTestProject.CableReport.UserControl;
using CheckBox = System.Windows.Controls.CheckBox;
using DataGrid = System.Windows.Controls.DataGrid;

namespace WpfTestProject.CableReport.Window
{
    public partial class ExportMain : System.Windows.Window
    {
        private Popup headerPopup;
        private Popup rowPopup;

        private readonly Action<int> AddRowToDataGridAction;
        private readonly Action<int> RemoveRowFromDataGridAction;

        public ExportMain()
        {
            InitializeComponent();

            AddRowToDataGridAction = new Action<int>(AddRowToDataGrid);
            RemoveRowFromDataGridAction = new Action<int>(RemoveRowFromDataGrid);

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
            SetPopupIsOpen(headerPopup, false);
            SetPopupIsOpen(rowPopup, false);
            
            ExportSecond second = new ExportSecond(infos);
            second.Owner = this;
            second.ShowDialog();
            
            DataInfos.Clear();
            List<ExportDataGridInfo> exportResult = second.GetExportResult();
            exportResult.ForEach(x=>DataInfos.Add(x));
        }

        private void ExportResultDataGrid_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var target = GetCurControlFromDataGrid(e);
            //e.Handled = true;
            SetPopupIsOpen(headerPopup, false);
            SetPopupIsOpen(rowPopup, false);
        }

        private void SetPopupIsOpen(Popup popup,bool isOpen)
        {
            if (popup != null)
            {
                if (popup.IsOpen == isOpen)
                {
                    popup.IsOpen = !isOpen;
                    popup.IsOpen = isOpen;
                }
                else popup.IsOpen = isOpen;
            }
        }

        private void ExportResultDataGrid_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var target = GetCurControlFromDataGrid(e);
            e.Handled = true;
            if (target is DataGridColumnHeader header)
            {
                if(headerPopup == null) headerPopup = new DataGridHelper(this.ExportResultDataGrid).GetHeaderPopup();
                SetPopupIsOpen(headerPopup, true);
                SetPopupIsOpen(rowPopup, false);
            }
            else if (target is DataGridRow row)
            {
                if (rowPopup == null) rowPopup = new DataGridHelper(this.ExportResultDataGrid).GetRowPopup(AddRowToDataGridAction,RemoveRowFromDataGridAction);
                int index = Convert.ToInt32(((ExportDataGridInfo)row.DataContext).Number) - 1;
                    
                foreach (Button btn in ((ListBox)rowPopup.Child).Items)
                {
                    if (btn is RowAddButton rowAddBtn) rowAddBtn.RowIndex = index;
                    if (btn is RowDelButton rowDelBtn) rowDelBtn.RowIndex = index;
                }
                SetPopupIsOpen(rowPopup, true);
                SetPopupIsOpen(headerPopup, false);
            }
            else
            {
                SetPopupIsOpen(headerPopup, false);
            }
        }


        private void AddRowToDataGrid(int index)
        {
            DataInfos.Insert(index+1,new ExportDataGridInfo());
            UpdateDataInfoNumber();
            SetPopupIsOpen(rowPopup, false);
        }

        private void RemoveRowFromDataGrid(int index)
        {
            DataInfos.RemoveAt(index);
            UpdateDataInfoNumber();
            SetPopupIsOpen(rowPopup, false);
        }

        private void UpdateDataInfoNumber()
        {
            for (var i = 0; i < DataInfos.Count; i++)
            {
                DataInfos[i].Number = (i + 1).ToString();
            }
        }


        private DependencyObject GetCurControlFromDataGrid(MouseButtonEventArgs e)
        {
            System.Windows.Point aP = e.GetPosition(ExportResultDataGrid);
            IInputElement obj = ExportResultDataGrid.InputHitTest(aP);
            DependencyObject target = obj as DependencyObject;
            
            while (target != null)
            {
                if (target is DataGridRow) break;

                if (target is DataGridColumnHeader)break;

                target = VisualTreeHelper.GetParent(target);
            }

            return target;
        }

        private void ExportMain_OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (rowPopup != null)
            {
                rowPopup.IsOpen = false;
                rowPopup = null;
            }

            if (headerPopup != null)
            {
                headerPopup.IsOpen = false;
                headerPopup = null;
            }
        }
    }
}