Public Class Frm_SepaMandato

    Private _SepaMandato As DTOSepaMandato
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSepaMandato)
        MyBase.New()
        Me.InitializeComponent()
        _SepaMandato = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.SepaMandato.Load(_SepaMandato, exs) Then
            With _SepaMandato
                Await Xl_Iban1.Load(.Iban)
                Xl_Contact21.Contact = .Lliurador
                TextBoxRef.Text = .Ref
                DateTimePickerFrom.Value = .FchFrom
                If .FchTo <> Nothing Then
                    CheckBoxFchTo.Checked = True
                    DateTimePickerTo.Visible = True
                    DateTimePickerTo.Value = .FchTo
                End If
                Xl_DocFile1.Load(.DocFile)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate,
        TextBoxRef.TextChanged,
        DateTimePickerFrom.ValueChanged,
        DateTimePickerTo.ValueChanged,
        Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SepaMandato
            .Lliurador = Xl_Contact21.Contact
            .Ref = TextBoxRef.Text
            .FchFrom = DateTimePickerFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerTo.Value
            Else
                .FchTo = Nothing
            End If
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.SepaMandato.Update(_SepaMandato, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SepaMandato))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.SepaMandato.Delete(_SepaMandato, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SepaMandato))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


