Public Class Xl_Albs

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Alb
        Fch
        CliNom
        Eur
        CashIco
        Transm
        Fra
        Usr
        Transport
    End Enum

    Public Shadows Sub Load(value As List(Of Alb))
        _ControlItems = New ControlItems
        For Each oItem As Alb In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As Alb
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Alb = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Alb)
                .HeaderText = "Albará"
                .DataPropertyName = "Alb"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CliNom)
                .HeaderText = "Client"
                .DataPropertyName = "CliNom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .DataPropertyName = "Eur"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.CashIco), DataGridViewImageColumn)
                .HeaderText = ""
                .DataPropertyName = "CashIco"
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Transm)
                .HeaderText = "Transmisió"
                .DataPropertyName = "Transm"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .DataPropertyName = "Fra"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .DataPropertyName = "Usr"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Transport)
                .HeaderText = "Transport"
                .DataPropertyName = "Transport"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of Alb)
        Dim retval As New List(Of Alb)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

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
            Dim oAlbs As List(Of Alb) = SelectedItems()

            Dim oDeliveries As New List(Of DTODelivery)
            For Each oAlb As Alb In oAlbs
                Dim oDelivery As New DTODelivery(oAlb.Guid)
                oDelivery.Id = oAlb.Id
                oDeliveries.Add(oDelivery)
            Next

            Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
            'AddHandler oMenu_Delivery.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Delivery.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.CashIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.CashCod
                    Case DTO.DTOCustomer.CashCodes.credit
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        e.Value = My.Resources.DollarBlue
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                        e.Value = My.Resources.DollarOrange2
                    Case DTO.DTOCustomer.CashCodes.Visa
                        e.Value = My.Resources.visa1
                End Select

        End Select

    End Sub

    Protected Class ControlItem
        Public Property Source As Alb


        Public Property Alb As Integer
        Public Property Fch As Date
        Public Property CliNom As String
        Public Property Eur As Decimal
        Public Property CashCod As DTOCustomer.CashCodes
        Public Property Transm As Integer
        Public Property Fra As Integer
        Public Property Usr As String
        Public Property Transport As String


        Public Sub New(oAlb As Alb)
            MyBase.New()
            _Source = oAlb
            With oAlb
                _Alb = .Id
                _Fch = .Fch
                _CliNom = .Client.Clx
                _Eur = .Total.Eur
                _CashCod = .CashCod
                If .Transmisio IsNot Nothing Then
                    _Transm = .Transmisio.Id
                End If
                If .Invoice IsNot Nothing Then
                    _Fra = .Invoice.Num
                End If
                _Usr = BLL.BLLUser.NicknameOrElse(.UsrCreated)
                If .Transportista IsNot Nothing Then
                    _Transport = BLLContact.NomComercialOrDefault(.Transportista)
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

