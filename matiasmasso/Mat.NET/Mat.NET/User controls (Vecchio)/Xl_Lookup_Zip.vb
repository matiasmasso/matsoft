Public Class Xl_Lookup_Zip
    Inherits Xl_LookupTextboxButton

    Private _Zip As DTOZip

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Zip() As DTOZip
        Get
            Return _Zip
        End Get
        Set(ByVal value As DTOZip)
            _Zip = value
            Refresca()
        End Set
    End Property


    Public Sub Clear()
        _Zip = Nothing
        Refresca()
    End Sub

    Private Sub Xl_LookupZip_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Zip(_Zip)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupZip_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oLocation As DTOLocation = Nothing
        If _Zip IsNot Nothing Then
            oLocation = _Zip.Location
        End If
        Dim oFrm As New Frm_Geo(oLocation, Frm_Geo.SelectModes.SelectZip)
        AddHandler oFrm.onItemSelected, AddressOf onZipSelected
        oFrm.Show()
    End Sub

    Private Sub onZipSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        _Zip = e.Argument
        Refresca()
    End Sub

    Private Sub Refresca()
        If _Zip Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = BLL.BLLZip.ZipyCit(_Zip)
        End If
    End Sub

End Class
