Public Class Menu_InvoiceReceived
    Inherits Menu_Base

    Private _InvoicesReceived As List(Of DTOInvoiceReceived)
    Private _InvoiceReceived As DTOInvoiceReceived

    Public Sub New(ByVal oInvoicesReceived As List(Of DTOInvoiceReceived))
        MyBase.New()
        _InvoicesReceived = oInvoicesReceived
        If _InvoicesReceived IsNot Nothing Then
            If _InvoicesReceived.Count > 0 Then
                _InvoiceReceived = _InvoicesReceived.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oInvoiceReceived As DTOInvoiceReceived)
        MyBase.New()
        _InvoiceReceived = oInvoiceReceived
        _InvoicesReceived = New List(Of DTOInvoiceReceived)
        If _InvoiceReceived IsNot Nothing Then
            _InvoicesReceived.Add(_InvoiceReceived)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Previsions())
        MyBase.AddMenuItem(MenuItem_Importacio)
        MyBase.AddMenuItem(MenuItem_Advanced())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _InvoicesReceived.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Previsions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Genera previsions"
        AddHandler oMenuItem.Click, AddressOf Do_Previsions
        Return oMenuItem
    End Function


    Private Function MenuItem_Importacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importacio"
        If _InvoicesReceived.First.Importacio Is Nothing Then
            oMenuItem.Enabled = False
        Else
            Dim oImportacio As New DTOImportacio(_InvoicesReceived.First.Importacio.Guid)
            If _InvoicesReceived.All(Function(x) oImportacio.Equals(x.Importacio)) Then
                Dim oMenu_Importacio As New Menu_Importacio(oImportacio)
                oMenuItem.DropDownItems.AddRange(oMenu_Importacio.Range)
            Else
                oMenuItem.Enabled = False
            End If
            oMenuItem.DropDownItems.Add(MenuItem_ClearImportacio)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_ClearImportacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desvincula de la importació"
        AddHandler oMenuItem.Click, AddressOf Do_ClearImportacio
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats"
        oMenuItem.DropDownItems.Add(MenuItem_Pdf)
        oMenuItem.DropDownItems.Add(MenuItem_Validate)
        oMenuItem.DropDownItems.Add(MenuItem_Delete)
        Return oMenuItem
    End Function


    Private Function MenuItem_Validate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Torna a validar"
        AddHandler oMenuItem.Click, AddressOf Do_Validate
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Enabled = _InvoicesReceived.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_InvoiceReceived(_InvoiceReceived)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Pdf()
        Dim exs As New List(Of Exception)
        If FEB.InvoiceReceived.Load(exs, _InvoiceReceived) Then
            Dim oStream As Byte() = LegacyHelper.PdfEdiInvoiceReceived.Factory(_InvoiceReceived, GlobalVariables.Emp)
            Dim oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oStream)
            If Not Await UIHelper.ShowStreamAsync(exs, oDocfile) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Validate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        For Each value In _InvoicesReceived
            Dim ex2 As New List(Of Exception)
            value = Await FEB.InvoiceReceived.Find(ex2, value.Guid)
            If ex2.Count = 0 Then
                Await FEB.InvoiceReceived.Update(exs, value)
            Else
                exs.AddRange(ex2)
            End If
        Next
        MyBase.ToggleProgressBarRequest(False)
        MyBase.RefreshRequest(Me, New MatEventArgs)
    End Sub

    Private Sub Do_Previsions()
        Dim oProveidorGuids = _InvoicesReceived.Select(Function(x) x.Proveidor.Guid).Distinct().ToList()
        If oProveidorGuids.Count = 1 Then
            Dim oProveidor As New DTOProveidor(oProveidorGuids.First())
            Dim oFrm As New Frm_Importacions(oProveidor, Defaults.SelectionModes.selection)
            AddHandler oFrm.ItemSelected, AddressOf Previsions_GoAhead
            oFrm.Show()
        Else
            UIHelper.WarnError(String.Format("Aquestes factures corresponen a {0} proveidors diferents", oProveidorGuids.Count))
        End If
    End Sub

    Private Async Sub Previsions_GoAhead(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oImportacio As DTOImportacio = e.Argument
        MyBase.ToggleProgressBarRequest(True)
        If Await FEB.InvoicesReceived.SetPrevisions(exs, _InvoicesReceived, oImportacio) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_ClearImportacio()
        Dim rc As MsgBoxResult
        If _InvoicesReceived.Count > 1 Then
            rc = MsgBox("Retirem la importació d'aquestes " & _InvoicesReceived.Count & " factures?", MsgBoxStyle.OkCancel)
        Else
            rc = MsgBox("Retirem la importació d'aquesta factura?", MsgBoxStyle.OkCancel)
        End If
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            MyBase.ToggleProgressBarRequest(True)
            If Await FEB.InvoicesReceived.ClearImportacio(exs, _InvoicesReceived) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
                MyBase.ToggleProgressBarRequest(False)
            Else
                MyBase.ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs, "error al retrocedir la factura")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub



    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        If _InvoicesReceived.Count > 1 Then
            rc = MsgBox("Retrocedim aquestes " & _InvoicesReceived.Count & " factures?", MsgBoxStyle.OkCancel)
        Else
            rc = MsgBox("Retrocedim aquesta factura?", MsgBoxStyle.OkCancel)
        End If
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.InvoicesReceived.Delete(exs, _InvoicesReceived) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al retrocedir la factura")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


