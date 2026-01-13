Public Class Xl_StpArts

    Private _Stp As Stp
    Private _Mgz As Mgz
    Private _ArtsForDisplay As ArtsForDisplay
    Private _DefaultArt As Art
    Private _MenuItemHideObsolets As ToolStripMenuItem
    Private _LastMouseDownRectangle As System.Drawing.Rectangle
    Private _AllowEvents As Boolean

    Public Property SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event SelectionChanged(sender As Object, e As EventArgs)
    Public Event ValueChanged(sender As Object, e As EventArgs)

    Private Enum Cols
        Id
        Nom
        LastProductionIco
        Stk
        Pn2
        Pn3
        ImgIco
        Pn1
    End Enum

    Private Sub Xl_StpArts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _MenuItemHideObsolets = New ToolStripMenuItem("Ocultar obsolets", Nothing, AddressOf Do_ShowObsolets)
        _MenuItemHideObsolets.CheckOnClick = True
        _MenuItemHideObsolets.Checked = True
    End Sub

    Public Sub LoadControl(oStp As Stp, oMgz As Mgz)
        _Stp = oStp
        _Mgz = oMgz
        LoadGrid()
    End Sub

    Public ReadOnly Property Art As Art
        Get
            Dim retval As Art = CurrentItem()
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        _AllowEvents = False
        _ArtsForDisplay = MaxiSrvr.ArtsForDisplay.AllFromStp(_Stp, _Mgz, _MenuItemHideObsolets.Checked)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("Id", "ref.")
            .Columns.Add("NomCurt", "nom")
            .Columns.Add(New DataGridViewImageColumn())
            .Columns.Add("UnitsInStock", "stock")
            .Columns.Add("UnitsInClients", "clients")
            .Columns.Add("UnitsInPot", "pot")
            .Columns.Add(New DataGridViewImageColumn())
            .Columns.Add("UnitsInProveidor", "prov")

            .DataSource = _ArtsForDisplay
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .ReadOnly = True
            .BackgroundColor = Color.White

            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "ref."
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .DataPropertyName = "NomCurt"
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.LastProductionIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            With .Columns(Cols.Stk)
                .DataPropertyName = "UnitsInStock"
                .HeaderText = "stock"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            With .Columns(Cols.Pn2)
                .DataPropertyName = "UnitsInClients"
                .HeaderText = "clients"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            With .Columns(Cols.Pn3)
                .DataPropertyName = "UnitsInPot"
                .HeaderText = "pot"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With

            With .Columns(Cols.ImgIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            With .Columns(Cols.Pn1)
                .DataPropertyName = "UnitsInProveidor"
                .HeaderText = "prov"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With

        End With

        If _ArtsForDisplay.Count > 0 Then
            DataGridView1.Rows(0).Selected = True
            DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols.Nom)
        End If

        Application.DoEvents()
        SetContextMenu()
        Cursor = Cursors.Default
        Dim oArgs As New MatEventArgs(CurrentItem)
        RaiseEvent SelectionChanged(Me, oArgs)
        _AllowEvents = True
    End Sub


    Private Function CurrentItem() As Art
        Dim retval As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = CType(oRow.DataBoundItem, Art)
        End If
        Return retval
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        'LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentItem()

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add(_MenuItemHideObsolets)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ShowObsolets(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub OnNewItemAdded(sender As Object, e As EventArgs)
        Dim oItem As Stp = sender
        Dim oItems As Stps = DataGridView1.DataSource
        oItems.Add(oItem)
        'RefreshRequest(sender, e)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Exit Sub
        Select Case e.ColumnIndex
            Case Cols.Pn1
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oArtForDisplay As ArtForDisplay = oRow.DataBoundItem
                If oArtForDisplay.UnitsInProveidor = 0 Then
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.White
                Else
                    Dim iStk As Integer = oArtForDisplay.UnitsInStock
                    Dim iPn2 As Integer = oArtForDisplay.UnitsInClients
                    Dim iPn1 As Integer = oArtForDisplay.UnitsInProveidor
                    Dim iPrv As Integer = oArtForDisplay.UnitsInPrevisio
                    If iPrv = 0 Then
                        e.CellStyle.BackColor = Color.White
                        e.CellStyle.SelectionBackColor = Color.White
                    ElseIf iPrv >= iPn2 - iStk Then
                        e.CellStyle.BackColor = Color.LightGreen
                        e.CellStyle.SelectionBackColor = Color.LightGreen
                    Else
                        If iPrv = iPn1 Then
                            e.CellStyle.BackColor = Color.Yellow
                            e.CellStyle.SelectionBackColor = Color.Yellow
                        Else
                            e.CellStyle.BackColor = Color.LightSalmon
                            e.CellStyle.SelectionBackColor = Color.LightSalmon
                        End If
                    End If
                End If

            Case Cols.LastProductionIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oArtForDisplay As ArtForDisplay = oRow.DataBoundItem
                e.Value = IIf(oArtForDisplay.LastProduction, My.Resources.wrong, My.Resources.empty)
            Case Cols.ImgIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oArtForDisplay As ArtForDisplay = oRow.DataBoundItem
                If oArtForDisplay.ImageExists Then
                    e.Value = My.Resources.img_16
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oArt As Art = CurrentItem()
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_Art(oArt, BLL.Defaults.SelectionModes.Browse)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                Dim oArgs As New MatEventArgs(oArt)
                RaiseEvent ValueChanged(Me, oArgs)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            Dim oArgs As New MatEventArgs(CurrentItem)
            RaiseEvent SelectionChanged(Me, oArgs)
        End If
    End Sub


    Private Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        If isDraggingImage(e) Then
            Dim sFilenames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            Dim sFilename As String = sFilenames(0)

            Dim oArt As Art = CurrentItem()

            Dim oImage As Image = Image.FromFile(sFilename)
            If Art.IsImageSizeLowRes(oImage.Size) Then
                oArt.UpdateBigImg(oImage)
            Else
                ' Dim sRootPath As String = ProductDownload.RootPath
                'If sFilename.ToLower.StartsWith(sRootPath.ToLower) Then
                'Dim oTarget As New DownloadTarget(oArt.Guid, DownloadTarget.Cods.Producte, DownloadSrc.Ids.Imatge_Alta_Resolucio)
                'oTarget.LoadFromFilename(sFilename)
                'Dim oFrm As New Frm_ProductDownload(oTarget)
                ' oFrm.Show()
                'Else
                ' Dim s As String = "les imatges d'alta resolució s'han de importar desde " & sRootPath
                'MsgBox(s, MsgBoxStyle.Exclamation)
            End If
        ElseIf e.Data.GetDataPresent("UniformResourceLocator") Then
            Dim sUrl As String = New IO.StreamReader(CType(e.Data.GetData("UniformResourceLocator"), IO.MemoryStream)).ReadToEnd
            Dim oArt As Art = CurrentItem()
            Dim oSku As New DTOProductSku(oArt.Guid)
            Dim oHighResImage As DTOHighResImage = BLL.BLLHighResImage.SetHighResImageSku(oSku, sUrl)
            If oHighResImage IsNot Nothing Then
                Dim oFrm As New Frm_HighResImage(oHighResImage)
                oFrm.Show()
            End If
        End If

    End Sub



    Private Sub DataGridView1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If isDraggingImage(e) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent("UniformResourceLocator") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = sender.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = sender.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then

            If isDraggingImage(e) Or e.Data.GetDataPresent("UniformResourceLocator") Then
                e.Effect = DragDropEffects.Copy

                Dim oclickedCell As DataGridViewCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                sender.CurrentCell = oclickedCell

                Dim oRow As DataGridViewRow = oclickedCell.OwningRow
                Dim iArt As Integer = oRow.Cells(Cols.Id).Value
                Dim oArt As Art = MaxiSrvr.Art.FromNum(BLL.BLLApp.Emp, iArt)

                Dim oArgs As New MatEventArgs(oArt)
                RaiseEvent SelectionChanged(Me, oArgs)
            Else
                e.Effect = DragDropEffects.None

            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                _LastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Not _LastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(_LastMouseDownRectangle.X, _LastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    'Dim oCell As DataGridViewCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oArt As Art = CurrentItem()
                    Dim oProduct As New Product(oArt)
                    Dim oRc As DragDropEffects = sender.DoDragDrop(oProduct, DragDropEffects.Copy)
                    Select Case oRc
                        Case DragDropEffects.Copy
                            RefreshRequestArts(sender, e)
                        Case DragDropEffects.None
                            'falla dragdrop
                    End Select
                End If
            End If
        End If
    End Sub


    Private Sub DataGridViewArts_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        If IsDBNull(oRow.Cells(Cols.Stk).Value) Then
            PaintGradientRowBackGround(DataGridView1, e, Color.LightSalmon)
        Else
            Dim iStk As Integer
            If Not IsDBNull(oRow.Cells(Cols.Stk).Value) Then
                iStk = oRow.Cells(Cols.Stk).Value
            End If

            Dim iPn2 As Integer
            If Not IsDBNull(oRow.Cells(Cols.Pn2).Value) Then
                iPn2 = oRow.Cells(Cols.Pn2).Value
            End If

            Select Case iStk
                Case Is > 0
                    Select Case iStk - iPn2
                        Case Is > 0
                            PaintGradientRowBackGround(DataGridView1, e, Color.LightGreen)
                        Case Else
                            PaintGradientRowBackGround(DataGridView1, e, Color.Yellow)
                    End Select
                Case Else
                    Dim oArtForDisplay As ArtForDisplay = oRow.DataBoundItem
                    Select Case oArtForDisplay.Obsoleto
                        Case True
                            PaintGradientRowBackGround(DataGridView1, e, Color.LightGray)
                        Case False
                            PaintGradientRowBackGround(DataGridView1, e, Color.LightSalmon)
                    End Select

            End Select
        End If


    End Sub



    Private Sub PaintGradientRowBackGround(ByVal oGrid As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            oGrid.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            oGrid.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Function isDraggingImage(ByVal e As System.Windows.Forms.DragEventArgs) As Boolean
        Dim retval As Boolean = False
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            Dim sFilenames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If sFilenames.Length > 0 Then
                Dim sFilename As String = sFilenames(0)
                If BLL.IsImage(sFilename) Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Public Function Excel() As Microsoft.Office.Interop.Excel.Application
        Dim retval As Microsoft.Office.Interop.Excel.Application = MatExcel.GetExcelFromDataGridView(DataGridView1)
        Return retval
    End Function

    Private Sub RefreshRequestArts(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If

    End Sub

End Class

