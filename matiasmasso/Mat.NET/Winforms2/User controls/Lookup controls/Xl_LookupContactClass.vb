Public Class Xl_LookupContactClass

    Inherits Xl_LookupTextboxButton

    Private _ContactClass As DTOContactClass

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property ContactClass() As DTOContactClass
        Get
            Return _ContactClass
        End Get
        Set(ByVal value As DTOContactClass)
            _ContactClass = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.ContactClass = Nothing
    End Sub

    Private Sub Xl_LookupContactClass_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_ContactClasses(_ContactClass, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.ItemSelected, AddressOf onContactClassSelected
        oFrm.Show()
    End Sub

    Private Sub onContactClassSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _ContactClass = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _ContactClass Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _ContactClass.Nom.Tradueix(Current.Session.Lang)
            Dim oMenu_ContactClass As New Menu_ContactClass(_ContactClass)
            AddHandler oMenu_ContactClass.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_ContactClass.Range)
        End If
    End Sub


End Class


