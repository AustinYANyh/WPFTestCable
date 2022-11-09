// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：NotifyBase.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月08日 14:04
// *   描    述：
// ==============================================================================================

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW.JD.CableReport.Model
{
    public class NotifyBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}