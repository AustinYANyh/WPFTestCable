// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：GlobalProperty.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月07日 11:24
// *   描    述：
// ==============================================================================================

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HW.JD.CableReport.Model;
using WpfTestProject.CableReport.UserControl;

namespace HW.JD.CableReport
{
    public class GlobalProperty:Control
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.RegisterAttached("ImageSource", typeof(ImageSource), typeof(GlobalProperty), new PropertyMetadata(default(ImageSource)));
        
        public static ImageSource GetImageSource(UIElement element)
        {
            return (ImageSource) element.GetValue(ImageSourceProperty);
        }

        public static void SetImageSource(UIElement element, ImageSource value)
        {
            element.SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty DataTypeProperty = DependencyProperty.RegisterAttached("DataType", typeof(InfoType), typeof(GlobalProperty), new PropertyMetadata(default(InfoType)));

        public static InfoType GetDataType(UIElement element)
        {
            return (InfoType) element.GetValue(DataTypeProperty);
        }

        public static void SetDataType(UIElement element, InfoType value)
        {
            element.SetValue(DataTypeProperty, value);
        }
    }
}