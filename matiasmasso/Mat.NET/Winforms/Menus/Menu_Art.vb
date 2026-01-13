Public Class Menu_Art
    Private _Sku As DTOProductSku
    Private _Skus As List(Of DTOProductSku)
    Private mArt As Art
    Private mArts As List(Of Art)
    Private _Mgz As DTOMgz
    Private mStk As Integer
    Private mPn2 As Integer
    Private mPn3 As Integer
    Private mPn1 As Integer
    Private mPrv As Integer
    Private mColor As System.Drawing.Color

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oArt As Art, Optional oMgz As DTOMgz = Nothing)
        MyBase.New()
        mArt = oArt
        mArts = New List(Of Art)
        mArts.Add(mArt)

        _Sku = New DTOProductSku(oArt.Guid)
        _Skus = New List(Of DTOProductSku)
        _Skus.Add(_Sku)

        _Mgz = oMgz
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Pn2(),
        MenuItem_Pn1(),
        MenuItem_Previsio(),
        MenuItem_Arc(),
        MenuItem_Atlas(),
        MenuItem_Chart(),
        MenuItem_Incidencies(),
        MenuItem_Incentius(),
        MenuItem_HistoricCostPriceLists(),
        MenuItem_Web(),
        MenuItem_CopyLink(),
        MenuItem_CopyLinkImg(),
        MenuItem_ImgExport(),
        MenuItem_ImgImport(),
        MenuItem_Circulars(),
        MenuItem_Avançats()})
    End Function


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
        'Dim iPn1 As Integer = mArt.Pn1
        'If iPn1 = 0 Then
        ' oMenuItem.Enabled = False
        ' Else
        ' End If
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

    Private Function MenuItem_Chart() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Chart
        oMenuItem.Text = "Trajectoria"
        oMenuItem.Image = My.Resources.notepad
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
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
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
        'oMenuItem.Image = My.Resources.candau
        If mArt Is Nothing Then
            oMenuItem.Visible = False
        Else
            oMenuItem.Visible = mArt.Image IsNot Nothing
        End If
        oMenuItem.ShortcutKeys = Shortcut.CtrlE

        AddHandler oMenuItem.Click, AddressOf Do_ExportImg
        Return oMenuItem
    End Function

    Private Function MenuItem_ImgImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar imatge"
        'oMenuItem.Image = My.Resources.candau
        oMenuItem.Visible = (BLL.BLLSession.Current.User.Rol.Id = DTORol.Ids.SuperUser)
        oMenuItem.ShortcutKeys = Shortcut.CtrlI

        AddHandler oMenuItem.Click, AddressOf Do_ImportImg
        Return oMenuItem
    End Function


    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Circulars() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Circulars..."
        oMenuItem.Image = My.Resources.MailSobreGroc
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                oMenuItem.DropDownItems.Add(MenuItem_SeBusca)
                oMenuItem.DropDownItems.Add(MenuItem_MailToPncs)
                oMenuItem.DropDownItems.Add(MenuItem_ExcelClientsSortits)
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

    Private Function MenuItem_ExcelClientsSortits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "excel amb clients en actiu sortits"
        AddHandler oMenuItem.Click, AddressOf Do_Excel_ClientsSortits
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
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                oMenuItem.DropDownItems.Add(SubMenuItem_SetLastUnits)
                oMenuItem.DropDownItems.Add(SubMenuItem_CustRefs)
                oMenuItem.DropDownItems.Add(SubMenuItem_NewEan)
        End Select
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SetLastUnits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "marcar com ultimes unitats"
        'oMenuItem.Image = My.Resources.Outlook_16

        AddHandler oMenuItem.Click, AddressOf Do_SetLastUnits
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
    Private Function SubMenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "descripcions"
        'oMenuItem.Image = My.Resources.Outlook_16

        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        If mArt.Virtual Then
            Dim oFrm As New Frm_ProductPack(_Sku)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            Dim oFrm As New Frm_Art(mArt)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_Pn2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'root.ShowArtPncs(mArt, DTOPurchaseOrder.Codis.Client)
        Dim oFrm As New Frm_ProductSkuPncs(_Sku, DTOPurchaseOrder.Codis.Client)
        oFrm.Show()
    End Sub

    Private Sub Do_Pn1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkuPncs(_Sku, DTOPurchaseOrder.Codis.Proveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_Previsio(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_SkuPrevisions(_Sku)
        oFrm.Show()
    End Sub

    Private Sub Do_Arc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Art_Arc(mArt, BLLApp.Mgz)
        oFrm.Show()
    End Sub

    Private Sub Do_HistoricCostPriceLists()
        Dim oFrm As New Frm_PriceListsxSku(_Sku)
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start("IExplore.exe", mArt.RoutingUrl(True))
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mArt.RoutingUrl(True), True)
    End Sub

    Private Sub Do_CopyLinkImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mArt.ImageUrlForMvc(True), True)
    End Sub


    Private Sub Do_ExportImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImg As System.Drawing.Image = mArt.Image
        Dim oFormat As System.Drawing.Imaging.ImageFormat = oImg.RawFormat
        Dim sImgFilter As String = "imatges format JPG (*.jpg)|*.jpg|imatges format GIF (*.gif)|*.gif|tots els arxius|*.*" 'IIf(oFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif), "(*.gif)|*.gif|", "(*.jpg)|*.jpg|") & "tots els arxius (*.*)|*.*|"
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        Dim oResult As System.Windows.Forms.DialogResult
        With oDlg
            .Title = "Guardar imatge de producte"
            .FileName = mArt.FullNom
            .Filter = sImgFilter
            '.FilterIndex = 1
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    oImg.Save(.FileName)
            End Select
        End With
    End Sub

    Private Sub Do_ImportImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFilter As String = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR IMATGE DE PRODUCTE (350X400 PIXELS)"
            .Filter = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
            Select Case .ShowDialog
                Case DialogResult.OK
                    Dim oImg As Image = System.Drawing.Image.FromFile(.FileName)
                    mArt.UpdateBigImg(oImg)
                    oImg = Nothing
                    RefreshRequest(sender, e)
            End Select
        End With
        oDlg = Nothing
    End Sub

    Private Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLProductSku.Load(_Sku)
        Dim oStat As New DTOStat(DTOStat.ConceptTypes.Geo, BLLApp.Lang)
        With oStat
            .Product = _Sku
            .ExpandToLevel = 5
            .Format = DTOStat.Formats.Units
        End With
        Dim oFrm As New Frm_Stats(oStat)
        oFrm.Show()
    End Sub

    Private Sub Do_Chart(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oProduct As New Product(mArt)
        Dim oFrm As New Frm_ProductChart(oProduct)
        oFrm.Show()
    End Sub

    Private Sub Do_Incidencies()
        Dim oQuery As DTOIncidenciaQuery = BLLIncidencias.DefaultQuery(BLL.BLLSession.Current.User)
        With oQuery
            .Product = New DTOProductSku(mArt.Guid)
            .Src = DTOIncidencia.Srcs.Producte
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Sub Do_SeBusca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sTo As String = "info@matiasmasso.es"
        Dim sBcc As String = ""
        For Each sEmailAdr As String In mArt.SeBuscaEmailsForMailing
            sBcc += sEmailAdr & ";"
        Next
        Dim sSubject As String = "SE BUSCA " & mArt.Nom_ESP
        Dim sBody As String = "Apreciado distribuidor:"
        sBody += vbCrLf & "A solicitud del consumidor del mensaje que copiamos a continuación, nos ponemos en contacto con Vds. por si les quedara alguna unidad en stock del producto de referencia."
        sBody += vbCrLf & "Si es el caso, por favor contacten directamente con él para su venta."
        sBody += vbCrLf & "La función de MATIAS MASSO, S.A. en esta operación es exclusivamente la de facilitar el contacto. No fijamos el precio ni nos hacemos cargo del transporte ni de la transacción, que deberán convenir entre las partes."
        sBody += vbCrLf & "Este es un servicio automatizado y gratuito de avisos que se le remite porque nos consta que en alguna ocasión ha adquirido Vd. este producto."
        sBody += vbCrLf & "No es nuestra intención molestarle con correo no solicitado. Si no desea continuar recibiendo este tipo de mensajes por favor háganoslo saber."
        sBody += vbCrLf & "Reciba un cordial saludo,"
        sBody += vbCrLf & "Matias Masso"

        Dim exs As New List(Of Exception)
        If Not MatOutlook.NewMessage(sTo, "", sBcc, sSubject, sBody, , , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If
    End Sub

    Private Sub Do_MailToPncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sTo As String = "info@matiasmasso.es"
        Dim sBcc As String = ""
        For Each sEmailAdr As String In mArt.EmailsFromPncs
            sBcc += sEmailAdr & ";"
        Next
        Dim sSubject As String = mArt.Nom_ESP
        Dim sBody As String = "Apreciado distribuidor:"
        sBody += vbCrLf & "Reciba un cordial saludo,"
        sBody += vbCrLf & "Matias Masso"

        Dim exs As New List(Of Exception)
        If Not MatOutlook.NewMessage(sTo, "", sBcc, sSubject, sBody, , , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If

    End Sub

    Private Sub Do_Excel_ClientsSortits()
        Dim oCsv As New FF(DTO.DTOFlatFile.Ids.Csv)
        Dim oProduct As New Product(mArt)
        For Each oClient As Client In oProduct.ClientsAmbSortides()
            If Not oClient.Obsoleto Then
                oCsv.AddValues(oClient.Nom_o_NomComercial, oClient.Address.Text, BLLAddress.ZipyCit(oClient.Address.Zip), oClient.DefaultTel)
            End If
        Next
        Dim sFilename As String = oCsv.Save
        Process.Start("Excel.exe", sFilename)
    End Sub

    Private Sub Do_SetLastUnits(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer = 0
        Dim exs As New List(Of Exception)
        For Each oArt As Art In mArts
            If Not oArt.LastProduction Then
                oArt.LastProduction = True
                oArt.Update(exs)
                i += 1
            End If
        Next
        MsgBox("actualitzats " & i.ToString & " articles de un total de " & mArts.Count.ToString)
    End Sub

    Private Sub Do_CustRefs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSku As DTOProductSku = BLL.BLLProductSku.Find(mArt.Guid)
        Dim oFrm As New Frm_CustomerProducts(oSku)
        oFrm.Show()
    End Sub

    Private Sub Do_NewEAN(ByVal sender As Object, ByVal e As System.EventArgs)
        BLLProductSku.Load(_Sku)
        If _Sku.Ean13 Is Nothing Then
            Dim rc As MsgBoxResult = MsgBox("assignem a aquest article un codi EAN del rang de codis assignats a M+O per AECOC?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim exs As New List(Of Exception)
                If BLLAecoc.NextEanToSku(_Sku, exs) Then
                    MsgBox("EAN " & _Sku.Ean13.Value & " assignat a " & mArt.Nom(BLL.BLLApp.Lang), MsgBoxStyle.Information, "MAT.NET")
                    RefreshRequest(Me, EventArgs.Empty)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
            End If
        Else
            MsgBox("Aquest article ja té codi de barres: " & _Sku.Ean13.Value, MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Function MakeFragment(ByVal HTML As String) As String
        ' Helper routine to build a properly-formatted HTML fragment.
        Const Version As String = "Version:1.0" & vbCrLf
        Const StartHTML As String = "StartHTML:"
        Const EndHTML As String = "EndHTML:"
        Const StartFragment As String = "StartFragment:"
        Const EndFragment As String = "EndFragment:"
        Const DocType = ""
        Const HTMLIntro As String = ""
        Const HTMLExtro As String = ""
        Const NumberLengthAndCR As Integer = 10
        ' Let the compiler determine the description length.

        Dim DescriptionLength As Integer = Version.Length + StartHTML.Length + EndHTML.Length + StartFragment.Length + EndFragment.Length + 4 * NumberLengthAndCR
        Dim Description As String = ""
        Dim StartHTMLIndex, EndHTMLIndex, StartFragmentIndex, EndFragmentIndex As Integer
        ' The HTML clipboard format is defined by using byte positions in the 
        'entire block where HTML text and
        'fragments start and end. These positions are written in a 
        'description. Unfortunately the positions depend on the
        'length of the description but the description may change with 
        'varying positions.
        'To solve this dilemma the offsets are converted into fixed length 
        'strings which makes it possible to know
        'the description length in advance.
        StartHTMLIndex = DescriptionLength
        StartFragmentIndex = StartHTMLIndex + DocType.Length + HTMLIntro.Length
        EndFragmentIndex = StartFragmentIndex + HTML.Length
        EndHTMLIndex = EndFragmentIndex + HTMLExtro.Length
        Dim sb As New System.Text.StringBuilder(Version)
        sb.Append(StartHTML)
        sb.Append(StartHTMLIndex.ToString.PadLeft(8, "0"))
        sb.AppendFormat(vbCrLf)
        sb.Append(EndHTML)
        sb.Append(EndHTMLIndex.ToString.PadLeft(8, "0"))
        sb.Append(vbCrLf)
        sb.Append(StartFragment)
        sb.Append(StartFragmentIndex.ToString.PadLeft(8, "0"))
        sb.Append(vbCrLf)
        sb.Append(EndFragment)
        sb.Append(EndFragmentIndex.ToString.PadLeft(8, "0"))
        sb.Append(vbCrLf)
        sb.Append(DocType)
        sb.Append(HTMLIntro)
        sb.Append(HTML)
        sb.Append(HTMLExtro)
        Return sb.ToString
    End Function

    Private Sub Do_Incentius()
        Dim oProduct As New DTOProduct(mArt.Guid)
        Dim oFrm As New Frm_Incentius(oProduct)
        oFrm.Show()
    End Sub

    Private Sub Do_LangText()
        Dim oLangText As DTOLangText = _Sku.Description
        oLangText.SetSrc(DTOLangText.SrcTypes.Sku, _Sku)
        Dim oFrm As New Frm_Translate(oLangText)
        oFrm.Show()
    End Sub

End Class
