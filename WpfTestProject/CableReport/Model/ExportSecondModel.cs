// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：ExportSecondModel.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月08日 14:03
// *   描    述：
// ==============================================================================================

using System.Collections.Generic;
using System.Linq;
using WpfTestProject.CableReport.Window;

namespace HW.JD.CableReport.Model
{
    public enum InfoType
    {
        None=0,
        NewCol,
        Number,
        CableNumber,
        CableInfo,
        CableSize,
        StartPointName,
        EndPointName,
        StartPointNumber,
        EndPointNumber,
    }
    public class DataGridInfo:NotifyBase
    {
        public InfoType DataType { get; set; }
        
        private string titleName;

        public string TitleName
        {
            get => titleName;
            set
            {
                titleName = value;
                OnPropertyChanged(nameof(TitleName));
            }
        }

        private bool isNeedRedStar;

        public bool IsNeedRedStar
        {
            get => isNeedRedStar;
            set
            {
                isNeedRedStar = value;
                OnPropertyChanged(nameof(IsNeedRedStar));
            }
        }
        

        private int excelIndex;

        public int ExcelIndex
        {
            get => excelIndex;
            set
            {
                excelIndex = value;
                OnPropertyChanged(nameof(ExcelIndex));
                OnPropertyChanged(nameof(DisplayCol));
            }
        }
        
        private string displayCol;

        public string DisplayCol
        {
            get
            {
                if (ExcelIndex == -1 || ExcelIndex > ExcelColumns.Count) return "Out Of Range";
                return ExcelColumns.ElementAt(ExcelIndex);
            }
            set {  }
        }

        private List<string> excelColumns = new List<string>();

        public List<string> ExcelColumns
        {
            get => excelColumns;
            set
            {
                excelColumns = value;
                OnPropertyChanged(nameof(ExcelColumns));
                OnPropertyChanged(nameof(DisplayCol));
            }
        }
    }

    public class SheetInfo:NotifyBase
    {
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            } 
        }

        private string content;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
    }
}