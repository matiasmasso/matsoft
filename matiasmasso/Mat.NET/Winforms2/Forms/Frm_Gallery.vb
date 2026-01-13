Public Class Frm_Gallery

    Private Async Sub Frm_Gallery_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Gallery1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Gallery1.RequestToAddNew
        Dim value = DTOGalleryItem.Factory()
        Dim oFrm As New Frm_GalleryItem(value)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Gallery1_RefreshRequest() Handles Xl_Gallery1.RequestToRefresh
        Await refresca()
    End Sub


    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Gallery1.Filter = e.Argument
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oItems = Await FEB.GalleryItems.All(exs)
        If exs.Count = 0 Then
            Xl_Gallery1.Load(oItems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class