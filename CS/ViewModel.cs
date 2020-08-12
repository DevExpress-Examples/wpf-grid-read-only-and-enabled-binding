using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Serialization;

namespace IsReadOnlyBindingExample {
    [XmlRoot("EmployeeTasks")]
    public class EmployeesTasks : List<EmployeeTask> {
        static IList<EmployeeTask> dataSource = null;
        static ObservableCollection<EmployeeTask> editableDataSource;
        static List<string> employeeNames;
        public static IList<EmployeeTask> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                dataSource = GetEmployeeTasks();               
                return dataSource;
            }
        }
        static IList<EmployeeTask> GetEmployeeTasks() {
            Assembly assembly = typeof(EmployeesTasks).Assembly;
            StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri("pack://application:,,,/IsReadOnlyBindingExample;component/EmployeeTasks.xml"));
            if(streamInfo != null) {
                using(Stream stream = streamInfo.Stream) {
                    XmlSerializer s = new XmlSerializer(typeof(EmployeesTasks), new XmlRootAttribute("EmployeeTasks"));
                    return (IList<EmployeeTask>)s.Deserialize(stream);
                }
            }
            return null;
        }
        public static ObservableCollection<EmployeeTask> EditableDataSource {
            get {
                if(editableDataSource != null)
                    return editableDataSource;
                editableDataSource = new ObservableCollection<EmployeeTask>(GetEmployeeTasks().Take(28));
                foreach(var item in editableDataSource) {
                    if(!item.IsRoot)
                        item.PropertyChanged += Item_PropertyChanged;
                }
                UpdateParentStatus();
                return editableDataSource;
            }
        }

        static void Item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == "Status") {
                UpdateParentStatus();
            }
        }

        static void UpdateParentStatus() {
            Dictionary<int, List<int>> d = new Dictionary<int, List<int>>();
            foreach(var item in EditableDataSource) {
                if(!item.IsRoot)
                    if(d.ContainsKey(item.ParentID))
                        d[item.ParentID].Add(item.Status);
                    else
                        d.Add(item.ParentID, new List<int>() { item.Status });
            }
            foreach(var item in d) {
                EditableDataSource.First(x => x.ID == item.Key).Status = (int)item.Value.Average();
            }
        }

        public static List<string> EmployeeNames {
            get {
                if(employeeNames != null)
                    return employeeNames;
                employeeNames = DataSource.Select(e => e.Employee).ToList();
                return employeeNames;
            }
        }
    }

    public class EmployeeTask: INotifyPropertyChanged  {
        public EmployeeTask() {
            Priority = Status = -1;
        }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }
        public bool IsCompleted { get { return Status == 100; } }

        int _status;
        public int Status { get { return _status; } set { _status = value; OnPropertyChanged(); OnPropertyChanged("IsCompleted"); } }

        public bool IsRoot { get { return ParentID == 0; } }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
