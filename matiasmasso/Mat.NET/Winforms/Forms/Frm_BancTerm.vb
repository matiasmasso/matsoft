Public Class Frm_BancTerm

    Private _BancTerm As DTOBancTerm
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBancTerm)
        MyBase.New()
        Me.InitializeComponent()
        _BancTerm = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _BancTerm
            Xl_Contact21.Contact = .Banc
            DateTimePicker1.Value = .Fch
            ComboBoxTarget.SelectedIndex = .Target
            CheckBoxEuribor.Checked = .IndexatAlEuribor
            Xl_PercentDiferencial.Value = .Diferencial
            Xl_AmtMinim.Value.Eur = .MinimDespesa
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        CheckBoxEuribor.CheckedChanged,
        ComboBoxTarget.SelectedIndexChanged,
         Xl_Contact21.AfterUpdate,
          DateTimePicker1.ValueChanged,
           Xl_AmtMinim.AfterUpdate,
            Xl_PercentDiferencial.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _BancTerm
            .Banc = New DTOBanc(Xl_Contact21.Contact.Guid)
            .Fch = DateTimePicker1.Value
            .Target = ComboBoxTarget.SelectedIndex
            .IndexatAlEuribor = CheckBoxEuribor.Checked
            .Diferencial = Xl_PercentDiferencial.Value
            .MinimDespesa = Xl_AmtMinim.Value.eur
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BancTerm.Update(_BancTerm, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancTerm))
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
            If Await FEB2.BancTerm.Delete(_BancTerm, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancTerm))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class
