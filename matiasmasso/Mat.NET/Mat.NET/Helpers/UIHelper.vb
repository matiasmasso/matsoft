Module UIHelper
    Public Enum SelModes
        Browse
        Selection
    End Enum

    Public Sub CopyLink(oDocFile As DTODocFile)
        Dim sUrl As String = BLL.BLLDocFile.DownloadUrl(oDocFile, True)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, sUrl)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Public Sub CopyLink(sUrl As String)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, sUrl)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub



    Public Sub WarnErrorOld(exs As List(Of Exception), Optional sMessage As String = "")
        Dim sb As New System.Text.StringBuilder
        If sMessage > "" Then sb.AppendLine(sMessage)
        sb.AppendLine(BLL.Defaults.ExsToMultiline(exs))
        Dim sFullMessage As String = sb.ToString
        MsgBox(sFullMessage, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Public Sub WarnError(ex As Exception, Optional sMessage As String = "")
        Dim exs As New List(Of Exception)
        exs.Add(ex)
        WarnError(exs, sMessage)
    End Sub

    Public Sub WarnError(exs As List(Of Exception), Optional sMessage As String = "")
        Dim sb As New System.Text.StringBuilder
        If sMessage > "" Then sb.AppendLine(sMessage)
        For Each ex As Exception In exs
            sb.AppendLine(ex.Message)
        Next
        Dim sFullMessage As String = sb.ToString
        MsgBox(sFullMessage, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Public Function WarnOrPassError(exs As List(Of Exception), Optional sMessage As String = "") As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        If sMessage > "" Then sb.AppendLine(sMessage)
        For Each ex As Exception In exs
            sb.AppendLine(ex.Message)
        Next
        Dim sFullMessage As String = sb.ToString

        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
                Dim rc As MsgBoxResult = MsgBox(sFullMessage, MsgBoxStyle.AbortRetryIgnore, "MAT.NET")
                retval = rc = MsgBoxResult.Ignore
            Case Else
                MsgBox(sFullMessage, MsgBoxStyle.Exclamation, "MAT.NET")
        End Select
        Return retval
    End Function

    Public Sub WarnError(sMessage As String)
        MsgBox(sMessage, MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Public Function LoadCsvDialog(ByRef oCsvFile As DTOCsv, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = "fitxers Csv (*.csv)|*.csv|tots els documents (*.*)|*.*"
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                If BLL.BLLCsv.FromFile(.FileName, oCsvFile, exs) Then
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al importar el fitxer")
                End If
            End If
        End With
        Return retval
    End Function

    Public Function SaveCsvDialog(oCsv As DTOCsv, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle > "" Then
                .Title = sTitle
                .FileName = sTitle & ".csv"
            End If
            .Filter = "fitxers Csv (*.csv)|*.csv|tots els documents (*.*)|*.*"
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                If BLL.BLLCsv.Save(.FileName, oCsv, exs) Then
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al desar el fitxer")
                End If
            End If
        End With
        Return retval
    End Function


    Public Function SaveFlatFileDialog(oFlatFile As DTOFlatFile, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .FileName = oFlatFile.Nom & ".txt"
            .Filter = "fitxers Txt (*.txt)|*.txt|tots els documents (*.*)|*.*"
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                Dim s As String = BLL.BLLFlatFile.ToString(oFlatFile, exs)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs, "error al redactar el fitxer")
                Else
                    If BLL.FileSystemHelper.SaveTextToFile(s, .FileName, exs) Then
                        retval = True
                    Else
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                End If
            End If
        End With
        Return retval
    End Function

    Public Function LoadPdfDialog(ByRef oMediaObject As MediaObject, Optional sTitle As String = "") As Boolean
        Dim retval As Boolean
        Dim oDlg As New OpenFileDialog
        With oDlg
            If sTitle > "" Then .Title = sTitle
            .Filter = "documents Pdf (*.pdf)|*.pdf|tots els documents (*.*)|*.*"
            If .ShowDialog Then
                Dim exs As New List(Of exception)
                Dim tmpMediaObject As MediaObject = MediaObject.FromFile(.FileName, exs)
                If exs.Count = 0 Then
                    oMediaObject = tmpMediaObject
                    retval = True
                Else
                    UIHelper.WarnError(exs, "error al importar document")
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
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                Dim tmp As DTODocFile = BLL_DocFile.FromFile(.FileName, exs)
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


    Public Sub ShowExcel(oSheet As DTOExcelSheet)
        MatExcel.FromExcelSheet(oSheet).Visible = True
    End Sub

    Public Sub ShowHtml(ByVal sFileNameOrUrl As String)
        If sFileNameOrUrl = "" Then
            UIHelper.WarnError("falta la adreça del fitxer o pàgina web")
        Else
            If sFileNameOrUrl.IndexOf("://") < 0 Then sFileNameOrUrl = "http://" & sFileNameOrUrl
            Dim sInfo As New ProcessStartInfo(sFileNameOrUrl)
            Try
                Process.Start(sInfo)
            Catch ex As Exception
                Dim exs As New List(Of exception)
                exs.Add(ex)
                UIHelper.WarnError(exs, "no es pot obrir '" & sFileNameOrUrl & "'")
            End Try
        End If

    End Sub

    Public Sub ShowStream(oDocFile As DTODocFile, Optional sSuggestedFileName As String = "", Optional BlDownloadFromWeb As Boolean = True)
        If oDocFile Is Nothing Then
            UIHelper.WarnError("document no disponible")
        Else
            If BLL.BLLDocFile.Exists(oDocFile.Hash, oDocFile.Fch) Then
                Process.Start(BLL.BLLDocFile.DownloadUrl(oDocFile, True))
            Else
                If sSuggestedFileName = "" Then sSuggestedFileName = BLL_DocFile.FileNameOrDefault(oDocFile)
                Select Case oDocFile.Mime
                    Case DTOEnums.MimeCods.Pdf, DTOEnums.MimeCods.Gif, DTOEnums.MimeCods.Jpg
                        Dim exs As New List(Of Exception)
                        If BLL.FileSystemHelper.SaveStream(oDocFile.Stream, exs, sSuggestedFileName) Then
                            Process.Start("IExplore.exe", sSuggestedFileName)
                        Else
                            UIHelper.WarnError(exs, "error al desar el fitxer")
                        End If
                    Case Else
                        UIHelper.WarnError("visor no implementat per aquest format" & vbCrLf & BLL_DocFile.Features(oDocFile))
                End Select
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


    Public Sub DataGridViewPaintGradientRowBackGround(ByVal oDataGridView As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus

        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            oDataGridView.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            oDataGridView.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

End Module
