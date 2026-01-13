Public Class DateTimePickerNullable
    Inherits DateTimePicker

    Public Sub New()
        MyBase.New
        MyBase.ShowCheckBox = True
        MyBase.Format = DateTimePickerFormat.Custom
        MyBase.CustomFormat = "''"
    End Sub

    Shadows Property Value As Nullable(Of Date)
        Get
            Dim retval As Nullable(Of Date) = Nothing
            If MyBase.Checked Then
                retval = MyBase.Value
            End If
            Return retval
        End Get

        Set(value As Nullable(Of Date))
            If value.HasValue Then
                MyBase.Value = value.Value
            Else
                Me.Clear()
            End If
        End Set
    End Property

    Public Shadows Sub Load(value As Object)
        If value Is Nothing Then
            Me.Clear()
        ElseIf TypeOf value Is Date Then
            MyBase.Value = value
        ElseIf TypeOf value Is Nullable(Of Date) Then
            If value.HasValue Then
                MyBase.Value = value.Value
            Else
                Me.Clear()
            End If
        End If
    End Sub

    Public Sub Clear()
        MyBase.Checked = False
        MyBase.Format = DateTimePickerFormat.Custom
        MyBase.CustomFormat = "''"
    End Sub

    Private Sub DateTimeFchCompra_ValueChanged(sender As Object, e As EventArgs) Handles MyBase.ValueChanged
        If MyBase.Checked Then
            MyBase.Format = DateTimePickerFormat.Short
        Else
            MyBase.Format = DateTimePickerFormat.Custom
            MyBase.CustomFormat = "''"
        End If
    End Sub


End Class
