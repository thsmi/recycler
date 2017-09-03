using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Recycler.Drives;
using Recycler.Ui;

namespace Recycler
{

    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }

    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ParameterString = parameter as string;
            if (ParameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object paramvalue = Enum.Parse(value.GetType(), ParameterString);
            return paramvalue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ParameterString = parameter as string;
            var valueAsBool = (bool)value;

            if (ParameterString == null || !valueAsBool)
            {
                try
                {
                    return Enum.Parse(targetType, "0");
                }
                catch (Exception)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            return Enum.Parse(targetType, ParameterString);
        }
    }
 

    public class Tmp
    {
        public void Empty()
        {
            NativeMethods.SHEmptyRecycleBin(IntPtr.Zero, null, NativeMethods.RecycleFlag.SHERB_NOSOUND | NativeMethods.RecycleFlag.SHERB_NOCONFIRMATION);
        }

        internal static class NativeMethods
        {
            internal enum RecycleFlag : int

            {
                // No dialog box confirming the deletion of the objects will be displayed.
                SHERB_NOCONFIRMATION = 0x00000001,
                // No dialog box indicating the progress will be displayed.
                SHERB_NOPROGRESSUI = 0x00000002,
                // No sound will be played when the operation is complete.
                SHERB_NOSOUND = 0x00000004 
            }

            [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
            internal static extern int SHEmptyRecycleBin(
                IntPtr hwnd, 
                string pszRootPath, 
                RecycleFlag dwFlags);
        }
    }

    // We do not use those methods...
    // ... because they apply only to the current session and are not persistent.
    // ... only the reg keys are persistent.
    public class Subst
    {

        public Boolean AddVirtualDrive(char letter, string path)
        {
            letter = char.ToUpper(letter);
            return NativeMethods.DefineDosDevice(0, letter + ":", path);
        }

        public Boolean RemoveVirtualDrive(char letter)
        {
            letter = char.ToUpper(letter);
            return NativeMethods.DefineDosDevice(NativeMethods.DDD_REMOVE_DEFINITION, letter + ":", null);
        }

        public static string GetDriveMapping(char letter)
        {
            letter = char.ToUpper(letter);
            StringBuilder targetPath = new StringBuilder(250);

            if (NativeMethods.QueryDosDevice(letter + ":", targetPath, targetPath.Capacity) != 0)
                return null;

            string path = targetPath.ToString();
            if (path.StartsWith(@"\??\"))
                return path.Substring(4);

            return path;
        }

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            internal static extern bool DefineDosDevice(
                uint dwFlags, 
                string lpDeviceName, 
                string lpTargetPath);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern uint QueryDosDevice(
                string lpDeviceName, 
                StringBuilder lpTargetPath, 
                int ucchMax);

            internal const uint DDD_REMOVE_DEFINITION = 0x00000002;
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lvDataBinding.ItemsSource = (new DriveRepository()).GetDrives();
        }
        
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int result;

            if (!int.TryParse(e.Text, out result) )
            {
                e.Handled = true;
            }
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            (new WindowHelper()).RemoveIcon(this);
        }

        private void OnDonateClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=8DWJHGLLWTP5N");
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/thsmi/recycler");
        }
    }
}
