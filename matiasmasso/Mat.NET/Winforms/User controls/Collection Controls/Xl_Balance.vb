Public Class Xl_Balance

    Inherits DataGridView

    Private _Value As DTOPgcClass
    Private _Lang As DTOLang
    Private _ControlItems As ControlItems

    Private Shadows FORECOLOR As Color
    Private Shadows BACKCOLOR As Color

    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestAnExcel(sender As Object, e As MatEventArgs)
    Public Event RequestAnExtracte(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Nom
        Current
        Previous
    End Enum

    Public Shadows Sub Load(value As DTOPgcClass, oLang As DTOLang)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Value = value
        _Lang = oLang
        Refresca()
    End Sub

    Public Sub Enable()
        MyBase.RowsDefaultCellStyle.ForeColor = FORECOLOR
        MyBase.RowsDefaultCellStyle.BackColor = BACKCOLOR
    End Sub

    Public Sub Disable()
        MyBase.RowsDefaultCellStyle.ForeColor = Color.Gray
        MyBase.RowsDefaultCellStyle.BackColor = Color.LightGray
    End Sub


    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        AddClass(_Value, _Value.Cod)

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub AddClass(oClass As DTOPgcClass, oCod As DTOPgcClass.Cods)
        Dim oControlItem As New ControlItem(oClass, oCod, _Lang)
        _ControlItems.Add(oControlItem)

        For Each oCtaSaldo As DTOBalanceSaldo In oClass.Ctas
            oControlItem = New ControlItem(oCtaSaldo, oCod, _Lang)
            _ControlItems.Add(oControlItem)
        Next

        For Each oChild As DTOPgcClass In oClass.Children
            AddClass(oChild, oCod)
        Next
    End Sub


    Public ReadOnly Property Value As DTOTemplate
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOTemplate = oControlItem.Source
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

        FORECOLOR = MyBase.RowsDefaultCellStyle.ForeColor
        BACKCOLOR = MyBase.RowsDefaultCellStyle.BackColor

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Current)
            .HeaderText = "Actual"
            .DataPropertyName = "Current"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Previous)
            .HeaderText = "Anterior"
            .DataPropertyName = "Previous"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOTemplate)
        Dim retval As New List(Of DTOTemplate)
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
            If TypeOf oControlItem.Source Is DTOPgcClass Then
                Dim oMenu_PgcClass As New Menu_PgcClass(oControlItem.Source)
                AddHandler oMenu_PgcClass.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_PgcClass.Range)
            ElseIf TypeOf oControlItem.Source Is DTOPgcCta Then
                Dim oMenu_PgcCta As New Menu_PgcCta(oControlItem.Source)
                AddHandler oMenu_PgcCta.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_PgcCta.Range)
                oContextMenu.Items.Add("Extracte", Nothing, AddressOf Do_Extracte)
            End If
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        RaiseEvent RequestAnExcel(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Extracte()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oCta As DTOPgcCta = oControlItem.Source
        RaiseEvent RequestAnExtracte(Me, New MatEventArgs(oCta))
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            If TypeOf oCurrentControlItem.Source Is DTOPgcClass Then
                Dim oSelectedValue As DTOPgcClass = CurrentControlItem.Source
                Dim oFrm As New Frm_PgcClass(oSelectedValue)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                Dim oSelectedValue As DTOBalanceSaldo = CurrentControlItem.Source
                'Dim oFrm As New Frm_Template(oSelectedValue.)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                'oFrm.Show()
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property Nom As String
        Property Current As Decimal
        Property Previous As Decimal

        Private Const IndentFactor As Integer = 3

        Public Sub New(Value As DTOPgcClass, oCod As DTOPgcClass.Cods, oLang As DTOLang)
            MyBase.New()
            _Source = Value

            Dim sb As New System.Text.StringBuilder
            sb.Append(New String(" ", IndentFactor * Value.Level))
            sb.Append(Value.Nom.Tradueix(oLang))
            _Nom = sb.ToString

            With Value

                If Not .HideFigures Then
                    Select Case oCod
                        Case DTOPgcClass.Cods.aA_Activo
                            _Current = .CurrentDeb - .CurrentHab
                            _Previous = .PreviousDeb - .PreviousHab
                        Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                            _Current = .CurrentHab - .CurrentDeb
                            _Previous = .PreviousHab - .PreviousDeb
                    End Select
                End If


            End With
        End Sub

        Public Sub New(Value As DTOBalanceSaldo, oCod As DTOPgcClass.Cods, oLang As DTOLang)
            MyBase.New()
            _Source = Value

            Dim sb As New System.Text.StringBuilder
            sb.Append(New String(" ", IndentFactor * 6))
            sb.Append(DTOPgcCta.FullNom(Value, oLang))
            _Nom = sb.ToString

            With Value
                Select Case oCod
                    Case DTOPgcClass.Cods.aA_Activo
                        _Current = .CurrentDeb - .CurrentHab
                        _Previous = .PreviousDeb - .PreviousHab
                    Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                        _Current = .CurrentHab - .CurrentDeb
                        _Previous = .PreviousHab - .PreviousDeb
                End Select

            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
