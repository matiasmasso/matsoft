Public Class Frm_BancTerms

    Private Async Sub Frm_BancTerms_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_BancTerms1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BancTerms1.RequestToAddNew
        Dim oTerm As New DTOBancTerm
        oTerm.Fch = Today
        Dim oFrm As New Frm_BancTerm(oTerm)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oTerms As List(Of DTOBancTerm) = Await FEB2.BancTerms.All(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Xl_BancTerms1.Load(oTerms)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_BancTerms1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BancTerms1.RequestToRefresh
        Await refresca()
    End Sub
End Class