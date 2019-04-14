using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Junk_Cleaner_.NET_WPF
{
    public class ErazedElements
    {
        private Grid parentGrid;
        private CheckBox chkIsActive;

        public string strName;
        public bool IsActive;
        public List<string> lstPaths;

        public ErazedElements(string strName, Grid parentGrid, List<string> lstPaths)
        {
            IsActive = true;
            this.parentGrid = parentGrid;
            this.lstPaths = lstPaths;
            this.strName = strName;
            ControlsInit();
        }

        private void ControlsInit()
        {
            try
            {
                parentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Auto) });
                chkIsActive = new CheckBox()
                {
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                    FontSize = 10,
                    IsChecked = true,
                    Content = strName,
                };
                chkIsActive.Checked += CheckBox_Checked;
                chkIsActive.Unchecked += CheckBox_Checked;
                Border brd = new Border
                {
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderBrush = new SolidColorBrush { Color = Color.FromRgb(102, 255, 179), Opacity = 0.1 },
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(2),
                    Opacity = 0.9
                };

                brd.Child = chkIsActive;
                brd.SetValue(Grid.RowProperty, parentGrid.RowDefinitions.Count - 1);
                brd.MouseEnter += brdMouseEnter;
                brd.MouseLeave += brdMouseLeave;
                parentGrid.Children.Add(brd);
            }
            catch (Exception){throw;}
        }

        private void brdMouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = new SolidColorBrush { Color = Color.FromRgb(70, 130, 180), Opacity = 0.5 };
            ((Border)sender).BorderThickness = new Thickness(2);
            ((Border)sender).Margin = new Thickness(1);
        }

        private void brdMouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = new SolidColorBrush { Color = Color.FromRgb(102, 255, 179), Opacity = 0.1 };
            ((Border)sender).BorderThickness = new Thickness(1);
            ((Border)sender).Margin = new Thickness(2);
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsActive = (bool)chkIsActive.IsChecked;
        }

    }
    public  class MicrosoftEdge
    {
        public bool blnInternetCache { get; set; }
        public bool blnInternetHistory { get; set; }
        public bool blnCookies { get; set; }
        public bool blnDownloadHistory { get; set; }
        public bool blnLastDownloadLocation { get; set; }
        public bool blnSession { get; set; }
        public bool blnRecentlyTypedURLs { get; set; }
        public bool blnSavedFormInformation { get; set; }
        public bool blnSavedPasswords { get; set; }
    }

    public class SystemElement
    {
        public string strImagePath { get; set; }
        public bool blnRecycleBin { get; set; }
        public bool blnTemporaryFiles { get; set; }
        public bool blnClipboard { get; set; }
        public bool blnMemoryDumps { get; set; }
        public bool blnWindowsLogFiles { get; set; }
        public bool blnWindowsErrorReporting { get; set; }
        public bool blnDnsCache { get; set; }
        public bool blnSavedFormInformation { get; set; }
        public bool blnSavedPasswords { get; set; }
    }
    
}
