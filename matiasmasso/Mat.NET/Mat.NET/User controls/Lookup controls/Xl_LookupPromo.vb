Public Class Xl_LookupPromo
    Inherits Xl_LookupTextboxButton

    Private _Promo As DTOPromo

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Promo() As DTOPromo
        Get
            Return _Promo
        End Get
        Set(ByVal value As DTOPromo)
            _Promo = value
            SetContextMenu()
            If _Promo Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Promo.Caption
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Promo = Nothing
    End Sub

    Private Sub Xl_LookupPromo_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Promos(BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPromoSelected
        oFrm.Show()
    End Sub

    Private Sub onPromoSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Promo = e.Argument
        SetContextMenu()
        MyBase.Text = Format(_Promo.FchFrom, "dd/MM/yy") & "-" & _Promo.Caption
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Promo IsNot Nothing Then
            Dim oMenu_Promo As New Menu_Promo(_Promo)
            AddHandler oMenu_Promo.AfterUpdate, AddressOf onPromoSelected
            oContextMenu.Items.AddRange(oMenu_Promo.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


End Class
