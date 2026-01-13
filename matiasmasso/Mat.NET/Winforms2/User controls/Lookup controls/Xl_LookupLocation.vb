Public Class Xl_LookupLocation

    Inherits Xl_LookupTextboxButton

    Private _LocationValue As DTOLocation

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property LocationValue() As DTOLocation
        Get
            Return _LocationValue
        End Get
        Set(ByVal value As DTOLocation)
            _LocationValue = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Location = Nothing
    End Sub

    Private Sub Xl_LookupLocation_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _LocationValue IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If Not FEB.Location.Load(_LocationValue, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectLocation, _LocationValue)
        AddHandler oFrm.onItemSelected, AddressOf onLocationSelected
        oFrm.Show()
    End Sub

    Private Sub onLocationSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _LocationValue = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _LocationValue Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _LocationValue.FullNom(Current.Session.Lang)
            Dim oMenu_Location As New Menu_Location(_LocationValue)
            AddHandler oMenu_Location.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Location.Range)
        End If
    End Sub


End Class


