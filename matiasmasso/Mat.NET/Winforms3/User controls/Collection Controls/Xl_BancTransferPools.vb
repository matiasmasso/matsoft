Public Class Xl_BancTransferPools

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBancTransferBeneficiari)
    Private _DefaultValue As DTOBancTransfer
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _Displaymode As DisplayModes
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Emissor
        Ref
        Fch
        Amt
        Beneficiari
        Banc
        Ico
        Concepte
    End Enum

    Private Enum DisplayModes
        SinglePool
        ManyPools
    End Enum

    Public Shadows Sub Load(oPools As List(Of DTOBancTransferPool), Optional oDefaultValue As DTOBancTransfer = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Displaymode = DisplayModes.ManyPools
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = New List(Of DTOBancTransferBeneficiari)
        For Each oPool As DTOBancTransferPool In oPools
            _Values.AddRange(oPool.Beneficiaris)
        Next

        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public Shadows Sub Load(oPool As DTOBancTransferPool, Optional oDefaultValue As DTOBancTransfer = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Displaymode = DisplayModes.SinglePool
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = oPool.Beneficiaris
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOBancTransferBeneficiari) = FilteredValues()
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
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Beneficiari)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOBancTransferBeneficiari)
        Dim retval As List(Of DTOBancTransferBeneficiari)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.Nom.ToLower.Contains(_Filter.ToLower))
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
        With MyBase.Columns(Cols.Emissor)
            .HeaderText = "Banc emissor"
            .DataPropertyName = "Emissor"
            .Visible = _Displaymode = DisplayModes.ManyPools
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ref)
            .HeaderText = "Ref"
            .DataPropertyName = "Ref"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .Visible = _Displaymode = DisplayModes.ManyPools
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Beneficiari)
            .HeaderText = "Beneficiari"
            .DataPropertyName = "Beneficiari"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Banc)
            .HeaderText = "Banc"
            .DataPropertyName = "Bank"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Concepte)
            .HeaderText = "Concepte"
            .DataPropertyName = "Concepte"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
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
            Select Case _Displaymode
                Case DisplayModes.SinglePool
                    Dim oMenu_BancTransferBeneficiari As New Menu_BancTransferBeneficiari(SelectedItems.First)
                    AddHandler oMenu_BancTransferBeneficiari.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_BancTransferBeneficiari.Range)
                    oContextMenu.Items.Add("-")
                Case DisplayModes.ManyPools
                    Dim oMenu_BancTransferPool As New Menu_BancTransferPool(SelectedItems.First.Parent)
                    AddHandler oMenu_BancTransferPool.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_BancTransferPool.Range)
                    oContextMenu.Items.Add("-")
            End Select
        End If
        oContextMenu.Items.Add("Nova transferencia de nómines al personal", Nothing, AddressOf Do_FactoryStaff)
        oContextMenu.Items.Add("Nova transferencia de comisions als representants", Nothing, AddressOf Do_FactoryReps)
        oContextMenu.Items.Add("Nova transferencia lliure", Nothing, AddressOf Do_FactoryAlt)
        oContextMenu.Items.Add("Nou traspas entre els nostres bancs", Nothing, AddressOf Do_Traspas)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_FactoryStaff()
        Dim oMode = Frm_BancTransferFactory.Modes.Staff
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(oMode))
    End Sub
    Private Sub Do_FactoryReps()
        Dim oMode = Frm_BancTransferFactory.Modes.Reps
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(oMode))
    End Sub
    Private Sub Do_FactoryAlt()
        Dim oMode = Frm_BancTransferFactory.Modes.NotSet
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(oMode))
    End Sub
    Private Sub Do_Traspas()
        Dim oMode = Frm_BancTransferFactory.Modes.Traspas
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(oMode))
    End Sub
    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBancTransferBeneficiari = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Select Case _Displaymode
                        Case DisplayModes.SinglePool
                            Dim oFrm As New Frm_BancTransferBeneficiari(oSelectedValue)
                            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Case DisplayModes.ManyPools

                            Dim oFrm As New Frm_BancTransferPool(oSelectedValue.Parent)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                    End Select
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

    Private Sub Xl_BancTransferPools_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Exceptions.Count > 0 Then e.Value = My.Resources.warn
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOBancTransferBeneficiari

        Property Emissor As String
        Property Ref As String
        Property Fch As Date
        Property Amt As Decimal
        Property Beneficiari As String
        Property Bank As String
        Property Concepte As String
        Property Exceptions As List(Of DTOIban.Exceptions)


        Public Sub New(value As DTOBancTransferBeneficiari)
            MyBase.New()
            _Source = value
            With value
                _Emissor = .Parent.BancEmissor.Abr
                _Ref = .Parent.Ref
                _Fch = .Parent.Cca.Fch
                _Amt = .Amt.Val
                _Beneficiari = .Contact.Nom
                If .BankBranch IsNot Nothing Then
                    If .BankBranch.Bank IsNot Nothing Then
                        _Bank = DTOBank.NomComercialORaoSocial(.BankBranch.Bank)
                    End If
                End If
                _Exceptions = New List(Of DTOIban.Exceptions)
                FEB.Iban.ValidateBankBranch(.BankBranch, _Exceptions)
                _Concepte = .Concepte
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

