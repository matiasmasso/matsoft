Public Class Menu_EDiversaOrder

    Inherits Menu_Base

    Private _EdiversaOrders As List(Of DTOEdiversaOrder)


    Public Sub New(ByVal oEdiversaOrders As List(Of DTOEdiversaOrder))
        MyBase.New()
        _EdiversaOrders = oEdiversaOrders
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Exceptions(),
        MenuItem_Restore(),
        MenuItem_Valida(),
        MenuItem_Procesa(),
        MenuItem_Descarta(),
        MenuItem_Confirma(),
        MenuItem_ReDto(),
        MenuItem_Excel()})

        ' MenuItem_ValidaAll(),

    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _EdiversaOrders.Count = 1
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Exceptions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Errors"
        oMenuItem.Enabled = _EdiversaOrders.Count = 1 AndAlso _EdiversaOrders(0).Exceptions.Count > 0
        AddHandler oMenuItem.Click, AddressOf Do_Exceptions
        Return oMenuItem
    End Function

    Private Function MenuItem_Restore() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Restaura al Edi original"
        AddHandler oMenuItem.Click, AddressOf Do_Restore
        Return oMenuItem
    End Function

    Private Function MenuItem_Valida() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Valida de nou"
        AddHandler oMenuItem.Click, AddressOf Do_Valida
        Return oMenuItem
    End Function

    Private Function MenuItem_ValidaAll() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Valida-ho tot"
        AddHandler oMenuItem.Click, AddressOf Do_ValidaAll
        Return oMenuItem
    End Function

    Private Function MenuItem_Procesa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Procesa"
        AddHandler oMenuItem.Click, AddressOf Do_Procesa
        Return oMenuItem
    End Function

    Private Function MenuItem_Descarta() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Descartar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Descarta
        Return oMenuItem
    End Function

    Private Function MenuItem_Confirma() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Confirmar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Confirm
        Return oMenuItem
    End Function

    Private Function MenuItem_ReDto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Refer descomptes"
        oMenuItem.Enabled = False 'no implementat
        AddHandler oMenuItem.Click, AddressOf Do_ReDto
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EDiversaOrder(_EdiversaOrders(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Descarta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        If Await FEB.EdiversaOrders.Descarta(exs, _EdiversaOrders) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Confirm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        If FEB.EdiversaOrder.Load(_EdiversaOrders(0), exs) Then
            Dim oConfirmation = DTOEdiversaOrdrsp.Factory(_EdiversaOrders(0))
            Dim oFrm As New Frm_EDiversaOrdrSp(oConfirmation)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Exceptions(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EDiversaExceptions(_EdiversaOrders(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Restore(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        For Each oOrder In _EdiversaOrders
            Dim oFile = oOrder.EdiversaFile
            If FEB.EdiversaFile.Load(oFile, exs) Then
                If Not Await FEB.EdiversaFileSystem.SaveInboxFile(oFile, exs) Then
                    UIHelper.WarnError(exs)
                End If
            End If
        Next

        If Await FEB.EdiversaOrders.Validate(_EdiversaOrders, exs) Then
            MyBase.ToggleProgressBarRequest(False)
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Valida(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.ToggleProgressBarRequest(True)
        Dim exs As New List(Of Exception)

        If Await FEB.EdiversaOrders.Validate(_EdiversaOrders, exs) Then
            MyBase.ToggleProgressBarRequest(False)
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ValidaAll(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.ToggleProgressBarRequest(True)
        Dim exs As New List(Of Exception)
        If Await FEB.EdiversaOrders.Validate(_EdiversaOrders, exs) Then
            MyBase.ToggleProgressBarRequest(False)
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Procesa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        For Each oOrder In _EdiversaOrders
            Await FEB.EdiversaOrders.Procesa(Current.Session.User, {oOrder}.ToList(), exs)
        Next
        If exs.Count = 0 Then
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ReDto()
        Dim sMessage As String = ""
        Dim exs As New List(Of DTOEdiversaException)
        'BLLEDiversaOrders.AmendAllDtos(_EdiversaOrders, sMessage, exs)
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
        If sMessage > "" Then
            MsgBox(sMessage, MsgBoxStyle.Information)
        Else
            MsgBox("no s'ha tingut que rectificar cap descompte", MsgBoxStyle.Information)
        End If
        Stop
        MyBase.RefreshRequest(Me, New MatEventArgs(_EdiversaOrders))
    End Sub

    Private Sub Do_Excel()
        Dim oCsv As DTOCsv = FEB.EdiversaOrders.Csv(_EdiversaOrders)
        UIHelper.SaveCsvDialog(oCsv, "desar llistat de comandes")
    End Sub

End Class


