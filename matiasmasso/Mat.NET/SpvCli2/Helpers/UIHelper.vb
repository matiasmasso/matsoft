Module UIHelper
    Public Enum SelModes
        Browse
        Selection
    End Enum

    Public Function CheckNetworkAvailability() As Boolean
        Do Until System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()
            Dim rc As MsgBoxResult = MsgBox("no hi ha connexió a Internet", MsgBoxStyle.RetryCancel)
            If rc = MsgBoxResult.Cancel Then
                Return False
            End If
        Loop
        Return True
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

    Public Function SavefileDialogFilter(oMime As MatHelperStd.MimeCods) As String
        Dim retval As String = String.Format("fitxers {0}|*.{0}|Tots els fitxers (*.*)|*.*", oMime.ToString)
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
            End With
        End If
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

    Public Sub WarnError(ex As DTOTaskResult.Exception, Optional sMessage As String = "")
        Dim exs As New List(Of Exception)
        exs.Add(New Exception(ex.Message))
        WarnError(exs, sMessage)
    End Sub

    Public Sub WarnError(exs As List(Of DTOTaskResult.Exception), Optional sMessage As String = "")
        Dim ex2 As New List(Of Exception)
        For Each ex In exs
            ex2.Add(New Exception(ex.Message))
        Next
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




    Public Sub ShowHtml(ByVal sFileNameOrUrl As String)
        If sFileNameOrUrl = "" Then
            UIHelper.WarnError("falta la adreça del fitxer o pàgina web")
        Else
            If sFileNameOrUrl.IndexOf("://") < 0 Then sFileNameOrUrl = "http://" & sFileNameOrUrl
            Dim sInfo As New ProcessStartInfo(sFileNameOrUrl)
            Try
                Process.Start(sInfo)
            Catch ex As Exception
                Dim exs As New List(Of Exception)
                exs.Add(ex)
                UIHelper.WarnError(exs, "no es pot obrir '" & sFileNameOrUrl & "'")
            End Try
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

    Public Function GetExcelFromDataGridView(ByVal oGrid As DataGridView, Optional ByRef oProgressBar As ProgressBar = Nothing) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet

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
            Dim oRow = retval.AddRow
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

End Module
