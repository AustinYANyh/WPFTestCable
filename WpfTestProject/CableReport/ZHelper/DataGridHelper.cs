// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：DataGridHelper.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月09日 10:22
// *   描    述：
// ==============================================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media.Animation;
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

        private RowAddButton addRowBtn = null; 
        private RowDelButton removeRowBtn = null;

        private readonly DataGrid dataGrid;
        public DataGridHelper(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
        }
        
        public Popup GetHeaderPopup()
        {
            Popup popup = new Popup();
            ListBox listBx = new ListBox();
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

                listBx.Items.Add(checkBox);
            }
            popup.Placement = PlacementMode.Mouse;
            popup.IsOpen = true;
            popup.Child = listBx;

            return popup;
        }

        public Popup GetRowPopup(Action<int> addAction, Action<int> removeAction)
        {
            Popup popup = new Popup();
            ListBox listBx = new ListBox();

            if (addRowBtn == null && removeRowBtn == null)
            {
                addRowBtn = new RowAddButton(addAction) { Content = "新增行" };
                removeRowBtn = new RowDelButton(removeAction) { Content = "移除行" };
            }

            listBx.Items.Add(addRowBtn);
            listBx.Items.Add(removeRowBtn);

            popup.Placement = PlacementMode.Mouse;
            popup.IsOpen = true;
            popup.Child = listBx;

            return popup;
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

    public class RowAddButton : Button
    {
        public int RowIndex = -1;
        private readonly Action<int> addAction;

        public RowAddButton(Action<int> addAction)
        {
            this.addAction = addAction;
        }
        protected override void OnClick()
        {
            if (RowIndex == -1) return;
            addAction(RowIndex);
        }
    }
    public class RowDelButton : Button
    {
        public int RowIndex = -1;
        private readonly Action<int> removeAction;

        public RowDelButton(Action<int> removeAction)
        {
            this.removeAction = removeAction;
        }

        protected override void OnClick()
        {
            if (RowIndex == -1) return;
            removeAction(RowIndex);
        }
    }
}