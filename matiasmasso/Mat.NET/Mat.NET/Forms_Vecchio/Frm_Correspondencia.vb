Public Class Frm_Correspondencia

    Private _Correspondencia As DTO.DTOCorrespondencia
    Private _Locked As Boolean
    Private _DirtyCell As Boolean
    Private mLastValidatedObject As Object
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Sub New(ByVal oCorrespondencia As DTO.DTOCorrespondencia)
        MyBase.New()
        Me.InitializeComponent()
        _Correspondencia = oCorrespondencia
        BLL_Correspondencia.Load(oCorrespondencia)
        With _Correspondencia
            DateTimePicker1.Value = .Fch
            TextBoxAtn.Text = .Atn
            TextBoxSubject.Text = .Subject
            RadioButtonSent.Checked = (.Cod = DTO.DTOCorrespondencia.Cods.Enviat)
            RadioButtonReceived.Checked = (.Cod = DTO.DTOCorrespondencia.Cods.Rebut)
            CheckBoxWord.Visible = RadioButtonSent.Checked

            Xl_Contacts_Insertable1.Load(.Contacts)
            Xl_DocFile1.Load(.DocFile)

            Dim exs As New List(Of Exception)
            If .Id > 0 Then
                CheckBoxWord.Checked = False
            End If
        End With

        PictureBoxLocked.Visible = _Locked
        ButtonDel.Enabled = Not _Locked
        mAllowEvents = True
    End Sub

    Private Sub SetDirty()
        Dim BlEnabled As Boolean = True
        If TextBoxSubject.Text = "" Then BlEnabled = False
        If Not (RadioButtonReceived.Checked Or RadioButtonSent.Checked) Then BlEnabled = False
        ButtonOk.Enabled = BlEnabled
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Correspondencia
            .Fch = DateTimePicker1.Value
            .Atn = TextBoxAtn.Text
            .Subject = TextBoxSubject.Text
            .Cod = GetCodFromRadioButtons()
            .Contacts = Xl_Contacts_Insertable1.Contacts

            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.value
            End If
        End With

        Dim exs as New List(Of exception)
        If BLL_Correspondencia.Update(_Correspondencia, exs) Then
            If RadioButtonSent.Checked And CheckBoxWord.Checked Then
                ' root.ShowMailWord(_Correspondencia)
            Else
                MsgBox("registre " & _Correspondencia.Id, MsgBoxStyle.Information, "MAT.NET CORRESPONDENCIA")
            End If

            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Correspondencia))
            Me.Close()
        Else
            MsgBox("error al desar el document a correspondencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSubject.TextChanged, TextBoxAtn.TextChanged, RadioButtonReceived.CheckedChanged, RadioButtonSent.CheckedChanged, DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SetDirty()
            CheckBoxWord.Visible = RadioButtonSent.Checked
        End If
    End Sub

    Private Function GetCodFromRadioButtons() As DTO.DTOCorrespondencia.Cods
        If RadioButtonReceived.Checked Then
            Return DTO.DTOCorrespondencia.Cods.Rebut
        Else
            Return DTO.DTOCorrespondencia.Cods.Enviat
        End If
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta correspondencia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BLL_Correspondencia.Delete(_Correspondencia, exs) Then
                MsgBox("correspondencia " & _Correspondencia.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Correspondencia))
                Me.Close()
            Else
                MsgBox("error al eliminar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        SetDirty()
    End Sub

    Private Sub Xl_Contacts_Insertable1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Insertable1.AfterUpdate
        SetDirty()
    End Sub
End Class