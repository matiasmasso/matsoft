Public Class Xl_Diari
    Private _Diari As DTODiari
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Shared iMaxCols As Integer = 9

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        concept
        tot
        Tpa
    End Enum

    Public Shadows Sub Load(oDiari As DTODiari, iParentIndex As Integer)
        If oDiari IsNot Nothing Then
            _Diari = oDiari
            _ControlItems = New ControlItems
            If _Diari.Items IsNot Nothing Then
                For Each oItem As DtoDiariItem In Children(oDiari, iParentIndex) ' _Diari.Items
                    'If oItem.Index = iParentIndex Or oItem.ParentIndex = iParentIndex Then
                    Dim oControlItem As New ControlItem(oItem)
                    _ControlItems.Add(oControlItem)
                    'End If
                Next
                LoadGrid()
            End If
        End If
    End Sub

    Private Function Children(oDiari As DTODiari, iParentIndex As Integer) As List(Of DtoDiariItem)
        Dim retval As List(Of DtoDiariItem) = oDiari.Items.Where(Function(x) x.Index = iParentIndex Or x.ParentIndex = iParentIndex).ToList
        Return retval
    End Function

    Public ReadOnly Property Value As DtoDiariItem
        Get
            Dim retval As DtoDiariItem = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim sFormat As String = "#,##0.00;-#,##0.00;#"

        With DataGridView1
            .SuspendLayout()
            .Enabled = False

            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
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
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.concept)
                .HeaderText = Current.Session.Lang.Tradueix("concepto", "concepte", "concept")
                .DataPropertyName = "concept"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.tot)
                .HeaderText = "total"
                .DataPropertyName = "tot"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = sFormat
            End With
            For i As Integer = 0 To _Diari.Brands.Count - 1
                .Columns.Add(New DataGridViewTextBoxColumn)
                With .Columns(Cols.Tpa + i)
                    .HeaderText = _Diari.Brands(i).nom.Tradueix(Current.Session.Lang)
                    .DataPropertyName = "B" & Format(i, "00")
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = sFormat
                End With
                If i = iMaxCols - 1 Then
                    If _Diari.Brands.Count > iMaxCols Then
                        .Columns(Cols.Tpa + i).HeaderText = _Diari.Lang.Tradueix("diversos", "diversos", "divers")
                    End If
                    Exit For
                End If
            Next

            .Enabled = True
            .ResumeLayout()
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

    Private Function SelectedItems() As List(Of DtoDiariItem)
        Dim retval As New List(Of DtoDiariItem)
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
            Select Case oControlItem.Source.Level
                Case DtoDiariItem.Levels.Pdc
                    'Dim oPdc = oControlItem.Source.PurchaseOrder
                    Dim oPdc = New DTOPurchaseOrder(New Guid(oControlItem.Source.Source.ToString()))
                    Dim oMenuPdc As New Menu_Pdc({oPdc}.ToList)
                    oContextMenu.Items.AddRange(oMenuPdc.Range)
            End Select
            oContextMenu.Items.Add(VolumeUpToHere())
            'Dim oMenu_StatItem As New Menu_StatItem(SelectedItems.First)
            'AddHandler oMenu_StatItem.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_StatItem.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function VolumeUpToHere() As String
        Dim retval As String = ""
        Dim oCell As DataGridViewCell = DataGridView1.CurrentCell
        If oCell.ColumnIndex > Cols.concept Then
            Dim DcTotal As Decimal = 0
            If oCell.RowIndex = 0 Then
                DcTotal = oCell.Value
            Else
                For iRow As Integer = oCell.RowIndex To DataGridView1.Rows.Count - 1
                    DcTotal += DataGridView1.Rows(iRow).Cells(oCell.ColumnIndex).Value
                Next
            End If
            retval = String.Format("fins aquí: {0:#,###.00}€", DcTotal)
        End If
        Return retval
    End Function

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem Is Nothing Then
                RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
            Else
                RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Property Source As DtoDiariItem
        Property Parent As Guid
        Property HasChildren As Boolean
        Property Key As Guid

        Property IsExpanded As Boolean
        Property Visible As Boolean

        Property concept As String
        Property tot As Decimal
        Property B00 As Decimal
        Property B01 As Decimal
        Property B02 As Decimal
        Property B03 As Decimal
        Property B04 As Decimal
        Property B05 As Decimal
        Property B06 As Decimal
        Property B07 As Decimal
        Property B08 As Decimal
        Property B09 As Decimal

        Public Sub New(oItem As DtoDiariItem)
            MyBase.New()
            _Source = oItem

            With oItem
                '_Parent = .Parent
                '_Key = .Key
                Select Case .Level
                    Case DtoDiariItem.Levels.Yea
                        _Visible = True
                        _HasChildren = True
                        _IsExpanded = True
                    Case DtoDiariItem.Levels.Mes
                        _Visible = True
                        _HasChildren = True
                        _IsExpanded = False
                    Case DtoDiariItem.Levels.Dia
                        _Visible = False
                        _HasChildren = True
                        _IsExpanded = False
                    Case DtoDiariItem.Levels.Pdc
                        _Visible = False
                        _HasChildren = False
                        _IsExpanded = False
                End Select

                _concept = .Text
                _tot = oItem.Values.Sum(Function(x) x)

                If oItem.Values.Count > 0 Then _B00 = oItem.Values(0)
                If oItem.Values.Count > 1 Then _B01 = oItem.Values(1)
                If oItem.Values.Count > 2 Then _B02 = oItem.Values(2)
                If oItem.Values.Count > 3 Then _B03 = oItem.Values(3)
                If oItem.Values.Count > 4 Then _B04 = oItem.Values(4)
                If oItem.Values.Count > 5 Then _B05 = oItem.Values(5)
                If oItem.Values.Count > 6 Then _B06 = oItem.Values(6)
                If oItem.Values.Count > 7 Then _B07 = oItem.Values(7)
                If oItem.Values.Count > 8 Then _B08 = oItem.Values(8)
                If oItem.Values.Count > 9 Then _B09 = oItem.Values(9)
                If oItem.Values.Count > iMaxCols Then
                    Dim rest As Decimal = 0
                    For i As Integer = iMaxCols To oItem.Values.Count - 1
                        rest += oItem.Values(i)
                    Next
                    '_B02 += rest
                    Dim oProp As System.Reflection.PropertyInfo = LastCol()
                    Dim oInitialValue As Decimal = oProp.GetValue(Me)
                    Dim oFinalValue As Decimal = oInitialValue + rest
                    oProp.SetValue(Me, oFinalValue)
                End If

            End With
        End Sub

        Public Function LastCol() As System.Reflection.PropertyInfo
            Dim sMaxColName As String = "B" & Format(iMaxCols - 1, "00")
            Dim retval As System.Reflection.PropertyInfo = Nothing
            For Each p As System.Reflection.PropertyInfo In Me.GetType().GetProperties()
                If p.CanRead Then
                    If p.Name = sMaxColName Then
                        retval = p
                        Exit For
                    End If
                    'Console.WriteLine("{0}: {1}", p.Name, p.GetValue(obj, Nothing))
                End If
            Next
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

