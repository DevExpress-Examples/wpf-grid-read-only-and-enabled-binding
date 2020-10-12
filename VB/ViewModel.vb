Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Resources
Imports System.Xml.Serialization

Namespace IsReadOnlyBindingExample
	<XmlRoot("EmployeeTasks")>
	Public Class EmployeesTasks
		Inherits List(Of EmployeeTask)

'INSTANT VB NOTE: The field dataSource was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared dataSource_Conflict As IList(Of EmployeeTask) = Nothing
'INSTANT VB NOTE: The field editableDataSource was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared editableDataSource_Conflict As ObservableCollection(Of EmployeeTask)
'INSTANT VB NOTE: The field employeeNames was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared employeeNames_Conflict As List(Of String)
		Public Shared ReadOnly Property DataSource() As IList(Of EmployeeTask)
			Get
				If dataSource_Conflict IsNot Nothing Then
					Return dataSource_Conflict
				End If
				dataSource_Conflict = GetEmployeeTasks()
				Return dataSource_Conflict
			End Get
		End Property
		Private Shared Function GetEmployeeTasks() As IList(Of EmployeeTask)
			Dim assembly As System.Reflection.Assembly = GetType(EmployeesTasks).Assembly
			Dim streamInfo As StreamResourceInfo = Application.GetResourceStream(New Uri("pack://application:,,,/IsReadOnlyBindingExample;component/EmployeeTasks.xml"))
			If streamInfo IsNot Nothing Then
				Using stream As Stream = streamInfo.Stream
					Dim s As New XmlSerializer(GetType(EmployeesTasks), New XmlRootAttribute("EmployeeTasks"))
					Return DirectCast(s.Deserialize(stream), IList(Of EmployeeTask))
				End Using
			End If
			Return Nothing
		End Function
		Public Shared ReadOnly Property EditableDataSource() As ObservableCollection(Of EmployeeTask)
			Get
				If editableDataSource_Conflict IsNot Nothing Then
					Return editableDataSource_Conflict
				End If
				editableDataSource_Conflict = New ObservableCollection(Of EmployeeTask)(GetEmployeeTasks().Take(28))
				For Each item As EmployeeTask In editableDataSource_Conflict
					If Not item.IsRoot Then
						AddHandler item.PropertyChanged, AddressOf Item_PropertyChanged
					End If
				Next item
				UpdateParentStatus()
				Return editableDataSource_Conflict
			End Get
		End Property

		Private Shared Sub Item_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
			If e.PropertyName = "Status" Then
				UpdateParentStatus()
			End If
		End Sub

		Private Shared Sub UpdateParentStatus()
			Dim d As New Dictionary(Of Integer, List(Of Integer))()
			For Each item As EmployeeTask In EditableDataSource
				If Not item.IsRoot Then
					If d.ContainsKey(item.ParentID) Then
						d(item.ParentID).Add(item.Status)
					Else
						d.Add(item.ParentID, New List(Of Integer)() From {item.Status})
					End If
				End If
			Next item
			For Each item As KeyValuePair(Of Integer, List(Of Integer)) In d
				EditableDataSource.First(Function(x) x.ID = item.Key).Status = CInt(Math.Truncate(item.Value.Average()))
			Next item
		End Sub

		Public Shared ReadOnly Property EmployeeNames() As List(Of String)
			Get
				If employeeNames_Conflict IsNot Nothing Then
					Return employeeNames_Conflict
				End If
				employeeNames_Conflict = DataSource.Select(Function(e) e.Employee).ToList()
				Return employeeNames_Conflict
			End Get
		End Property
	End Class

	Public Class EmployeeTask
		Implements INotifyPropertyChanged

		Public Sub New()
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: Priority = Status = -1;
			Status = -1
			Priority = Status
		End Sub
		Public Property ID() As Integer
		Public Property ParentID() As Integer
		Public Property Name() As String
		Public Property Description() As String
		Public Property Employee() As String
		Public Property StartDate() As DateTime
		Public Property DueDate() As DateTime
		Public Property Priority() As Integer
		Public ReadOnly Property HasDescription() As Boolean
			Get
				Return Not String.IsNullOrEmpty(Description)
			End Get
		End Property
		Public ReadOnly Property IsCompleted() As Boolean
			Get
				Return Status = 100
			End Get
		End Property

		Private _status As Integer
		Public Property Status() As Integer
			Get
				Return _status
			End Get
			Set(ByVal value As Integer)
				_status = value
				OnPropertyChanged()
				OnPropertyChanged("IsCompleted")
			End Set
		End Property

		Public ReadOnly Property IsRoot() As Boolean
			Get
				Return ParentID = 0
			End Get
		End Property

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal propertyName As String = "")
			PropertyChangedEvent?.Invoke(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
