Public Class Xl_Month
    Inherits ComboBox

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Sub Load(oLang As DTOLang)
        Dim months As New List(Of String)
        For i As Integer = 1 To 12
            months.Add(oLang.MesAbr(i))
        Next
        MyBase.DataSource = months
        MyBase.SelectedIndex = Today.Month - 1
        _AllowEvents = True
    End Sub

    Private Sub Xl_Month_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Month))
    End Sub

    Public Property Month As Integer
        Get
            Return MyBase.SelectedIndex + 1
        End Get
        Set(value As Integer)
            MyBase.SelectedIndex = value - 1
        End Set
    End Property
End Class
