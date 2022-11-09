// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：DataGridHelper.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月09日 10:22
// *   描    述：
// ==============================================================================================

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfTestProject.CableReport.UserControl;

namespace HW.JD.CableReport.ZHelper
{
    public class DataGridHelper
    {
        private bool alwaysTrue = true;
        
        public bool AlwaysTrue
        {
            get => alwaysTrue;
            set => alwaysTrue = true;
        }
        
        private readonly DataGrid dataGrid;
        public DataGridHelper(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }
        
        public void AddCheckBox()
        {
            ContextMenu menu = new ContextMenu();
            foreach (DataGridColumn dataGridColumn in dataGrid.Columns)
            {
                var header = GetHeaderFromColumn(dataGridColumn,out bool redStar);
                CheckBox checkBox = new CheckBox();
                checkBox.Content = header;
                
                if (redStar)
                {
                    checkBox.IsChecked = true;
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding("AlwaysTrue") {Source = this});
                }
                else
                {
                    SetBindingForCheckBox(dataGridColumn, checkBox);
                }
                menu.Items.Add(checkBox);
            }
            dataGrid.ContextMenu = menu;
        }

        private string GetHeaderFromColumn(DataGridColumn dataGridColumn,out bool redStar)
        {
            redStar = false;
            string header = string.Empty;
            if (dataGridColumn.Header is string column)
            {
                header = column;
            }
            else if (dataGridColumn.Header is TextBox textBox)
            {
                header = textBox.Text.Trim();
            }
            else if (dataGridColumn.Header is TextBlockWithRedStar textBlockWithRedStar)
            {
                redStar = true;
                header = textBlockWithRedStar.Header;
            }

            return header;
        }

        private void SetBindingForCheckBox(DataGridColumn dataGridColumn, CheckBox checkBox)
        {
            checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding("Visibility")
            {
                Source = dataGridColumn,
                Converter = new VisibilityToBoolConvertor()
            });
        }
    }
}