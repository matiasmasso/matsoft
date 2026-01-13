
Public Class Frm_ContactDoc
    Private _ContactDoc As DTOContactDoc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oContactDoc As DTOContactDoc)
        MyBase.New()
        Me.InitializeComponent()
        _ContactDoc = oContactDoc
        refresca()
    End Sub

    Public Sub refresca()
        UIHelper.LoadComboFromEnum(ComboBoxSrc, GetType(DTOContactDoc.Types))
        With _ContactDoc
            TextBoxContact.Text = .Contact.Nom
            ComboBoxSrc.SelectedValue = .Type
            DateTimePicker1.Value = .Fch
            TextBoxRef.Text = .Ref
            CheckBoxObsolet.Checked = .Obsoleto
            Xl_DocFile1.Load(.DocFile)
            ButtonDel.Enabled = Not .IsNew
            _AllowEvents = True
        End With
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxSrc.SelectedIndexChanged,
     DateTimePicker1.ValueChanged,
      TextBoxRef.TextChanged,
       CheckBoxObsolet.CheckedChanged,
        Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If ComboBoxSrc.SelectedIndex < 0 Then
            MsgBox("falta especificar tipus de document", MsgBoxStyle.Information, "MAT.NET")
        Else
            With _ContactDoc
                .Type = ComboBoxSrc.SelectedValue
                .Fch = DateTimePicker1.Value
                .Ref = TextBoxRef.Text
                .Obsoleto = CheckBoxObsolet.Checked
                .DocFile = Xl_DocFile1.Value
            End With

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.ContactDoc.Update(_ContactDoc, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ContactDoc))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar el document")
            End If
        End If

    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem el document?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ContactDoc.Delete(_ContactDoc, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ContactDoc))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class