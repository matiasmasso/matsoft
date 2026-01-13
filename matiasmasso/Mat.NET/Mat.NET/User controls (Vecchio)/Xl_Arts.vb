Public Class Xl_Arts
    Private _Product As Product
    Private _Products As Products

    Private _Related As DTO.DTOProduct.Relateds = DTO.DTOProduct.Relateds.NotSet
    Private mArt As Art
    Private mMgz As DTOMgz = BLL.BLLApp.Mgz
    Private mSrcNom As String = ""
    Private mLastMouseDownRectangle As System.Drawing.Rectangle
    Private mInclouArtsObsolets As Boolean = False
    Private mInclouAllItems As Boolean = True

    Private Enum Cols
        Id
        Nom
        LastProduction
        LastProductionIco
        Stk
        Pn2
        Pn3
        Img
        ImgIco
        Pn1
        Prv
        Obsoleto
    End Enum

    Public Sub SetAccessoriesFrom(ByVal oProduct As Product)
        _Related = DTO.DTOProduct.Relateds.Accessories
        _Product = oProduct
        mSrcNom = _Product.Nom(BLL.BLLApp.Lang)
        LoadGrid()
    End Sub

    Public Sub SetSparesFromStp(ByVal oProduct As Product)
        _Related = DTO.DTOProduct.Relateds.Spares
        _Product = oProduct
        mSrcNom = _Product.Nom(BLL.BLLApp.Lang)
        LoadGrid()
    End Sub


    Private Sub LoadGrid()
        Dim oLang As DTOLang = BLL.BLLApp.Lang
        mMgz = BLL.BLLApp.Mgz
        Cursor = Cursors.WaitCursor

        Dim oTb As New DataTable
        With oTb.Columns
            .Add("GUID", System.Type.GetType("System.Guid"))
            .Add("NOM", System.Type.GetType("System.String"))
            .Add("LASTPRODUCTION", System.Type.GetType("System.Boolean"))
            .Add("LASTPRODUCTIONICO", System.Type.GetType("System.Byte[]"))
            .Add("STK", System.Type.GetType("System.Int32"))
            .Add("PN2", System.Type.GetType("System.Int32"))
            .Add("PN3", System.Type.GetType("System.Int32"))
            .Add("IMG", System.Type.GetType("System.Boolean"))
            .Add("IMGICO", System.Type.GetType("System.Byte[]"))
            .Add("PN1", System.Type.GetType("System.Int32"))
            .Add("PRV", System.Type.GetType("System.String"))
            .Add("EX", System.Type.GetType("System.Boolean"))
        End With

        Select Case _Related
            Case DTO.DTOProduct.Relateds.Accessories
                _Products = _Product.Accessoris
            Case DTO.DTOProduct.Relateds.Spares
                _Products = _Product.Spares
            Case Else
                Exit Sub
        End Select

        For Each oProduct As Product In _Products
            Dim Skip As Boolean = oProduct.Obsoleto() And Not mInclouArtsObsolets
            If Not Skip Then
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("GUID") = oProduct.Guid
                oRow("NOM") = oProduct.Nom(oLang)
                Dim iStk, iPn2, iPn3, iPn1, iPrv As Integer
                Dim BlLastProduction = False
                Dim BlImage As Boolean = False
                If oProduct.ValueType = Product.ValueTypes.Art Then
                    Dim oArt As Art = oProduct.Value
                    iStk = oArt.Stk(mMgz)
                    iPn2 = oArt.Pn2
                    iPn3 = oArt.Pot()
                    iPn1 = oArt.Pn1
                    iPrv = oArt.Previsio()
                    'oMgz.GetArtStk(oArt, Nothing, iStk, iPn2, iPn3, iPn1, iPrv)
                    BlLastProduction = oArt.LastProduction
                    BlImage = oArt.Image IsNot Nothing
                End If
                oRow("LASTPRODUCTION") = BlLastProduction
                oRow("STK") = iStk
                oRow("PN2") = iPn2
                oRow("PN3") = iPn3
                oRow("IMG") = BlImage
                oRow("PN1") = iPn1
                oRow("PRV") = iPrv
                oRow("EX") = oProduct.Obsoleto
            End If
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .BackgroundColor = Color.White

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.LastProduction)
                .Visible = False
            End With
            With .Columns(Cols.LastProductionIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            With .Columns(Cols.Stk)
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
                .HeaderText = "pot"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            With .Columns(Cols.Img)
                .Visible = False
            End With
            With .Columns(Cols.ImgIco)
                .HeaderText = ""
                .Width = 18
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
            End With
            With .Columns(Cols.Pn1)
                .HeaderText = "prov"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.SelectionBackColor = Color.Transparent
                .DefaultCellStyle.SelectionForeColor = Color.Black
            End With
            With .Columns(Cols.Prv)
                .Visible = False
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With
            SetContextMenu()
        End With
        Cursor = Cursors.Default
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Pn1
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim iPn1 As Integer = 0
                If Not IsDBNull(oRow.Cells(Cols.Pn1).Value) Then
                    iPn1 = oRow.Cells(Cols.Pn1).Value
                End If
                If iPn1 = 0 Then
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.SelectionBackColor = Color.White
                Else
                    Dim iStk As Integer = 0
                    If Not IsDBNull(oRow.Cells(Cols.Stk).Value) Then
                        iStk = oRow.Cells(Cols.Stk).Value
                    End If
                    Dim iPn2 As Integer = 0
                    If Not IsDBNull(oRow.Cells(Cols.Pn2).Value) Then
                        iPn2 = oRow.Cells(Cols.Pn2).Value
                    End If
                    Dim iPrv As Integer = 0
                    If Not IsDBNull(oRow.Cells(Cols.Prv).Value) Then
                        iPrv = oRow.Cells(Cols.Prv).Value
                    End If
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

            Case Cols.ImgIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Img).Value
                    Case True
                        e.Value = My.Resources.img_16
                    Case Else
                        e.Value = My.Resources.empty
                End Select

            Case Cols.LastProductionIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.LastProduction).Value
                    Case 0
                        e.Value = My.Resources.empty
                    Case Else
                        e.Value = My.Resources.wrong
                End Select
        End Select
    End Sub



    Private Function CurrentProduct() As Product
        Dim oProduct As Product = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            oProduct = New Product(oGuid)
        End If
        Return oProduct
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oProduct As Product = CurrentProduct()
        Dim oMenuItem As ToolStripMenuItem

        If oProduct IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("producte")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_Product As New Menu_Product(oProduct)
            AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Product.Range)

            oMenuItem = New ToolStripMenuItem
            With oMenuItem
                .Text = "treu-lo d'aquí"
                .Image = My.Resources.del
            End With
            AddHandler oMenuItem.Click, AddressOf MenuItemArt_Remove
            oContextMenu.Items.Add(oMenuItem)

            oContextMenu.Items.Add("-")
        End If

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "afegir producte existent"
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemArt_AddNew
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "inclou tots els " & IIf(_Related = DTO.DTOProduct.Relateds.Accessories, "accessoris", "recanvis")
            .Checked = mInclouAllItems
            .CheckOnClick = True
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemArt_InclouAllItems
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "inclou obsolets"
            .Checked = mInclouArtsObsolets
            .CheckOnClick = True
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemArt_ChangeObsolets
        oContextMenu.Items.Add(oMenuItem)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    '

    Private Sub MenuItemArt_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTpaFrom As Tpa = _Product.Tpa
        Dim oParent As New Product(oTpaFrom)
        Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.SelectProduct, oParent)
        AddHandler oFrm.AfterSelect, AddressOf onNewChildSelected
        oFrm.Show()
    End Sub

    Private Sub onNewChildSelected(sender As Object, e As MatEventArgs)
        Dim oProduct As Product = e.Argument
        Do_AddNew(oProduct)
        RefreshRequest(sender, e)
    End Sub

    Private Sub MenuItemArt_InclouAllItems(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        mInclouAllItems = oMenuItem.Checked
        RefreshRequest(sender, e)
    End Sub

    Private Sub MenuItemArt_ChangeObsolets(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        mInclouArtsObsolets = oMenuItem.Checked
        RefreshRequest(sender, e)
    End Sub

    Private Sub MenuItemArt_Remove(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oProduct As Product = CurrentProduct()
        Dim sRel As String = ""
        Select Case _Related
            Case DTO.DTOProduct.Relateds.Accessories
                sRel = "accessori"
            Case DTO.DTOProduct.Relateds.Spares
                sRel = "recanvi"
        End Select
        Dim s As String = oProduct.Nom(BLL.BLLApp.Lang) & vbCrLf & "el treiem de " & sRel & " de " & mSrcNom & "?" & vbCrLf & "aixó no l'esborrará del cataleg"
        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim SQL As String = "DELETE ARTSPARE WHERE COD=@COD AND TargetGuid=@PARENTGUID AND ProductGuid=@CHILDGUID"
            MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@COD", CInt(_Related).ToString, "@PARENTGUID", _Product.Guid.ToString, "@CHILDGUID", oProduct.Guid.ToString)
            RefreshRequest(sender, e)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1


        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oProduct As Product = CurrentProduct()
        Select Case oProduct.ValueType
            Case Product.ValueTypes.Art
                Dim oArt As Art = CType(oProduct.Value, Art)
                Dim oFrm As New Frm_Art(oArt)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Product.ValueTypes.Stp
                Dim oStp As Stp = CType(oProduct.Value, Stp)
                Dim oFrm As New Frm_Stp(oStp, BLL.Defaults.SelectionModes.Browse)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Product.ValueTypes.Tpa
                Dim oTpa As Tpa = CType(oProduct.Value, Tpa)
                Dim oFrm As New Frm_Tpa(oTpa)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub



    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        If IsDBNull(oRow.Cells(Cols.Stk).Value) Then
            PaintGradientRowBackGround(DataGridView1, e, Color.LightSalmon)
        Else
            Dim iStk As Integer = oRow.Cells(Cols.Stk).Value
            Dim iPn2 As Integer = oRow.Cells(Cols.Pn2).Value
            Select Case iStk
                Case Is > 0
                    Select Case iStk - iPn2
                        Case Is > 0
                            PaintGradientRowBackGround(DataGridView1, e, Color.LightGreen)
                        Case Else
                            PaintGradientRowBackGround(DataGridView1, e, Color.Yellow)
                    End Select
                Case Else
                    Dim BlObsoleto As Boolean = CType(oRow.Cells(Cols.Obsoleto).Value, Boolean)
                    Select Case BlObsoleto
                        Case True
                            oRow.DefaultCellStyle.BackColor = Color.LightGray
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


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(GetType(Product))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oChild As Product = e.Data.GetData(GetType(Product))
        Do_AddNew(oChild)
        e.Effect = DragDropEffects.None
    End Sub

    Private Sub Do_AddNew(oChild As Product)
        Dim s As String = "assignem " & oChild.Nom(BLL.BLLApp.Lang) & vbCrLf
        Select Case _Related
            Case DTO.DTOProduct.Relateds.Accessories
                s = s & "com a accessori de " & mSrcNom & "?"
            Case DTO.DTOProduct.Relateds.Spares
                s = s & "com a recanvi de " & mSrcNom & "?"
        End Select

        Dim exs As New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            If ArtLoader.InsertAccesoryOrSpare(_Product, oChild, _Related, exs) Then
                RefreshRequest(Nothing, EventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al importar producte")
            End If
        End If
    End Sub
End Class
