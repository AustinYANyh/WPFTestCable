// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：ExportMainModel.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月08日 13:57
// *   描    述：
// ==============================================================================================

using System.Collections.Generic;
using WpfTestProject.CableReport.Window;

namespace HW.JD.CableReport.Model
{
    public class ExportDataGridInfo : NotifyBase
    {
        private string number;
        private string cableNumber;
        private string cableInfo;
        private string cableSize;
        private string startPointName;
        private string endPointName;
        private string startPointNumber;
        private string endPointNumber;


        //支持新增列
        private string newCol1;
        private string newCol2;
        private string newCol3;
        private string newCol4;
        private string newCol5;
        private string newCol6;
        private string newCol7;
        private string newCol8;

        /// <summary>
        /// 序号
        /// </summary>
        public string Number
        {
            get => number;
            set
            {
                if (value == number) return;
                number = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 电缆编号
        /// </summary>
        public string CableNumber
        {
            get => cableNumber;
            set
            {
                if (value == cableNumber) return;
                cableNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 电缆型号
        /// </summary>
        public string CableInfo
        {
            get => cableInfo;
            set
            {
                if (value == cableInfo) return;
                cableInfo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 电缆规格
        /// </summary>
        public string CableSize
        {
            get => cableSize;
            set
            {
                if (value == cableSize) return;
                cableSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 起点名称
        /// </summary>
        public string StartPointName
        {
            get => startPointName;
            set
            {
                if (value == startPointName) return;
                startPointName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 终点名称
        /// </summary>
        public string EndPointName
        {
            get => endPointName;
            set
            {
                if (value == endPointName) return;
                endPointName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 起点编号
        /// </summary>
        public string StartPointNumber
        {
            get => startPointNumber;
            set
            {
                if (value == startPointNumber) return;
                startPointNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 终点编号
        /// </summary>
        public string EndPointNumber
        {
            get => endPointNumber;
            set
            {
                if (value == endPointNumber) return;
                endPointNumber = value;
                OnPropertyChanged();
            }
        }

        public string NewCol1
        {
            get => newCol1;
            set
            {
                if (value == newCol1) return;
                newCol1 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol2
        {
            get => newCol2;
            set
            {
                if (value == newCol2) return;
                newCol2 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol3
        {
            get => newCol3;
            set
            {
                if (value == newCol3) return;
                newCol3 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol4
        {
            get => newCol4;
            set
            {
                if (value == newCol4) return;
                newCol4 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol5
        {
            get => newCol5;
            set
            {
                if (value == newCol5) return;
                newCol5 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol6
        {
            get => newCol6;
            set
            {
                if (value == newCol6) return;
                newCol6 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol7
        {
            get => newCol7;
            set
            {
                if (value == newCol7) return;
                newCol7 = value;
                OnPropertyChanged();
            }
        }

        public string NewCol8
        {
            get => newCol8;
            set
            {
                if (value == newCol8) return;
                newCol8 = value;
                OnPropertyChanged();
            }
        }
    }
}