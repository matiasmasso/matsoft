Public Class Xl_Fiscal_Irpf
    Inherits _Xl_ReadOnlyDatagridview

    Private _value As DTOIrpf
    Private _ControlItems As ControlItems
    Private _Exercici As DTOExercici
    Private _AllowEvents As Boolean


    Private Enum Cols
        Txt
        Perceptors
        Base
        Quota
        Tipus
        Saldo
    End Enum


    Public Shadows Sub Load(value As DTOIrpf)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            _Exercici = DTOExercici.Current(Current.Session.Emp)
            SetProperties()
            PropertiesSet = True
        End If

        _value = value
        Refresca()
    End Sub

    Public ReadOnly Property SubCtas() As List(Of DTOBaseQuota)
        Get
            Dim retval As List(Of DTOBaseQuota) = BaseQuotas(ControlItem.LinCods.Contact)
            Return retval
        End Get
    End Property

    Public ReadOnly Property Ctas() As List(Of DTOBaseQuota)
        Get
            Dim retval As List(Of DTOBaseQuota) = BaseQuotas(ControlItem.LinCods.Cta)
            Return retval
        End Get
    End Property

    Private Function BaseQuotas(oLinCod As ControlItem.LinCods) As List(Of DTOBaseQuota)
        Dim retval As New List(Of DTOBaseQuota)
        For Each oControlItem As ControlItem In _ControlItems.ToList.Where(Function(x) x.Lincod = oLinCod)
            Dim item As New DTOBaseQuota(DTOAmt.Factory(oControlItem.Base))
            With item
                .Source = oControlItem.Source
                .Quota = DTOAmt.Factory(oControlItem.Quota)
                .CalcTipus()
            End With
            retval.Add(item)
        Next
        Return retval
    End Function


    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        LoadTotal()
        LoadMod111()
        LoadMod115()

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        Dim iParentsLength As Integer = [Enum].GetValues(GetType(ControlItem.LinCods)).Length
        Dim oParents(iParentsLength) As ControlItem
        For idx As Integer = 0 To MyBase.Rows.Count - 1
            Dim oRow As DataGridViewRow = MyBase.Rows(idx)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oParents(oControlItem.Lincod) = oControlItem
            oRow.Visible = IsVisible(oControlItem, oParents)
        Next

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function IsVisible(oControlItem As ControlItem, oParents As ControlItem()) As Boolean
        Dim retval As Boolean = True
        For i As Integer = oControlItem.Lincod - 1 To 0 Step -1
            If Not oParents(i).IsExpanded Then
                retval = False
            End If
        Next
        Return retval
    End Function

    Private Sub LoadTotal()
        Dim oControlItem As New ControlItem(ControlItem.LinCods.Total, "", "Total previsió", _value.items, _value.saldos)
        _ControlItems.Add(oControlItem)
    End Sub

    Private Sub LoadMod111()
        'Dim items = _value.Items.Where(Function(x) x.Ccb.cta.cod = DTOPgcPlan.Ctas.IrpfProfessionals Or x.Ccb.cta.cod = DTOPgcPlan.Ctas.IrpfTreballadors).ToList
        Dim items = _value.items.Where(Function(x) x.Ccb.cta.cod = DTOPgcPlan.Ctas.IrpfTreballadors Or x.Ccb.cta.cod = DTOPgcPlan.Ctas.IrpfProfessionals).ToList
        Dim oControlItem As New ControlItem(ControlItem.LinCods.Model, "111", "Model 111 Irpf", items, _value.saldos)
        _ControlItems.Add(oControlItem)
        LoadItems(items)
    End Sub

    Private Sub LoadMod115()
        Dim items As List(Of DTOIrpf.Item) = _value.items.Where(Function(x) x.Ccb.cta.cod = DTOPgcPlan.Ctas.IrpfLloguers).ToList
        Dim oControlItem As New ControlItem(ControlItem.LinCods.Model, "115", "Model 115 Lloguers", items, _value.saldos)
        _ControlItems.Add(oControlItem)
        LoadItems(items)
    End Sub



    Private Sub LoadItems(items As List(Of DTOIrpf.Item))
        Dim oControlItem As ControlItem = Nothing
        Dim oCtas = items.GroupBy(Function(x) x.Ccb.cta.Guid).Select(Function(y) y.First.Ccb.cta).ToList
        For Each oCta In oCtas
            Dim oCtaItems = items.Where(Function(x) x.Ccb.cta.Guid.Equals(oCta.Guid)).ToList
            Dim iPerceptors As Integer = oCtaItems.GroupBy(Function(x) x.Ccb.contact.Guid).Count
            Dim oCce As New DTOCce(_Exercici, oCta)
            oControlItem = New ControlItem(ControlItem.LinCods.Cta, oCce, DTOPgcCta.FullNom(oCta, Current.Session.Lang), oCtaItems, _value.saldos)
            _ControlItems.Add(oControlItem)

            Dim oContacts = oCtaItems.GroupBy(Function(x) x.Ccb.contact.Guid).Select(Function(y) y.First.Ccb.contact).ToList
            For Each oContact In oContacts
                Dim oCcd = DTOCcd.Factory(_Exercici, oCta, oContact)
                Dim oContactItems = oCtaItems.Where(Function(x) x.Ccb.contact.Guid.Equals(oContact.Guid)).ToList
                oControlItem = New ControlItem(ControlItem.LinCods.Contact, oCcd, oContact.nom, oContactItems, _value.saldos)
                _ControlItems.Add(oControlItem)

                For Each Item As DTOIrpf.Item In oContactItems
                    oControlItem = New ControlItem(ControlItem.LinCods.Cca, Item.Ccb, Item.Ccb.cca.concept, {Item}, _value.saldos)
                    _ControlItems.Add(oControlItem)
                Next
            Next
        Next
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
        Dim oSelectedItems = SelectedControlItems.ToList.Where(Function(x) x.Lincod = oControlItem.Lincod)
        Dim iSelectedItemsCount = oSelectedItems.Count
        If iSelectedItemsCount > 1 Then
            Dim DcBases As Decimal = oSelectedItems.Sum(Function(x) x.Base)
            Dim DcQuotas As Decimal = oSelectedItems.Sum(Function(x) x.Quota)
            Dim s As String = String.Format("{0} linies total {1:N2} bases i {2:N2} quotes", iSelectedItemsCount, DcBases, DcQuotas)
            oContextMenu.Items.Add(s)
        End If
        If oControlItem IsNot Nothing Then
            Select Case oControlItem.Lincod
                Case ControlItem.LinCods.Cca
                    If SelectedControlItems.Count > 1 Then oContextMenu.Items.Add("-")
                    Dim oCcb As DTOCcb = oControlItem.Source
                    Dim oMenu As New Menu_Cca(oCcb.cca)
                    oContextMenu.Items.AddRange(oMenu.Range)
                    oContextMenu.Items.Add("-")
                Case ControlItem.LinCods.Contact
                    If SelectedControlItems.Count > 1 Then oContextMenu.Items.Add("-")
                    Dim oMenu As New Menu_Ccd(oControlItem.Source)
                    oContextMenu.Items.AddRange(oMenu.Range)
                    oContextMenu.Items.Add("-")
                Case ControlItem.LinCods.Cta
                    If SelectedControlItems.Count > 1 Then oContextMenu.Items.Add("-")
                    Dim oMenu As New Menu_Cce(oControlItem.Source)
                    oContextMenu.Items.AddRange(oMenu.Range)
                    oContextMenu.Items.Add("-")
            End Select
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Perceptors)
            .HeaderText = "Perceptors"
            .DataPropertyName = "Perceptors"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Base)
            .HeaderText = "Base"
            .DataPropertyName = "Base"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Quota)
            .HeaderText = "Quota"
            .DataPropertyName = "Quota"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Tipus)
            .HeaderText = "Tipus"
            .DataPropertyName = "Tipus"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Saldo)
            .HeaderText = "Saldo"
            .DataPropertyName = "Saldo"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub

    Private Sub Xl_Fiscal_Irpf_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim iIndex As Integer = oRow.Index
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        Dim oLinCod As ControlItem.LinCods = oControlitem.Lincod
        Dim IsExpanded As Boolean = Not oControlitem.IsExpanded
        oControlitem.IsExpanded = IsExpanded
        For i As Integer = iIndex + 1 To MyBase.Rows.Count - 1
            oRow = MyBase.Rows(i)
            oControlitem = oRow.DataBoundItem
            Select Case oControlitem.Lincod
                Case <= oLinCod
                    Exit For
                Case = oLinCod + 1
                    oRow.Visible = IsExpanded
                Case Else
                    If Not IsExpanded Then oRow.Visible = False
            End Select
        Next
    End Sub

    Private Sub Xl_Fiscal_Irpf_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.Lincod
            Case ControlItem.LinCods.Cta
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 204)
            Case ControlItem.LinCods.Contact
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(204, 255, 204)
            Case ControlItem.LinCods.Cca
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 255)
        End Select
    End Sub


    Private Sub Do_Excel()
        Dim sTitle As String = String.Format("M+O Irpf {0}", _Exercici.Year)
        Dim oExcelSheet As New MatHelper.Excel.Sheet(sTitle)
        Dim oRow As MatHelper.Excel.Row = oExcelSheet.AddRow()
        With oExcelSheet
            .AddColumn("Concepte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Base", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Quota", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Perceptors", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Base", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Quota", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Base", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Quota", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Previsió", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        For Each oControl As ControlItem In _ControlItems
            oRow = oExcelSheet.AddRow()
            Select Case oControl.Lincod
                Case ControlItem.LinCods.Total
                    oRow.AddCell("Total previsió")
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell(oControl.Quota)
                Case ControlItem.LinCods.Model
                    oRow = oExcelSheet.AddRow()
                    oRow.AddCell(oControl.Txt)
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell(oControl.Base)
                    oRow.AddCell(oControl.Quota)
                Case ControlItem.LinCods.Cta
                    oRow.AddCell(oControl.Txt)
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell(oControl.Perceptors)
                    oRow.AddCell(oControl.Base)
                    oRow.AddCell(oControl.Quota)
                Case ControlItem.LinCods.Contact
                    oRow.AddCell(oControl.Txt)
                Case ControlItem.LinCods.Cca
                    oRow.AddCell(oControl.Txt)
                    oRow.AddCell(oControl.Base)
                    oRow.AddCell(oControl.Quota)
            End Select
        Next
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oExcelSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Fiscal_Irpf_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As Object
        Property Items As IEnumerable(Of DTOIrpf.Item)
        Property Txt As String
        Property Perceptors As Integer
        Property Base As Decimal
        Property Quota As Decimal
        Property Tipus As Decimal
        Property Saldo As Decimal
        Property Lincod As LinCods

        Property IsExpanded As Boolean

        Public Enum LinCods
            Total
            Model
            Cta
            Contact
            Cca
        End Enum

        Public Sub New(oLinCod As LinCods, oSource As Object, sTxt As String, items As IEnumerable(Of DTOIrpf.Item), oSaldos As List(Of DTOPgcSaldo))
            _Lincod = oLinCod
            _Source = oSource
            _Items = items
            _Txt = paddedTxt(oLinCod, sTxt)
            _Base = items.Sum(Function(x) x.Base.Eur)
            _Quota = items.Sum(Function(x) DTOCcb.Credit(x.Ccb).Eur)
            If _Base <> 0 Then _Tipus = 100 * _Quota / _Base

            _Perceptors = items.GroupBy(Function(x) x.Ccb.contact.Guid).Count
            Select Case oLinCod
                Case LinCods.Total, LinCods.Model
                    _IsExpanded = True
                Case LinCods.Cta
                    _IsExpanded = True
                    Dim oCce As DTOCce = oSource
                    Dim oSaldo = oSaldos.FirstOrDefault(Function(x) x.Epg.Guid.Equals(oCce.Cta.Guid) And x.Contact Is Nothing)
                    If oSaldo IsNot Nothing Then
                        _Saldo = oSaldo.SdoCreditor.eur
                    End If
                Case LinCods.Contact
                    Dim oCcd As DTOCcd = oSource
                    Dim oSaldo = oSaldos.FirstOrDefault(Function(x) x.Epg.Guid.Equals(oCcd.Cta.Guid) And x.Contact IsNot Nothing AndAlso x.Contact.Equals(oCcd.Contact))
                    If oSaldo IsNot Nothing Then
                        _Saldo = oSaldo.SdoCreditor.eur
                    End If
            End Select
        End Sub


        Private Function paddedTxt(oLinCod As LinCods, sTxt As String)
            Dim sb As New Text.StringBuilder
            Dim padding As New String(" ", 4)
            For i As Integer = 0 To oLinCod
                sb.Append(padding)
            Next
            sb.Append(sTxt)
            Dim retval As String = sb.ToString
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class
End Class
