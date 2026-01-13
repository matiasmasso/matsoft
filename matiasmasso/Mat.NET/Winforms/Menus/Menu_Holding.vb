Public Class Menu_Holding
    Inherits Menu_Base

    Private _Holdings As List(Of DTOHolding)
    Private _Holding As DTOHolding

    Public Sub New(ByVal oHoldings As List(Of DTOHolding))
        MyBase.New()
        _Holdings = oHoldings
        If _Holdings IsNot Nothing Then
            If _Holdings.Count > 0 Then
                _Holding = _Holdings.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oHolding As DTOHolding)
        MyBase.New()
        _Holding = oHolding
        _Holdings = New List(Of DTOHolding)
        If _Holding IsNot Nothing Then
            _Holdings.Add(_Holding)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Clusters())
        MyBase.AddMenuItem(MenuItem_SellOut())
        If _Holding.Equals(DTOHolding.Wellknown(DTOHolding.Wellknowns.ElCorteIngles)) Then
            MyBase.AddMenuItem(MenuItem_EciOutdated())
        End If
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Holdings.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Clusters() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clusters"
        oMenuItem.Enabled = _Holdings.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Clusters
        Return oMenuItem
    End Function

    Private Function MenuItem_SellOut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "SellOut"
        oMenuItem.Enabled = _Holdings.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SellOut
        Return oMenuItem
    End Function

    Private Function MenuItem_EciOutdated() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ECI pendents fora de termini"
        oMenuItem.Enabled = _Holdings.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ExcelEciPending
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Holding(_Holding)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Clusters(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CustomerClusters(_Holding)
        oFrm.Show()
    End Sub

    Private Async Sub Do_SellOut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.yeas)
        If exs.Count = 0 Then
            FEB2.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Holding, {_Holding})
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ExcelEciPending(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        'Dim oSheet = Await FEB2.Holding.ExcelEciPending(exs)
        If exs.Count = 0 Then
            'If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
            'End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Holding.Delete(exs, _Holdings.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

