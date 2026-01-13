Public Class Xl_ContactPnds
    Inherits DataGridView

    Private _Pnds As List(Of DTOPnd)
    Private _Impagats As List(Of DTOImpagat)
    Private _DefaultValue As DTOVisaEmisor

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Vto
        Amt
        Cta
        Fra
        Fch
        Obs
    End Enum

    Public Shadows Sub Load(oPnds As List(Of DTOPnd), oImpagats As List(Of DTOImpagat))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Pnds = oPnds
        _Impagats = oImpagats
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOPnd In _Pnds
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        For Each oItem As DTOImpagat In _Impagats
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Obs)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

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

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Vto)
            .HeaderText = "Venciment"
            .DataPropertyName = "Vto"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 65
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
        With MyBase.Columns(Cols.Cta)
            .HeaderText = "Compte"
            .DataPropertyName = "Cta"
            .Width = 120
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Obs)
            .HeaderText = "Forma de pago"
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
            Dim oMenu_Pnd As New Menu_Pnd(SelectedItems.First)
            AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pnd.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_ContactPnds_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Pnd
                        Dim oPnd As DTOPnd = oControlItem.Source
                        If oPnd.Csb IsNot Nothing Then
                            e.Value = My.Resources.candau
                        End If
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Select Case oCurrentControlItem.LinCod
                Case ControlItem.LinCods.Pnd
                    Dim oPnd As DTOPnd = oCurrentControlItem.Source
                    Dim oFrm As New Frm_Contact_Pnd(oPnd)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case ControlItem.LinCods.Impagat
                    Dim oImpagat As DTOImpagat = oCurrentControlItem.Source
                    Dim oFrm As New Frm_Impagat(oImpagat)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOBaseGuid

        Property Vto As Date
        Property Amt As Decimal
        Property Cta As String
        Property Fra As String
        Property Fch As Date
        Property Obs As String
        Property LinCod As LinCods

        Public Enum LinCods
            Pnd
            Impagat
        End Enum

        Shared ReadOnly Property CtaImpagats As DTOPgcCta
            Get
                Static retval As DTOPgcCta
                If retval Is Nothing Then
                    Dim exs As New List(Of Exception)
                    retval = FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
                End If
                Return retval
            End Get
        End Property

        Public Sub New(value As DTOPnd)
            MyBase.New()
            _Source = value
            With value
                _Vto = .Vto
                _Amt = .Amt.Eur
                _Cta = DTOPgcCta.FullNom(.Cta, DTOApp.current.lang)
                _Fra = .FraNum
                _Fch = .Fch
                _Obs = .Fpg
                _LinCod = LinCods.Pnd
            End With
        End Sub

        Public Sub New(value As DTOImpagat)
            MyBase.New()
            _Source = value
            With value
                _Vto = .Csb.Vto
                _Amt = .Csb.Amt.Eur
                _Cta = DTOPgcCta.FullNom(CtaImpagats, DTOApp.current.lang)
                _Fra = .Csb.FraNum
                _Fch = .FchAFP
                _Obs = .Obs
                _LinCod = LinCods.Impagat
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


