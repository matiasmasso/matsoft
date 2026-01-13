Public Class Xl_Gir_SelBancs2
    Private _ControlItems As ControlItems
    Private _Csas As List(Of DTOCsa)
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Checked
        Banc
        Tot
        Min
        Max
        Clasificacio
        Despeses
        Tae
    End Enum

    Public Shadows Sub Load(oCsas As List(Of DTOCsa))
        _Csas = oCsas
        refresca()
    End Sub

    Public Sub refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        For Each oCsa In _Csas
            Dim oItem As New ControlItem(oCsa)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
        'If DataGridView1.DataSource Is Nothing Then
        'Else
        'DataGridView1.Refresh()
        'End If
        _AllowEvents = True
    End Sub


    Public Function Csas() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oItem As ControlItem In _ControlItems
            Dim oCsa As DTOCsa = oItem.Source
            oCsa.NominalMaxim = New DTOAmt(oItem.Max)
            oCsa.NominalMinim = New DTOAmt(oItem.Min)
            oCsa.Enabled = oItem.Checked
            retval.Add(oItem.Source)
        Next
        Return retval
    End Function

    Public Function NonEmptyCsas() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                Dim oCsa As DTOCsa = oItem.Source
                If oCsa.Items.Count > 0 Then
                    oCsa.NominalMaxim = New DTOAmt(oItem.Max)
                    oCsa.NominalMinim = New DTOAmt(oItem.Min)
                    oCsa.Enabled = True
                    retval.Add(oItem.Source)
                End If
            End If
        Next
        Return retval
    End Function

    Public Function Despeses() As DTOAmt
        Dim Eur As Decimal
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                Eur += oItem.Despeses
            End If
        Next
        Dim retval As New DTOAmt(Eur)
        Return retval
    End Function

    Private Function CheckedItems() As List(Of ControlItem)
        Dim retval As New List(Of ControlItem)
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                retval.Add(oItem)
            End If
        Next
        Return retval
    End Function

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            '.ReadOnly = True

            .Columns.Add(New DataGridViewCheckBoxColumn)
            With CType(.Columns(Cols.Checked), DataGridViewCheckBoxColumn)
                .DataPropertyName = "Checked"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                '.DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Banc)
                .HeaderText = "Entitat"
                .DataPropertyName = "Banc"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Tot)
                .HeaderText = "Girat"
                .DataPropertyName = "Tot"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Min)
                .HeaderText = "Minim"
                .DataPropertyName = "Min"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Max)
                .HeaderText = "Maxim"
                .DataPropertyName = "Max"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Clasificacio)
                .HeaderText = "Classificació"
                .DataPropertyName = "Classificacio"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Despeses)
                .HeaderText = "Despeses"
                .DataPropertyName = "Despeses"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Tae)
                .HeaderText = "Tae"
                .DataPropertyName = "Tae"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 %;-#,###0.00 %;#"
            End With


        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Select Case e.ColumnIndex
            Case Cols.Max, Cols.Min
                'RefreshRequest()
        End Select
    End Sub



    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Checked, Cols.Max, Cols.Min
                If _AllowEvents Then
                    RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Checked
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
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

    Private Function SelectedItems() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
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
            'Dim oMenu_Csb As New Menu_Csb(SelectedItems.First)
            'AddHandler oMenu_Csb.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Csb.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
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
        Property Source As DTOCsa

        Property Checked As Boolean
        Property Banc As String
        Property Tot As Decimal
        Property Min As Decimal
        Property Max As Decimal
        Property Classificacio As Decimal
        Property Despeses As Decimal
        Property Tae As Decimal

        Public Sub New(oCsa As DTOCsa)
            MyBase.New()
            _Source = oCsa
            With oCsa
                _Banc = .Banc.Abr
                _Tot = BLL.BLLCsbs.TotalNominal(.Items).Eur

                If .Classificacio IsNot Nothing Then
                    _Classificacio = .Classificacio.Eur
                End If

                If .NominalMaxim IsNot Nothing Then
                    _Max = .NominalMaxim.Eur
                End If

                If .NominalMinim IsNot Nothing Then
                    _Min = .NominalMinim.Eur
                End If

                'If .NominalMinim IsNot Nothing Then
                ' _Min = .NominalMinim.Eur
                ' End If
                ' If .NominalMaxim IsNot Nothing Then
                ' _Max = .NominalMaxim.Eur
                ' End If
                _Despeses = BLL.BLLCsa.TotalDespeses(oCsa).Eur
                _Tae = BLL.BLLCsa.TAE(oCsa)
                _Checked = True
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

