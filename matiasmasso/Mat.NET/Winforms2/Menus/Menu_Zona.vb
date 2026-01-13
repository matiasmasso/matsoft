Public Class Menu_Zona
    Inherits Menu_Base

    Private _Zona As DTOZona
    Private _Zonas As List(Of DTOZona)
    Private _Mode As DTO.Defaults.SelectionModes

    Public Event AfterSelectLocation(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oZona As DTOZona, Optional oMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Mode = oMode
        _Zona = oZona
        _Zonas = New List(Of DTOZona)
        If _Zona IsNot Nothing Then
            _Zonas.Add(_Zona)
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oZonas As List(Of DTOZona), Optional oMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Mode = oMode
        _Zonas = oZonas
        If _Zonas.Count > 0 Then
            _Zona = _Zonas.First
        End If
        AddMenuItems()
    End Sub


    Public Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom()
        Dim oFrm As New Frm_Zona(_Zona)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.Yeas, DTOStat.Formats.Units)
        If exs.Count = 0 Then
            FEB.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Atlas, _Zonas.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Delete()
        Dim message As String = "Cal sel·leccionar una zona on assignar les poblacions i registres que estaven assignats a la zona que volem eliminar"
        Dim rc = MsgBox(message, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim oCountry = _Zona.Country
            oCountry.zonas.Remove(_Zona)
            Dim oCountries = {oCountry}.ToList
            Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona,, oCountries)
            AddHandler oFrm.onItemSelected, AddressOf onZonaToSelected
            oFrm.Show()
        End If

    End Sub

    Private Async Sub onZonaToSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oZonaTo = e.Argument
        If Await FEB.Zona.Delete(exs, _Zona, oZonaTo) Then
            MyBase.RefreshRequest(Me, New MatEventArgs(_Zona))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class

