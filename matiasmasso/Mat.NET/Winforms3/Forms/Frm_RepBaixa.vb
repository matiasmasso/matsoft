

Public Class Frm_RepBaixa

    Private _Rep As DTORep = Nothing

    Public Event afterupdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
    End Sub

    Private Sub Frm_RepBaixa_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Rep.Load(exs, _Rep) Then
            TextBoxRepNom.Text = _Rep.NicknameOrNom()
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Rep.Baixa(exs, Current.Session.Emp, _Rep, DateTimePicker1.Value, CheckBoxRemovePriviledges.Checked) Then
            RaiseEvent afterupdate(Me, New MatEventArgs(_Rep))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al donar de baixa el representant")
        End If
    End Sub

End Class