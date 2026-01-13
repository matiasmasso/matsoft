Public Class Xl_DocFile_Srcs
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Categoria
        Fch
        Concepte
    End Enum

    Public WriteOnly Property DataSource As List(Of DTODocFileSrc)
        Set(value As List(Of DTODocFileSrc))
            _ControlItems = New ControlItems
            For Each oItem As DTODocFileSrc In value
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
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

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Categoria)
                .HeaderText = "Categoria"
                .DataPropertyName = "Categoria"
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
            With .Columns(Cols.Concepte)
                .HeaderText = "Concepte"
                .DataPropertyName = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTODocFileSrc)
        Dim retval As New List(Of DTODocFileSrc)
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
            Select Case oControlItem.Source.SrcCod
                Case DTODocFile.Cods.Assentament
                    Dim oCca As Cca = oControlItem.Specific
                    Dim oMenu_Cca As New Menu_Cca(oCca)
                    oContextMenu.Items.AddRange(oMenu_Cca.Range)
                Case DTODocFile.Cods.Pdc
                    Dim oPdc As Pdc = oControlItem.Specific
                    Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                    oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            End Select
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTODocFileSrc

        Public Property Specific As Object
        Public Property Categoria As String
        Public Property Fch As Date
        Public Property Concepte As String

        Public Sub New(oDocFileSrc As DTODocFileSrc)
            MyBase.New()
            _Source = oDocFileSrc
            Select Case _Source.SrcCod
                Case DTODocFile.Cods.Assentament
                    Dim oCca As New Cca(oDocFileSrc.SrcGuid)
                    _Categoria = "assentament"
                    If oCca.Ccd = DTOCca.CcdEnum.Unknown Then
                        _Categoria = "assentament/" & oCca.Ccd.ToString
                    End If
                    _Fch = oCca.fch
                    _Concepte = oCca.Txt
                    _Specific = oCca
                Case DTODocFile.Cods.Pdc
                    Dim oPdc As New Pdc(oDocFileSrc.SrcGuid)
                    _Categoria = "comanda"
                    _Fch = oPdc.Fch
                    _Concepte = oPdc.FullConcepte
                    _Specific = oPdc
                Case Else
                    _Categoria = _Source.SrcCod.ToString
                    _Fch = _Source.DocFile.Fch
                    _Concepte = "(no implementat)"
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

