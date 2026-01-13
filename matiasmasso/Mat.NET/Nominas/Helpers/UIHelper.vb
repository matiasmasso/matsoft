Imports MatHelperCFwk
Imports MatHelperStd

Module UIHelper
    Public Enum SelModes
        Browse
        Selection
    End Enum

    Public Sub ToggleProggressBar(ByRef panel As Panel, visible As Boolean)
        Select Case visible
            Case True
                Dim oProgressBar As ProgressBar = Nothing
                For Each oChild In panel.Controls
                    If TypeOf oChild Is Button Or TypeOf oChild Is Label Then
                        oChild.Visible = False
                        'DirectCast(oChild, Button).Visible = False
                    ElseIf TypeOf oChild Is ProgressBar Then
                        oProgressBar = oChild
                    End If
                Next
                If oProgressBar Is Nothing Then
                    oProgressBar = New ProgressBar
                    With oProgressBar
                        .Dock = DockStyle.Top
                        .Style = ProgressBarStyle.Marquee
                    End With
                    panel.Controls.Add(oProgressBar)
                End If
                oProgressBar.Visible = visible
            Case Else
                For Each oChild In panel.Controls
                    If TypeOf oChild Is Button Or TypeOf oChild Is Label Then
                        oChild.Visible = True
                        'DirectCast(oChild, Button).Visible = True
                    End If
                    If TypeOf oChild Is ProgressBar Then
                        panel.Controls.Remove(oChild)
                    End If
                Next
                'panel.Parent.Cursor = Cursors.Default
        End Select
    End Sub


    Public Async Function FadeIn(oFrm As Form, Optional interval As Integer = 80) As Task
        Do While oFrm.Opacity < 1.0
            Await Task.Delay(interval)
            oFrm.Opacity += 0.05
        Loop
        oFrm.Opacity = 1.0
    End Function

    Public Async Function FadeOut(oFrm As Form, Optional interval As Integer = 80) As Task
        Do While oFrm.Opacity > 0.0
            Await Task.Delay(interval)
            oFrm.Opacity -= 0.05
        Loop
        oFrm.Opacity = 0.0
    End Function

    Public Function checkConnectivity()
        Do Until System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()
            Dim rc As MsgBoxResult = MsgBox("no hi ha connexió a Internet", MsgBoxStyle.RetryCancel)
            If rc = MsgBoxResult.Cancel Then
                Return False
            End If
        Loop
        Return True
    End Function

    Public Sub CopyLink(oDocFile As DTODocFile, Optional msg As Boolean = True)
        Dim sUrl As String = FEB.DocFile.DownloadUrl(oDocFile, True)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, sUrl)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Public Sub CopyLink(sUrl As String)
        CopyToClipboard(sUrl, "enllaç copiat al portapapers")
    End Sub

    Public Sub CopyToClipboard(src As String, Optional sMsg As String = "")
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, src)
        Clipboard.SetDataObject(data_object, True)
        If sMsg > "" Then
            MsgBox(sMsg, MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Public Function SavefileDialogFilter(oMime As MimeCods) As String
        Dim retval As String = String.Format("fitxers {0}|*.{0}|Tots els fitxers (*.*)|*.*", oMime.ToString())
        Return retval
    End Function

    Public Function DataGridviewCurrentCell(oGrid As DataGridView) As DTODatagridviewCell
        Dim retval As DTODatagridviewCell = Nothing
        If oGrid.CurrentCell IsNot Nothing Then
            retval = New DTODatagridviewCell
            With retval
                .ColumnIndex = oGrid.CurrentCell.ColumnIndex
                If oGrid.Rows.Count > 0 Then
                    .RowIndex = oGrid.CurrentRow.Index
                End If
                If oGrid.FirstDisplayedScrollingRowIndex() >= 0 Then
                    .FirstDisplayedScrollingRowIndex = oGrid.FirstDisplayedScrollingRowIndex()
                End If
                If oGrid.SortedColumn IsNot Nothing Then
                    .SortedColumn = oGrid.SortedColumn.Index
                    .SortOrder = oGrid.SortOrder
                End If
            End With
        End If
        Return retval
    End Function

    Public Sub SetDataGridviewCurrentCell(ByRef oGrid As DataGridView, oCell As DTODatagridviewCell)
        If oCell Is Nothing Then
            oGrid.ClearSelection()
        Else
            With oGrid
                If .Rows.Count = 0 Then
                Else
                    Select Case oCell.SortOrder
                        Case SortOrder.Ascending
                            oGrid.Sort(oGrid.Columns(oCell.SortedColumn), System.ComponentModel.ListSortDirection.Ascending)
                        Case SortOrder.Descending
                            oGrid.Sort(oGrid.Columns(oCell.SortedColumn), System.ComponentModel.ListSortDirection.Descending)
                    End Select

                    If oCell.RowIndex > .Rows.Count - 1 Then
                        Try
                            .CurrentCell = .Rows(.Rows.Count - 1).Cells(oCell.ColumnIndex)
                        Catch ex As Exception

                        End Try
                    Else
                        If oCell.FirstDisplayedScrollingRowIndex < .Rows.Count Then
                            If Not oGrid.Rows(oCell.RowIndex).Frozen Then
                                .FirstDisplayedScrollingRowIndex() = oCell.FirstDisplayedScrollingRowIndex
                                For iCol As Integer = 0 To oGrid.Columns.Count - 1
                                    If .Rows(oCell.RowIndex).Cells(iCol).Visible Then
                                        .CurrentCell = .Rows(oCell.RowIndex).Cells(iCol)
                                        Exit For
                                    End If
                                Next
                            End If

                        End If
                    End If

                End If
            End With
        End If
    End Sub

    Public Sub WarnErrorOld(exs As List(Of Exception), Optional sMessage As String = "")
        Dim sb As New System.Text.StringBuilder
        If sMessage > "" Then sb.AppendLine(sMessage)
        sb.AppendLine(ExceptionsHelper.ToFlatString(exs))
        Dim sFullMessage As String = sb.ToString
        MsgBox(sFullMessage, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Public Sub WarnError(ex As Exception, Optional sMessage As String = "")
        Dim exs As New List(Of Exception)
        exs.Add(ex)
        WarnError(exs, sMessage)
    End Sub
    Public Sub WarnError(oTaskResult As DTOTaskResult)
        Dim sb As New Text.StringBuilder
        sb.AppendLine(oTaskResult.Msg)
        For Each ex In oTaskResult.Exceptions
            sb.AppendLine(ex.Message)
        Next
        MsgBox(sb.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub


    Public Sub WarnError(exs As List(Of DTOEdiversaException), Optional sMessage As String = "")
        Dim ex2 As List(Of Exception) = DTOEdiversaException.ToSystemExceptions(exs)
        WarnError(ex2, sMessage)
    End Sub

    Public Function WarnError(exs As List(Of Exception), Optional sMessage As String = "", Optional oMsgBoxStyle As MsgBoxStyle = MsgBoxStyle.Exclamation) As MsgBoxResult
        Dim sb As New System.Text.StringBuilder
        If sMessage > "" Then sb.AppendLine(sMessage)
        For Each ex As Exception In exs
            sb.AppendLine(ex.Message)
        Next
        Dim sFullMessage As String = sb.ToString
        Dim retval As MsgBoxResult = MsgBox(sFullMessage, oMsgBoxStyle, "MAT.NET")
        Return retval
    End Function

    Public Sub WarnError(sMessage As String)
        MsgBox(sMessage, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Public Function LoadExcelDialog(ByRef filename As String, Optional title As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            If title > "" Then .Title = title
            .Filter = "fitxers excel (*.xls, *.xlsx)|*.xls;*.xlsx|tots els documents (*.*)|*.*"
            .FileName = filename
            .AddExtension = True
            .DefaultExt = ".xlsx"
            If .ShowDialog = DialogResult.OK Then
                filename = .FileName
                retval = True
            End If
        End With
        Return retval
    End Function

    Public Function LoadExcelSheetDialog(ByRef oSheet As MatHelper.Excel.Sheet, Optional sTitle As String = "", Optional hasHeaderRow As Boolean = False) As Boolean
        Dim retval As Boolean

        Dim sFilename As String = ""
        If oSheet IsNot Nothing Then
            sFilename = oSheet.Filename
        End If

        If LoadExcelDialog(sFilename, sTitle) Then
            Dim exs As New List(Of Exception)
            Dim oBook = MatHelper.Excel.ClosedXml.Read(exs, sFilename, hasHeaderRow)
            If exs.Count = 0 Then
                oSheet = oBook.Sheets.FirstOrDefault()
                retval = True
            Else
                UIHelper.WarnError(exs, "error al importar el fitxer")
            End If
        End If
        Return retval
    End Function

    Public Function LoadCsvDialog(ByRef oCsvFile As DTOCsv, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = "fitxers Csv (*.csv)|*.csv|tots els documents (*.*)|*.*"
            .AddExtension = True
            .DefaultExt = ".csv"
            If oCsvFile IsNot Nothing Then
                .FileName = oCsvFile.Filename
            End If
            If .ShowDialog Then
                Dim oFileStream As IO.FileStream = Nothing
                Dim exs As New List(Of Exception)
                Try
                    oFileStream = New IO.FileStream(.FileName, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim oStreamReader As New System.IO.StreamReader(oFileStream, System.Text.Encoding.Default)
                    Dim src As String = oStreamReader.ReadToEnd()
                    oStreamReader.Close()
                    oCsvFile = DTOCsv.Factory(src)
                    retval = True
                Catch ex As Exception
                    UIHelper.WarnError(exs, "error al importar el fitxer")
                End Try
            End If
        End With
        Return retval
    End Function

    Public Function Save(oDocFile As DTODocFile, sFilename As String, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            If sFilename = "" Then
                If oDocFile.Filename = "" Then
                    sFilename = CryptoHelper.StringToHexadecimal(oDocFile.Hash) & "." & oDocFile.Mime.ToString
                Else
                    sFilename = oDocFile.Filename
                End If

            End If

            retval = FileSystemHelper.SaveStream(oDocFile.Stream, exs, sFilename)
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function SaveCsvDialog(oCsv As DTOCsv, Optional sTitle As String = "", Optional sFilename As String = "", Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean

        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle = "" Then sTitle = oCsv.Filename
            If sTitle > "" Then
                .Title = sTitle
                If sFilename = "" Then sFilename = oCsv.Filename
                If sFilename > "" Then
                    If Not sFilename.EndsWith(".csv") Then
                        sFilename += ".csv"
                    End If
                    .FileName = sFilename
                End If
            End If
            .Filter = "fitxers Csv (*.csv)|*.csv|tots els documents (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                If exs Is Nothing Then exs = New List(Of Exception)
                Try
                    If System.IO.File.Exists(.FileName) Then
                        IO.File.Delete(.FileName)
                    End If

                    Dim oFileStream As New System.IO.FileStream(.FileName, IO.FileMode.CreateNew, IO.FileAccess.ReadWrite)
                    Dim oSw As New System.IO.StreamWriter(oFileStream, System.Text.Encoding.Default)
                    For Each oRow As DTOCsvRow In oCsv.Rows
                        Dim sLine As String = String.Join(";", oRow.Cells.ToArray())
                        oSw.WriteLine(sLine)
                    Next
                    oSw.Flush()
                    oSw.Close()
                    retval = True

                Catch ex As Exception
                    UIHelper.WarnError(exs, "error al desar el fitxer")
                End Try

            End If
        End With
        Return retval
    End Function



    Public Function SaveExcelDialog(oSheet As MatHelper.Excel.Sheet, Optional ShowProgress As MatHelperCFwk.ProgressBarHandler = Nothing, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle = "" Then sTitle = oSheet.Filename
            If sTitle > "" Then
                .Title = sTitle
                If oSheet.Filename > "" Then
                    Dim sFilename As String = oSheet.Filename
                    If Not sFilename.EndsWith(".xlsx") Then
                        sFilename += ".xlsx"
                    End If
                    .FileName = sFilename
                End If
            End If
            .Filter = "fitxers Excel (*.xlsx, *.xls)|*.xlsx;*.xls|tots els documents (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                oSheet.Filename = .FileName
                Dim oBytes = MatHelper.Excel.ClosedXml.Bytes(oSheet)

                Dim exs As New List(Of Exception)
                Try
                    IO.File.WriteAllBytes(.FileName, oBytes)

                Catch ex As Exception
                    exs.Add(ex)
                End Try
                If exs.Count = 0 Then
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al desar el fitxer")
                End If
            End If
        End With
        Return retval
    End Function

    Public Function SaveExcelDialog(oBook As MatHelper.Excel.Book, Optional ShowProgress As MatHelperCFwk.ProgressBarHandler = Nothing, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle = "" Then sTitle = oBook.Filename
            If sTitle > "" Then
                .Title = sTitle
                If oBook.Filename > "" Then
                    Dim sFilename As String = oBook.Filename
                    If Not sFilename.EndsWith(".xlsx") Then
                        sFilename += ".xlsx"
                    End If
                    .FileName = sFilename
                End If
            End If
            .Filter = "fitxers Excel (*.xlsx, *.xls)|*.xlsx;*.xls|tots els documents (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                oBook.Filename = .FileName
                Dim exs As New List(Of Exception)
                Try
                    Dim oBytes = MatHelper.Excel.ClosedXml.Bytes(oBook)
                    If oBytes IsNot Nothing Then
                        If FileSystemHelper.SaveStream(oBytes, exs, oBook.Filename) Then
                            Process.Start(oBook.Filename)
                            retval = True
                        End If
                    End If

                Catch ex As Exception
                    exs.Add(ex)
                End Try

                If exs.Count = 0 Then
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al desar el fitxer")
                End If
            End If
        End With
        Return retval
    End Function

    Public Function SaveXmlFileDialog(XmlSource As String, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = "fitxers Xml (*.xml)|*.xml|tots els documents (*.*)|*.*"
            .FileName = sTitle
            .AddExtension = True
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs, "error al redactar el fitxer")
                Else
                    If FileSystemHelper.SaveTextToFile(XmlSource, .FileName, exs) Then
                        retval = True
                    Else
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                End If
            End If
        End With
        Return retval
    End Function

    Public Function SaveTextFileDialog(TxtSource As String, Optional sTitle As String = "", Optional sFilename As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = "fitxers Txt (*.txt)|*.txt|tots els documents (*.*)|*.*"
            .FileName = IIf(sFilename = "", sTitle, sFilename)
            .AddExtension = True
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs, "error al redactar el fitxer")
                Else
                    If FileSystemHelper.SaveTextToFile(TxtSource, .FileName, exs) Then
                        retval = True
                    Else
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                End If
            End If
        End With
        Return retval
    End Function


    Public Function LoadPdfDialog(ByRef oDocFile As DTODocFile, Optional sTitle As String = "", Optional sFilter As String = "documents Pdf (*.pdf)|*.pdf|tots els documents (*.*)|*.*") As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = sFilter
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)

                Dim tmp = DocfileHelper.Factory(.FileName, exs)
                If exs.Count = 0 Then
                    oDocFile = tmp
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al importar document")
                End If
            End If
        End With
        Return retval
    End Function

    Public Function ShowExcel(oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oSheet Is Nothing Then
            exs.Add(New Exception("Excel buit"))
        Else
            Try
                Dim oStream As Byte() = MatHelper.Excel.ClosedXml.Bytes(oSheet)
                If oSheet.Filename = "" Then
                    oSheet.Filename = Guid.NewGuid.ToString & ".xlsx".Replace(Chr(34), Chr(32))
                ElseIf System.IO.Path.GetExtension(oSheet.Filename) <> ".xlsx" Then
                    oSheet.Filename += ".xlsx"
                End If
                If FileSystemHelper.SaveStream(oStream, exs, oSheet.Filename) Then
                    Process.Start(oSheet.Filename)
                    retval = True
                End If
            Catch ex As Exception
                exs.Add(ex)
            End Try
        End If
        Return retval
    End Function

    Public Function ShowExcel(exs As List(Of Exception), oStream As Byte(), Optional filename As String = "") As Boolean
        Dim retval As Boolean
        If oStream Is Nothing Then
            exs.Add(New Exception("Excel buit"))
        Else
            If filename = "" Then filename = Guid.NewGuid.ToString.Replace(Chr(34), Chr(32))
            If Not filename.EndsWith(".xlsx") Then filename = filename & ".xlsx"
            If FileSystemHelper.SaveStream(oStream, exs, filename) Then
                Process.Start(filename)
                retval = True
            End If
        End If
        Return retval
    End Function

    Public Function ShowExcel(oBook As MatHelper.Excel.Book, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oBook Is Nothing Then
            exs.Add(New Exception("Excel buit"))
        Else
            Dim oStream As Byte() = MatHelper.Excel.ClosedXml.Bytes(oBook)
            If oStream IsNot Nothing Then

                If oBook.Filename = "" Then
                    oBook.Filename = Guid.NewGuid.ToString & ".xlsx".Replace(Chr(34), Chr(32))
                ElseIf System.IO.Path.GetExtension(oBook.Filename) <> ".xlsx" Then
                    oBook.Filename += ".xlsx"
                End If


                If FileSystemHelper.SaveStream(oStream, exs, oBook.Filename) Then
                    Process.Start(oBook.Filename)
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Public Sub ShowCsv(oCsv As DTOCsv)
        UIHelper.SaveCsvDialog(oCsv, oCsv.Title, oCsv.Filename)
    End Sub

    Public Sub ShowHtml(ByVal sFileNameOrUrl As String)
        If sFileNameOrUrl = "" Then
            UIHelper.WarnError("falta la adreça del fitxer o pàgina web")
        Else
            If sFileNameOrUrl.IndexOf("://") < 0 Then sFileNameOrUrl = "https://" & sFileNameOrUrl
            Dim sInfo As New ProcessStartInfo(sFileNameOrUrl)
            Try
                'Process.Start(sInfo)

                Dim pr As New Process()
                pr.StartInfo.FileName = sFileNameOrUrl
                pr.Start()
            Catch ex As Exception
                Dim exs As New List(Of Exception)
                exs.Add(ex)
                UIHelper.WarnError(exs, "no es pot obrir '" & sFileNameOrUrl & "'")
            End Try
        End If

    End Sub

    Public Async Function ShowStreamAsync(exs As List(Of Exception), oDocFile As DTODocFile, Optional sSuggestedFileName As String = "", Optional BlDownloadFromWeb As Boolean = True) As Task(Of Boolean)
        Dim retval As Boolean
        If oDocFile Is Nothing Then
            exs.Add(New Exception("document no disponible"))
        ElseIf oDocFile.Stream IsNot Nothing Then
            If sSuggestedFileName = "" Then sSuggestedFileName = FEB.DocFile.FileNameOrDefault(oDocFile)
            Select Case oDocFile.Mime
                Case MimeCods.Pdf, MimeCods.Gif, MimeCods.Jpg, MimeCods.Png, MimeCods.Mov, MimeCods.Mp4
                    If FileSystemHelper.SaveStream(oDocFile.Stream, exs, sSuggestedFileName) Then
                        Try
                            Process.Start(sSuggestedFileName)
                            retval = True
                        Catch ex As Exception
                            exs.Add(ex)
                        End Try
                    Else
                        exs.AddRange(exs)
                    End If
                Case Else
                    exs.Add(New Exception("visor no implementat per aquest format" & vbCrLf & DTODocFile.Features(oDocFile)))
            End Select

        Else
            Dim pDocfile = Await FEB.DocFile.FindWithStream(oDocFile.Hash, exs)
            If exs.Count = 0 Then
                If pDocfile Is Nothing Then
                    If sSuggestedFileName = "" Then sSuggestedFileName = FEB.DocFile.FileNameOrDefault(oDocFile)
                    Select Case oDocFile.Mime
                        Case MimeCods.Pdf, MimeCods.Gif, MimeCods.Jpg
                            If FileSystemHelper.SaveStream(oDocFile.Stream, exs, sSuggestedFileName) Then
                                Try
                                    Process.Start(sSuggestedFileName)
                                    retval = True
                                Catch ex As Exception
                                    exs.Add(ex)
                                End Try
                            Else
                                exs.AddRange(exs)
                            End If
                        Case Else
                            exs.Add(New Exception("visor no implementat per aquest format" & vbCrLf & DTODocFile.Features(oDocFile)))
                    End Select
                ElseIf pDocfile.Stream Is Nothing Then
                    exs.Add(New Exception("document buit"))
                Else
                    Try
                        If sSuggestedFileName = "" Then sSuggestedFileName = FEB.DocFile.FileNameOrDefault(pDocfile)
                        If FileSystemHelper.SaveStream(pDocfile.Stream, exs, sSuggestedFileName) Then
                            Try
                                Process.Start(sSuggestedFileName)
                                retval = True
                            Catch ex As Exception
                                exs.Add(ex)
                            End Try
                        Else
                            exs.AddRange(exs)
                        End If

                        'Process.Start(FEB.DocFile.DownloadUrl(oDocFile, True))
                        retval = True
                    Catch ex As Exception
                        exs.Add(ex)
                    End Try
                End If
            Else
                exs.AddRange(exs)
            End If
        End If
        Return retval
    End Function

    Public Sub ShowPdf(oDocFile As DTODocFile, Optional sSuggestedFileName As String = "", Optional BlDownloadFromWeb As Boolean = True)
        If oDocFile Is Nothing Then
            UIHelper.WarnError("document no disponible")
        Else
            If oDocFile.Stream Is Nothing Then
                Dim url = FEB.DocFile.DownloadUrl(oDocFile, True)
                Process.Start(url)
            Else
                ShowPdf(oDocFile.Stream, sSuggestedFileName, BlDownloadFromWeb)
            End If

        End If
    End Sub

    Public Sub ShowPdf(oBuffer As Byte(), Optional sSuggestedFileName As String = "", Optional BlDownloadFromWeb As Boolean = True)
        If oBuffer Is Nothing Then
            UIHelper.WarnError("document no disponible")
        Else
            Dim exs As New List(Of Exception)
            If FileSystemHelper.SaveStream(oBuffer, exs, sSuggestedFileName) Then
                'Process.Start("IExplore.exe", sSuggestedFileName)
                Process.Start(sSuggestedFileName)
            Else
                UIHelper.WarnError(exs, "error al desar el fitxer")
            End If
        End If
    End Sub



    Public Sub LoadComboFromEnum(ByVal oCombobox As ComboBox, ByVal oEnumType As Type, Optional ByVal sNullText As String = "", Optional ByVal DefaultValue As Integer = 0)
        If sNullText = "" Then sNullText = "(seleccionar codi)"
        Dim oTb As New System.Data.DataTable
        oTb.Columns.Add("COD", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As System.Data.DataRow

        Dim v As Integer
        Dim idx As Integer = 0
        Dim iSelectedIndex As Integer = 0
        For Each v In [Enum].GetValues(oEnumType)
            oRow = oTb.NewRow
            oRow(0) = v
            oTb.Rows.Add(oRow)
            If DefaultValue = CInt(v) Then iSelectedIndex = idx
            idx += 1
        Next

        Dim i As Integer = 0
        Dim s As String '= [Enum].Parse(GetType(test), test.uno)
        'For Each s In [Enum].GetNames(GetType(test))
        For Each s In [Enum].GetNames(oEnumType)
            If s = "NotSet" Then
                oTb.Rows(i)(1) = sNullText
            Else
                oTb.Rows(i)(1) = s
            End If
            i = i + 1
        Next

        With oCombobox
            .DataSource = oTb
            .ValueMember = "COD"
            .DisplayMember = "NOM"
            If iSelectedIndex <= .Items.Count - 1 Then
                .SelectedIndex = iSelectedIndex
            End If
        End With
    End Sub


    Public Sub DataGridViewPaintGradientRowBackGround(ByRef oDataGridView As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus

        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle(
            0, e.RowBounds.Top,
            oDataGridView.Columns.GetColumnsWidth(
            DataGridViewElementStates.Visible) -
            oDataGridView.HorizontalScrollingOffset + 1,
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush(
        rowBounds,
        oColor,
        oBgColor,
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Public Function GetExcelFromDataGridView(ByVal oGrid As DataGridView, Optional ByRef oProgressBar As ProgressBar = Nothing) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet

        For j As Integer = 0 To oGrid.Columns.Count - 1
            Try
                If oGrid.Columns(j).Visible Then
                    retval.AddColumn(oGrid.Columns(j).HeaderText)
                End If
            Catch ex As Exception
            End Try
        Next

        If oProgressBar IsNot Nothing Then
            With oProgressBar
                .Maximum = oGrid.Rows.Count
                .Value = 0
                .Visible = True
            End With
            Application.DoEvents()
        End If

        For Each oDataGridViewRow As DataGridViewRow In oGrid.Rows
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            For j As Integer = 0 To oGrid.Columns.Count - 1
                Try
                    If oGrid.Columns(j).Visible Then
                        oRow.AddCell(oDataGridViewRow.Cells(j).Value)
                    End If
                Catch ex As Exception
                End Try
            Next
            If oProgressBar IsNot Nothing Then
                oProgressBar.Increment(1)
                Application.DoEvents()
            End If
        Next

        If oProgressBar IsNot Nothing Then
            oProgressBar.Visible = False
            Application.DoEvents()
        End If

        Return retval
    End Function

    Public Function GetSetting(oSetting As DTOSession.Settings) As String
        Dim retval = GetSetting(oSetting.ToString())
        Return retval
    End Function

    Public Function GetSetting(sSetting As String) As String
        Dim sSection As String = DTOApp.Current.Id.ToString
        If DTOApp.Current.Id = DTOApp.AppTypes.matNet Then sSection = "Mat.Net"
        Dim retval As String = Microsoft.VisualBasic.Interaction.GetSetting("MatSoft", sSection, sSetting)
        Return retval
    End Function

    Public Sub SaveSetting(oSetting As DTOSession.Settings, sValue As String)
        SaveSettingString(oSetting.ToString, sValue)
    End Sub

    Public Sub SaveSettingString(sSetting As String, sValue As String)
        Dim sSection As String = DTOApp.Current.Id.ToString
        If DTOApp.Current.Id = DTOApp.AppTypes.matNet Then sSection = "Mat.Net"
        Microsoft.VisualBasic.Interaction.SaveSetting("MatSoft", sSection, sSetting, sValue)
    End Sub

End Module
