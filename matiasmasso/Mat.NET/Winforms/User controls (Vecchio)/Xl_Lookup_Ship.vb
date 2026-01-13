

Public Class Xl_Lookup_Ship
    Inherits Xl_LookupTextboxButton

    Private mShip As Ship

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Ship() As Ship
        Get
            Return mShip
        End Get
        Set(ByVal value As Ship)
            mShip = value
            If mShip Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mShip.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Ship = Nothing
    End Sub

    Private Sub Xl_LookupShip_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Ships(bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.AfterSelect, AddressOf onShipSelected
        oFrm.Show()
    End Sub

    Private Sub onShipSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mShip = CType(sender, Ship)
        MyBase.Text = mShip.Nom
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class
