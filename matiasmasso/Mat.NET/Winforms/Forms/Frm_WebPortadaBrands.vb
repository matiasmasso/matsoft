Public Class Frm_WebPortadaBrands
    Private Async Sub Frm_WebPortadaBrands_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim values = Await FEB2.WebPortadaBrands.All(exs)
        If exs.Count = 0 Then
            Xl_WebPortadaBrands1.Load(values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_WebPortadaBrands1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_WebPortadaBrands1.RequestToAddNew
        Dim value As New DTOWebPortadaBrand()
        Dim oFrm As New Frm_WebPortadaBrand(value)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_WebPortadaBrands1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_WebPortadaBrands1.RequestToDelete
        Dim oValueToDelete As DTOWebPortadaBrand = e.Argument
        Dim exs As New List(Of Exception)
        If Await FEB2.WebPortadaBrand.Delete(oValueToDelete, exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_WebPortadaBrands1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WebPortadaBrands1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub Xl_WebPortadaBrands1_RequestToSort(sender As Object, e As MatEventArgs) Handles Xl_WebPortadaBrands1.RequestToSort
        Dim exs As New List(Of Exception)
        If Not Await FEB2.WebPortadaBrands.Sort(exs, Xl_WebPortadaBrands1.Values) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class