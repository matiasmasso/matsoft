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

    Private Sub Frm_Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub LoadRols()
        Dim oRols As List(Of DTORol) = BLL.BLLRols.All(BLL.BLLSession.Current.User.Lang)
        Xl_Rols1.Load(oRols)
    End Sub


    Private Sub ButtonAddArea_Click(sender As Object, e As EventArgs) Handles ButtonAddArea.Click
        _Mailing.Areas.Add(Xl_LookupArea1.Area)
        Xl_Areas1.Load(_Mailing.Areas)
        ButtonAddArea.Enabled = False
        refresca()
    End Sub

    Private Sub refresca()
        BLL.BLLMailing.Load(_Mailing)
        ToolStripStatusLabel1.Text = _Mailing.Users.Count & " destinataris"
        Xl_Users1.Load(_Mailing.Users)
    End Sub

    Private Sub Xl_Areas1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_Areas1.RequestToRemove
        Dim oArea As DTOArea = e.Argument
        Dim idx As Integer = _Mailing.Areas.IndexOf(oArea)
        _Mailing.Areas.RemoveAt(idx)
        refresca()
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        ButtonAddArea.Enabled = True
    End Sub

    Private Sub Xl_LookupRol1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRol1.AfterUpdate
        ButtonAddRol.Enabled = True
    End Sub

    Private Sub ButtonAddRol_Click(sender As Object, e As EventArgs) Handles ButtonAddRol.Click
        _Mailing.Rols.Add(Xl_LookupRol1.Rol)
        Xl_Rols1.Load(_Mailing.Rols)
        ButtonAddRol.Enabled = False
        refresca()
    End Sub
End Class