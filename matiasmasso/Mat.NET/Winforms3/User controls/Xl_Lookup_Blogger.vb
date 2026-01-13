Public Class Xl_Lookup_Blogger
    Inherits Xl_LookupTextboxButton
    Private _Blogger As DTOBlogger

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Blogger() As DTOBlogger
        Get
            Return _Blogger
        End Get
        Set(ByVal value As DTOBlogger)
            _Blogger = value
            If _Blogger Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Blogger.Title
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Blogger = Nothing
    End Sub

    Private Sub Xl_LookupDTOBlogger_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Bloggers(DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onBloggerSelected
        oFrm.Show()
    End Sub

    Private Sub onBloggerSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Blogger = e.Argument
        MyBase.Text = _Blogger.Title
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class


