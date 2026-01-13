

Public Class Frm_ContractCodi
    Private _Codi As DTOContractCodi = Nothing
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCodi As DTOContractCodi)
        MyBase.New()
        Me.InitializeComponent()
        _Codi = oCodi
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        If _Codi.IsNew Then
            Me.Text = "NOU CODI DE CONTRACTE"
        Else
            Me.Text = _Codi.Nom
        End If
        TextBoxNom.Text = _Codi.Nom
        CheckBoxAmortitzable.Checked = _Codi.Amortitzable

        ButtonDel.Enabled = Not _Codi.IsNew
        _AllowEvents = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     CheckBoxAmortitzable.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Codi
            .nom = TextBoxNom.Text
            .amortitzable = CheckBoxAmortitzable.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.ContractCodi.Update(_Codi, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Codi))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
        Me.Close()
    End Sub


    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest codi?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.ContractCodi.Delete(_Codi, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Codi))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class