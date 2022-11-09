// ============================================================================================
// *   Copyright (C) 2022 All rights reserved.
// *
// *   文件名称：DiyHeader.xaml.cs
// *   创 建 者：yanyunhao
// *   创建日期：2022年11月07日 15:20
// *   描    述：
// ==============================================================================================

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfTestProject.CableReport.UserControl
{
    public partial class TextBlockWithRedStar : System.Windows.Controls.UserControl
    {
        public TextBlockWithRedStar()
        {
            InitializeComponent();
            
            // TitleTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Header") {Source = this});
            // RedStartTextBlock.SetBinding(TextBlock.VisibilityProperty, new Binding("IsNeedRedStar") {Source = this, Converter = new BooleanToVisibilityConverter()});
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header), typeof(string), typeof(TextBlockWithRedStar), new PropertyMetadata(" "));

        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty IsNeedRedStarProperty = DependencyProperty.Register(
            nameof(IsNeedRedStar), typeof(bool), typeof(TextBlockWithRedStar), new PropertyMetadata(true));

        public bool IsNeedRedStar
        {
            get => (bool) GetValue(IsNeedRedStarProperty);
            set => SetValue(IsNeedRedStarProperty, value);
        }
    }

    public class BoolToVisibilityConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visibilityValue)
            {
                return visibilityValue == true ? Visibility.Visible : Visibility.Collapsed;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}