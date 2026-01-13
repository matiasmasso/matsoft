Public Class Frm_BancVtos

    Private _Banc As DTOBanc

    Public Sub New(oBanc As DTOBanc)
        MyBase.New
        InitializeComponent()

        _Banc = oBanc
    End Sub

    Private Async Sub Frm_BancVtos_LoadAsync(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim iYears = Await FEB.Csas.YearsAsync(_Banc, exs)
        If exs.Count = 0 Then
            Xl_Years1.Load(iYears)
            Await refresca()
            Xl_TextboxSearch1.Visible = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim iYear As Integer = CurrentYear()
        Me.Text = String.Format("Efectes presentats a {0}", _Banc.abrOrNom)
        ProgressBar1.Visible = True
        Dim oCsbs = Await FEB.Csbs.All(exs, GlobalVariables.Emp, _Banc, iYear)
        If exs.Count = 0 Then
            Xl_CsbVtosxBanc1.Load(oCsbs)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_CsbVtosxBanc1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CsbVtosxBanc1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_CsbVtosxBanc1.Filter = Xl_TextboxSearch1.Value
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub

    Private Function CurrentYear() As Integer
        Dim retval As Integer = Xl_Years1.Value
        Return retval
    End Function
End Class