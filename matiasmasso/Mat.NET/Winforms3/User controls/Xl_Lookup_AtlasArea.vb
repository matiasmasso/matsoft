Public Class Xl_Lookup_DTOArea
    Inherits Xl_LookupTextboxButton

    Private _Atlas As DTOAtlas2
    Private _Area As DTOArea

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oAtlas As DTOAtlas2, Optional value As DTOArea = Nothing)
        _Atlas = oAtlas
        _Area = value
        refresca()
    End Sub


    Public Property Area() As DTOArea
        Get
            Return _Area
        End Get
        Set(ByVal value As DTOArea)
            _Area = value
            refresca()
        End Set
    End Property

    Private Sub refresca()
        If _Area Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = DTOArea.NomOrDefault(_Area)
        End If
    End Sub

    Public Sub Clear()
        Me.Area = Nothing
    End Sub

    Private Sub Xl_LookupArea_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny, _Area)
        AddHandler oFrm.OnItemSelected, AddressOf OnItemSelected
        oFrm.Show()
    End Sub

    Private Sub OnItemSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Area = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub


End Class


