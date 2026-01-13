

Public Class Xl_Lookup_WebRoute

    Inherits Xl_LookupTextboxButton

    Private mWebPage As DTOWebPage

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property WebPage() As DTOWebPage
        Get
            Return mWebPage
        End Get
        Set(ByVal value As DTOWebPage)
            mWebPage = value
            If mWebPage Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mWebPage.Route & " (" & mWebPage.Url & ")"
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.WebPage = Nothing
    End Sub

    Private Sub Xl_Lookup_WebPage_Doubleclick(sender As Object, e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_WebRoute(mWebPage)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_Lookup_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_WebRoutes(BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.AfterSelect, AddressOf onItemSelected
        oFrm.Show()
    End Sub

    Private Sub onItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mWebPage = sender
        MyBase.Text = mWebPage.Route & " (" & mWebPage.Url & ")"
        RaiseEvent AfterUpdate(Me, New MatEventArgs(mWebPage))
    End Sub

    Private Sub RefreshRequest()
        MyBase.Text = mWebPage.Route & " (" & mWebPage.Url & ")"
    End Sub
End Class


