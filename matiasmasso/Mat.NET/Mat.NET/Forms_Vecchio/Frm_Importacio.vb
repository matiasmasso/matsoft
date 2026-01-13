Imports System.Xml

Public Class Frm_Importacio

    Private mImportacio As Importacio
    'Private mMgz As DTOMgz
    Private mNewRemesa As Boolean
    Private mDirtyCell As Boolean
    Private mDsArts As DataSet
    Private mDsDocs As DataSet
    Private mLastValidatedObject As Object
    Private mConfirmacioCamio As Xml.XmlDocument
    'Private mEmp as DTOEmp
    Private _DocFile As DTODocFile
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Enum ColsArts
        Guid
        Ico
        Id
        Nom
        Qty
        Cfm
    End Enum

    Private Enum ColsDocs
        Src
        Guid
        Ico
        Txt
        Hash
    End Enum

    Private Enum Tabs
        Gral
        Docs
        Arts
    End Enum

    'Public Sub OldNew(ByVal oImportacio As Importacio, ByVal oDocFile As DTODocFile)
    'MyBase.New()
    'Me.InitializeComponent()

    '_DocFile = oDocFile

    'mImportacio = oImportacio
    'mMgz = BLL.BLLApp.Mgz
    'SetImportacio()
    'Me.Show()
    'ImportaDocFile(oDocFile)
    'End Sub

    Public Sub New(ByVal oImportacio As Importacio)
        MyBase.New()
        Me.InitializeComponent()

        mImportacio = oImportacio
        ImportacioLoader.Load(mImportacio)
        'mMgz = BLL.BLLApp.Mgz
        SetImportacio()
    End Sub

    Public Sub New(ByVal oDoc As Xml.XmlDocument)
        MyBase.New()
        Me.InitializeComponent()

        LoadIncoterms()
        Dim rc As Boolean = False
        Dim sErr As String = ""
        Dim sTipo As String = oDoc.DocumentElement.GetAttribute("TYPE")
        If sTipo = "AVISOCAMION" Then
            Dim sNum As String = oDoc.DocumentElement.GetAttribute("NUMERO")
            If IsNumeric(sNum) Then
                Dim sFch As String = oDoc.DocumentElement.GetAttribute("DATE")
                Dim sGuid As String = oDoc.DocumentElement.GetAttribute("Guid")
                If IsDate(sFch) Then
                    Dim DtFch As Date = CDate(sFch)
                    Dim oImportacio As New Importacio(New Guid(sGuid))
                    Me.Text = "REMESA DE IMPORTACIO NUM." & mImportacio.Id
                    RefrescaGral()
                    MergeConfirmacioCamio(oDoc)
                    Xl_ImportacioDocs1.Load(oImportacio)
                    rc = True
                Else
                    sErr = "Data incorrecte " & sFch
                End If
            Else
                sErr = "NO HI HA NUMERO DE REMESA"
            End If
        Else
            sErr = "FITXER DESCONEGUT"
        End If

        If Not rc Then
            MsgBox(sErr, MsgBoxStyle.Exclamation, "MAT.NET")
        End If

    End Sub


    Private Sub Frm_Importacio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AllowDrop = True
        _AllowEvents = True
    End Sub

    Private Sub SetImportacio()
        LoadIncoterms()
        If mImportacio.IsNew Then
            mNewRemesa = True
            Me.Text = "NOVA REMESA DE IMPORTACIO"
            TextBoxYea.Text = Today.Year
            DateTimePicker1.Value = Today
            EnableButton()
            ButtonDel.Enabled = False
        Else
            Me.Text = "REMESA DE IMPORTACIO NUM." & mImportacio.Id
            TextBoxYea.ReadOnly = True
            DateTimePicker1.Value = mImportacio.Fch
            'EnableButton()
            ButtonDel.Enabled = True
        End If
        RefrescaGral()
        LoadArts(mImportacio.AvisCamio)
        Xl_ImportacioDocs1.Load(mImportacio)

    End Sub


    Private Sub RefrescaGral()
        With mImportacio
            If .Proveidor IsNot Nothing Then
                Xl_ContactPrv.Contact = .Proveidor
                PictureBoxLogoPrv.Image = .Proveidor.Img48
            End If
            If .Transportista IsNot Nothing Then
                Xl_ContactTrp.Contact = MaxiSrvr.Contact.FromNum(.Transportista.Emp, .Transportista.Id)
                PictureBoxLogoTrp.Image = .Transportista.Img48
            End If
            TextBoxYea.Text = .Yea
            TextBoxBultos.Text = .Bultos
            TextBoxKg.Text = .Kg
            TextBoxM3.Text = .M3
            TextBoxObs.Text = .Obs
            Xl_Lookup_Ship1.Ship = .Ship
            If .Amt IsNot Nothing Then TextBoxAmt.Text = .Amt.CurFormat
            TextBoxIntrastat.Text = .Intrastats
            If .IncoTerm IsNot Nothing Then ComboBoxIncoterms.SelectedValue = .IncoTerm.Id
            If .CodiMercancia IsNot Nothing Then
                TextBoxCodiMercancia.Text = .CodiMercancia.Id
                TextBoxDscMercancia.Text = .CodiMercancia.Dsc
            End If
            Xl_PaisOrigen.Country = .CountryOrigen
        End With
    End Sub

    Private Sub Frm_Importacio_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim fileNames() As String = Nothing

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                fileNames = e.Data.GetData(DataFormats.FileDrop)
                Dim sFilename As String = fileNames(0)
                ' get the actual raw file into memory
                Dim oFileStream As New System.IO.FileStream(sFilename, IO.FileMode.Open)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oFileStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oFileStream.Length)
                oBinaryReader.Close()
                ImportaConfirmacioCamio(oStream, sFilename)
            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                '
                ' the first step here is to get the filename
                ' of the attachment and
                ' build a full-path name so we can store it 
                ' in the temporary folder
                '
                ' set up to obtain the FileGroupDescriptor 
                ' and extract the file name
                Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)

                ' used to build the filename from the FileGroupDescriptor block
                Dim sfilename As String = ""
                For i As Integer = 76 To 512
                    If fileGroupDescriptor(i) = 0 Then Exit For
                    sfilename = sfilename & Convert.ToChar(fileGroupDescriptor(i))
                Next
                theStream.Close()

                '
                ' Second step:  we have the file name.  
                ' Now we need to get the actual raw
                ' data for the attached file .
                '

                ' get the actual raw file into memory
                Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
                ImportaConfirmacioCamio(oStream, sfilename)
            Else
                MsgBox("format desconegut")
            End If
        Catch ex As Exception
            MsgBox("Error in DragDrop function: " + ex.Message)
        End Try
    End Sub

    Private Sub ImportaConfirmacioCamio(ByVal oStream As Byte(), Optional ByVal sFilename As String = "")
        Dim sB As New System.Text.StringBuilder
        For i As Integer = 0 To oStream.Length - 1
            sB.Append(Chr(oStream(i)))
        Next
        mConfirmacioCamio = New Xml.XmlDocument
        mConfirmacioCamio.LoadXml(sB.ToString)
        MergeConfirmacioCamio(mConfirmacioCamio)
    End Sub

    Private Function GetPurchaseOrderItems(ByVal oXmlDoc As XmlDocument) As List(Of PurchaseOrderItem)
        Dim retval As New List(Of PurchaseOrderItem)
        Dim oItem As PurchaseOrderItem
        If oXmlDoc.DocumentElement IsNot Nothing Then
            For Each oNode As XmlNode In oXmlDoc.DocumentElement.SelectSingleNode("ITEMS").ChildNodes
                oItem = New PurchaseOrderItem
                With oItem
                    If oNode.Attributes("QTY") IsNot Nothing Then
                        .Qty = oNode.Attributes("QTY").Value
                    End If
                    .Sku = ProductSkuLoader.FromNum(BLL.BLLApp.Emp, oNode.Attributes("REF").Value)
                End With
                retval.Add(oItem)
            Next
        End If

        Return retval
    End Function

    Private Sub MergeConfirmacioCamio(ByVal oXmlDoc As XmlDocument)
        Dim oItems As List(Of PurchaseOrderItem) = GetPurchaseOrderItems(oXmlDoc)
        Xl_ImportacioArts1.Load(oItems)
    End Sub


    Private Sub Frm_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mImportacio
            .Proveidor = New Proveidor(Xl_ContactPrv.Contact.Guid)
            If Xl_ContactTrp.Contact IsNot Nothing Then
                .Transportista = New Transportista(Xl_ContactTrp.Contact.Guid)
            End If
            .Fch = DateTimePicker1.Value
            .Bultos = IIf(IsNumeric(TextBoxBultos.Text), TextBoxBultos.Text, 0)
            .Kg = IIf(IsNumeric(TextBoxKg.Text), TextBoxKg.Text, 0)
            .M3 = IIf(IsNumeric(TextBoxM3.Text), TextBoxM3.Text, 0)
            .Obs = TextBoxObs.Text
            .Ship = Xl_Lookup_Ship1.Ship
            .AvisCamio = GetXmlArts()
            If mConfirmacioCamio IsNot Nothing Then
                .ConfirmacioCamio = mConfirmacioCamio
            End If
            .CodiMercancia = New MaxiSrvr.CodiMercancia(TextBoxCodiMercancia.Text)
            .IncoTerm = New MaxiSrvr.IncoTerm(ComboBoxIncoterms.SelectedValue)
            .CountryOrigen = Xl_PaisOrigen.Country
        End With

        Dim exs As New List(Of Exception)
        If mImportacio.Update(exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(mImportacio))
            If mNewRemesa Then
                MsgBox("remesa registrada amb el numero " & mImportacio.Id)
            End If
            Me.Close()
        Else
            MsgBox("error al desar la remesa " & mImportacio.Id & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Function CreateDataTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Guid", System.Type.GetType("System.Guid")))
            .Add(New DataColumn("ICO", System.Type.GetType("System.Byte[]")))
            .Add(New DataColumn("ART", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("NOM", System.Type.GetType("System.String")))
            .Add(New DataColumn("QTY", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("CFM", System.Type.GetType("System.Int32")))
        End With
        Return oTb
    End Function



    Private Function GetXmlArts(Optional ByVal BlOnlySelectedRows As Boolean = False) As XmlDocument

        Dim oDom As New XmlDocument

        Dim sNum As String = Guid.NewGuid().ToString.Substring(1, 4)

        Dim oNodeRoot As XmlElement = oDom.CreateElement("DOCUMENT")
        oNodeRoot.SetAttribute("TYPE", "AVISOCAMION")
        oNodeRoot.SetAttribute("DATE", Today.ToShortDateString)
        oNodeRoot.SetAttribute("NUMERO", sNum)
        'oNodeRoot.SetAttribute("TITLE", TextBoxObs.Text)
        oDom.AppendChild(oNodeRoot)

        Dim oNodeRte As XmlElement = oDom.CreateElement("REMITE")
        oNodeRte.SetAttribute("NIF", "A58007857")
        oNodeRte.SetAttribute("NOM", "MATIAS MASSO, S.A.")
        oNodeRoot.AppendChild(oNodeRte)

        Dim oNodeDestino As XmlElement = oDom.CreateElement("DESTINO")
        oNodeDestino.SetAttribute("NIF", "A62572342")
        oNodeDestino.SetAttribute("NOM", "VIVACE LOGISTICA, S.A.")
        oNodeRoot.AppendChild(oNodeDestino)

        Dim oNodeExp As XmlElement = oDom.CreateElement("EXPEDICION")
        oNodeExp.SetAttribute("NUMPROVEEDOR", Xl_ContactPrv.Contact.Id.ToString)
        oNodeExp.SetAttribute("PROCEDENCIA", Xl_ContactPrv.Contact.Nom)
        oNodeExp.SetAttribute("TRANSITARIO", TextBoxObs.Text)
        oNodeExp.SetAttribute("LLEGADA", DateTimePicker1.Value.ToShortDateString)
        oNodeExp.SetAttribute("BULTOS", TextBoxBultos.Text)
        oNodeExp.SetAttribute("KG", TextBoxKg.Text)
        oNodeExp.SetAttribute("M3", TextBoxM3.Text)
        oNodeRoot.AppendChild(oNodeExp)

        Dim oNodeItems As XmlElement = oDom.CreateElement("ITEMS")
        oNodeRoot.AppendChild(oNodeItems)

        Dim oItem As PurchaseOrderItem = Nothing
        For Each oItem In Xl_ImportacioArts1.PurchaseOrderItems
            Dim oNodeItem As XmlElement = oDom.CreateElement("ITEM")
            'oNodeItem.SetAttribute("Guid", oItem.Sku.Guid.ToString)
            oNodeItem.SetAttribute("QTY", oItem.Qty)
            oNodeItem.SetAttribute("REF", oItem.Sku.Sku)
            oNodeItem.SetAttribute("DSC", oItem.Sku.Nom_Esp)
            oNodeItems.AppendChild(oNodeItem)

        Next

        Return oDom
    End Function

    Private Sub LoadArts(ByVal oXmlDoc As XmlDocument)
        If oXmlDoc IsNot Nothing Then
            Dim oItems As List(Of PurchaseOrderItem) = GetPurchaseOrderItems(oXmlDoc)
            Xl_ImportacioArts1.Load(oItems)
        End If
    End Sub

    Private Sub PartialMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFilename As String = MaxiSrvr.TmpFolder & "AvisArribadaCamion " & mImportacio.Id & ".xml"
        GetXmlArts(True).Save(sFilename)
        SendMail(sFilename)
        My.Computer.FileSystem.DeleteFile(sFilename)
    End Sub

    Public Sub SendMail(ByVal sFilename As String)
        Dim oMgz As Mgz = New Mgz(BLL.BLLApp.Mgz.Guid)

        Dim sTo As String = oMgz.Email
        Dim oCc As System.Net.Mail.MailAddressCollection = BLL.BLLSubscription.EmailAddressCollection(DTOSubscription.Ids.AvisArribadaCamion)
        Dim oAttachments As New ArrayList
        oAttachments.Add(sFilename)
        Dim exs As New List(Of Exception)
        If BLL.MailHelper.SendMail(sTo, oCc, , Me.Text, , , oAttachments, exs) Then
            MsgBox("missatge enviat correctament a " & sTo, MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("missatge no enviat." & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
        End If
        oAttachments.Clear()

    End Sub



    Private Sub Xl_ContactPrv_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactPrv.AfterUpdate
        Dim oProveidor As New Proveidor(Xl_ContactPrv.Contact.Guid)
        mImportacio = oProveidor.NewImportacio
        SetImportacio()
    End Sub

    Private Sub Xl_ContactTrp_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactTrp.AfterUpdate
        PictureBoxLogoTrp.Image = Xl_ContactTrp.Contact.Img48
    End Sub

    Private Sub ToolStripButtonMailMgz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonMailMgz.Click
        Dim sFilename As String = MaxiSrvr.TmpFolder & "AvisArribadaCamion " & mImportacio.Id & ".xml"
        GetXmlArts.Save(sFilename)
        SendMail(sFilename)
        'My.Computer.FileSystem.DeleteFile(sFilename)
    End Sub

    Private Sub Control_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePicker1.ValueChanged, _
     TextBoxBultos.TextChanged, _
      TextBoxKg.TextChanged, _
       TextBoxM3.TextChanged, _
        TextBoxObs.TextChanged, _
         Xl_PaisOrigen.AfterUpdate, _
          Xl_ImportacioArts1.AfterUpdate, _
           ComboBoxIncoterms.SelectedIndexChanged

        If _AllowEvents Then EnableButton()
    End Sub

    Private Sub EnableButton()
        ButtonOk.Enabled = True
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDsArts).Visible = True
    End Sub

    Private Sub LoadIncoterms()
        Dim SQL As String = "SELECT ID FROM INCOTERMS ORDER BY ID"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        With ComboBoxIncoterms
            .ValueMember = "ID"
            .DisplayMember = "ID"
            .DataSource = oDs.Tables(0)
        End With
    End Sub

    Private Sub ButtonSearchCodiMercancia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchCodiMercancia.Click
        Dim oFrm As New Frm_CodisMercancia(Frm_CodisMercancia.Modes.ForSelection)
        AddHandler oFrm.AfterSelect, AddressOf OnCodiMercanciaChanged
        oFrm.Show()
    End Sub

    Private Sub OnCodiMercanciaChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCodiMercancia As MaxiSrvr.CodiMercancia = CType(sender, MaxiSrvr.CodiMercancia)
        TextBoxCodiMercancia.Text = oCodiMercancia.Id
        TextBoxDscMercancia.Text = oCodiMercancia.Dsc
        If _AllowEvents Then EnableButton()
    End Sub


    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la remesa " & mImportacio.Id & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If mImportacio.Delete(exs) Then
                RaiseEvent AfterUpdate(mImportacio, EventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la remesa")
            End If
        End If
    End Sub

    Private Sub Xl_ImportacioDocs1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ImportacioDocs1.AfterUpdate
        If _AllowEvents Then
            mImportacio.Amt = New Amt()
            For Each oItem As ImportacioItem In mImportacio.Items
                mImportacio.Amt.Add(oItem.Amt)
            Next
            TextBoxAmt.Text = mImportacio.Amt.CurFormat
            EnableButton()
        End If
    End Sub
End Class