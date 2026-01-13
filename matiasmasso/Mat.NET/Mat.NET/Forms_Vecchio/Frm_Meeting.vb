Public Class Frm_Meeting
    Private _Meeting As DTOMeeting
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOMeeting)
        MyBase.New()
        Me.InitializeComponent()
        _Meeting = value
        BLL_Meeting.Load(_Meeting)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Meeting
            ComboBoxMedia.SelectedIndex = .Media
            DateTimePicker1.Value = .Fch
            DateTimePicker2.Value = .Fch
            Xl_Contact21.Contact = .Place
            TextBoxSubject.Text = .Subject
            Xl_Usuaris1.Load(.Presents)
            TextBoxPresentOthers.Text = .PresentOthers
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
         ComboBoxMedia.SelectedIndexChanged, _
          DateTimePicker1.ValueChanged, _
           DateTimePicker2.ValueChanged, _
            Xl_Contact21.AfterUpdate, _
             TextBoxSubject.TextChanged, _
              TextBoxPresentOthers.TextChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        Dim DtFch As New Date()
        With _Meeting
            .Media = ComboBoxMedia.SelectedIndex
            .Fch = New Date(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, DateTimePicker1.Value.Day, DateTimePicker2.Value.Hour, DateTimePicker2.Value.Minute, 0)
            .Place = Xl_Contact21.Contact
            .Subject = TextBoxSubject.Text
            .Presents = Xl_Usuaris1.Values
            .PresentOthers = TextBoxPresentOthers.Text
        End With

        If Meetingloader.Update(_Meeting, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Meeting))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Meetingloader.Delete(_Meeting, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Meeting))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_Lookup_Usuari1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_Usuari1.AfterUpdate
        If _AllowEvent Then
            ButtonPresent.Enabled = True
        End If
    End Sub

    Private Sub ButtonPresent_Click(sender As Object, e As EventArgs) Handles ButtonPresent.Click
        Dim oUsers As List(Of DTOUser) = Xl_Usuaris1.Values
        oUsers.Add(Xl_Lookup_Usuari1.User)
        Xl_Usuaris1.Load(oUsers)
        ButtonOk.Enabled = True
        ButtonPresent.Enabled = False
        Xl_Lookup_Usuari1.Clear()
    End Sub
End Class


