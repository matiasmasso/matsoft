Public Class Xl_ProveidorVtos

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPnd)
    Private _DefaultValue As DTOPnd
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        concept
        amt
        fra
        fch
        obs
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPnd), Optional oDefaultValue As DTOPnd = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values.OrderBy(Function(x) x.FraNum).OrderBy(Function(y) y.Contact.FullNom).OrderBy(Function(z) z.Vto).ToList
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPnd) = FilteredValues()
        Dim DtVto As Date = Nothing
        Dim oContact As New DTOContact
        _ControlItems = New ControlItems
        For Each oItem As DTOPnd In oFilteredValues.Where(Function(x) x.Cod = DTOPnd.Codis.Creditor)
            If oContact.UnEquals(oItem.Contact) Then
                If Not oContact.IsNew Then
                    Dim oCredits = _Values.Where(Function(x) x.Contact.Equals(oContact) And x.Vto = DtVto And x.Cod = DTOPnd.Codis.Deutor)
                    For Each credit In oCredits
                        Dim oControlCredit As New ControlItem(credit)
                        _ControlItems.Add(oControlCredit)
                    Next
                End If
                oContact = oItem.Contact
            End If

            If oItem.Vto <> DtVto Then
                DtVto = oItem.Vto
                If _ControlItems.Count > 0 Then _ControlItems.Add(New ControlItem())
                _ControlItems.Add(New ControlItem(DtVto))
            End If

            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        For Each oControlItem In _ControlItems.ToList.Where(Function(x) x.linCod = ControlItem.linCods.vto)
            DtVto = oControlItem.Source
            Dim parcial = VtoItems(DtVto).Sum(Function(x) x.amt)
            oControlItem.amt = parcial
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.concept)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function VtoItems(DtVto As Date) As List(Of ControlItem)
        Dim retval = _ControlItems.Where(Function(x) x.linCod = ControlItem.linCods.pnd).Where(Function(y) DirectCast(y.Source, DTOPnd).Vto = DtVto).ToList
        Return retval
    End Function

    Private Function FilteredValues() As List(Of DTOPnd)
        Dim retval As List(Of DTOPnd)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.FullNom.ToLower.Contains(_Filter.ToLower) Or x.FraNum.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPnd
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPnd = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPnd.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.concept)
            .HeaderText = "Proveidor"
            .DataPropertyName = "Concept"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fra)
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.obs)
            .HeaderText = "Observacions"
            .DataPropertyName = "Obs"
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

    Private Function SelectedItems() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
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
            Select Case oControlItem.linCod
                Case ControlItem.linCods.pnd
                    Dim oMenu_Pnd As New Menu_Pnd(SelectedItems.First)
                    AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pnd.Range)
            End Select

        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Select Case oCurrentControlItem.linCod
                Case ControlItem.linCods.pnd
                    Dim oSelectedValue As DTOPnd = CurrentControlItem.Source
                    Select Case _SelectionMode
                        Case DTO.Defaults.SelectionModes.Browse
                            'Dim oFrm As New Frm_Pnd(oSelectedValue)
                            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            'oFrm.Show()
                        Case DTO.Defaults.SelectionModes.Selection
                            RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
                    End Select
            End Select
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.linCod
            Case ControlItem.linCods.blank
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            Case ControlItem.linCods.vto
                oRow.DefaultCellStyle.BackColor = Color.AliceBlue
            Case ControlItem.linCods.pnd
                oRow.DefaultCellStyle.BackColor = Color.White
                If DirectCast(oControlItem.Source, DTOPnd).Cod = DTOPnd.Codis.Deutor Then
                    oRow.DefaultCellStyle.ForeColor = Color.Crimson
                End If
        End Select
    End Sub




    Protected Class ControlItem
        Property Source As Object

        Property linCod As linCods
        Property concept As String
        Property amt As Decimal
        Property fra As String
        Property fch As Nullable(Of Date)
        Property obs As String

        Public Enum linCods
            blank
            vto
            pnd
        End Enum


        Public Sub New()
            MyBase.New()
            _Source = Nothing
            _linCod = linCods.blank
        End Sub

        Public Sub New(value As Date)
            MyBase.New()
            _Source = value
            _linCod = linCods.vto
            _concept = value.ToShortDateString
        End Sub

        Public Sub New(value As DTOPnd)
            MyBase.New()
            _Source = value
            _linCod = linCods.pnd
            With value
                _concept = .Contact.FullNom
                If value.Cod = DTOPnd.Codis.Deutor Then
                    _amt = - .Amt.eur
                Else
                    _amt = .Amt.eur
                End If
                _fra = .FraNum
                _fch = .Fch
                _obs = .Fpg
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Public Function Excel(sTitle As String, oLang As DTOLang) As MatHelper.Excel.Sheet

        Dim retval As New MatHelper.Excel.Sheet(sTitle)
        With retval
            .AddColumn("Deutor/Creditor", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Eur", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Compte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Factura", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Status", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Observacions", MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With

        If _ControlItems.Count > 0 Then

            For Each oControlItem In _ControlItems
                Dim oRow = retval.AddRow()
                Select Case oControlItem.linCod
                    Case ControlItem.linCods.blank
                    Case ControlItem.linCods.vto
                        oRow.AddCell(oControlItem.concept)
                        Dim itemsCount = VtoItems(oControlItem.Source).Count
                        oRow.AddFormula("SUM(R[+1]C[0]:R[+" & itemsCount & "]C[0])")
                    Case ControlItem.linCods.pnd
                        Dim oPnd As DTOPnd = oControlItem.Source
                        With oRow
                            .AddCell(oControlItem.concept)
                            .AddCell(oControlItem.amt)
                            .AddCell(DTOPgcCta.FullNom(oPnd.Cta, oLang))
                            .AddCell(oPnd.FraNum)
                            .AddCell(oPnd.Fch)
                            .AddCell(oPnd.Status.ToString())
                            .AddCell(oPnd.Fpg)
                        End With
                End Select
            Next
        End If

        Return retval
    End Function

End Class

