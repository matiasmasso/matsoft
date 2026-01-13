Public Class Xl_IntrastatSkus

    Inherits _Xl_ReadOnlyDatagridview

    Private _Intrastat As DTOIntrastat
    Private _Values As List(Of DTOProductSku)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        brand
        category
        sku
        MadeIn
        CodiMercancia
        Kg
    End Enum

    Public Shadows Sub Load(oIntrastat As DTOIntrastat)
        _Intrastat = oIntrastat

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = DTOIntrastat.SkusDeclarables(_Intrastat)
        Refresca()
    End Sub

    Public ReadOnly Property Warn As Boolean
        Get
            Dim retval As Boolean = _ControlItems.Any(Function(x) x.Warn)
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oLang = Current.Session.Lang
        Dim oFilteredValues As List(Of DTOProductSku) = _Values.
            Where(Function(p) p.noStk = False).
            OrderBy(Function(x) x.nom.Tradueix(oLang)).OrderBy(Function(y) y.category.nom.Tradueix(oLang)).OrderBy(Function(z) z.category.brand.nom.Tradueix(oLang)).ToList

        _ControlItems = New ControlItems
        Dim pSku As New DTOProductSku
        For Each oSku In oFilteredValues
            If oSku.UnEquals(pSku) Then
                pSku = oSku
                Dim oControlItem As New ControlItem(_Intrastat, pSku)
                _ControlItems.Add(oControlItem)
            End If
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.nom.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOProductSku
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSku = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowProductSku.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.brand)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Brand"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.category)
            .HeaderText = "Categoria"
            .DataPropertyName = "Category"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.MadeIn)
            .HeaderText = "Made In"
            .DataPropertyName = "MadeIn"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _Intrastat.Flujo = DTOIntrastat.Flujos.Introduccion
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CodiMercancia)
            .HeaderText = "Codi Mercancia"
            .DataPropertyName = "CodiMercancia"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .HeaderText = "Kg Net"
            .DataPropertyName = "Kg"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 Kg;-#,###0.00 Kg;#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_ProductSku As New Menu_ProductSku(SelectedItems.First)
            AddHandler oMenu_ProductSku.AfterUpdate, AddressOf onSkuUpdate
            oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub onSkuUpdate(sender As Object, e As MatEventArgs)
        Dim oSku As DTOProductSku = e.Argument
        For i As Integer = 0 To _Values.Count - 1
            If _Values(i).Equals(oSku) Then
                _Values(i) = oSku
            End If
        Next
        MyBase.RefreshRequest(Me, e)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSku = CurrentControlItem.Source
            Dim oFrm As New Frm_ProductSku(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Private Sub MyBase_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Warn Then
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Brand As String
        Property Category As String
        Property Sku As String
        Property MadeIn As String
        Property CodiMercancia As String
        Property Kg As Decimal

        Property Warn As Boolean

        Public Sub New(oIntrastat As DTOIntrastat, value As DTOProductSku)
            MyBase.New()
            _Source = value

            _Brand = DTOProduct.BrandNom(value)
            _Category = DTOProduct.CategoryNom(value)
            _Sku = value.nom.Tradueix(Current.Session.Lang)
            _MadeIn = DTOProductSku.MadeInOrInheritedISO(value)

            Dim oCodiMercancia = DTOProductSku.CodiMercanciaOrInherited(value)
            If oCodiMercancia IsNot Nothing Then
                _CodiMercancia = oCodiMercancia.Id
            End If

            _Kg = DTOIntrastat.SkuKg(value)
            _Warn = DTOIntrastat.Partida.Warn(oIntrastat.Flujo, _Kg, oCodiMercancia, DTOProductSku.MadeInOrInherited(value))

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

