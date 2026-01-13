Public Class Frm_ExerciciApertura
    Private _Exercici As DTOExercici

    Private Sub Frm_ExerciciApertura_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Exercici = DTOExercici.Current(Current.Session.Emp)
        _Exercici.Year = _Exercici.Year - 8
        Label1.Text = String.Format("exercici {0}", _Exercici.Year)
        ButtonApertura.Text = String.Format("{0} {1}", ButtonApertura.Text, _Exercici.Year)
        ButtonTancament.Text = String.Format("{0} {1}", ButtonTancament.Text, _Exercici.Previous.Year)
        ButtonTancamentUndo.Text = String.Format("{0} {1}", ButtonTancamentUndo.Text, _Exercici.Previous.Year)
        ButtonCcaRenum.Text = String.Format("{0} {1}", ButtonCcaRenum.Text, _Exercici.Previous.Year)
        ButtonCcaRenum2.Text = String.Format("{0} {1}", ButtonCcaRenum2.Text, _Exercici.Year)
    End Sub

    Private Async Sub ButtonApertura_Click(sender As Object, e As EventArgs) Handles ButtonApertura.Click
        Xl_ProgressBar1.Visible = True
        Dim exs As New List(Of Exception)
        If Not Await FEB.Exercici.Apertura(exs, _Exercici, Current.Session.User, AddressOf Xl_ProgressBar1.ShowProgress) Then
            UIHelper.WarnError(exs)
        End If
        Xl_ProgressBar1.Visible = False
    End Sub

    Private Async Sub ButtonTancament_Click(sender As Object, e As EventArgs) Handles ButtonTancament.Click
        Xl_ProgressBar1.Visible = True
        Dim exs As New List(Of Exception)
        If Not Await FEB.Exercici.Tancament(exs, _Exercici.Previous, Current.Session.User, AddressOf Xl_ProgressBar1.ShowProgress) Then
            UIHelper.WarnError(exs)
        End If
        Xl_ProgressBar1.Visible = False
    End Sub

    Private Async Sub ButtonTancamentUndo_Click(sender As Object, e As EventArgs) Handles ButtonTancamentUndo.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Exercici.EliminaTancaments(exs, _Exercici.Previous) Then
            MsgBox("Assentaments de tancament exercici " & _Exercici.Previous.Year & " eliminats", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonCcaRenum_Click(sender As Object, e As EventArgs) Handles ButtonCcaRenum.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim iCount = Await FEB.Exercici.RenumeraAssentaments(exs, _Exercici.Previous)
        ProgressBar1.Visible = False
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonCcaRenum2_Click(sender As Object, e As EventArgs) Handles ButtonCcaRenum2.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim iCount = Await FEB.Exercici.RenumeraAssentaments(exs, _Exercici)
        ProgressBar1.Visible = False
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class