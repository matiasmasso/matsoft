Public Class Xl_Importacions

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOImportacio)
    Private _DefaultValue As DTOImportacio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        ETD
        ETA
        Amt
        Ico
        Goods
        TrpCost
        Bultos
        Kg
        M3
        IncoTerms
        PaisOrigen
        Prv
    End Enum

    Public Shadows Sub Load(values As List(Of DTOImportacio), Optional oDefaultValue As DTOImportacio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOImportacio) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOImportacio In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOImportacio
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportacio = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowImportacio.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .DataPropertyName = "Id"
            .HeaderText = "Num"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
            .DefaultCellStyle.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ETD)
            .DataPropertyName = "ETD"
            .HeaderText = "ETD"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ETA)
            .DataPropertyName = "ETA"
            .HeaderText = "ETA"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .DataPropertyName = "Amt"
            .HeaderText = "Valor"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
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
        With MyBase.Columns(Cols.Goods)
            .DataPropertyName = "Goods"
            .HeaderText = "Mercancia"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 80
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.TrpCost)
            .DataPropertyName = "TrpCost"
            .HeaderText = "Transport"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Bultos)
            .DataPropertyName = "Bultos"
            .HeaderText = "Bultos"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
            .DefaultCellStyle.Format = "#,##0;-#,##0;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .DataPropertyName = "Kg"
            .HeaderText = "Pes brut"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 Kg;-#,##0.00 Kg;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M3)
            .DataPropertyName = "M3"
            .HeaderText = "Volum"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Format = "#,##0.00 m3;-#,##0.00 m3;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.IncoTerms)
            .DataPropertyName = "IncoTerms"
            .HeaderText = "ICT"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PaisOrigen)
            .DataPropertyName = "PaisOrigen"
            .HeaderText = "Origen"
            .DefaultCellStyle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Prv)
            .DataPropertyName = "Prv"
            .HeaderText = "Proveidor"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Public Function SelectedItems() As List(Of DTOImportacio)
        Dim retval As New List(Of DTOImportacio)
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
            Dim oMenu_Importacio As New Menu_Importacio(SelectedItems)
            AddHandler oMenu_Importacio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Importacio.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ETA
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source.Arrived Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
            Case Cols.PaisOrigen
                'Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                'Dim oControlItem As ControlItem = oRow.DataBoundItem
                'If oControlItem.PaisOrigen = "CN" Then
                ' e.CellStyle.BackColor = Color.LightGray
                'End If
            Case Cols.TrpCost
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim DcDivisa As Decimal = oControlItem.Source.Amt.Eur
                If e.Value <> 0 And DcDivisa <> 0 Then
                    Dim DcTransport As Decimal = e.Value
                    Dim DcRate As Decimal = DcTransport / DcDivisa
                    If DcRate > (5 / 100) Then
                        e.CellStyle.BackColor = Color.LightSalmon
                    End If
                End If
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oImportacio As DTOImportacio = oControlItem.Source
                If oImportacio.Amt.Eur = 0 And oImportacio.Goods = 0 Then
                ElseIf oImportacio.Amt.Eur = oImportacio.Goods Then
                    e.Value = My.Resources.vb
                Else
                    e.Value = My.Resources.warning
                End If
        End Select
    End Sub

    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim exs As New List(Of Exception)
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oImportacio = Await FEB2.Importacio.Find(oControlItem.Source.Guid, exs)
        If exs.Count = 0 Then
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Importacio(oImportacio)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent ItemSelected(Me, New MatEventArgs(oImportacio))
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oImportacio As DTOImportacio = oControlItem.Source

        If oImportacio.Disabled Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub


