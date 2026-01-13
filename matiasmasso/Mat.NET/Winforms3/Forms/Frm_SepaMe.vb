Public Class Frm_SepaMe

    Private _SepaMe As DTOSepaMe
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSepaMe)
        MyBase.New()
        Me.InitializeComponent()
        _SepaMe = value
    End Sub

    Private Async Sub Frm_SepaMe_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.SepaMe.Load(exs, _SepaMe) Then
            With _SepaMe
                Xl_Contact2Banc.Contact = .Banc
                Xl_Contact2Lliurador.Contact = .Lliurador
                TextBoxRef.Text = .Ref
                DateTimePickerFchFrom.Value = .FchFrom
                If .FchTo > DateTimePickerFchTo.MinDate Then
                    CheckBoxFchTo.Checked = True
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                Await Xl_DocFile1.Load(.DocFile)
                Xl_UsrLog1.Load(.UsrLog)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            Xl_Contact2Banc.AfterUpdate,
             Xl_Contact2Lliurador.AfterUpdate,
               TextBoxRef.TextChanged,
                DateTimePickerFchFrom.ValueChanged,
                 CheckBoxFchTo.CheckedChanged,
                  DateTimePickerFchTo.ValueChanged,
                   Xl_DocFile1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SepaMe
            .Banc = New DTOBanc(Xl_Contact2Banc.Contact.Guid)
            .Lliurador = Xl_Contact2Lliurador.Contact
            .Ref = TextBoxRef.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.SepaMe.Upload(exs, _SepaMe) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SepaMe))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.SepaMe.Delete(exs, _SepaMe) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SepaMe))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


