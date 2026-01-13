Public Class Xl_LookupZip
    Inherits Xl_LookupTextboxButton

    Private _Zip As DTOZip

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oZip As DTOZip)
        _Zip = oZip
        Refresca()
    End Sub

    Public ReadOnly Property Zip() As DTOZip
        Get
            Return _Zip
        End Get
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
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZip, oLocation)
        AddHandler oFrm.onItemSelected, AddressOf onZipSelected
        oFrm.Show()
    End Sub

    Private Sub onZipSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Zip = e.Argument
        RaiseEvent AfterUpdate(Me, e)
        Refresca()
    End Sub

    Private Sub Refresca()
        If _Zip Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = DTOZip.FullNom(_Zip, Current.Session.Lang)
        End If
    End Sub

End Class
