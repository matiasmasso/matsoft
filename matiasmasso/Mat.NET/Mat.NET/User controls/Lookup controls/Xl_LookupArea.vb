Public Class Xl_LookupArea

    Inherits Xl_LookupTextboxButton

    Private _Area As DTOArea

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Area() As DTOArea
        Get
            Return _Area
        End Get
        Set(ByVal value As DTOArea)
            _Area = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Area = Nothing
    End Sub

    Private Sub Xl_LookupArea_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _Area IsNot Nothing Then BLL.BLLArea.Load(_Area)
        Dim oFrm As New Frm_Areas(_Area, Frm_Areas.SelModes.SelectArea)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Area = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Area Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = BLL.BLLArea.FullNomSegmented(_Area, BLL.BLLSession.Current.User.Lang)
            Dim oMenu_Area As New Menu_Area(_Area)
            AddHandler oMenu_Area.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Area.Range)
        End If
    End Sub

End Class