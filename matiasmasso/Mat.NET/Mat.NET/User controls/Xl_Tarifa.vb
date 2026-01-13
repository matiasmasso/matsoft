Public Class Xl_Tarifa
    Private _Tarifa As DTOCustomerTarifa
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        nom
        cost
        rrpp
        dto
    End Enum

    Public Shadows Sub Load(value As DTOCustomerTarifa)
        _Tarifa = value
        _ControlItems = New ControlItems
        For Each oBrand As DTOProductBrand In _Tarifa.Brands
            If BLL.BLLCliProductsBlocked.IsAllowed(_Tarifa.CliProductsBlocked, oBrand) Then
                Dim oControlItem As New ControlItem(oBrand)
                _ControlItems.Add(oControlItem)
                For Each oCategory As DTOProductCategory In oBrand.Categories
                    If BLL.BLLCliProductsBlocked.IsAllowed(oCategory, _Tarifa.Customer) Then
                        oControlItem = New ControlItem(oCategory)
                        _ControlItems.Add(oControlItem)
                        For Each oSku As DTOProductSku In oCategory.Skus
                            oControlItem = New ControlItem(oSku)
                            _ControlItems.Add(oControlItem)
                        Next
                    End If
                Next
            End If
        Next
        LoadGrid()
    End Sub

    Public Sub Disable()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            For Each oCell As DataGridViewCell In oRow.Cells
                With oCell.Style
                    .BackColor = Color.FromKnownColor(KnownColor.Control)
                    .SelectionBackColor = oCell.Style.BackColor
                End With
            Next
        Next
    End Sub


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Producte"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.cost)
                .HeaderText = "Cost"
                .DataPropertyName = "Cost"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.rrpp)
                .HeaderText = "PVP"
                .DataPropertyName = "RRPP"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.dto)
                .HeaderText = "Dte"
                .DataPropertyName = "Dto"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
                .DefaultCellStyle.ForeColor = Color.Red
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oProduct As DTOProduct = CurrentControlItem.Source
            If TypeOf oProduct Is DTOProductBrand Then
                Dim oMenu As New Menu_ProductBrand(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
            ElseIf TypeOf oProduct Is DTOProductCategory Then
                Dim oMenu As New Menu_ProductCategory(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
            ElseIf TypeOf oProduct Is DTOProductSku Then
                Dim oMenu As New Menu_ProductSku(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
            End If
            oContextMenu.Items.Add("-")

        End If

        oContextMenu.Items.Add("exportar a Excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add("exportar a Csv", Nothing, AddressOf Do_Csv)
        oContextMenu.Items.Add("Web", Nothing, AddressOf Do_Web)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As DTOExcelSheet = BLL.BLLCustomerTarifa.excel(_Tarifa)
        UIHelper.ShowExcel(oSheet)
    End Sub

    Private Sub Do_Csv()
        Dim oCsv As DTOCsv = BLL.BLLCustomerTarifa.Csv(_Tarifa)
        UIHelper.SaveCsvDialog(oCsv, "exportar tarifa a Csv")
    End Sub

    Private Sub Do_Web()
        Dim sUrl As String = BLL.BLLCustomerTarifa.Url(_Tarifa, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oProduct As DTOProduct = oControlItem.Source
                If TypeOf oProduct Is DTOProductBrand Then
                ElseIf TypeOf oProduct Is DTOProductCategory Then
                    With e.CellStyle
                        .Padding = New Padding(20, 0, 0, 0)
                    End With
                ElseIf TypeOf oProduct Is DTOProductSku Then
                    With e.CellStyle
                        .Padding = New Padding(40, 0, 0, 0)
                    End With
                End If
            Case Cols.dto
                With e.CellStyle
                    .ForeColor = Color.Gray
                End With
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oProduct As DTOProduct = CurrentControlItem.Source
        If TypeOf oProduct Is DTOProductBrand Then
            Dim oFrm As New Frm_ProductBrand(oProduct)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            Dim oFrm As New Frm_ProductCategory(oProduct)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oProduct Is DTOProductSku Then
            Dim oFrm As New Frm_ProductSku(oProduct)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oProduct As DTOProduct = oControlItem.Source
        If TypeOf oProduct Is DTOProductBrand Then
            With oRow.DefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
            End With
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            With oRow.DefaultCellStyle
                .BackColor = Color.LightBlue
                .ForeColor = Color.Black
            End With
        ElseIf TypeOf oProduct Is DTOProductSku Then
            With oRow.DefaultCellStyle
                .BackColor = Color.White
                .ForeColor = Color.Black
            End With
        End If
        With oRow.DefaultCellStyle
            .SelectionBackColor = .BackColor
            .SelectionForeColor = .ForeColor
        End With

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOProduct

        Property Nom As String
        Property Cost As Decimal
        Property RRPP As Decimal
        Property Dto As Decimal

        Public Sub New(value As DTOProductBrand)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
            End With
        End Sub

        Public Sub New(value As DTOProductCategory)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
            End With
        End Sub

        Public Sub New(value As DTOProductSku)
            MyBase.New()
            _Source = value
            With value
                _Nom = .NomCurt
                _Cost = .TarifaCost.Eur
                _RRPP = .RRPP.Eur
                If _RRPP <> 0 Then
                    _Dto = 100 * (1 - _Cost / _RRPP)
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sKey As String = TextBoxSearch.Text.ToLower
        Dim oControlItems As ControlItems = Nothing
        If sKey = "" Then
            oControlItems = _ControlItems
        Else
            oControlItems = New ControlItems
            Dim oBrand As New DTOProductBrand
            Dim oCategory As New DTOProductCategory
            For Each item As ControlItem In _ControlItems
                If TypeOf item.Source Is DTOProductSku Then
                    Dim oSku As DTOProductSku = item.Source
                    If item.Nom.ToLower.Contains(sKey) Or oSku.NomLlarg.ToLower.Contains(sKey) Then
                        If Not oSku.Category.Brand.Equals(oBrand) Then
                            oBrand = oSku.Category.Brand
                            oControlItems.Add(New ControlItem(oBrand))
                        End If
                        If Not oSku.Category.Equals(oCategory) Then
                            oCategory = oSku.Category
                            oControlItems.Add(New ControlItem(oCategory))
                        End If
                        oControlItems.Add(item)
                    End If
                End If
            Next
        End If
        DataGridView1.DataSource = oControlItems
    End Sub
End Class
