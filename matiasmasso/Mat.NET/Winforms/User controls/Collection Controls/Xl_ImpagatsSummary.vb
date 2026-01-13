Public Class Xl_ImpagatsSummary
    Inherits DataGridView

    Private _Values As List(Of DTOImpagat)
    Private _Mems As List(Of DTOMem)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        LastVto
        Ico
        Client
        Nominal
        Gastos
        PagatACompte
        Pendent
    End Enum

    Public Shadows Sub Load(values As List(Of DTOImpagat), oMems As List(Of DTOMem))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Mems = oMems
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oControlItems As New List(Of ControlItem)
        Dim oContact As New DTOContact
        Dim oControlItem As New ControlItem(oContact, _Mems)

        Dim oContacts = _Values.GroupBy(Function(x) x.Csb.Contact.Guid).Select(Function(y) y.First.Csb.Contact).ToList

        For Each oContact In oContacts
            Dim oImpagats = _Values.Where(Function(x) x.Csb.Contact.Equals(oContact)).ToList
            oControlItem = New ControlItem(oContact, _Mems)
            oControlItems.Add(oControlItem)
            oControlItem.Nominal = oImpagats.Sum(Function(x) x.Csb.Amt.Eur)
            oControlItem.Gastos = oImpagats.Sum(Function(x) x.Gastos.Val)
            oControlItem.PagatACompte = oImpagats.Sum(Function(x) x.PagatACompte.Eur)
            oControlItem.LastVto = oImpagats.Max(Function(x) x.Csb.Vto)
        Next

        _ControlItems = New ControlItems
        For Each oControlItem In oControlItems.OrderByDescending(Function(x) x.Pendent)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If oCell Is Nothing And _ControlItems.Count > 0 Then
            Me.CurrentCell = Me.Rows(0).Cells(Cols.Client)
        Else
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        End If

        'MyBase.Refresh()

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub RefreshFromDetail(sender As Object, e As MatEventArgs)
        Dim oUpdatedItem As DTOImpagat = e.Argument
        Dim oOriginalItem As DTOImpagat = _Values.Find(Function(x) x.Equals(oUpdatedItem))
        If oOriginalItem IsNot Nothing Then
            Dim idx As Integer = _Values.IndexOf(oOriginalItem)
            _Values(idx) = oUpdatedItem
        End If
        Refresca()
    End Sub

    Public ReadOnly Property Value As DTOContact
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOContact = oControlItem.Source
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
        With MyBase.Columns(Cols.LastVto)
            .HeaderText = "Ult.vto"
            .DataPropertyName = "LastVto"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
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
        With MyBase.Columns(Cols.Client)
            .HeaderText = "Lliurat"
            .DataPropertyName = "Client"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nominal)
            .HeaderText = "Nominal"
            .DataPropertyName = "Nominal"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Gastos)
            .HeaderText = "Gastos"
            .DataPropertyName = "Gastos"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PagatACompte)
            .HeaderText = "a compte"
            .DataPropertyName = "PagatACompte"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pendent)
            .HeaderText = "Pendent"
            .DataPropertyName = "Pendent"
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

    Private Function SelectedItems() As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
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
            Dim oMenu_Impagat As New Menu_Contact(SelectedItems.First)
            AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Impagat.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Do_Refresca)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Refresca(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub



    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim oRow As DataGridViewRow = MyBase.CurrentRow
            Dim oContact As DTOContact = CurrentControlItem.Source
            Dim oExercici As DTOExercici = DTOExercici.Current(Current.Session.Emp)
            Dim oCta = Await FEB2.PgcCta.FromCod(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_Extracte(oContact, oCta, oExercici)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            Dim item As ControlItem = CurrentControlItem()
            If item IsNot Nothing Then
                RaiseEvent ValueChanged(Me, New MatEventArgs(item.Source))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                'Select Case DirectCast(oRow.Cells(Cols.IcoCod).Value, Icons)
                'Case Icons.Empty
                'e.Value = My.Resources.empty
                'Case Icons.NoCuadra
                'e.Value = My.Resources.warn
                'Case Icons.NoAsnef
                'e.Value = My.Resources.NoPark
                ' End Select
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        If IsDBNull(oRow.Cells(Cols.LastVto).Value) Then
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim oContact As DTOContact = oControlItem.Source
            Dim oMem As DTOMem = _Mems.Find(Function(x) x.Contact.Equals(oContact))
            Dim BlShowUp As Boolean = False
            If oMem Is Nothing Then
                BlShowUp = True
            ElseIf oMem.Fch < oControlItem.LastVto Then
                BlShowUp = True
            End If

            If BlShowUp Then
                oRow.DefaultCellStyle.BackColor = Color.Yellow
            Else
                oRow.DefaultCellStyle.BackColor = Color.White
            End If
        End If

    End Sub


    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle(
            0, e.RowBounds.Top,
            MyBase.Columns.GetColumnsWidth(
            DataGridViewElementStates.Visible) -
            MyBase.HorizontalScrollingOffset + 1,
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush(
        rowBounds,
        oColor,
        oBgColor,
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOContact

        Property LastVto As Date
        Property Client As String
        Property Nominal As Decimal
        Property Gastos As Decimal
        Property PagatACompte As Decimal

        Public Enum Icons
            Empty
            NoCuadra
            NoAsnef
        End Enum

        ReadOnly Property Pendent As Decimal
            Get
                Dim retval As Decimal = _Nominal + _Gastos - _PagatACompte
                Return retval
            End Get
        End Property


        Public Sub New(value As DTOContact, oMems As List(Of DTOMem))
            MyBase.New()
            _Source = value
            With value
                _Client = value.FullNom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

