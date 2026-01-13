Public Class Menu_TpvLog
    Inherits Menu_Base

    Private _TpvLogs As List(Of DTOTpvLog)
    Private _TpvLog As DTOTpvLog


    Public Sub New(ByVal oTpvLogs As List(Of DTOTpvLog))
        MyBase.New()
        _TpvLogs = oTpvLogs
        If _TpvLogs IsNot Nothing Then
            If _TpvLogs.Count > 0 Then
                _TpvLog = _TpvLogs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oTpvLog As DTOTpvLog)
        MyBase.New()
        _TpvLog = oTpvLog
        _TpvLogs = New List(Of DTOTpvLog)
        If _TpvLog IsNot Nothing Then
            _TpvLogs.Add(_TpvLog)
        End If
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_Titular())
        MyBase.AddMenuItem(MenuItem_Alb())
        MyBase.AddMenuItem(MenuItem_Pdc())
        MyBase.AddMenuItem(MenuItem_Impagat())
        MyBase.AddMenuItem(menuitem_LogResponse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _TpvLogs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Titular() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Titular"
        oMenuItem.Enabled = _TpvLog.Titular IsNot Nothing

        If oMenuItem.Enabled Then
            Dim oMenu_Contact As New Menu_Contact(_TpvLog.Titular)
            oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        End If

        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _TpvLogs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Alb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Albará"
        oMenuItem.Visible = _TpvLog.Mode = DTOTpvRequest.Modes.Alb
        AddHandler oMenuItem.Click, AddressOf Do_Alb
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda"
        oMenuItem.Visible = _TpvLog.Mode = DTOTpvRequest.Modes.Pdc
        AddHandler oMenuItem.Click, AddressOf Do_Pdc
        Return oMenuItem
    End Function

    Private Function MenuItem_Impagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Impagat"
        oMenuItem.Visible = _TpvLog.Mode = DTOTpvRequest.Modes.Impagat
        AddHandler oMenuItem.Click, AddressOf Do_Impagat
        Return oMenuItem
    End Function

    Private Function menuitem_LogResponse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Procesa"
        oMenuItem.Enabled = Not _TpvLog.ProcessedSuccessfully
        AddHandler oMenuItem.Click, AddressOf Do_LogResponse
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
        Dim oFrm As New Frm_TpvLog(_TpvLog)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl = FEB2.TpvLog.CustomRequestUrl(_TpvLog)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Async Sub Do_Alb()
        Dim exs As New List(Of Exception)
        If _TpvLog.Request IsNot Nothing Then
            Dim oDelivery = Await FEB2.Delivery.Find(_TpvLog.Request.Guid, exs)
            If exs.Count = 0 Then
                Dim oCustomer As DTOCustomer = oDelivery.Customer

                If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_Pdc()
        Dim exs As New List(Of Exception)
        If _TpvLog.Request IsNot Nothing Then
            Dim oPurchaseOrder As DTOPurchaseOrder = _TpvLog.Request
            If FEB2.PurchaseOrder.Load(exs, oPurchaseOrder) Then
                If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Do_Impagat()
        If _TpvLog.Request IsNot Nothing Then
            Dim oImpagat As DTOImpagat = _TpvLog.Request
            Dim oFrm As New Frm_Impagat(oImpagat)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Async Sub Do_LogResponse()
        Dim exs As New List(Of Exception)
        If _TpvLog.User Is Nothing Then _TpvLog.User = Current.Session.User
        If Await FEB2.TpvRedSys.LogResponse(Current.Session.Emp, _TpvLog, exs) Then
            MyBase.RefreshRequest(Me, New MatEventArgs(_TpvLog))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.TpvLog.Delete(_TpvLogs.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el log")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

