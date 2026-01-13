Public Class Xl_Amortizations
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOAmortization)
    Private _SumasySaldos As DTOSumasYSaldos

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Nom
        Fch
        Adquisicio
        Amortitzat
        Saldo
    End Enum

    Public Shadows Sub Load(values As List(Of DTOAmortization), oSumasySaldos As DTOSumasYSaldos)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SumasySaldos = oSumasySaldos
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOAmortization) = FilteredValues()
        Dim oCta As New DTOPgcCta
        Dim oControlItem As ControlItem = Nothing
        Dim oControlCta As ControlItem = Nothing
        Dim oControlTotal As New ControlItem
        _ControlItems = New ControlItems
        _ControlItems.Add(oControlTotal)

        For Each oItem As DTOAmortization In oFilteredValues

            Dim oSaldo As DTOAmt = DTOAmortization.Saldo(oItem)
            If oSaldo.IsNotZero Or MyBase.DisplayObsolets Then
                If oItem.Cta.UnEquals(oCta) Then
                    oCta = oItem.Cta
                    oControlCta = New ControlItem(oCta)
                    _ControlItems.Add(oControlCta)
                End If

                Dim oAmortitzat = DTOAmortization.Amortitzat(oItem)
                oControlItem = New ControlItem(oItem)
                With oControlCta
                    .Adquisicio += oItem.Amt.Eur
                    .Amortitzat += oAmortitzat.Eur
                    .Saldo = .Adquisicio - .Amortitzat
                End With
                With oControlTotal
                    .Adquisicio += oItem.Amt.Eur
                    .Amortitzat += oAmortitzat.Eur
                    .Saldo = .Adquisicio - .Amortitzat
                End With
                _ControlItems.Add(oControlItem)
            End If
        Next

        For Each oControlItem In _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Cta)
            Dim DcImmobilitzat As Decimal = 0
            Dim oCodImmobilitzat As DTOPgcPlan.Ctas = DirectCast(oControlItem.Source, DTOPgcCta).Codi
            Dim oSSImmobilitzat As DTOSumasYSaldosItem = _SumasySaldos.items.Find(Function(x) x.Codi = oCodImmobilitzat)
            If oSSImmobilitzat Is Nothing Then
                If oControlItem.Adquisicio <> 0 Then
                    oControlItem.Warning = True
                    oControlItem.Message += "Adquisició sense comtabilitzar"
                End If
            Else
                If oSSImmobilitzat.SdoFinal <> oControlItem.Adquisicio Then
                    oControlItem.Warning = True
                    oControlItem.Message += "Adquirit per " & DTOAmt.Factory(oControlItem.Adquisicio).formatted & " en balanç per " & Format(oSSImmobilitzat.SdoFinal, "#,##0.00")
                End If
            End If

            Dim DcAmortAcumul As Decimal = 0
            Dim oCodAmortAcumul As DTOPgcPlan.Ctas = DTOAmortization.CtaCodAmrtAcumulada(oCodImmobilitzat)
            If oCodAmortAcumul = DTOPgcPlan.Ctas.NotSet Then
                If oControlItem.Amortitzat <> 0 Then
                    oControlItem.Warning = True
                    oControlItem.Message += "El compte " & DirectCast(oControlItem.Source, DTOPgcCta).Id & " figura com no amortitzable pero s'ha amortitzat per " & Format(oControlItem.Amortitzat, "#,##0.00")
                End If
            Else
                Dim oSSAmortAcumul As DTOSumasYSaldosItem = _SumasySaldos.items.Find(Function(x) x.Codi = oCodAmortAcumul)
                If oSSAmortAcumul Is Nothing Then
                    If oControlItem.Amortitzat <> 0 Then
                        oControlItem.Warning = True
                        oControlItem.Message += "Amortització no surt al balanç"
                    End If
                Else
                    If oSSAmortAcumul.SdoFinalCreditor <> oControlItem.Amortitzat Then
                        oControlItem.Warning = True
                        Dim sMessage As String = String.Format("Amortitzat per {0:#,###.##} En balanç per {1:#,###.##} Diferencia {2:#,###.##}", oControlItem.Amortitzat, oSSAmortAcumul.SdoFinalCreditor, oControlItem.Amortitzat - oSSAmortAcumul.SdoFinalCreditor)
                        oControlItem.Message += sMessage
                        oControlTotal.Message += oControlItem.Nom & " " & sMessage
                    End If
                End If
            End If

        Next

        oControlTotal.Warning = _ControlItems.ToList.Exists(Function(x) x.Warning = True)

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOAmortization)
        Dim retval As List(Of DTOAmortization)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Dsc.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOAmortization
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOAmortization = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.DisplayObsolets = False

        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
        With MyBase.Columns(Cols.Adquisicio)
            .HeaderText = "Adquisicio"
            .DataPropertyName = "Adquisicio"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amortitzat)
            .HeaderText = "Amortitzat"
            .DataPropertyName = "Amortitzat"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Saldo)
            .HeaderText = "Saldo"
            .DataPropertyName = "Saldo"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.ClearSelection()
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

    Private Function SelectedItems() As List(Of DTOAmortization)
        Dim retval As New List(Of DTOAmortization)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.LinCod = ControlItem.LinCods.Item Then
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 And CurrentControlItem.LinCod = ControlItem.LinCods.Item Then
            retval.Add(CurrentControlItem.Source)
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
            Dim oSelectedItems As List(Of DTOAmortization) = SelectedItems()
            If oSelectedItems.Count > 0 Then
                Dim oAmortization As DTOAmortization = SelectedItems.First
                If oAmortization IsNot Nothing Then
                    Dim oMenu_Amortization As New Menu_Amortization(SelectedItems.First)
                    AddHandler oMenu_Amortization.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Amortization.Range)
                    oContextMenu.Items.Add("-")
                End If
            End If
        End If
        AddMenuItemObsolets(oContextMenu)
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oCta As DTOPgcCta = Nothing
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            If TypeOf oCurrentControlItem.Source Is DTOPgcCta Then
                oCta = oCurrentControlItem.Source
            Else
                Dim oSelectedValue As DTOAmortization = CurrentControlItem.Source
                oCta = oSelectedValue.Cta
            End If
        End If
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(oCta))
    End Sub


    Private Sub Do_Excel()
        Dim oSheet = FEB2.Amortizations.Excel(SelectedItems)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOAmortization = CurrentControlItem.Source
            Dim oFrm As New Frm_Amortization(oSelectedValue)
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

    Private Sub Xl_Amortizations_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Warning Then
                    e.Value = My.Resources.warning
                End If
            Case Cols.Nom
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oLinCod As ControlItem.LinCods = oControlItem.LinCod
                Select Case oLinCod
                    Case ControlItem.LinCods.Total
                    Case ControlItem.LinCods.Cta
                        oRow.Cells(e.ColumnIndex).Style.Padding = New Padding(20, 0, 0, 0)
                    'oRow.DefaultCellStyle
                    Case Else
                        oRow.Cells(e.ColumnIndex).Style.Padding = New Padding(40, 0, 0, 0)
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oLinCod As ControlItem.LinCods = oControlItem.LinCod
        Select Case oLinCod
            Case ControlItem.LinCods.Total
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
            Case ControlItem.LinCods.Cta
                oRow.DefaultCellStyle.BackColor = Color.Yellow
            Case ControlItem.LinCods.Item
                Dim oAmortitzacio As DTOAmortization = oControlItem.Source
                If DTOAmortization.Saldo(oAmortitzacio).IsZero Then
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                End If
        End Select
    End Sub

    Private Sub Xl_Amortizations_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Message > "" Then
                e.ToolTipText = oControlItem.Message
            End If
        End If
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property LinCod As LinCods
        Property Nom
        Property Fch
        Property Adquisicio
        Property Amortitzat As Decimal
        Property Saldo
        Property Warning As Boolean
        Property Message As String

        Public Enum LinCods
            Total
            Cta
            Item
        End Enum

        Public Sub New()
            MyBase.New
            _LinCod = LinCods.Total
            _Nom = "Total"
        End Sub

        Public Sub New(oCta As DTOPgcCta)
            MyBase.New
            _Source = oCta
            _LinCod = LinCods.Cta
            _Nom = DTOPgcCta.FullNom(oCta, Current.Session.User.Lang)
        End Sub

        Public Sub New(value As DTOAmortization)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Item
                _Nom = .Dsc
                _Fch = .Fch
                _Adquisicio = .Amt.Eur
                _Amortitzat = .Items.Sum(Function(x) x.Amt.Eur)
                _Saldo = _Adquisicio - _Amortitzat
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

