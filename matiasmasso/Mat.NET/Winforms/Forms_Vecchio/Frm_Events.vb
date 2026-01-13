Public Class Frm_Events
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oDefaultValue As DTOEvento, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New
        InitializeComponent()
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Events_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub Xl_Noticias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oNoticias = Await FEB2.Noticias.Headers(exs, DTOContent.Srcs.Eventos)
        If exs.Count = 0 Then
            Xl_Noticias1.Load(oNoticias, DTOContent.Srcs.Eventos, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Noticias1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class