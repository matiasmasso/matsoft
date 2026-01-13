Public Class Frm_MailingConsumers
    Private _Mailing As DTOMailing

    Public Sub New(Optional oMailing As DTOMailing = Nothing)
        MyBase.New()
        Me.InitializeComponent()

        If oMailing Is Nothing Then
            _Mailing = New DTOMailing
            With _Mailing
                .Rols = New List(Of DTORol)
                .Areas = New List(Of DTOArea)
            End With
        Else
            _Mailing = oMailing
        End If
    End Sub

    Private Async Sub Frm_Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadRols()
        Await refresca()
    End Sub

    Private Async Function LoadRols() As Task
        Dim exs As New List(Of Exception)
        Dim oRols = Await FEB.Rols.All(exs)
        If exs.Count = 0 Then
            Xl_Rols1.Load(oRols)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub ButtonAddArea_Click(sender As Object, e As EventArgs) Handles ButtonAddArea.Click
        _Mailing.Areas.Add(Xl_LookupArea1.Area)
        Xl_Areas1.Load(_Mailing.Areas)
        ButtonAddArea.Enabled = False
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oMailing = Await FEB.Mailing.Load(exs, Current.Session.Emp, _Mailing)
        If exs.Count = 0 Then
            _Mailing = oMailing
            ToolStripStatusLabel1.Text = _Mailing.Users.Count & " destinataris"
            Xl_Users1.Load(_Mailing.Users)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Areas1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_Areas1.RequestToRemove
        Dim oArea As DTOArea = e.Argument
        Dim idx As Integer = _Mailing.Areas.IndexOf(oArea)
        _Mailing.Areas.RemoveAt(idx)
        Await refresca()
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        ButtonAddArea.Enabled = True
    End Sub

    Private Sub Xl_LookupRol1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRol1.AfterUpdate
        ButtonAddRol.Enabled = True
    End Sub

    Private Async Sub ButtonAddRol_Click(sender As Object, e As EventArgs) Handles ButtonAddRol.Click
        _Mailing.Rols.Add(Xl_LookupRol1.Rol)
        Xl_Rols1.Load(_Mailing.Rols)
        ButtonAddRol.Enabled = False
        Await refresca()
    End Sub


End Class