Public Class Xl_Tarifa
    Private _Tarifa As DTOCustomerTarifa
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        customRef
        nom
        rrpp
        dt1
        tarifa
        dt2
        net
        cost
        margin
    End Enum

    Public Sub DisplayCostColumns(BlDisplay As Boolean)
        DataGridView1.Columns(Cols.net).Visible = BlDisplay
        DataGridView1.Columns(Cols.cost).Visible = BlDisplay
        DataGridView1.Columns(Cols.margin).Visible = BlDisplay
    End Sub

    Public Shadows Sub Load(value As DTOCustomerTarifa)
        _Tarifa = value
        _ControlItems = New ControlItems

        Dim oBrand As DTOProductBrand = Nothing
        Dim oCategory As DTOProductCategory = Nothing
        Dim oSku As DTOProductSku = Nothing
        Try
            For Each oBrand In _Tarifa.Brands
                Dim oControlItem As New ControlItem(oBrand)
                _ControlItems.Add(oControlItem)
                For Each oCategory In oBrand.categories
                    oControlItem = New ControlItem(oCategory)
                    _ControlItems.Add(oControlItem)
                    For Each oSku In oCategory.skus
                        oControlItem = New ControlItem(oSku)
                        _ControlItems.Add(oControlItem)
                    Next
                Next
            Next

        Catch ex As Exception
            Stop
        End Try
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

            TextBoxSearch_TextChanged(Me, EventArgs.Empty) 'crida a .DataSource = _ControlItems tenint en compte el filtre present
            '.DataSource = _ControlItems

            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.customRef)
                .HeaderText = "Custom ref"
                .DataPropertyName = "CustomRef"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.nom)
                .HeaderText = "Producte"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
            With .Columns(Cols.dt1)
                .HeaderText = "Dte"
                .DataPropertyName = "Dt1"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
                .DefaultCellStyle.ForeColor = Color.Red
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.tarifa)
                .HeaderText = "Tarifa"
                .DataPropertyName = "tarifa"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.dt2)
                .HeaderText = "Dte"
                .DataPropertyName = "Dt2"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
                .DefaultCellStyle.ForeColor = Color.Red
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.net)
                .HeaderText = "Net"
                .DataPropertyName = "Net"
                .Width = 65
                .Visible = False
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.cost)
                .HeaderText = "Cost"
                .DataPropertyName = "Cost"
                .Width = 65
                .Visible = False
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.margin)
                .HeaderText = "Marge"
                .DataPropertyName = "Margin"
                .Width = 50
                .Visible = False
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
                Dim oMenuCaption As ToolStripMenuItem = oContextMenu.Items.Add("Marca comercial")
                Dim oMenu As New Menu_ProductBrand(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oMenuCaption.DropDownItems.AddRange(oMenu.Range)
            ElseIf TypeOf oProduct Is DTOProductCategory Then
                Dim oMenuCaption As ToolStripMenuItem = oContextMenu.Items.Add("Categoría")
                Dim oMenu As New Menu_ProductCategory(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oMenuCaption.DropDownItems.AddRange(oMenu.Range)
            ElseIf TypeOf oProduct Is DTOProductSku Then
                Dim oMenuCaption As ToolStripMenuItem = oContextMenu.Items.Add("Producte")
                Dim oMenu As New Menu_ProductSku(oProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oMenuCaption.DropDownItems.AddRange(oMenu.Range)
                oContextMenu.Items.Add("analisis de preu", Nothing, AddressOf Do_PriceDetails)
            End If
            oContextMenu.Items.Add("-")

        End If

        oContextMenu.Items.Add("exportar a Excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add("Web", Nothing, AddressOf Do_Web)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_PriceDetails()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oSku As DTOProductSku = oControlItem.Source
        Dim oCustomer = _Tarifa.Customer
        Dim oFrm As New Frm_PriceDetails(oCustomer, oSku)
        oFrm.Show()
    End Sub

    Private Sub Do_Excel()
        Dim oLang As DTOLang = DTOApp.current.lang
        Dim oSheet = _Tarifa.Excel(oLang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Do_Web()
        Dim sUrl As String = FEB2.CustomerTarifa.Url(_Tarifa, True)
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
            Case Cols.dt1, Cols.net
                With e.CellStyle
                    .ForeColor = Color.Gray
                End With
            Case Cols.margin
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.LinCod = ControlItem.LinCods.Sku And oControlItem.Net > 0 And oControlItem.Cost > 0 Then
                    Select Case e.Value
                        Case < 0
                            With e.CellStyle
                                .ForeColor = Color.White
                                .BackColor = ColorTranslator.FromHtml("#D20000")
                            End With
                        Case < 10
                            With e.CellStyle
                                .ForeColor = Color.White
                                .BackColor = ColorTranslator.FromHtml("#CE3D00")
                            End With
                        Case < 20
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#CA7800")
                        Case < 30
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#C6B200")
                        Case < 50
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#9CC200")
                        Case Else
                            e.CellStyle.BackColor = ColorTranslator.FromHtml("#60BF00")
                    End Select
                End If
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

        Property CustomRef As String
        Property Nom As String
        Property RRPP As Decimal
        Property Dt1 As Decimal
        Property Tarifa As Decimal
        Property Dt2 As Decimal
        Property Net As Decimal
        Property Cost As Decimal
        Property Margin As Decimal

        Property LinCod As LinCods

        Public Enum LinCods
            Brand
            Category
            Sku
        End Enum

        Public Sub New(value As DTOProductBrand)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Brand
            With value
                _Nom = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

        Public Sub New(value As DTOProductCategory)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Category
            With value
                _Nom = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

        Public Sub New(value As DTOProductSku)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Sku
            With value
                _CustomRef = .refCustomer
                _Nom = .nom.Tradueix(Current.Session.Lang)
                If .Price IsNot Nothing Then
                    _Tarifa = .Price.Eur
                End If
                If .RRPP IsNot Nothing Then
                    _RRPP = .RRPP.Eur
                    If _RRPP <> 0 Then
                        _Dt1 = 100 * (1 - _Tarifa / _RRPP)
                    End If
                End If
                _Dt2 = .CustomerDto
                _Net = Math.Round(_Tarifa * (100 - _Dt2) / 100, 2, MidpointRounding.AwayFromZero)
                If .Cost IsNot Nothing And _Net > 0 Then
                    _Cost = .Cost.Eur
                    If _Cost <> 0 Then
                        _Margin = 100 * (_Net / _Cost - 1)
                    End If
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
                    If item.Nom.ToLower.Contains(sKey) Or oSku.nomLlarg.Contains(sKey) Then
                        If Not oSku.category.brand.Equals(oBrand) Then
                            oBrand = oSku.category.brand
                            oControlItems.Add(New ControlItem(oBrand))
                        End If
                        If Not oSku.category.Equals(oCategory) Then
                            oCategory = oSku.category
                            oControlItems.Add(New ControlItem(oCategory))
                        End If
                        oControlItems.Add(item)
                    End If
                End If
            Next
        End If
        'DataGridView1.Columns(Cols.customRef).Visible = oControlItems.ToList.Any(Function(x) x.CustomRef.isNotEmpty())
        DataGridView1.DataSource = oControlItems
    End Sub
End Class
