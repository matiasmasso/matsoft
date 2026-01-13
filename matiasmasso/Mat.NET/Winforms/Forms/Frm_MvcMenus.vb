Public Class Frm_MvcMenus

    Private Async Sub Frm_MvcMenus_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oMvcMenus As List(Of DTOMenu) = Await FEB2.MvcMenus.All(exs)
        If exs.Count = 0 Then
            Xl_MvcMenus1.Load(oMvcMenus)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub Xl_MvcMenus1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_MvcMenus1.RequestToAddNew
        Dim item As New DTOMenu

        Dim oFrm As New Frm_MvcMenu(item)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_MvcMenus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MvcMenus1.RequestToRefresh
        Await refresca()
    End Sub
End Class