
Public Class Frm_CliDoc
    Private _CliDoc As CliDoc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCliDoc As CliDoc)
        MyBase.New()
        Me.InitializeComponent()
        _CliDoc = oCliDoc
        refresca()
    End Sub

    Public Sub refresca()
        UIHelper.LoadComboFromEnum(ComboBoxSrc, GetType(CliDoc.Types))
        With _CliDoc
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
    ComboBoxSrc.SelectedIndexChanged, _
     DateTimePicker1.ValueChanged, _
      TextBoxRef.TextChanged, _
       CheckBoxObsolet.CheckedChanged, _
        Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If ComboBoxSrc.SelectedIndex < 0 Then
            MsgBox("falta especificar tipus de document", MsgBoxStyle.Information, "MAT.NET")
        Else
            With _CliDoc
                .Type = ComboBoxSrc.SelectedValue
                .Fch = DateTimePicker1.Value
                .Ref = TextBoxRef.Text
                .Obsoleto = CheckBoxObsolet.Checked
                .DocFile = Xl_DocFile1.Value
            End With

            Dim exs as New List(Of exception)
            If CliDocLoader.Update(_CliDoc, exs) Then
                RaiseEvent AfterUpdate(_CliDoc, New System.EventArgs)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al desar el document")
            End If
        End If

    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem el document?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If CliDocLoader.Delete(_CliDoc, exs) Then
                RaiseEvent AfterUpdate(_CliDoc, New System.EventArgs)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar el document")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class