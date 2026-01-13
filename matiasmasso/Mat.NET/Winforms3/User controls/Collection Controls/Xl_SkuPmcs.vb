Public Class Xl_SkuPmcs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of Models.SkuInOutModel.Item)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToUpdatePmcs(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Alb
        Fch
        Nom
        Inp
        Out
        Stk
        Eur
        Dto
        Net
        Inv
        Pmc
        Ico
        Mrg
    End Enum

    Public Shadows Sub Load(values As List(Of Models.SkuInOutModel.Item))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues = _Values

        Dim iStk As Integer
        Dim DcInventari As Decimal
        Dim DcPreuMigDeCost As Decimal

        _ControlItems = New ControlItems
        For Each oItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, iStk, DcInventari, DcPreuMigDeCost)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Value As DTODeliveryItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As New DTODeliveryItem(oControlItem.Source.Guid)
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowDeliveryItem.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Alb)
            .HeaderText = "Albará"
            .DataPropertyName = "Alb"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Procedencia/Destinació"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Inp)
            .HeaderText = "Entrades"
            .DataPropertyName = "Inp"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Out)
            .HeaderText = "Sortides"
            .DataPropertyName = "Out"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stk)
            .HeaderText = "Stock"
            .DataPropertyName = "Stk"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Preu"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Net)
            .HeaderText = "Net"
            .DataPropertyName = "Net"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Inv)
            .HeaderText = "Inventari"
            .DataPropertyName = "Inv"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pmc)
            .HeaderText = "Cost mig"
            .DataPropertyName = "Pmc"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Mrg)
            .HeaderText = "Marge"
            .DataPropertyName = "Mrg"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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

    Private Function SelectedItems() As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(New DTODeliveryItem(oControlItem.Source.Guid))
        Next

        If retval.Count = 0 Then retval.Add(New DTODeliveryItem(CurrentControlItem.Source.Guid))
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
            Dim oMenu_DeliveryItem As New Menu_DeliveryItem(SelectedItems.First)
            AddHandler oMenu_DeliveryItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_DeliveryItem.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("desar els nous preus mitjos de cost", My.Resources.disk, AddressOf Do_SavePmcs)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SavePmcs()
        RaiseEvent RequestToUpdatePmcs(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue = CurrentControlItem.Source

            Dim oDelivery As New DTODelivery(oSelectedValue.DeliveryGuid)
            Dim oCustomer As New DTOCustomer(oSelectedValue.ContactGuid)
            Dim exs As New List(Of Exception)
            If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_SkuPmcs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem = oControlItem.Source
                If oItem.Pmc <> oControlItem.Pmc Then e.Value = My.Resources.warn
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As Models.SkuInOutModel.Item

        Property Alb
        Property Fch
        Property Nom
        Property Inp
        Property Out
        Property Stk
        Property Eur
        Property Dto
        Property Net
        Property Inv
        Property Pmc
        Property Mrg


        Public Sub New(value As Models.SkuInOutModel.Item, ByRef iStk As Integer, ByRef DcInventari As Decimal, ByRef DcPreuMigDeCost As Decimal)
            MyBase.New()
            _Source = value

            Dim iCod As Integer = value.Cod
            Dim BlInput As Boolean = iCod < 50
            Dim iQty As Integer = value.Qty

            Dim DcEur As Decimal = value.Eur
            Dim DcDto As Decimal = value.Dto
            Dim oDto As DTOAmt = DTOAmt.Factory(value.Eur).Percent(DcDto)
            Dim oNet As DTOAmt = DTOAmt.Factory(value.Eur).Substract(oDto)
            Dim DcNet As Decimal = oNet.Eur
            Dim DcMrg As Decimal

            If BlInput Then
                iStk += iQty
                DcInventari = DcInventari + iQty * DcNet
                If iStk > 0 Then
                    DcPreuMigDeCost = Math.Round(DcInventari / iStk, 2, MidpointRounding.AwayFromZero)
                Else
                    DcPreuMigDeCost = DcNet
                End If
            Else
                iStk -= iQty
                DcInventari = DcInventari - iQty * DcPreuMigDeCost
                If DcPreuMigDeCost <> 0 Then
                    DcMrg = 100 * (DcNet - DcPreuMigDeCost) / DcPreuMigDeCost
                End If
            End If

            With value
                _Alb = .DeliveryId
                _Fch = .Fch
                _Nom = .Nom
                _Inp = IIf(BlInput, iQty, 0)
                _Out = IIf(BlInput, 0, iQty)
                _Stk = iStk
                _Eur = DcEur
                _Dto = DcDto
                _Net = DcNet
                _Inv = DcInventari
                _Pmc = DcPreuMigDeCost
                _Mrg = DcMrg
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
