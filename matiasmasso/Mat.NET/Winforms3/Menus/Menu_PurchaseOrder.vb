Public Class Menu_PurchaseOrder
    Inherits Menu_Base

    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _PurchaseOrder As DTOPurchaseOrder

    Public Sub New(ByVal oPurchaseOrders As List(Of DTOPurchaseOrder))
        MyBase.New()
        _PurchaseOrders = oPurchaseOrders
        _PurchaseOrder = _PurchaseOrders.First
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oPurchaseOrder As DTOPurchaseOrder)
        MyBase.New()
        _PurchaseOrder = oPurchaseOrder
        _PurchaseOrders = New List(Of DTOPurchaseOrder)
        _PurchaseOrders.Add(_PurchaseOrder)
        AddMenuItems()
    End Sub



    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Web())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_DeletePendingItems())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _PurchaseOrders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        'oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _PurchaseOrders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        'oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _PurchaseOrders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_DeletePendingItems() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar pendents d'entrega"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_DeletePendingItems
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

    Private Async Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, _PurchaseOrder) Then
            Select Case _PurchaseOrder.Cod
                Case DTOPurchaseOrder.Codis.Client
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _PurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(_PurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Case DTOPurchaseOrder.Codis.Proveidor
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, _PurchaseOrder.Proveidor, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder_Proveidor(_PurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Else
                    MsgBox("no implementat per aquest tipus de comanda", MsgBoxStyle.Critical)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Do_Web()
        Dim sUrl As String = FEB.PurchaseOrder.Url(_PurchaseOrder, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink()
        Dim sUrl As String
        If _PurchaseOrders.Count = 1 Then
            sUrl = FEB.PurchaseOrder.Url(_PurchaseOrder, True)
        Else
            Dim sb As New Text.StringBuilder
            For Each item In _PurchaseOrders
                sb.AppendLine(FEB.PurchaseOrder.Url(item, True))
            Next
            sUrl = sb.ToString
        End If

        Clipboard.SetText(sUrl)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Async Sub Do_DeletePendingItems()
        Dim rc As MsgBoxResult
        If _PurchaseOrders.Count = 1 Then
            rc = MsgBox("Eliminem totes les quantitats pendents d'entrega d'aquesta comanda?", MsgBoxStyle.OkCancel)
        Else
            rc = MsgBox("Eliminem totes les quantitats pendents d'entrega d'aquestes " & _PurchaseOrders.Count & " comandes?", MsgBoxStyle.OkCancel)
        End If
        If rc = MsgBoxResult.Ok Then
            Dim oFrm As New Frm_Progress("elimina pendents", "Elimina les quantitats pendents de entrega de " & _PurchaseOrders.Count & " comandes")
            oFrm.SetStart()
            oFrm.Show()

            Dim exs As New List(Of Exception)
            If Await FEB.PurchaseOrders.DeletePendingItems(_PurchaseOrders, AddressOf oFrm.ShowProgress, exs) Then
                oFrm.SetEnd("quantitats pendents eliminades")
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar linies de comanda")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_Delete()
        Dim msg As String = ""
        If _PurchaseOrders.Count = 1 Then
            msg = String.Format("Eliminem comanda {0}?", _PurchaseOrder.num)
        Else
            msg = String.Format("Eliminem les {0} comandes sel·leccionades?", _PurchaseOrders.Count)
        End If
        Dim rc As MsgBoxResult = MsgBox(msg, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            MyBase.ToggleProgressBarRequest(True)
            Dim exs As New List(Of Exception)
            Dim oGuids = _PurchaseOrders.Select(Function(x) x.Guid).ToList()
            If Await FEB.PurchaseOrder.Delete(exs, oGuids) Then
                MsgBox("comanda " & _PurchaseOrder.Num & " eliminada", MsgBoxStyle.Information, "M+O")
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
            MyBase.ToggleProgressBarRequest(False)
        Else
            MyBase.ToggleProgressBarRequest(False)
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

