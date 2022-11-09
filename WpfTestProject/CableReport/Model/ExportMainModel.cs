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
        public class ExportDataGridInfo:NotifyBase
        {
            public int NewColumnIndex { get; set; } = -1;

            public List<string> NewColumnData { get; set; } = new List<string>();
            
            private string number;
            private string cableNumber;
            private string cableInfo;
            private string cableSize;
            private string startPointName;
            private string endPointName;
            private string startPointNumber;
            private string endPointNumber;

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
        }
}