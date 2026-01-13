Public Class Frm_PremiumCustomer
    Private _PremiumCustomer As DTOPremiumCustomer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPremiumCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _PremiumCustomer = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.premiumCustomer.Load(_PremiumCustomer, exs) Then
            With _PremiumCustomer
                Xl_LookupPremiumLine1.PremiumLine = .PremiumLine
                Xl_Contact21.Contact = .Customer
                Select Case .Codi
                    Case DTOPremiumCustomer.Codis.Included
                        RadioButtonInclos.Checked = True
                    Case DTOPremiumCustomer.Codis.Excluded
                        RadioButtonExclos.Checked = True
                End Select
                TextBoxObs.Text = .Obs
                Xl_DocFile1.Load(.DocFile)
                Xl_UsrLog1.Load(.UsrLog)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
       Xl_LookupPremiumLine1.AfterUpdate,
        Xl_Contact21.AfterUpdate,
           TextBoxObs.TextChanged,
            Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonExclos.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If CurrentCodi() = DTOPremiumCustomer.Codis.NotSet Then
            UIHelper.WarnError("cal triar si està inclos o exclos")
        Else
            With _PremiumCustomer
                .PremiumLine = Xl_LookupPremiumLine1.PremiumLine
                .Customer = DTOCustomer.FromContact(Xl_Contact21.Contact)
                .Codi = CurrentCodi()
                .Obs = TextBoxObs.Text
                .UsrLog.UsrLastEdited = Current.Session.User
                If Xl_DocFile1.IsDirty Then
                    .DocFile = Xl_DocFile1.Value
                End If
            End With

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.premiumCustomer.Update(exs, _PremiumCustomer) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PremiumCustomer))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar")
            End If
        End If

    End Sub

    Private Function CurrentCodi() As DTOPremiumCustomer.Codis
        Dim retval As DTOPremiumCustomer.Codis = DTOPremiumCustomer.Codis.NotSet
        If RadioButtonExclos.Checked Then
            retval = DTOPremiumCustomer.Codis.Excluded
        ElseIf RadioButtonInclos.Checked Then
            retval = DTOPremiumCustomer.Codis.Included
        End If
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.premiumCustomer.Delete(_PremiumCustomer, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PremiumCustomer))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


