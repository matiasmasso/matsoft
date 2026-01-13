Public Class Menu_Location
    Inherits Menu_Base

    Private _Locations As List(Of DTOLocation)
    Private _Location As DTOLocation

    Public Sub New(ByVal oLocation As DTOLocation)
        MyBase.New()
        _Location = oLocation
        _Locations = New List(Of DTOLocation)
        If _Location IsNot Nothing Then
            _Locations.Add(_Location)
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oLocations As List(Of DTOLocation))
        MyBase.New()
        _Locations = oLocations
        If _Locations.Count > 0 Then
            _Location = _Locations.First
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_Del())
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

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Location(_Location)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.Yeas, DTOStat.Formats.Units)
        If exs.Count = 0 Then
            FEB.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Atlas, _Locations.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem població " & _Location.Nom & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Location.Delete(exs, _Location) Then
                MyBase.RefreshRequest(Me, New MatEventArgs(_Location))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


