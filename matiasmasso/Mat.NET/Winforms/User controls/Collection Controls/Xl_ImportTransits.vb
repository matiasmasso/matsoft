Public Class Xl_ImportTransits
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOImportTransit)
    Private _DefaultValue As DTOImportTransit
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
        Eur
        Fras
        Albs
    End Enum

    Public Shadows Sub Load(values As List(Of DTOImportTransit), Optional oDefaultValue As DTOImportTransit = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oFilteredValues As List(Of DTOImportTransit) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOImportTransit In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, oLang)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOImportTransit
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportTransit = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowImportTransit.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Numero"
            .DataPropertyName = "Id"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fras)
            .HeaderText = "Factures"
            .DataPropertyName = "Fras"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Albs)
            .HeaderText = "Albarans"
            .DataPropertyName = "Albs"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
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

    Private Function SelectedItems() As List(Of DTOImportTransit)
        Dim retval As New List(Of DTOImportTransit)
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
            Dim oMenu_ImportTransit As New Menu_Importacio(SelectedItems.First)
            AddHandler oMenu_ImportTransit.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ImportTransit.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("registrar assentament", Nothing, AddressOf Do_Cca)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_Cca()
        Dim exs As New List(Of Exception)
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oTransit As DTOImportTransit = oControlItem.Source
        Dim DtFch As Date = oTransit.YearMonthFras.LastFch
        Dim oUser As DTOUser = Current.Session.User
        Dim oCcas As New List(Of DTOCca)

        Dim oCca As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.Transit)
        With oCca
            .Ref = oTransit
            .Concept = String.Format("Remesa {0} {1}", oTransit.Id, oTransit.Proveidor.FullNom)
        End With

        Dim oCtas = Await FEB2.PgcCtas.All(exs)
        Dim oCtaMercanciesTransit = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.mercanciesTransit)
        Dim oCtaInventari = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Inventari)

        oCca.AddDebit(oTransit.Amt, oCtaMercanciesTransit)
        oCca.AddSaldo(oCtaInventari)
        oCcas.Add(oCca)

        DtFch = oTransit.YearMonthAlbs.FirstFch
        Dim oCca2 As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.Transit)
        With oCca2
            .Ref = oTransit
            .Concept = String.Format("Remesa {0} {1}", oTransit.Id, oTransit.Proveidor.FullNom)
        End With

        oCca2.AddCredit(oTransit.Amt, oCtaMercanciesTransit)
        oCca2.AddSaldo(oCtaInventari)
        oCcas.Add(oCca2)

        If Await FEB2.Ccas.Update(exs, oCcas) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOImportTransit = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Importacio(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOImportTransit

        Property Id As Integer
        Property Nom As String
        Property Eur As Decimal
        Property Fras As String
        Property Albs As String


        Public Sub New(value As DTOImportTransit, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                _Id = .Id
                _Nom = .Proveidor.FullNom
                _Eur = .Amt.Eur
                _Fras = DTOYearMonth.Formatted(.YearMonthFras, oLang)
                _Albs = DTOYearMonth.Formatted(.YearMonthAlbs, oLang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

