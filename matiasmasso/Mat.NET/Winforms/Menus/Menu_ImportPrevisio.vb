Public Class Menu_ImportPrevisio
    Inherits Menu_Base

    Private _ImportPrevisions As List(Of DTOImportPrevisio)
    Private _ImportPrevisio As DTOImportPrevisio

    Public Event onPurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oImportPrevisions As List(Of DTOImportPrevisio))
        MyBase.New()
        _ImportPrevisions = oImportPrevisions
        If _ImportPrevisions IsNot Nothing Then
            If _ImportPrevisions.Count > 0 Then
                _ImportPrevisio = _ImportPrevisions.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oImportPrevisio As DTOImportPrevisio)
        MyBase.New()
        _ImportPrevisio = oImportPrevisio
        _ImportPrevisions = New List(Of DTOImportPrevisio)
        If _ImportPrevisio IsNot Nothing Then
            _ImportPrevisions.Add(_ImportPrevisio)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Errors(),
        MenuItem_Zoom(),
        MenuItem_Delete(),
        MenuItem_AvisCamion()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Errors() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Errors"
        oMenuItem.ForeColor = Color.Red

        If _ImportPrevisions(0).Errors.Count = 0 Then
            oMenuItem.Visible = False
        Else
            If _ImportPrevisions(0).Errors.Exists(Function(x) x = DTOImportPrevisio.ValidationErrors.SkuNotFound) Then
                Dim oErrorItem As ToolStripMenuItem = oMenuItem.DropDownItems.Add("article no registrat")
                oErrorItem.DropDownItems.Add("seleccionar del cataleg", Nothing, AddressOf Do_SelectSku)
            End If
            If _ImportPrevisions(0).Errors.Exists(Function(x) x = DTOImportPrevisio.ValidationErrors.OrderNotFound) Then
                'Dim oErrorItem As ToolStripMenuItem = oMenuItem.DropDownItems.Add("article no demanat")
                'oErrorItem.DropDownItems.Add("seleccionar comanda", Nothing, AddressOf Do_SelectPnc)
            End If
        End If

        Return oMenuItem
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"

        If _ImportPrevisions.Count = 1 Then
            If _ImportPrevisions.First.Sku Is Nothing Then
                oMenuItem.Enabled = False
            End If
        Else
            oMenuItem.Enabled = False
        End If

        If oMenuItem.Enabled Then
            Dim oMenuArt As New Menu_ProductSku(_ImportPrevisio.Sku)
            oMenuItem.DropDownItems.AddRange(oMenuArt.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_AvisCamion() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avis Camion al magatzem"
        oMenuItem.Visible = _ImportPrevisions.Count > 1
        AddHandler oMenuItem.Click, AddressOf Do_AvisCamion
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Visible = False 'no eliminem directament sino que acceptem tota la llista
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Importacio(_ImportPrevisio.Importacio, Frm_Importacio.Tabs.Previsio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ImportPrevisions.Delete(exs, _ImportPrevisions) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_SelectSku()
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku)
        AddHandler oFrm.onItemSelected, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_SelectPnc()
        Dim oFrm As New Frm_Contact_Pncs(_ImportPrevisio.Importacio.Proveidor, DTOPurchaseOrder.Codis.Proveidor, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPncSelected
        oFrm.Show()
    End Sub

    Private Sub onPncSelected(sender As Object, e As MatEventArgs)
        Dim oPurchaseOrderItem As DTOPurchaseOrderItem = e.Argument
        _ImportPrevisio.PurchaseOrderItem = oPurchaseOrderItem

        RaiseEvent onPurchaseOrderItemUpdateRequest(Me, New MatEventArgs(_ImportPrevisio))
    End Sub

    Private Async Sub Do_AvisCamion()
        Dim exs As New List(Of Exception)
        Dim oImportacio As DTOImportacio = _ImportPrevisions.First.Importacio
        If FEB2.Importacio.Load(exs, oImportacio) Then
            Dim XmlSrc As String = FEB2.ImportPrevisions.AvisCamionXml(_ImportPrevisions, exs)
            Dim sFilename As String = FileSystemHelper.PathToTmp & String.Format("ArribadaCamion {0}.{1}.xml", oImportacio.Id, oImportacio.Yea)
            FileSystemHelper.SaveTextToFile(XmlSrc, sFilename, exs)
            Dim oAttachments As New ArrayList
            If exs.Count = 0 Then
                Dim sTo As String = Await FEB2.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.EmailTransmisioVivace, exs)
                If exs.Count = 0 Then
                    If sTo.IndexOf(";") >= 0 Then
                        sTo = sTo.Substring(0, sTo.IndexOf(";"))
                    End If

                    Dim oMailMessage = DTOMailMessage.Factory(sTo)
                    With oMailMessage
                        .cc = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.AvisArribadaCamion)
                        .Subject = String.Format("Avis a Vivace arribada remesa {0}/{1} {2}", oImportacio.Id, oImportacio.Yea, oImportacio.Proveidor.FullNom)
                        .Body = "Adjuntem fitxer amb la mercancia prevista"
                        .AddAttachment(sFilename)
                    End With

                    If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
            oAttachments = Nothing
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


