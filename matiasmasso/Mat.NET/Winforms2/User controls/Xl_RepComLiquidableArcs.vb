Public Class Xl_RepComLiquidableArcs
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Alb
        Fch
        ArtNom
        Qty
        Preu
        Dto
        Amt
        ComisioPercent
        ComValue
    End Enum


    Public WriteOnly Property LineItmArcs As List(Of DTODeliveryItem)
        Set(value As List(Of DTODeliveryItem))
            _ControlItems = New ControlItems
            If value IsNot Nothing Then

                For Each oItem As DTODeliveryItem In value
                    Dim oControlItem As New ControlItem(oItem)
                    _ControlItems.Add(oControlItem)
                Next
            End If
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            For i As Integer = Cols.Alb To Cols.ComValue
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False

            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Alb)
                .HeaderText = "Albará"
                .DataPropertyName = "Alb"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "Producte"
                .DataPropertyName = "ArtNom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "Unitats"
                .DataPropertyName = "Qty"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0"
            End With
            With .Columns(Cols.Preu)
                .HeaderText = "Preu"
                .DataPropertyName = "Preu"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "Dte"
                .DataPropertyName = "Dto"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .DataPropertyName = "Amt"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With
            With .Columns(Cols.ComisioPercent)
                .HeaderText = "Tipus"
                .DataPropertyName = "ComisioPercent"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0\%;-0\%;#"
            End With
            With .Columns(Cols.ComValue)
                .HeaderText = "Comisió"
                .DataPropertyName = "ComValue"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem)
        Return retval
    End Function

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_RepComLiquidable As New Menu_LineItmArc(CurrentItem.Source)
            'oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        'Select Case oItem.Updated
        '    Case True
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        '    Case Else
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTODeliveryItem
        Public Property Alb As Integer
        Public Property Fch As Date
        Public Property ArtNom As String
        Public Property Qty As Integer
        Public Property Preu As Decimal
        Public Property Dto As Decimal
        Public Property Amt As Decimal
        Public Property ComisioPercent As Decimal
        Public Property ComValue As Decimal

        Public Sub New(oLineItmArc As DTODeliveryItem)
            MyBase.New()
            _Source = oLineItmArc
            With _Source
                _Alb = .Delivery.Id
                _Fch = .Delivery.Fch
                If .PurchaseOrderItem IsNot Nothing Then
                    _ArtNom = .PurchaseOrderItem.Sku.NomLlarg.Tradueix(Current.Session.Lang)
                    _ComisioPercent = .PurchaseOrderItem.RepCom.Com
                End If
                _Qty = .Qty
                _Preu = .Price.Eur
                _Dto = .Dto
                _Amt = oLineItmArc.Import.Eur
                If .RepComLiquidable IsNot Nothing Then
                    _ComValue = .RepComLiquidable.Comisio.Eur
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As ControlItem)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ControlItem
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

    End Class

End Class


