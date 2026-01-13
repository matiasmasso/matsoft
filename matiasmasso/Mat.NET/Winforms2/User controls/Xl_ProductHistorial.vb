Public Class Xl_ProductHistorial

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of Models.SkuInOutModel.Item)
    Private _IsBundle As Boolean
    Private _User As DTOUser
    Private _Stk As Integer
    Private _Mode As Modes
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _ExtendedMenuItem As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestForExcel(sender As Object, e As MatEventArgs)

    Public Enum Modes
        All
        Entrades
        Sortides
    End Enum


    Private Enum Cols
        transm
        alb
        Fch
        Cli
        Inp
        Out
        Stk
        Preu
        Dto
        Pmc
        Margin
    End Enum

    Public Shadows Sub Load(user As DTOUser, values As List(Of Models.SkuInOutModel.Item), isBundle As Boolean, mode As Modes)
        _User = user
        _Values = values
        _IsBundle = isBundle
        _Mode = mode
        _Stk = values.Sum(Function(x) IIf(x.Cod < 50, x.Qty, -x.Qty))
        _ExtendedMenuItem = New ToolStripMenuItem("inclou cost mig", Nothing, AddressOf Do_Extended)
        With _ExtendedMenuItem
            .CheckOnClick = True
            .Checked = False
        End With

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Function isBundle() As Boolean
        Return _IsBundle
    End Function

    Private Sub Refresca()
        _AllowEvents = False

        MyBase.Columns(Cols.Stk).Visible = (Filter = "" And _Mode = Modes.All And Not isBundle())
        MyBase.Columns(Cols.Inp).Visible = (_Mode = Modes.All Or _Mode = Modes.Entrades)
        MyBase.Columns(Cols.Out).Visible = (_Mode = Modes.All Or _Mode = Modes.Sortides)

        Dim oFilteredValues As List(Of Models.SkuInOutModel.Item) = FilteredValues()

        _ControlItems = New ControlItems
        Dim oPreviousItem As New ControlItem(Nothing, _Stk)
        For Each oItem In oFilteredValues
            Dim iStk As Integer = oPreviousItem.Stk - oPreviousItem.Inp + oPreviousItem.Out
            Dim oControlItem As New ControlItem(oItem, iStk)
            _ControlItems.Add(oControlItem)
            oPreviousItem = oControlItem
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function FilteredValues() As List(Of Models.SkuInOutModel.Item)
        Dim retval As List(Of Models.SkuInOutModel.Item)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
        End If

        Select Case _Mode
            Case Modes.Entrades
                retval = retval.Where(Function(x) x.Cod < 50).ToList
            Case Modes.Sortides
                retval = retval.Where(Function(x) x.Cod >= 50).ToList
        End Select
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

    Public ReadOnly Property Value As DTODeliveryItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As New DTODeliveryItem(oControlItem.Source.Guid)
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.transm)
            .HeaderText = "Transm"
            .DataPropertyName = "Transm"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.alb)
            .HeaderText = "Albarà"
            .DataPropertyName = "Alb"
            .Width = 40
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
        With MyBase.Columns(Cols.Cli)
            .HeaderText = "Procedencia/destinació"
            .DataPropertyName = "Cli"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Inp)
            .HeaderText = "Entrades"
            .DataPropertyName = "Inp"
            .Width = 55
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Out)
            .HeaderText = "Sortides"
            .DataPropertyName = "Out"
            .Width = 55
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
        With MyBase.Columns(Cols.Preu)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
            .Width = 55
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dte"
            .DataPropertyName = "Dto"
            .Width = 35
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
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
            .Visible = False
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Margin)
            .HeaderText = "Marge"
            .DataPropertyName = "Margin"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
            .Visible = False
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
            Dim oItem As New DTODeliveryItem(oControlItem.Source.Guid)
            oItem.Delivery = New DTODelivery(oControlItem.Source.DeliveryGuid)
            retval.Add(oItem)
        Next

        If retval.Count = 0 Then
            Dim oControlItem = CurrentControlItem()
            Dim oItem As New DTODeliveryItem(oControlItem.Source.Guid)
            oItem.Delivery = New DTODelivery(oControlItem.Source.DeliveryGuid)
            retval.Add(oItem)
        End If
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
        End If

        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts
                oContextMenu.Items.Add(_ExtendedMenuItem)
        End Select
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Extended(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        MyBase.Columns(Cols.Pmc).Visible = oMenuItem.Checked
        MyBase.Columns(Cols.Margin).Visible = oMenuItem.Checked
    End Sub

    Private Sub Do_Excel()
        RaiseEvent RequestForExcel(Me, MatEventArgs.Empty)
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
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        Dim item = oControlitem.Source
        If item.TransmId = 0 Then
            UIHelper.DataGridViewPaintGradientRowBackGround(Me, e, Color.Yellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Models.SkuInOutModel.Item
        Property Transm As Integer
        Property Alb As Integer
        Property Fch As Nullable(Of Date)
        Property Cli As String
        Property Inp As Integer
        Property Out As Integer
        Property Stk As Integer
        Property Preu As Decimal
        Property Dto As Decimal
        Property Pmc As Decimal
        Property Margin As Decimal

        Public Sub New(value As Models.SkuInOutModel.Item, stk As Integer)
            MyBase.New()
            _Source = value

            If value IsNot Nothing Then
                With value
                    If .TransmId > 0 Then
                        _Transm = .TransmId
                    End If
                    _Alb = .DeliveryId
                    _Fch = .Fch
                    _Cli = .Nom

                    Dim PreuVisible As Boolean

                    If .Cod < 50 Then
                        _Inp = .Qty
                        Select Case Current.Session.Rol.id
                            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.taller
                                PreuVisible = True
                        End Select
                    Else
                        _Out = .Qty
                        Dim DcPreuNet = .Eur * (100 - .Dto) / 100
                        If .Pmc > 0 Then
                            _Margin = 100 * DcPreuNet / .Pmc - 100
                        End If
                        PreuVisible = True
                    End If

                    If PreuVisible And .Eur <> 0 Then
                        _Preu = .Eur
                    End If
                    _Dto = .Dto
                    _Pmc = .Pmc
                End With
            End If

            _Stk = stk
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


