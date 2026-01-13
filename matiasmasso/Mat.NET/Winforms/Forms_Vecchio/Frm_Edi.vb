Public Class Frm_Edi
    Private mMenuItemHideProcessed As ToolStripMenuItem
    Private mAllowEvents As Boolean

    Private Enum Tabs
        Inbox
        Outbox
    End Enum

    Private Enum Cols
        Guid
        Filename
        FchCreated
        Fch
        Amt
        Sender
        Receiver
        Result
        Doc
    End Enum

    Private Sub Frm_Edi_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        mMenuItemHideProcessed = New ToolStripMenuItem("ocultar fitxers ja processats", Nothing, AddressOf onHideProcessed)
        mMenuItemHideProcessed.CheckOnClick = True
        mMenuItemHideProcessed.Checked = True

        LoadHeader()
    End Sub

    Private Sub LoadHeader()
        Dim SQL As String = "SELECT Tag FROM EDI GROUP BY Tag ORDER BY Tag"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With CurrentHeaderGrid()
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(0)
                .HeaderText = "missatge"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

        Dim sDefaultValue As String = EdiFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
        For Each oRow As DataGridViewRow In CurrentHeaderGrid.Rows
            If Not IsDBNull(oRow.Cells(0).Value) Then
                If oRow.Cells(0).Value = sDefaultValue Then
                    CurrentHeaderGrid.CurrentCell = oRow.Cells(0)
                    oRow.Selected = True
                    LoadDetail()
                    Exit For
                End If
            End If
        Next

    End Sub

    Private Function CurrentTag() As EdiFile.Tags
        Dim retval As EdiFile.Tags = EdiFile.Tags.Unknown
        Dim oRow As DataGridViewRow = CurrentHeaderGrid.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCell As DataGridViewCell = oRow.Cells(0)
            Dim sTagNom As String = oCell.Value.ToString
            Dim oTag As EdiFile.Tags = EdiFile.Tags.Unknown
            If [Enum].TryParse(Of EdiFile.Tags)(sTagNom, oTag) Then
                retval = oTag
            End If
        End If
        Return retval
    End Function

    Private Function CurrentEdiFile() As EdiFile
        Dim retval As EdiFile = Nothing
        Dim oRow As DataGridViewRow = CurrentDetailGrid.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCell As DataGridViewCell = oRow.Cells(Cols.Guid)
            Dim oGuid As Guid = CType(oCell.Value, Guid)
            Select Case CurrentTag()
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    retval = New EdiFile_ORDERS_D_96A_UN_EAN008(oGuid)
                Case EdiFile.Tags.DESADV_D_96A_UN_EAN005
                    'retval = New EdiFile_DESADV_D_96A_UN_EAN005(oGuid)
                Case EdiFile.Tags.REMADV_D_96A_UN_EAN003
                    retval = New EdiFile_REMADV_D_96A_UN_EAN003(oGuid)
                Case Else
                    retval = New EdiFile(oGuid)
            End Select
        End If
        Return retval
    End Function

    Private Function CurrentEdiVersaFile() As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        Dim oRow As DataGridViewRow = CurrentDetailGrid.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCell As DataGridViewCell = oRow.Cells(Cols.Guid)
            Dim oGuid As Guid = CType(oCell.Value, Guid)
            retval = BLLEdiversaFile.Find(oGuid)
            BLLEdiversaFile.LoadSegments(retval)
        End If
        Return retval
    End Function

    Private Function CurrentEdiFiles() As EdiFiles
        Dim retval As New EdiFiles
        Dim item As EdiFile = Nothing
        For Each oRow As DataGridViewRow In DataGridViewInboxDetail.SelectedRows
            Dim oCell As DataGridViewCell = oRow.Cells(Cols.Guid)
            Dim oGuid As Guid = CType(oCell.Value, Guid)
            Select Case CurrentTag()
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    item = New EdiFile_ORDERS_D_96A_UN_EAN008(oGuid)
                Case EdiFile.Tags.DESADV_D_96A_UN_EAN005
                    'item = New EdiFile_DESADV_D_96A_UN_EAN005(oGuid)
                Case EdiFile.Tags.REMADV_D_96A_UN_EAN003
                    item = New EdiFile_REMADV_D_96A_UN_EAN003(oGuid)
                Case Else
                    item = New EdiFile(oGuid)
            End Select
            retval.Add(item)
        Next orow
        Return retval
    End Function

    Private Function CurrentIO() As EdiFile.IOcods
        Dim retval As EdiFile.IOcods = EdiFile.IOcods.NotSet
        Select Case TabControl1.SelectedIndex
            Case Tabs.Inbox
                retval = EdiFile.IOcods.Inbox
            Case Tabs.Outbox
                retval = EdiFile.IOcods.Outbox
        End Select
        Return retval
    End Function

    Private Function CurrentFilter() As EdiFiles.Filters
        Dim retval As EdiFiles.Filters
        Select Case mMenuItemHideProcessed.Checked
            Case True
                retval = EdiFiles.Filters.ShowPending
            Case Else
                retval = EdiFiles.Filters.ShowAll
        End Select
        Return retval
    End Function

    Private Function CurrentHeaderGrid() As DataGridView
        Dim retval As DataGridView = Nothing
        Select Case CurrentIO()
            Case EdiFile.IOcods.Inbox
                retval = DataGridViewInboxHeader
            Case EdiFile.IOcods.Outbox
                retval = DataGridViewOutboxHeader
        End Select
        Return retval
    End Function

    Private Function CurrentDetailGrid() As DataGridView
        Dim retval As DataGridView = Nothing
        Select Case CurrentIO()
            Case EdiFile.IOcods.Inbox
                retval = DataGridViewInboxDetail
            Case EdiFile.IOcods.Outbox
                retval = DataGridViewOutboxDetail
        End Select
        Return retval
    End Function

    Private Sub LoadDetail()
        Dim oTag As EdiFile.Tags = CurrentTag()
        mAllowEvents = False
        With CurrentDetailGrid()
            .RowTemplate.Height = .Font.Height * 1.3
            .AutoGenerateColumns = False

            Select Case oTag
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    .DataSource = EdiFiles_ORDERS_D_96A_UN_EAN008.All(CurrentIO, CurrentFilter, TextBoxFiltro.Text)
                Case Else
                    .DataSource = EdiFiles.All(CurrentIO, oTag.ToString, CurrentFilter, TextBoxFiltro.Text)
            End Select

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            .Columns.Clear()
            .Columns.Add("Guid", "Guid")
            .Columns.Add("Filename", "fitxer")
            .Columns.Add("FchCreated", "rebut")
            .Columns.Add("Fch", "data")
            .Columns.Add("Amt", "import")
            .Columns.Add("Sender", "remitent")
            .Columns.Add("Receiver", "destinatari")
            .Columns.Add("Result", "resultat")

            Select Case oTag
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    .Columns.Add("DocumentNumber", "comanda")
                Case EdiFile.Tags.DESADV_D_96A_UN_EAN005
                    .Columns.Add("DocumentNumber", "albará")
            End Select

            With .Columns(Cols.Guid)
                .DataPropertyName = "Guid"
                .Visible = False
            End With
            With .Columns(Cols.Filename)
                .HeaderText = "fitxer"
                .DataPropertyName = "Filename"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.FchCreated)
                .HeaderText = "rebut"
                .DataPropertyName = "FchCreated"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 105
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .DataPropertyName = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .Width = 70
            End With


            Select Case CurrentIO()
                Case EdiFile.IOcods.Inbox
                    With .Columns(Cols.Sender)
                        .HeaderText = "remitent"
                        .DataPropertyName = "SenderEanOrNom"
                        '.DataPropertyName = "Sender"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    End With
                    .Columns(Cols.Receiver).Visible = False
                Case EdiFile.IOcods.Outbox
                    With .Columns(Cols.Receiver)
                        .HeaderText = "destinatari"
                        .DataPropertyName = "ReceiverEanOrNom"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                        .Visible = True
                    End With
                    .Columns(Cols.Sender).Visible = False
            End Select


            Select Case oTag
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    With .Columns(Cols.Doc)
                        .HeaderText = "comanda"
                        .DataPropertyName = "DocumentNumber"
                        '.DataPropertyName = "Sender"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    End With
                Case EdiFile.Tags.DESADV_D_96A_UN_EAN005
                    With .Columns(Cols.Doc)
                        .HeaderText = "albará"
                        .DataPropertyName = "DocumentNumber"
                        '.DataPropertyName = "Sender"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    End With
                Case Else
                    With .Columns(Cols.Sender)
                        .HeaderText = "remitent"
                        .DataPropertyName = "Sender"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    End With
            End Select

            With .Columns(Cols.Result)
                .DataPropertyName = "Result"
                .Visible = False
            End With

            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(Cols.Fch)
            End If
        End With


        SetContextMenu2()
        mAllowEvents = True
    End Sub


    Private Sub DataGridViewHeader_SelectionChanged(sender As Object, e As System.EventArgs) Handles _
        DataGridViewInboxHeader.SelectionChanged, _
         DataGridViewOutboxHeader.SelectionChanged
        If mAllowEvents Then
            LoadDetail()
        End If
    End Sub

    Private Sub onHideProcessed(sender As Object, e As System.EventArgs)
        If mAllowEvents Then
            LoadDetail()
        End If
    End Sub

    Private Sub SetContextMenu2()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oFile As EdiFile = CurrentEdiFile()
        If oFile IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("Veure fitxer EDI", Nothing, AddressOf Zoom)
            oContextMenu.Items.Add(oMenuItem)

            Dim oTag As EdiFile.Tags = CurrentTag()
            Select Case oTag
                Case EdiFile.Tags.ORDERS_D_96A_UN_EAN008
                    oMenuItem = New ToolStripMenuItem("processa comanda", Nothing, AddressOf Do_ProcesaOrders)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("processa totes les comandes validades", Nothing, AddressOf Do_ProcesaAll)
                    oContextMenu.Items.Add(oMenuItem)
                Case EdiFile.Tags.ORDRSP_D_96A_UN_EAN005
                    oMenuItem = New ToolStripMenuItem("processa confirmació de comanda", Nothing, AddressOf Do_Procesa)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("processa totes les confirmacions validades", Nothing, AddressOf Do_ProcesaAll)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("veure confirmació de comanda", Nothing, AddressOf Do_ShowPdcConfirm)
                    If oFile.Result <> EdiFile.Results.Processed Then oMenuItem.Enabled = False
                    oContextMenu.Items.Add(oMenuItem)
                Case EdiFile.Tags.DESADV_D_96A_UN_EAN005
                    oMenuItem = New ToolStripMenuItem("processa albará", Nothing, AddressOf Do_Procesa)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("processa tots els albarans validats", Nothing, AddressOf Do_ProcesaAll)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("veure la comanda", Nothing, AddressOf Do_ShowPdcFromDesadv)
                    oContextMenu.Items.Add(oMenuItem)

                Case EdiFile.Tags.INVOIC_D_93A_UN_EAN007
                Case EdiFile.Tags.GENRAL_D_96A_UN_EAN003
                    oMenuItem = New ToolStripMenuItem("envia per email", Nothing, AddressOf Do_MailGenral)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("mostra el text", Nothing, AddressOf Do_ShowGenral)
                    oContextMenu.Items.Add(oMenuItem)
                Case EdiFile.Tags.REMADV_D_96A_UN_EAN003
                    oMenuItem = New ToolStripMenuItem("processa remesa", Nothing, AddressOf Do_ProcesaRemadv)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("processa totes les remeses validades", Nothing, AddressOf Do_ProcesaRemadvs)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("detall de la remesa de cobrament", Nothing, AddressOf Do_ShowRemadv)
                    oContextMenu.Items.Add(oMenuItem)
                Case EdiFile.Tags.SLSRPT_D_96A_UN_EAN004
                Case EdiFile.Tags.GENRAL_D_96A_UN_EAN003
            End Select

            oMenuItem = New ToolStripMenuItem("retrocedeix status a per processar", Nothing, AddressOf Do_Retrocedeix)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("elimina", Nothing, AddressOf Do_Elimina)
            oContextMenu.Items.Add(oMenuItem)

            oContextMenu.Items.Add("-")
        End If

        oMenuItem = New ToolStripMenuItem("importar fitxer", Nothing, AddressOf Do_Importar)
        oContextMenu.Items.Add(oMenuItem)

        oContextMenu.Items.Add(mMenuItemHideProcessed)

        CurrentDetailGrid.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_EdiFile(CurrentEdiFile)
        AddHandler oFrm.afterUpdate, AddressOf RefreshRequest2
        oFrm.Show()
    End Sub

    Private Sub Do_ShowGenral()
        Dim oFileOld As EdiFile = CurrentEdiFile()
        Dim oFile As DTOEdiversaFile = BLLEdiversaFile.Find(oFileOld.Guid)
        Dim sBody As String = BLLEdiversaGenral.body(oFile)
        MsgBox(sBody)
    End Sub

    Private Sub Do_MailGenral()
        Dim oFileOld As EdiFile = CurrentEdiFile()
        Dim oFile As DTOEdiversaFile = BLLEdiversaFile.Find(oFileOld.Guid)

        Dim exs As New List(Of Exception)
        If BLLEdiversaGenral.Mail(oFile, exs) Then
            MsgBox("missatge enviat correctament", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ShowPdcFromDesadv()
        'Dim oDesadv As EdiFile_DESADV_D_96A_UN_EAN005 = CurrentEdiFile()
        'Dim sPdcRef As String = oDesadv.NumeroDeComanda
        'Dim oPdc As Pdc = Pdc.FromPdcRef(sPdcRef)
        'If oPdc IsNot Nothing Then
        ' Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
        ' Dim exs As New List(Of Exception)
        ' If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
        ' UIHelper.WarnError(exs)
        ' Else
        ' Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
        ' 'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        ' oFrm.Show()
        '  End If
        ' End If
    End Sub

    Private Sub Do_Procesa()
        MsgBox("aquesta operació ja no es fa per aquest programa." & vbCrLf & "Utilitzar el de Directori->Clients->Comandes Edi", MsgBoxStyle.Exclamation)
        'Dim oEdiFile As EdiFile = CurrentEdiFile()
        'Dim exs As New List(Of Exception)
        'If oEdiFile.Procesa(exs) Then
        ' RefreshRequest2()
        ' Else
        ' Dim oFrm As New Frm_eDiversaInboxErrors(oEdiFile, exs)
        ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        ' oFrm.Show()
        ' End If
    End Sub

    Private Sub Do_Importar()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar fitxer EDI de Ediversa"
            .Filter = "fitxer pla ediversa (*.pla)|*.pla|tots els fitxers (*.*)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim oEdiFile As EdiFile = EdiFile.FromFile(.FileName)
                Dim exs As New List(Of Exception)
                If oEdiFile.Update(exs) Then
                    RefreshRequest2()
                Else
                    UIHelper.WarnError(exs, "error all desar el fitxer")
                End If
            End If
        End With
    End Sub

    Private Sub Do_ProcesaAll()
        MsgBox("aquesta operació ja no es fa per aquest programa." & vbCrLf & "Utilitzar el de Directori->Clients->Comandes Edi", MsgBoxStyle.Exclamation)
        'Dim oTag As EdiFile.Tags = CurrentTag()
        'Dim oEdiFiles As New EdiFiles
        'For i As Integer = CurrentDetailGrid.Rows.Count - 1 To 0 Step -1
        ' Dim oRow As DataGridViewRow = CurrentDetailGrid.Rows(i)
        ' Dim oCell As DataGridViewCell = oRow.Cells(Cols.Guid)
        ' Dim oGuid As Guid = CType(oCell.Value, Guid)
        ' oEdiFiles.Add(oTag, oGuid)
        ' Next
        '
        '        Dim iCount As Integer
        '        Dim iCountDone As Integer
        '        Dim exs As New List(Of Exception)
        '        oEdiFiles.Procesa(iCount, iCountDone, exs)
        '        If iCountDone > 0 Then
        '        MsgBox("processats " & iCount & " missatges", MsgBoxStyle.Information, "MAT.NET")
        '        RefreshRequest2()
        '        Else
        '        MsgBox("cap dels " & iCount & " missatges ha passat la validació", MsgBoxStyle.Exclamation, "MAT.NET")
        '        End If

        '        If exs.Count > 0 Then
        '        If oTag = EdiFile.Tags.ORDERS_D_96A_UN_EAN008 Then
        '        Dim oExcelSheet As New DTOExcelSheet
        '        For Each oFile As EdiFile_ORDERS_D_96A_UN_EAN008 In oEdiFiles
        '        If oFile.ValidationErrors.Count > 0 Then
        '        oExcelSheet.AddRow()
        '        oExcelSheet.AddRowWithCells("cliente", oFile.SenderEanOrNom)
        '        If oFile.NADDP.Nom > "" Then oExcelSheet.AddRowWithCells("destino", oFile.NADDP.Nom)
        '        oExcelSheet.AddRowWithCells("pedido", oFile.DocumentNumber)
        '        oExcelSheet.AddRowWithCells("fecha", oFile.Fch)
        '        For Each oErr As Exception In oFile.ValidationErrors
        '        oExcelSheet.AddRow()
        '        oExcelSheet.AddRowWithCells("", "incidencia:", oErr.Message)
        'If TypeOf oErr.Sender Is EdiLineItem Then
        'Dim oLine As EdiLineItem = oErr.Sender
        'oExcelSheet.AddRowWithCells("", "", "linea:", oLine.LineNumber)
        'oExcelSheet.AddRowWithCells("", "", "EAN:", IIf(oLine.ArtEan.Value > 0, "", oLine.ArtEan.Value))
        'If oLine.PIALIN_Client IsNot Nothing Then oExcelSheet.AddRowWithCells("", "", "ref.cliente:", oLine.PIALIN_Client.Referencia)
        'If oLine.PIALIN_Proveidor IsNot Nothing Then oExcelSheet.AddRowWithCells("", "", "ref.proveedor:", oLine.PIALIN_Proveidor.Referencia)
        'If oLine.Descripcio IsNot Nothing Then oExcelSheet.AddRowWithCells("", "", "descripcion:", oLine.Descripcio)
        'oExcelSheet.AddRowWithCells("", "", "unidades:", oLine.QtyPedida)
        'oExcelSheet.AddRowWithCells("", "", "precio:", oLine.PreuBrut)
        'If oLine.ALCLIN_TD IsNot Nothing Then oExcelSheet.AddRowWithCells("", "", "descuento:", oLine.ALCLIN_TD.Percentatge)
        'End If
        '       Next
        '       End If
        '       Next
        '      UIHelper.ShowExcel(oExcelSheet)
        '      End If
        '
        '        End If
    End Sub


    Private Sub Do_Retrocedeix()
        Dim oEdiversaFile As DTOEdiversaFile = BLLEdiversaFile.Find(CurrentEdiFile.Guid)
        With oEdiversaFile
            .Result = EdiFile.Results.Pending
            .ResultBaseGuid = Nothing
            Dim exs As New List(Of Exception)
            If BLLEdiversaFile.Update(oEdiversaFile, exs) Then
                If .Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString And .IOCod = DTOEdiversaFile.IOcods.Inbox Then
                    Dim oEdiversaOrder As New DTOEdiversaOrder(.Guid)
                    If BLLEdiversaOrder.Load(oEdiversaOrder) Then
                        If Not BLLEdiversaOrder.Retrocedeix(oEdiversaOrder, exs) Then
                            UIHelper.WarnError(exs, "error al retrocedir el fitxer")
                        End If
                    End If
                End If
                RefreshRequest2()
            Else
                    UIHelper.WarnError(exs, "error al retrocedir el fitxer")
            End If
        End With
    End Sub

    Private Sub Do_Elimina()
        Dim exs As New List(Of Exception)
        Dim oEdiFile As EdiFile = CurrentEdiFile()
        With oEdiFile
            '.SetItm()
            Dim oSegments As EdiSegments = .Segments
            .Result = EdiFile.Results.Rejected
            .ResultGuid = Nothing

            If .Update(exs) Then
                RefreshRequest2()
            Else
                UIHelper.WarnError(exs, "error al eliminar el ftxer")
            End If
        End With
    End Sub

    Private Sub Do_ShowPdcConfirm()
        Dim oEdiFile As EdiFile = CurrentEdiFile()
        Dim oGuid As Guid = oEdiFile.ResultGuid
        'Dim oPdcConfirm As New PdcConfirm(oGuid)
        'Dim oFrm As New Frm_PdcConfirm(oPdcConfirm)
        'oFrm.Show()
    End Sub

    Private Sub Do_ShowRemadv()
        Dim oEdiFile As EdiFile_REMADV_D_96A_UN_EAN003 = CurrentEdiFile()
        Dim oFrm As New Frm_EdiversaRemadv(oEdiFile)
        oFrm.Show()
    End Sub

    Private Sub Do_ProcesaRemadv()
        Dim oEdiversaFile As DTOEdiversaFile = CurrentEdiVersaFile()
        Dim exs As New List(Of Exception)
        If BLLEdiversaFile.Procesa(oEdiversaFile, exs) Then
            RefreshRequest2()
            'MsgBox("Remesa processada", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If

        'Dim oEdiFile As EdiFile_REMADV_D_96A_UN_EAN003 = CurrentEdiFile()
        'Dim exs As New List(Of Exception)
        'If oEdiFile.Procesa(exs) Then
        ' RefreshRequest2()
        ' 'MsgBox("Remesa processada", MsgBoxStyle.Information)
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
    End Sub

    Private Sub Do_ProcesaRemadvs()
        Dim oEdiFiles As EdiFiles = CurrentEdiFiles
        Dim exs As New List(Of Exception)
        Dim iCount As Integer, iCountDone As Integer
        If oEdiFiles.Procesa(iCount, iCountDone, exs) Then
            MsgBox("Remesa processada", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ProcesaOrders()
        Dim oEdiFiles As EdiFiles = CurrentEdiFiles()
        Dim sb As New Text.StringBuilder
        Dim oEdiversaFile As DTOEdiversaFile = CurrentEdiVersaFile()

        Dim exs As New List(Of Exception)
        If BLLEdiversaFile.Procesa(oEdiversaFile, exs) Then
            sb.AppendLine("processat " & oEdiversaFile.Tag & " " & oEdiversaFile.FileName)
        Else
            sb.AppendLine("processat amb errors" & oEdiversaFile.Tag & " " & oEdiversaFile.FileName)
                For Each ex As Exception In exs
                    sb.AppendLine(ex.Message)
                Next
            End If
        MsgBox(sb.ToString)
    End Sub

    Private Sub DataGridViewDetail_DoubleClick(sender As Object, e As System.EventArgs) Handles _
        DataGridViewInboxDetail.DoubleClick,
         DataGridViewOutboxDetail.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridViewDetail_SelectionChanged(sender As Object, e As System.EventArgs) Handles _
        DataGridViewInboxDetail.SelectionChanged, _
         DataGridViewOutboxDetail.SelectionChanged
        If mAllowEvents Then
            SetContextMenu2()
        End If
    End Sub


    Private Sub RefreshRequest2()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.FchCreated
        Dim oGrid As DataGridView = CurrentDetailGrid()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadDetail()

        If oGrid.Rows.Count = 0 Then
        Else
            If iFirstRow < oGrid.Rows.Count Then
                oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow
            End If

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CurrentIO()
            Case EdiFile.IOcods.Outbox
                Static BlLoadedOutbox As Boolean
                If Not BlLoadedOutbox Then
                    LoadHeader()
                    BlLoadedOutbox = True
                End If
        End Select
    End Sub


    Private Sub DataGridViewOutboxDetail_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles _
        DataGridViewOutboxDetail.RowPrePaint

        Dim oRow As DataGridViewRow = DataGridViewOutboxDetail.Rows(e.RowIndex)
        Dim oResult As EdiFile.Results = CType(oRow.Cells(Cols.Result).Value, EdiFile.Results)

        Select Case oResult
            Case EdiFile.Results.Pending
                oRow.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
        End Select

    End Sub

    Private Sub ButtonFilter_Click(sender As Object, e As System.EventArgs) Handles ButtonFilter.Click
        RefreshRequest2()
        ButtonFilter.Enabled = False
    End Sub

    Private Sub TextBoxFiltro_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxFiltro.TextChanged
        ButtonFilter.Enabled = True
    End Sub
End Class