Public Class Xl_BancTransferBeneficiarisFactory
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBancTransferBeneficiari)
    Private _DefaultValue As DTOBancTransferBeneficiari
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Cta
        IcoQuadra
        Iban
        IcoIban
        Amt
        Concept
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBancTransferBeneficiari), Optional oDefaultValue As DTOBancTransferBeneficiari = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOBancTransferBeneficiari)
        Get
            Return _Values.Where(Function(x) x.Amt.isPositive()).ToList()
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOBancTransferBeneficiari) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOBancTransferBeneficiari In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBancTransferBeneficiari
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBancTransferBeneficiari = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cta)
            .HeaderText = "Compte"
            .DataPropertyName = "Cta"
            .Width = 120
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.IcoQuadra), DataGridViewImageColumn)
            .DataPropertyName = "IcoQuadra"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Iban)
            .HeaderText = "Iban"
            .DataPropertyName = "Iban"
            .Width = 120
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.IcoIban), DataGridViewImageColumn)
            .DataPropertyName = "IcoIban"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Concept)
            .HeaderText = "Concepte"
            .DataPropertyName = "Concept"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 120
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

    Private Function SelectedItems() As List(Of DTOBancTransferBeneficiari)
        Dim retval As New List(Of DTOBancTransferBeneficiari)
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
            Dim oMenu_BancTransferBeneficiari As New Menu_BancTransferBeneficiari(SelectedItems.First)
            AddHandler oMenu_BancTransferBeneficiari.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BancTransferBeneficiari.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("retirar", My.Resources.aspa, AddressOf Do_Remove)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Shadows Sub RefreshRequest(sender As Object, e As MatEventArgs)
        Refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        Dim oBeneficiari As DTOBancTransferBeneficiari = oControlitem.Source
        _Values.Remove(oBeneficiari)
        Refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBancTransferBeneficiari = CurrentControlItem.Source
            Dim oFrm As New Frm_BancTransferBeneficiari(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf onItemChanged
            oFrm.Show()

        End If
    End Sub

    Private Sub onItemChanged(sender As Object, e As MatEventArgs)
        Dim item As DTOBancTransferBeneficiari = e.Argument
        Dim oPreviousVersion As DTOBancTransferBeneficiari = _Values.Find(Function(x) x.Equals(item))
        oPreviousVersion = item
        Refresca()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_BancTransferBeneficiarisFactory_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoIban
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Exceptions.Count > 0 Then
                    e.Value = My.Resources.warning
                End If
            Case Cols.Amt
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Amt <= 0 Then
                    e.CellStyle.ForeColor = Color.Red
                End If

        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOBancTransferBeneficiari

        Property Nom As String
        Property Cta As String
        Property CtaQuadra As Boolean
        Property Iban As String
        Property Amt As Decimal
        Property Concept As String
        Property Exceptions As List(Of DTOIban.Exceptions)

        Public Sub New(value As DTOBancTransferBeneficiari)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Contact.Nom
                _Cta = DTOPgcCta.FullNom(value.Cta, DTOApp.current.lang)
                _Iban = .Account
                _Amt = .Amt.Eur
                _Concept = .Concepte
                _Exceptions = New List(Of DTOIban.Exceptions)
                FEB2.Iban.ValidateBankBranch(.BankBranch, _Exceptions)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

