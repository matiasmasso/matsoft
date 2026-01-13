Public Class Xl_Lookup_Location
    Inherits Xl_LookupTextboxButton
    Private _Location As DTOLocation

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property LocationValue As DTOLocation
        Get
            Return MyBase.Value
        End Get
        Set(value As DTOLocation)
            MyBase.Value = value
            _Location = value
            refresca()
        End Set
    End Property

    Private Sub Xl_Location_onLookUpRequest(sender As Object, e As EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As Frm_Geo
        If _Location Is Nothing Then
            Dim oDefaultLocation As DTOLocation = BLL.BLLApp.Org.Address.Zip.Location
            oFrm = New Frm_Geo(oDefaultLocation, Frm_Geo.SelectModes.SelectLocation)
        Else
            oFrm = New Frm_Geo(_Location, Frm_Geo.SelectModes.SelectLocation)
        End If
        AddHandler oFrm.onItemSelected, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oLocation As DTOLocation = e.Argument
        If Not oLocation.Equals(_Location) Then
            _Location = oLocation
            refresca()
            MyBase.IsDirty = True
            RaiseEvent AfterUpdate(Me, e)
        End If
    End Sub

    Private Sub refresca()
        If _Location Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = BLL.BLLLocation.FullNom(_Location, BLL.BLLApp.Lang)
            Dim oMenu_Location As New Menu_Location(_Location)
            AddHandler oMenu_Location.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Location.Range)
        End If
    End Sub
End Class


