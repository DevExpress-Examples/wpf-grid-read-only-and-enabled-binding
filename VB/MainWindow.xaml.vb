Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid.TreeList

Namespace IsReadOnlyBindingExample
	Partial Public Class MainWindow
		Inherits ThemedWindow

		Public Sub New()
			InitializeComponent()
		End Sub
	End Class

	Public Class EmployeeTaskImageSelector
		Inherits DevExpress.Xpf.Grid.TreeListNodeImageSelector

		Private Shared Function GetSvgImage(ByVal imageName As String) As ImageSource
			Dim extension = New SvgImageSourceExtension() With {
				.Uri = New Uri(String.Format("pack://application:,,,/IsReadOnlyBindingExample;component/Images/{0}.svg", imageName)),
				.Size = New System.Windows.Size(16, 16)
			}
			Return CType(extension.ProvideValue(Nothing), ImageSource)
		End Function

		Private Shared TaskImages As List(Of ImageSource)
		Shared Sub New()
			TaskImages = New List(Of ImageSource)()
			TaskImages.Add(GetSvgImage("Task"))
			TaskImages.Add(GetSvgImage("Note"))
		End Sub
		Public Overrides Function [Select](ByVal rowData As TreeListRowData) As ImageSource
			If rowData.Level = 0 Then
				Return TaskImages(0)
			End If
			Return TaskImages(1)
		End Function
	End Class
End Namespace
