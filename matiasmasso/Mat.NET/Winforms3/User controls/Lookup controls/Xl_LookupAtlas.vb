Public Class Xl_LookupAtlas
    Inherits Xl_LookupTextboxButton

    Private _AllItems As IEnumerable(Of DTOContact)
    Private _SelectedItems As IEnumerable(Of DTOContact)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub load(oAllItems As IEnumerable(Of DTOContact), oSelectedItems As IEnumerable(Of DTOContact))
        _AllItems = oAllItems
        _SelectedItems = oSelectedItems
        If _SelectedItems.Count = 0 Then
            MyBase.Text = ""
        Else
            MyBase.Text = "(diversos)" 'DTOAtlas.Caption(_SelectedItems.areas)
        End If

    End Sub

    Public Shadows ReadOnly Property SelectedContacts As IEnumerable(Of DTOContact)
        Get
            Return _SelectedItems
        End Get
    End Property

    Public Sub Clear()
        load(_AllItems, New List(Of DTOContact))
    End Sub

    Private Sub Xl_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_FilterAtlas(_AllItems, _SelectedItems)
        AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        oFrm.Show()
    End Sub

    Private Sub refreshRequest(sender As Object, e As MatEventArgs)
        load(_AllItems, e.Argument)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class

