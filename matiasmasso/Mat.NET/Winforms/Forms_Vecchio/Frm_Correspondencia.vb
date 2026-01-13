Public Class Frm_Correspondencia

    Private _Correspondencia As DTOCorrespondencia
    Private _Locked As Boolean
    Private _DirtyCell As Boolean
    Private mLastValidatedObject As Object
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Sub New(ByVal oCorrespondencia As DTOCorrespondencia)
        MyBase.New()
        Me.InitializeComponent()
        _Correspondencia = oCorrespondencia
    End Sub

    Private Async Sub Frm_Correspondencia_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Correspondencia.Load(_Correspondencia, exs) Then
            With _Correspondencia
                If .IsNew Then
                    LabelUsuari.Visible = False
                Else
                    LabelUsuari.Text = DTOUsrLog.LogText(.UserCreated, .UserLastEdited, .FchCreated, .FchLastEdited)
                End If
                DateTimePicker1.Value = .Fch
                TextBoxAtn.Text = .Atn
                TextBoxSubject.Text = .Subject
                RadioButtonSent.Checked = (.Cod = DTOCorrespondencia.Cods.Enviat)
                RadioButtonReceived.Checked = (.Cod = DTOCorrespondencia.Cods.Rebut)
                CheckBoxWord.Visible = RadioButtonSent.Checked

                Xl_Contacts_Insertable1.Load(.Contacts)
                Await Xl_DocFile1.Load(.DocFile)

                If .Id > 0 Then
                    CheckBoxWord.Checked = False
                End If
            End With

            PictureBoxLocked.Visible = _Locked
            ButtonDel.Enabled = Not _Locked
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub SetDirty()
        Dim BlEnabled As Boolean = True
        If TextBoxSubject.Text = "" Then BlEnabled = False
        If Not (RadioButtonReceived.Checked Or RadioButtonSent.Checked) Then BlEnabled = False
        ButtonOk.Enabled = BlEnabled
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        ButtonOk.Enabled = False
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        With _Correspondencia
            .fch = DateTimePicker1.Value
            .atn = TextBoxAtn.Text
            .subject = TextBoxSubject.Text
            .cod = GetCodFromRadioButtons()
            .contacts = Xl_Contacts_Insertable1.Contacts
            If .userLastEdited Is Nothing Then
                .userLastEdited = Current.Session.User
            End If
            .docFile = Xl_DocFile1.Value
        End With


        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Correspondencia.Upload(_Correspondencia, exs) Then
            If RadioButtonSent.Checked And CheckBoxWord.Checked Then
                ' root.ShowMailWord(_Correspondencia)
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                MsgBox("correspondencia registrada correctament", MsgBoxStyle.Information, "MAT.NET Correspondencia")
            End If

            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Correspondencia))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el document a correspondencia")
        End If

    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSubject.TextChanged, TextBoxAtn.TextChanged, RadioButtonReceived.CheckedChanged, RadioButtonSent.CheckedChanged, DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SetDirty()
            CheckBoxWord.Visible = RadioButtonSent.Checked
        End If
    End Sub

    Private Function GetCodFromRadioButtons() As DTOCorrespondencia.Cods
        If RadioButtonReceived.Checked Then
            Return DTOCorrespondencia.Cods.Rebut
        Else
            Return DTOCorrespondencia.Cods.Enviat
        End If
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta correspondencia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Correspondencia.Delete(_Correspondencia, exs) Then
                MsgBox("correspondencia " & _Correspondencia.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Correspondencia))
                Me.Close()
            Else
                MsgBox("error al eliminar el document" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterUpdate
        SetDirty()
    End Sub

    Private Sub Xl_Contacts_Insertable1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Insertable1.AfterUpdate
        SetDirty()
    End Sub


End Class