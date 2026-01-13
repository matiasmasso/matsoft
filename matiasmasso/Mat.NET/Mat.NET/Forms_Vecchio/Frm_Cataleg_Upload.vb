Imports System.IO


Public Class Frm_Cataleg_Upload
    Private Enum Csvs
        TpaNom
        StpNom
        Ref
        Nom
        Prv
        Myd
        Cost
        TarifaA
        TarifaB
        Pvp
        Ean
    End Enum

    Private Enum Cols
        Ico
        ArtId
        TpaId
        TpaNom
        StpId
        StpNom
        Ref
        Nom
        Prv
        Myd
        Cost
        TarifaA
        TarifaB
        Pvp
        Ean
        Err
    End Enum

    Private Sub Frm_Cataleg_Upload_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Instruccions per pujar un cataleg:")
        sb.AppendLine("Arrossegar (o importar amb la dreta del ratolí) un Excel guardat en format csv amb les següents columnes:")
        sb.AppendLine("MARCA: el nom de la marca, que ha de coincidir amb el de la base de dades. Si no, en creará una de nova")
        sb.AppendLine("CATEGORIA: nom de la subdivisió de la marca, que ha de coincidir amb el de la base de dades. Si no, en creará una de nova")
        sb.AppendLine("REF: referencia del proveidor, si n'hi ha")
        sb.AppendLine("NOM: el que el diferencia dels altres dins la seva categoría de productes. Per exemple, el nom del color")
        sb.AppendLine("PRV: el nom per comandes a proveidor, si es diferent del MYD")
        sb.AppendLine("MYD: el nom complert del producte tal com ha d'apareixer en les factures")
        sb.AppendLine("COST: preu net que ens factura el proveidor")
        sb.AppendLine("TARIFAA: preu per defecte que facturem")
        sb.AppendLine("TARIFAB: preu per determinats clients, amb un recarrec del 10%")
        sb.AppendLine("PVP: preu recomenat de venda al public, segons marge estipulat a la marca")
        sb.AppendLine("EAN: codi de barres de 13 digits")
        LabelGuide.Text = sb.ToString

        SetContextMenu()
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


    Private Sub Frm_Cataleg_Upload_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim fileNames() As String = Nothing

        'Try
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            fileNames = e.Data.GetData(DataFormats.FileDrop)
            Dim sFilename As String = fileNames(0)
            ' get the actual raw file into memory

            Dim oStream As StreamReader = New StreamReader(sFilename, BLL.FileSystemHelper.DefaultEncoding)
            Dim exs as List(Of exception) = ImportFile(oStream)
            If exs.Count > 0 Then
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If

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

            Dim oStream As StreamReader = New StreamReader(oMemStream, BLL.FileSystemHelper.DefaultEncoding)
            Dim exs as List(Of exception) = ImportFile(oStream)
            If exs.Count > 0 Then
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("format desconegut")
        End If
        'Catch ex As Exception
        'MsgBox("Error in DragDrop function: " + ex.Message)
        'End Try
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        If DataGridView1.Visible Then
            oMenuItem = New ToolStripMenuItem("especificar producte", Nothing, AddressOf SetProduct)
            oContextMenu.Items.Add(oMenuItem)
            DataGridView1.ContextMenuStrip = oContextMenu
        Else
            oMenuItem = New ToolStripMenuItem("importar nou cataleg", Nothing, AddressOf UploadFile)
            oContextMenu.Items.Add(oMenuItem)
            Me.ContextMenuStrip = oContextMenu
            LabelGuide.ContextMenuStrip = oContextMenu
        End If

    End Sub

    Private Sub UploadFile(sender As Object, e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar cataleg Excel guardat com a CSV"
            .Filter = "documents csv (*.csv)|*.csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim oStream As StreamReader = Nothing
                Try
                    oStream = New StreamReader(.FileName, BLL.FileSystemHelper.DefaultEncoding)
                Catch ex As System.IO.IOException
                    MsgBox("el fitxer está obert. Cal tancar-lo primer", MsgBoxStyle.Exclamation, "MAT.NET")
                End Try

                Dim exs as List(Of exception) = ImportFile(oStream)
                If exs.Count > 0 Then
                    MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
                End If

                oStream.Close()
                oStream = Nothing
            End If
        End With
    End Sub

    Private Sub SetProduct(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.SelectProduct)
        AddHandler oFrm.AfterSelect, AddressOf onProductSet
        oFrm.Show()
    End Sub

    Private Sub onProductSet(sender As Object, e As MatEventArgs)
        Dim oProduct As Product = e.Argument
        If oProduct IsNot Nothing Then
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            Dim OldTpaId As Integer = CellIntValue(oRow, Cols.TpaId)
            Dim OldStpId As Integer = CellIntValue(oRow, Cols.StpId)
            Dim OldArtId As Integer = CellIntValue(oRow, Cols.ArtId)
            Select Case oProduct.ValueType
                Case Product.ValueTypes.Tpa
                    Dim oTpa As Tpa = CType(oProduct.Value, Tpa)
                    If OldTpaId <> oTpa.Id Then
                        oRow.Cells(Cols.TpaId).Value = oTpa.Id
                        oRow.Cells(Cols.TpaNom).Value = oTpa.Nom
                    End If
                Case Product.ValueTypes.Stp
                    Dim oStp As Stp = CType(oProduct.Value, Stp)
                    If OldStpId <> oStp.Id Then
                        oRow.Cells(Cols.StpId).Value = oStp.Id
                        oRow.Cells(Cols.StpNom).Value = oStp.Nom
                    End If
                    If OldTpaId <> oStp.Tpa.Id Then
                        oRow.Cells(Cols.TpaId).Value = oStp.Tpa.Id
                        oRow.Cells(Cols.TpaNom).Value = oStp.Tpa.Nom
                    End If
                    'Case Product.ValueTypes.Categoria
                    '    Dim oCategoria As Categoria = CType(oProduct.Value, Categoria)
                    '    If OldCtgId <> oCategoria.Id Then
                    ' oRow.Cells(Cols.CtgId).Value = oCategoria.Id
                    ' oRow.Cells(Cols.CtgNom).Value = oCategoria.Nom_ESP
                    ' End If
                    'If OldStpId <> oCategoria.Stp.Id Then
                    ' oRow.Cells(Cols.StpId).Value = oCategoria.Stp.Id
                    ' oRow.Cells(Cols.StpNom).Value = oCategoria.Stp.Nom
                    ' End If
                    ' If OldTpaId <> oCategoria.Stp.Tpa.Id Then
                    ' oRow.Cells(Cols.TpaId).Value = oCategoria.Stp.Tpa.Id
                    ' oRow.Cells(Cols.TpaNom).Value = oCategoria.Stp.Tpa.Nom
                    ' End If
                Case Product.ValueTypes.Art
                    Dim oArt As Art = CType(oProduct.Value, Art)
                    'oRow.Cells(Cols.ArtId).Value = oArt.Id
                    'If OldCtgId <> oArt.Categoria.Id Then
                    ' oRow.Cells(Cols.CtgId).Value = oArt.Categoria.Id
                    ' oRow.Cells(Cols.CtgNom).Value = oArt.Categoria.Nom_ESP
                    ' End If
                    'If OldStpId <> oArt.Categoria.Stp.Id Then
                    ' oRow.Cells(Cols.StpId).Value = oArt.Categoria.Stp.Id
                    ' oRow.Cells(Cols.StpNom).Value = oArt.Categoria.Stp.Nom
                    ' End If
                    'If OldTpaId <> oArt.Categoria.Stp.Tpa.Id Then
                    ' oRow.Cells(Cols.TpaId).Value = oArt.Categoria.Stp.Tpa.Id
                    ' oRow.Cells(Cols.TpaNom).Value = oArt.Categoria.Stp.Tpa.Nom
                    'End If
            End Select
        End If

    End Sub

    Private Function ImportFile(sr As StreamReader) as List(Of exception)
        Dim exs as new list(Of Exception)
        Dim sFileTitleRow As String = sr.ReadLine
        Dim sExpectedTitleRow As String = "MARCA;CATEGORIA;REF;NOM;PRV;MYD;COST;TARIFAA;TARIFAB;PVP;EAN"
        If sFileTitleRow <> sExpectedTitleRow Then
            exs.Add(New Exception("enunciats de les columnes incorrectes:" & vbCrLf & "fitxer: " & sFileTitleRow & vbCrLf & "esperat: " & sExpectedTitleRow))
        Else

            Dim oTb As DataTable = GetTableFromStreamReader(sr)

            With DataGridView1
                With .RowTemplate
                    .Height = DataGridView1.Font.Height * 1.3
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False

                With .Columns(Cols.Ico)
                    .HeaderText = ""
                    .Width = 16
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.ArtId)
                    .Visible = False
                End With

                With .Columns(Cols.TpaId)
                    .Visible = False
                End With

                With .Columns(Cols.TpaNom)
                    .HeaderText = "MARCA"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.StpId)
                    .Visible = False
                End With

                With .Columns(Cols.StpNom)
                    .HeaderText = "CATEGORIA"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With

                With .Columns(Cols.Ref)
                    .HeaderText = "REF"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.Nom)
                    .HeaderText = "NOM"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.Prv)
                    .HeaderText = "PRV"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.Myd)
                    .HeaderText = "MYD"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With

                With .Columns(Cols.Cost)
                    .HeaderText = "COST"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

                With .Columns(Cols.TarifaA)
                    .HeaderText = "TARIFAA"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

                With .Columns(Cols.TarifaB)
                    .HeaderText = "TARIFAB"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

                With .Columns(Cols.Pvp)
                    .HeaderText = "PVP"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

                With .Columns(Cols.Ean)
                    .HeaderText = "EAN"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With

                With .Columns(Cols.Err)
                    .Visible = False
                End With

                .Visible = True
            End With

            LabelGuide.Visible = False
            SetContextMenu()

        End If
        Return exs
    End Function

    Private Function GetTableFromStreamReader(sr As StreamReader) As DataTable
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oTb As DataTable = CreateDataTable()
        Dim line As String
        Do
            line = sr.ReadLine()
            If line Is Nothing Then Exit Do

            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            Dim camps As Array = line.Split(";")

            If camps.Length > Csvs.Nom Then
                Dim sTpaNom As String = camps(Csvs.TpaNom)
                Dim sStpNom As String = camps(Csvs.StpNom)

                oRow(Cols.TpaNom) = sTpaNom
                oRow(Cols.StpNom) = sStpNom

                Dim oTpa As Tpa = Tpa.FromNom(oEmp, sTpaNom)
                If oTpa IsNot Nothing Then
                    oRow(Cols.TpaId) = oTpa.Id
                    Dim oStp As Stp = Stp.FromNom(oTpa, sStpNom)
                    If oStp IsNot Nothing Then
                        oRow(Cols.StpId) = oStp.Id
                    End If
                End If

                oRow(Cols.Ref) = camps(Csvs.Ref)
                oRow(Cols.Nom) = camps(Csvs.Nom)

            End If

            If camps.Length > Csvs.Prv Then
                oRow(Cols.Prv) = camps(Csvs.Prv)
            End If

            If camps.Length > Csvs.Nom Then
                oRow(Cols.Myd) = camps(Csvs.Myd)
            End If

            If camps.Length > Csvs.Cost Then
                oRow(Cols.Cost) = camps(Csvs.Cost)
            End If

            If camps.Length > Csvs.TarifaA Then
                oRow(Cols.TarifaA) = camps(Csvs.TarifaA)
            End If

            If camps.Length > Csvs.TarifaB Then
                oRow(Cols.TarifaB) = camps(Csvs.TarifaB)
            End If

            If camps.Length > Csvs.Pvp Then
                oRow(Cols.Pvp) = camps(Csvs.Pvp)
            End If

            If camps.Length > Csvs.Ean Then
                oRow(Cols.Ean) = New maxisrvr.ean13(camps(Csvs.Ean).ToString).Value
            End If

        Loop
        sr.Close()
        sr = Nothing
        Return oTb
    End Function

    Private Function CreateDataTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("ICO", System.Type.GetType("System.Byte[]"))
            .Add("ARTID", System.Type.GetType("System.Int32"))
            .Add("TPAID", System.Type.GetType("System.Int32"))
            .Add("TPANOM", System.Type.GetType("System.String"))
            .Add("STPID", System.Type.GetType("System.Int32"))
            .Add("STPNOM", System.Type.GetType("System.String"))
            .Add("REF", System.Type.GetType("System.String"))
            .Add("NOM", System.Type.GetType("System.String"))
            .Add("PRV", System.Type.GetType("System.String"))
            .Add("MYD", System.Type.GetType("System.String"))
            .Add("COST", System.Type.GetType("System.Decimal"))
            .Add("TARIFAA", System.Type.GetType("System.Decimal"))
            .Add("TARIFAB", System.Type.GetType("System.Decimal"))
            .Add("PVP", System.Type.GetType("System.Decimal"))
            .Add("EAN", System.Type.GetType("System.String"))
            .Add("ERR", System.Type.GetType("System.String"))
        End With
        Return oTb
    End Function

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.TpaNom
                If IsNotEmptyCell(e.RowIndex, Cols.TpaId) Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
            Case Cols.StpNom
                If IsNotEmptyCell(e.RowIndex, Cols.StpId) Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
            Case Cols.Nom, Cols.Myd, Cols.Ref
                If IsNotEmptyCell(e.RowIndex, Cols.ArtId) Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
            Case Cols.Ean
                If e.Value > "" Then
                    Dim oEan As New maxisrvr.ean13(e.Value)
                    If Not oEan.ValidationResult = maxisrvr.ean13.Errors.Ok Then
                        e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                    End If
                End If
            Case Cols.Ico
                Dim BlErrs As Boolean
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow IsNot Nothing Then
                    If Not IsDBNull(oRow.Cells(Cols.Err).Value) Then
                        BlErrs = oRow.Cells(Cols.Err).Value > ""
                    End If
                End If
                If BlErrs Then
                    e.Value = maxisrvr.GetByteArrayFromImg(My.Resources.warn)
                Else
                    e.Value = maxisrvr.GetByteArrayFromImg(My.Resources.empty)
                End If
        End Select
    End Sub

    Private Function IsNotEmptyCell(RowIndex As Integer, oCol As Cols) As Boolean
        Dim retval As Boolean = False
        Dim oRow As DataGridViewRow = DataGridView1.Rows(RowIndex)
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(oCol).Value) Then
                If oRow.Cells(oCol).Value > 0 Then retval = True
            End If
        End If
        Return retval
    End Function

    Private Function CellIntValue(oRow As DataGridViewRow, oCol As Cols) As Integer
        Dim retval As Integer = 0
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(oCol).Value) Then
                retval = CInt(oRow.Cells(oCol).Value)
            End If
        End If
        Return retval
    End Function

    Private Sub DataGridView1_CellToolTipTextNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            If Not IsDBNull(oRow.Cells(Cols.Err).Value) Then
                Dim sToolTip As String = oRow.Cells(Cols.Err).Value
                If sToolTip > "" Then
                    e.ToolTipText = sToolTip
                End If
            End If
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim oTpa As Tpa = Nothing
        Dim oStp As Stp = Nothing
        Dim oArt As Art = Nothing

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim oRow As DataGridViewRow = DataGridView1.Rows(i)

            Dim iTpa As Integer = CellIntValue(oRow, Cols.TpaId)
            If iTpa = 0 Then
                oTpa = New Tpa(BLL.BLLApp.Emp)
                oTpa.Nom = oRow.Cells(Cols.TpaNom).Value
                oTpa.Update()
                oStp = Nothing
                For j As Integer = i To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(j).Cells(Cols.TpaNom).Value = oTpa.Nom Then
                        DataGridView1.Rows(j).Cells(Cols.TpaId).Value = oTpa.Id
                    End If
                Next
            Else
                If oTpa Is Nothing Then
                    oTpa = New Tpa(BLL.BLLApp.Emp, iTpa)
                    oStp = Nothing
                Else
                    If oTpa.Id <> iTpa Then
                        oTpa = New Tpa(BLL.BLLApp.Emp, iTpa)
                        oStp = Nothing
                    End If
                End If
            End If


            Dim iStp As Integer = CellIntValue(oRow, Cols.StpId)
            If iStp = 0 Then
                oStp = New Stp(oTpa)
                oStp.Nom = oRow.Cells(Cols.StpNom).Value
                Dim exs as New List(Of exception)
                StpLoader.Update(oStp, exs)
                For j As Integer = i To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(j).Cells(Cols.TpaId).Value = oStp.Tpa.Id Then
                        If DataGridView1.Rows(j).Cells(Cols.StpNom).Value = oStp.Nom Then
                            DataGridView1.Rows(j).Cells(Cols.StpId).Value = oStp.Id
                        End If
                    End If
                Next
            Else
                If oStp Is Nothing Then
                    oStp = New Stp(oTpa, iStp)
                Else
                    If oStp.Id <> iStp Then
                        oStp = New Stp(oTpa, iStp)
                    End If
                End If
            End If

            Dim iArt As Integer = CellIntValue(oRow, Cols.ArtId)
            If iArt = 0 Then
                oArt = New Art(oStp)
                oArt.NomCurt = oRow.Cells(Cols.Nom).Value
                oArt.NomPrv = oRow.Cells(Cols.Prv).Value
                oArt.Nom_ESP = oRow.Cells(Cols.Myd).Value
                '                oArt.Hereda = oArt.Categoria.TarifaA.Eur > 0
                oArt.Dimensions = New ArtDimensions()
                oArt.Dimensions.Hereda = oArt.Hereda
                oArt.Cost = New maxisrvr.Amt(CDec(oRow.Cells(Cols.Cost).Value))
                oArt.TarifaA = New maxisrvr.Amt(CDec(oRow.Cells(Cols.TarifaA).Value))
                oArt.TarifaB = New maxisrvr.Amt(CDec(oRow.Cells(Cols.TarifaB).Value))
                oArt.Pvp = New maxisrvr.Amt(CDec(oRow.Cells(Cols.Pvp).Value))
                oArt.Cbar = New maxisrvr.ean13(oRow.Cells(Cols.Ean).Value)
                If oRow.Cells(Cols.Ref).Value > "" Then
                    oArt.CodPrv = oRow.Cells(Cols.Ref).Value
                    oArt.Keys.Add(oRow.Cells(Cols.Ref).Value)
                End If
                Dim sKey As String = oArt.Stp.ClauPrefix
                If sKey > "" Then sKey = sKey & " "
                sKey = sKey & oArt.NomCurt
                oArt.Keys.Add(sKey)

                Dim exs as New List(Of exception)
                If oArt.Update( exs) Then
                    oRow.Cells(Cols.ArtId).Value = oArt.Id
                Else
                    MsgBox("error al desar l'article" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                oArt = Nothing
                Stop 'falta implantar preus a futur
            End If

        Next

        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
        MsgBox("cataleg pujat", MsgBoxStyle.Information, "MAT.NET")
    End Sub


End Class