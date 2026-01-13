Public Class Menu_Art
    Private _Sku As DTOProductSku
    Private _Skus As List(Of DTOProductSku)
    Private mArt As Art
    Private mArts As Arts
    Private _Mgz As DTOMgz
    Private mStk As Integer
    Private mPn2 As Integer
    Private mPn3 As Integer
    Private mPn1 As Integer
    Private mPrv As Integer
    Private mColor As System.Drawing.Color

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oArt As Art, Optional oMgz As DTOMgz = Nothing)
        MyBase.New()
        mArt = oArt
        mArts = New Arts
        mArts.Add(mArt)

        _Sku = New DTOProductSku(oArt.Guid)
        _Skus = New List(Of DTOProductSku)
        _Skus.Add(_Sku)

        _Mgz = oMgz
    End Sub


    Public Sub New(ByVal oSku As DTOProductSku, Optional oMgz As DTOMgz = Nothing)
        MyBase.New()
        _Sku = oSku
        _Skus = New List(Of DTOProductSku)
        _Skus.Add(_Sku)

        mArt = New Art(oSku.Guid)
        mArts = New Arts
        mArts.Add(mArt)

        _Mgz = oMgz
    End Sub

    Public Sub New(ByVal oArts As Arts, Optional oMgz As DTOMgz = Nothing)
        MyBase.New()
        mArts = oArts
        mArt = mArts(0)

        _Skus = New List(Of DTOProductSku)
        For Each oArt As Art In oArts
            Dim oSku As New DTOProductSku(oArt.Guid)
            _Skus.Add(oSku)
        Next
        _Sku = _Skus(0)

        _Mgz = oMgz
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Pn2(), _
        MenuItem_Pn1(), _
        MenuItem_Previsio(), _
        MenuItem_Arc(), _
        MenuItem_Atlas(), _
        MenuItem_Chart(), _
        MenuItem_Incidencies(), _
        MenuItem_Incentius(), _
        MenuItem_Web(), _
        MenuItem_CopyLink(), _
        MenuItem_CopyLinkImg(), _
        MenuItem_ImgExport(), _
        MenuItem_ImgImport(), _
        MenuItem_HiResImg(), _
        MenuItem_HiResImgImport(), _
        MenuItem_Circulars(), _
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
        Dim iPn1 As Integer = mArt.Pn1
        If iPn1 = 0 Then
            oMenuItem.Enabled = False
        Else
            AddHandler oMenuItem.Click, AddressOf Do_Pn1
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Previsio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Previsio"
        'oMenuItem.Image = My.Resources.prismatics
        If mArt.Previsio = 0 Then
            oMenuItem.Enabled = False
        Else
            AddHandler oMenuItem.Click, AddressOf Do_Previsio
        End If
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
        oMenuItem.Visible = mArt.Image IsNot Nothing
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

    Private Function MenuItem_HiResImg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "imatges alta resolució"
        'oMenuItem.Image = My.Resources.candau
        oMenuItem.ShortcutKeys = Shortcut.CtrlJ

        AddHandler oMenuItem.Click, AddressOf Do_HiResImgs
        Return oMenuItem
    End Function

    Private Function MenuItem_HiResImgImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "registrar imatge alta resolució"
        'oMenuItem.Image = My.Resources.candau
        oMenuItem.Visible = (BLL.BLLSession.Current.User.Rol.Id = DTORol.Ids.SuperUser)
        oMenuItem.ShortcutKeys = Shortcut.CtrlJ

        AddHandler oMenuItem.Click, AddressOf Do_ImportHiResImg
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
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
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
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
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


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Art(mArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pn2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowArtPncs(mArt, DTOPurchaseOrder.Codis.Client)
    End Sub

    Private Sub Do_Pn1(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowArtPncs(mArt, DTOPurchaseOrder.Codis.Proveidor)
    End Sub

    Private Sub Do_Previsio(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowArtPrevisions(mArt)
    End Sub

    Private Sub Do_Arc(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowArtArc(mArt, _Mgz)
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
        Dim oDlg As New Windows.Forms.SaveFileDialog
        Dim oResult As Windows.Forms.DialogResult
        With oDlg
            .Title = "Guardar imatge de producte"
            .FileName = mArt.FullNom
            .Filter = sImgFilter
            '.FilterIndex = 1
            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
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

    Private Sub Do_HiResImgs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductGallery(_Sku)
        oFrm.Show()
    End Sub

    Private Sub Do_ImportHiResImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFilter As String = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Registrar imatge de alta resolució"
            .Filter = "*.jpg|*.jpg|*.gif|*.gif|tots els arxius|*.*"
            'Dim sCurrentDirectory As String = Environment.CurrentDirectory
            'Dim sRootPath As String = HighResImage.RootPath
            'If Not sCurrentDirectory.StartsWith(sRootPath) Then
            '.InitialDirectory = HighResImage.RootPath
            'End If
            Select Case .ShowDialog
                Case DialogResult.OK
                    'Dim oTarget As New DownloadTarget(mArt.Guid, DownloadTarget.Cods.Producte, DownloadSrc.Ids.Imatge_Alta_Resolucio)
                    'oTarget.LoadFromFilename(.FileName)
                    'Dim oFrm As New Frm_ProductDownload(oTarget)
                    'oFrm.Show()
            End Select
        End With
        oDlg = Nothing
    End Sub

    Private Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLProductSku.Load(_Sku)
        Dim oStat As New DTOStat
        With oStat
            .Lang = BLL.BLLApp.Lang
            .Product = _Sku
            .ConceptType = DTOStat.ConceptTypes.Geo
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
        Dim oQuery As DTOIncidenciaQuery = BLL_Incidencies.DefaultQuery(BLL.BLLSession.Current.User)
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
                oCsv.AddValues(oClient.Nom_o_NomComercial, oClient.Adr.Text, oClient.Adr.Zip.ZipyCit, oClient.DefaultTel)
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
        Dim oFrm As New frm_ArtCustRefs(mArt)
        oFrm.Show()
    End Sub

    Private Sub Do_NewEAN(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("assignem a aquest article un codi EAN del rang de codis assignats a M+O per AECOC?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim oEan As MaxiSrvr.Ean13 = AECOC.NextEanToArt(mArt)
            MsgBox("EAN " & oEan.Value & " assignat a " & mArt.Nom(BLL.BLLApp.Lang), MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
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
        Dim oProduct As New Product(mArt)
        Dim oFrm As New Frm_Incentius(oProduct)
        oFrm.Show()
    End Sub

End Class
