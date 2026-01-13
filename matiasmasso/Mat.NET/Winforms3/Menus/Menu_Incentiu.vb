

Public Class Menu_Incentiu
    Inherits Menu_Base

    Private _Incentiu As DTOIncentiu


    Public Sub New(ByVal oIncentiu As DTOIncentiu)
        MyBase.New()
        _Incentiu = oIncentiu
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_BrowseParticipants())
        MyBase.AddMenuItem(MenuItem_Excelresults)
        MyBase.AddMenuItem(MenuItem_ExcelDetall)
        MyBase.AddMenuItem(MenuItem_ExcelDeliveryAddresses)
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_BrowseParticipants() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Participants"
        oMenuItem.Image = My.Resources.People_Blue
        AddHandler oMenuItem.Click, AddressOf Do_BrowseParticipants
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelResults() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel resultats"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_ExcelResults
        Return oMenuItem
    End Function


    Private Function MenuItem_ExcelDetall() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel linies de comanda"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_ExcelDetall
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelDeliveryAddresses() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel adreces entrega"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_ExcelDeliveryAddresses
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Incentiu(_Incentiu)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB.Incentiu.Url(_Incentiu, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_BrowseParticipants(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB.Incentiu.UrlParticipants(_Incentiu, True)
        UIHelper.ShowHtml(url)
    End Sub


    Private Async Sub Do_ExcelResults(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.Incentiu.ExcelResults(exs, _Incentiu)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ExcelDetall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.Incentiu.ExcelDetall(exs, _Incentiu, Current.Session.User)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_ExcelDeliveryAddresses(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim oSheet = Await FEB.Incentiu.ExcelDeliveryAddresses(exs, _Incentiu, Current.Session.User)
        MyBase.ToggleProgressBarRequest(False)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class
