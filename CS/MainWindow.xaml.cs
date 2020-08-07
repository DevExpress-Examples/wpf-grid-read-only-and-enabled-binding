using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.TreeList;

namespace IsReadOnlyBindingExample {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            InitializeComponent();
        }
    }

    public class EmployeeTaskImageSelector : DevExpress.Xpf.Grid.TreeListNodeImageSelector {

        static ImageSource GetSvgImage(string imageName) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(string.Format("pack://application:,,,/IsReadOnlyBindingExample;component/Images/{0}.svg", imageName)), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }

        static List<ImageSource> TaskImages;
        static EmployeeTaskImageSelector() {
            TaskImages = new List<ImageSource>();
            TaskImages.Add(GetSvgImage("Task"));
            TaskImages.Add(GetSvgImage("Note"));
        }
        public override ImageSource Select(TreeListRowData rowData) {
            if(rowData.Level == 0)
                return TaskImages[0];
            return TaskImages[1];
        }
    }


    public class IsParentRowConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var row = value as EmployeeTask;
            if(row != null && row.IsRoot())
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
