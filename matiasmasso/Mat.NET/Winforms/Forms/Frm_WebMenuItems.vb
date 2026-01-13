Public Class Frm_WebMenuItems

    Public Sub New(Optional oMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Private Async Sub Frm_WebMenuItems_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.WebMenuGroups.All(exs)
        If exs.Count = 0 Then
            Xl_WebMenuItems1.Load(items, Current.Session.Lang)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class