Imports OfficeOpenXml.Drawing.Chart
Imports SixLabors.ImageSharp
Imports Windows.Networking.NetworkOperators

Public Class Menu_ProductSku
    Inherits Menu_Base

    Private _ProductSku As DTOProductSku
    Private _ProductSkus As List(Of DTOProductSku)

    Public Sub New(ByVal oProductSku As DTOProductSku)
        MyBase.New()
        _ProductSku = oProductSku
        _ProductSkus = New List(Of DTOProductSku)
        If _ProductSku IsNot Nothing Then
            _ProductSkus.Add(_ProductSku)
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oProductSkus As List(Of DTOProductSku))
        MyBase.New()
        _ProductSkus = oProductSkus
        If _ProductSkus.Count > 0 Then
            _ProductSku = _ProductSkus.First
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_LangText())
        MyBase.AddMenuItem(MenuItem_Pn2())
        MyBase.AddMenuItem(MenuItem_Pn1())
        MyBase.AddMenuItem(MenuItem_Previsio())
        MyBase.AddMenuItem(MenuItem_Arc())
        MyBase.AddMenuItem(MenuItem_RankingCustomers())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_Atlas())
        MyBase.AddMenuItem(MenuItem_Wtbols())
        MyBase.AddMenuItem(MenuItem_Incidencies())
        MyBase.AddMenuItem(MenuItem_Incentius())
        MyBase.AddMenuItem(MenuItem_HistoricCostPriceLists())
        MyBase.AddMenuItem(MenuItem_Web())
        MyBase.AddMenuItem(MenuItem_CopyRef())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_CopyLinkImg())
        MyBase.AddMenuItem(MenuItem_ImgExport())
        MyBase.AddMenuItem(MenuItem_ImgImport())
        MyBase.AddMenuItem(MenuItem_Circulars())
        MyBase.AddMenuItem(MenuItem_Avançats())
        MyBase.AddMenuItem(MenuItem_Delete())
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

    Private Function MenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = Current.Session.Tradueix("nombres y descripciones", "noms i descripcions", "names & descriptions")
        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function

    Private Function MenuItem_Pn2() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clients"
        AddHandler oMenuItem.Click, AddressOf Do_Pn2
        Return oMenuItem
    End Function

    Private Function MenuItem_Pn1() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proveidors"
        AddHandler oMenuItem.Click, AddressOf Do_Pn1
        Return oMenuItem
    End Function

    Private Function MenuItem_Previsio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Previsio"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Previsio
        Return oMenuItem
    End Function

    Private Function MenuItem_Arc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Historial"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Arc
        Return oMenuItem
    End Function


    Private Function MenuItem_Incidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.Image = My.Resources.Spv
        AddHandler oMenuItem.Click, AddressOf Do_Incidencies
        Return oMenuItem
    End Function
    Private Function MenuItem_HistoricCostPriceLists() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Historic tarifes"
        AddHandler oMenuItem.Click, AddressOf Do_HistoricCostPriceLists
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        oMenuItem.DropDownItems.Add("ES", Nothing, AddressOf Do_Web_ES)
        oMenuItem.DropDownItems.Add("PT", Nothing, AddressOf Do_Web_PT)
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyRef() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar referencia"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyRef
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.DropDownItems.Add("ES", Nothing, AddressOf Do_CopyLink_ES)
        oMenuItem.DropDownItems.Add("PT", Nothing, AddressOf Do_CopyLink_PT)
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLinkImg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a imatge"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkImg
        Return oMenuItem
    End Function

    Private Function MenuItem_ImgExport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar imatge"
        AddHandler oMenuItem.Click, AddressOf Do_ExportImg
        Return oMenuItem
    End Function

    Private Function MenuItem_ImgImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar imatge"
        'oMenuItem.Image = My.Resources.candau
        oMenuItem.Visible = (Current.Session.User.Rol.id = DTORol.Ids.SuperUser)
        oMenuItem.ShortcutKeys = Shortcut.CtrlI

        AddHandler oMenuItem.Click, AddressOf Do_ImportImg
        Return oMenuItem
    End Function

    Private Function MenuItem_RankingCustomers() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ranking clients"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RankingCustomers
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        Return oMenuItem
    End Function

    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Wtbols() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "botigues online"
        oMenuItem.Enabled = _ProductSkus.Count = 1
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Wtbols
        Return oMenuItem
    End Function

    Private Function MenuItem_Circulars() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Circulars..."
        oMenuItem.Image = My.Resources.MailSobreGroc
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                oMenuItem.DropDownItems.Add(MenuItem_SeBusca)
                oMenuItem.DropDownItems.Add(MenuItem_MailToPncs)
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_SeBusca() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Se busca..."
        AddHandler oMenuItem.Click, AddressOf Do_SeBusca
        Return oMenuItem
    End Function

    Private Function MenuItem_MailToPncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "a clients amb comanda pendent"
        AddHandler oMenuItem.Click, AddressOf Do_MailToPncs
        Return oMenuItem
    End Function


    Private Function MenuItem_Incentius() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "incentius"
        AddHandler oMenuItem.Click, AddressOf Do_Incentius
        Return oMenuItem
    End Function

    Private Function MenuItem_Avançats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats..."
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                oMenuItem.DropDownItems.Add(SubMenuItem_Pmcs)
                oMenuItem.DropDownItems.Add(SubMenuItem_CustRefs)
                oMenuItem.DropDownItems.Add(SubMenuItem_NewEan)
                oMenuItem.DropDownItems.Add(SubMenuItem_SetObsolets)
        End Select
        Return oMenuItem
    End Function


    Private Function SubMenuItem_Pmcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "marges comercials"
        'oMenuItem.Image = My.Resources.Outlook_16

        AddHandler oMenuItem.Click, AddressOf Do_Pmcs
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CustRefs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "referencies de client"
        'oMenuItem.Image = My.Resources.Outlook_16

        AddHandler oMenuItem.Click, AddressOf Do_CustRefs
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewEan() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "assignar nou EAN de n/rang AECOC"
        'oMenuItem.Image = My.Resources.Outlook_16

        AddHandler oMenuItem.Click, AddressOf Do_NewEAN
        Return oMenuItem
    End Function


    Private Function SubMenuItem_SetObsolets() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case _ProductSkus.Count
            Case 1
                oMenuItem.Text = "deixar obsolet"
            Case Else
                oMenuItem.Text = String.Format("deixar obsolets {0} articles", _ProductSkus.Count)
        End Select

        AddHandler oMenuItem.Click, AddressOf Do_SetObsolets
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
        If _ProductSku.isBundle Then
            Dim oFrm As New Frm_SkuBundle(_ProductSku)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            Dim oFrm As New Frm_Art(_ProductSku)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_Pn2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkuPncs(_ProductSku, DTOPurchaseOrder.Codis.client)
        oFrm.Show()
    End Sub

    Private Sub Do_Pn1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkuPncs(_ProductSku, DTOPurchaseOrder.Codis.proveidor)
        oFrm.Show()
        'Dim oArt As New Art(_ProductSku.Guid)
        'Dim oFrm As New Frm_Art_Pncs
        'With oFrm
        ' .Cod = DTOPurchaseOrder.Codis.Proveidor
        ' .Art = oArt
        ' .Show()
        ' End With
    End Sub

    Private Sub Do_Previsio(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_SkuPrevisions(_ProductSku)
        oFrm.Show()
    End Sub

    Private Sub Do_Arc(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oArt As New Art(_ProductSku.Guid)
        'Dim oFrm As New Frm_Art_Arc(oArt, GlobalVariables.Emp.Mgz)
        Dim oFrm As New Frm_Product_Historial(_ProductSku)
        oFrm.Show()
    End Sub

    Private Sub Do_HistoricCostPriceLists()
        Dim oFrm As New Frm_PriceListsxSku(_ProductSku)
        oFrm.Show()
    End Sub

    Private Sub Do_Web_ES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = _ProductSku.GetUrl(DTOLang.ESP, AbsoluteUrl:=True)
        Process.Start(url)
    End Sub
    Private Sub Do_Web_PT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = _ProductSku.GetUrl(DTOLang.POR, AbsoluteUrl:=True)
        Process.Start(url)
    End Sub

    Private Sub Do_CopyLink_ES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_ProductSku, exs) Then
            Dim url As String = _ProductSku.GetUrl(DTOLang.ESP, AbsoluteUrl:=True)
            Clipboard.SetDataObject(url, True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyLink_PT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_ProductSku, exs) Then
            Dim url As String = _ProductSku.GetUrl(DTOLang.POR, AbsoluteUrl:=True)
            Clipboard.SetDataObject(url, True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyRef(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_ProductSku, exs) Then
            Dim sRef As String = _ProductSku.refProveidor
            If sRef = "" Then
                MsgBox("Producte sense referencia", MsgBoxStyle.Exclamation)
            Else
                Clipboard.SetDataObject(_ProductSku.refProveidor, True)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyLinkImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB2.ProductSku.UrlImageZoom(_ProductSku, True)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Async Sub Do_ExportImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oImg = Await FEB2.ProductSku.Image(exs, _ProductSku)
        If exs.Count = 0 Then
            Dim sImgFilter As String = "imatges format JPG (*.jpg)|*.jpg|imatges format GIF (*.gif)|*.gif|tots els arxius|*.*" 'IIf(oFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif), "(*.gif)|*.gif|", "(*.jpg)|*.jpg|") & "tots els arxius (*.*)|*.*|"
            Dim oDlg As New System.Windows.Forms.SaveFileDialog
            Dim oResult As System.Windows.Forms.DialogResult
            With oDlg
                .Title = "Guardar imatge de producte"
                .FileName = DTOProductSku.FullNom(_ProductSku)
                .Filter = sImgFilter
                oResult = .ShowDialog
                Select Case oResult
                    Case System.Windows.Forms.DialogResult.OK
                        Dim oLegacyImage = LegacyHelper.ImageHelper.Converter(oImg)
                        oLegacyImage.Save(.FileName)
                End Select
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ImportImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sFilter As String = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR IMATGE DE PRODUCTE (350X400 PIXELS)"
            .Filter = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
            Select Case .ShowDialog
                Case DialogResult.OK
                    If FEB2.ProductSku.Load(_ProductSku, exs) Then
                        _ProductSku.image = SixLabors.ImageSharp.Image.Load(.FileName)
                        If Await FEB2.ProductSku.Update(_ProductSku, exs) Then
                            RefreshRequest(sender, New MatEventArgs(_ProductSku))
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        End With
        oDlg = Nothing
    End Sub

    Private Async Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Geo, DTOSellOut.Formats.Units)
        If exs.Count = 0 Then
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, _ProductSkus.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Wtbols(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductWtbols(_ProductSku)
        oFrm.Show()
    End Sub

    Private Async Sub Do_RankingCustomers()
        Dim exs As New List(Of Exception)
        Dim oRanking As DTORanking = Await FEB2.Ranking.CustomerRanking(exs, Current.Session.User, _ProductSkus.First)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Ranking(oRanking)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Geo, DTOSellOut.Formats.Units)
        If exs.Count = 0 Then
            Dim oValues = _ProductSkus.Select(Function(x) New DTOGuidNom With {.Guid = x.Guid, .Nom = DTOProductSku.FullNom(x)}).ToList
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, oValues)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Do_Incidencies()
        Dim oQuery = DTOIncidenciaQuery.Factory(Current.Session.User)
        With oQuery
            .Product = _ProductSku
            .Src = DTOIncidencia.Srcs.Producte
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Async Sub Do_SeBusca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Product.Load(_ProductSku, exs) Then
            Dim sTo As String = "info@matiasmasso.es"
            Dim sBody As String = "Apreciado distribuidor:"
            sBody += vbCrLf & "A solicitud del consumidor del mensaje que copiamos a continuación, nos ponemos en contacto con Vds. por si les quedara alguna unidad en stock del producto de referencia."
            sBody += vbCrLf & "Si es el caso, por favor contacten directamente con él para su venta."
            sBody += vbCrLf & "La función de MATIAS MASSO, S.A. en esta operación es exclusivamente la de facilitar el contacto. No fijamos el precio ni nos hacemos cargo del transporte ni de la transacción, que deberán convenir entre las partes."
            sBody += vbCrLf & "Este es un servicio automatizado y gratuito de avisos que se le remite porque nos consta que en alguna ocasión ha adquirido Vd. este producto."
            sBody += vbCrLf & "No es nuestra intención molestarle con correo no solicitado. Si no desea continuar recibiendo este tipo de mensajes por favor háganoslo saber."
            sBody += vbCrLf & "Reciba un cordial saludo,"
            sBody += vbCrLf & "Matias Masso"

            Dim oUsers = Await FEB2.ProductSku.SeBuscaEmailsForMailing(exs, _ProductSku)
            If exs.Count = 0 Then
                Dim oMailMessage = DTOMailMessage.Factory(sTo)
                With oMailMessage
                    .Bcc = oUsers.Select(Function(x) x.EmailAddress).ToList
                    .subject = "Se busca " & _ProductSku.nomLlarg.Tradueix(Current.Session.Lang)
                    .Body = sBody
                    .BodyFormat = DTOMailMessage.MessageBodyFormats.ASCII
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
    End Sub

    Private Async Sub Do_MailToPncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_ProductSku, exs) Then
            Dim sTo As String = "info@matiasmasso.es"
            Dim sBody As String = "Apreciado distribuidor:"
            sBody += vbCrLf & "Reciba un cordial saludo,"
            sBody += vbCrLf & "Matias Masso"

            Dim oUsers = Await FEB2.ProductSku.EmailsFromPncs(exs, _ProductSku)
            If exs.Count = 0 Then
                Dim oMailMessage = DTOMailMessage.Factory(sTo)
                With oMailMessage
                    .Bcc = oUsers.Select(Function(x) x.EmailAddress).ToList
                    .subject = _ProductSku.nomLlarg.Tradueix(Current.Session.Lang)
                    .Body = sBody
                    .BodyFormat = DTOMailMessage.MessageBodyFormats.ASCII
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
    End Sub

    Private Sub Do_Pmcs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_SkuPmcs(_ProductSku)
        oFrm.Show()
    End Sub

    Private Sub Do_CustRefs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CustomerProducts(_ProductSku)
        oFrm.Show()
    End Sub

    Private Async Sub Do_NewEAN(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_ProductSku, exs) Then
            If _ProductSku.Ean13 Is Nothing Then
                Dim rc As MsgBoxResult = MsgBox("assignem a aquest article un codi EAN del rang de codis assignats a M+O per AECOC?", MsgBoxStyle.OkCancel, "MAT.NET")
                If rc = MsgBoxResult.Ok Then
                    Dim oEan = Await FEB2.Aecoc.NextEanToSku(Current.Session.Emp, _ProductSku, exs)
                    If exs.Count = 0 Then
                        _ProductSku.ean13 = oEan
                        MsgBox("EAN " & _ProductSku.ean13.value & " assignat a " & _ProductSku.nom.Tradueix(Current.Session.Lang), MsgBoxStyle.Information, "MAT.NET")
                        'RefreshRequest(Me, New MatEventArgs(_ProductSku))
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
                End If
            Else
                MsgBox("Aquest article ja té codi de barres: " & _ProductSku.Ean13.Value, MsgBoxStyle.Information, "MAT.NET")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(_ProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_SetObsolets()
        Dim ex2 As New List(Of Exception)
        For Each item As DTOProductSku In _ProductSkus
            FEB2.ProductSku.Load(item, ex2)
            item.Obsoleto = True
            Dim exs As New List(Of Exception)
            If Not Await FEB2.ProductSku.Update(item, exs) Then
                ex2.Add(New Exception(String.Format("{0} no s'ha pogut desar: {1}", item.nomLlarg.Tradueix(Current.Session.Lang), exs.First.Message)))
            End If
        Next
        If ex2.Count > 0 Then
            UIHelper.WarnError(ex2)
        End If
        RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Incentius()
        Dim oProduct As New DTOProduct(_ProductSku.Guid)
        Dim oFrm As New Frm_Incentius(oProduct)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sNom As String = DTOProductSku.FullNom(_ProductSku)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & sNom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ProductSku.Delete(_ProductSku, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el producte")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


