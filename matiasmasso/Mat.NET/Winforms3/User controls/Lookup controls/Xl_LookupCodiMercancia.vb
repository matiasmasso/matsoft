Public Class Xl_LookupCodiMercancia
    Inherits Xl_LookupTextboxButton

    Private _CodiMercancia As DTOCodiMercancia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property CodiMercancia() As DTOCodiMercancia
        Get
            Return _CodiMercancia
        End Get
        Set(ByVal value As DTOCodiMercancia)
            _CodiMercancia = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.CodiMercancia = Nothing
    End Sub

    Private Sub Xl_LookupCodiMercancia_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _CodiMercancia IsNot Nothing Then FEB.CodiMercancia.Load(_CodiMercancia, exs)
        Dim oFrm As New Frm_CodisMercancia(_CodiMercancia, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onCodiMercanciaSelected
        oFrm.Show()
    End Sub

    Private Sub onCodiMercanciaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _CodiMercancia = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _CodiMercancia Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOCodiMercancia.FullNom(_CodiMercancia)
            Dim oMenu_CodiMercancia As New Menu_CodiMercancia(_CodiMercancia)
            AddHandler oMenu_CodiMercancia.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_CodiMercancia.Range)
        End If
    End Sub

    Private Sub Xl_LookupCodiMercancia_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_CodiMercancia(_CodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class