#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            'PictureBox1.BackColor = Color.LemonChiffon
            '    or none of the above
        ElseIf (e.Data.GetDataPresent(GetType(DTODelivery))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(List(Of DTODelivery)))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragOver
        Dim oPoint = MyBase.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = MyBase.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = MyBase.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            MyBase.CurrentCell = oclickedCell
        End If
    End Sub

    Private Async Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        Dim oDocFiles As New List(Of DTODocFile)
        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        If e.Data.GetDataPresent(GetType(DTODelivery)) Then
            Dim oDelivery As DTODelivery = e.Data.GetData(GetType(DTODelivery))
            Dim oImportacio As DTOImportacio = CurrentControlItem.Source
            If FEB2.Importacio.Load(exs, oImportacio) Then
                Dim oItemToAdd As New DTOImportacioItem(oDelivery.Guid)
                oItemToAdd.Parent = oImportacio
                oItemToAdd.SrcCod = DTOImportacioItem.SourceCodes.alb
                oImportacio.items.Add(oItemToAdd)
                Dim id = Await FEB2.Importacio.Update(oImportacio, exs)
                If exs.Count = 0 Then
                Else
                    oImportacio.Items.Remove(oItemToAdd)
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If

        ElseIf e.Data.GetDataPresent(GetType(List(Of DTODelivery))) Then
            Dim oDeliveries As List(Of DTODelivery) = e.Data.GetData(GetType(DTODelivery))
            Dim oImportacio As DTOImportacio = CurrentControlItem.Source
            If FEB2.Importacio.Load(exs, oImportacio) Then
                Dim oItemsToAdd As New List(Of DTOImportacioItem)
                For Each oDelivery As DTODelivery In oDeliveries
                    Dim oItemToAdd As New DTOImportacioItem(oDelivery.Guid)
                    oItemToAdd.Parent = oImportacio
                    oItemToAdd.SrcCod = DTOImportacioItem.SourceCodes.Alb
                    oItemsToAdd.Add(oItemToAdd)
                Next


                oImportacio.items.AddRange(oItemsToAdd)
                Dim id = Await FEB2.Importacio.Update(oImportacio, exs)
                If exs.Count = 0 Then
                Else
                    UIHelper.WarnError(exs)
                    For Each item As DTOImportacioItem In oItemsToAdd
                        oImportacio.Items.Remove(item)
                    Next
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        ElseIf DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            Dim oImportacio As DTOImportacio = CurrentControlItem.Source
            Dim oDocFile As DTODocFile = oDocFiles.First
            Dim oFrm As New Frm_ImportacioItem(oImportacio, oDocFile)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    MyBase.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = MyBase.CurrentRow
                    Dim oImportacio As DTOImportacio = CurrentControlItem.Source
                    sender.DoDragDrop(oImportacio, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub




#End Region



    Protected Class ControlItem
        Public Property Source As DTOImportacio

        Public Property Yea As Integer
        Public Property Id As Integer
        Public Property ETD As Date
        Public Property ETA As Date
        Public Property Amt As String
        Public Property Goods As Decimal
        Public Property TrpCost As Decimal
        Public Property Bultos As Integer
        Public Property Kg As Decimal
        Public Property M3 As Decimal
        Public Property IncoTerms As String
        Public Property PaisOrigen As String
        Public Property Prv As String

        Public Sub New(oImportacio As DTOImportacio)
            MyBase.New()
            _Source = oImportacio

            Dim oCostMercancia As DTOAmt = DTOImportacio.CostMercancia(oImportacio)
            Dim oCostTransport As DTOAmt = DTOImportacio.CostTransport(oImportacio)
            With oImportacio
                _Yea = .Yea
                _Id = .Id
                _ETD = .FchETD
                _ETA = .FchETA
                If oCostMercancia IsNot Nothing Then
                    _Amt = DTOAmt.CurFormatted(oCostMercancia)
                End If
                _Goods = .Goods

                If oCostTransport IsNot Nothing Then
                    _TrpCost = oCostTransport.Eur
                End If
                _Bultos = .Bultos
                _Kg = .Kg
                _M3 = .m3

                If .incoTerm IsNot Nothing Then
                    _IncoTerms = .incoTerm.Id.ToString
                End If
                If .CountryOrigen IsNot Nothing Then
                    _PaisOrigen = .CountryOrigen.ISO
                End If
                If .Proveidor IsNot Nothing Then
                    _Prv = .Proveidor.FullNom
                End If

            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


