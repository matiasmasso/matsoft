Public Class Xl_PrNumeros
    Private _PrNumeros As PrNumeros
    Private _ControlItems As ControlItems

    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Text
        Thumb
    End Enum


    Public Shadows Sub Load(oPrNumeros As PrNumeros)
        _PrNumeros = oPrNumeros
        LoadYears(oPrNumeros)
        LoadYear(Xl_Exercicis1.Value)
    End Sub

    Public ReadOnly Property Value As PrNumero
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As PrNumero = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub LoadYears(oPrNumeros As PrNumeros)
        Dim oExercicis As New List(Of DTOExercici)
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oExercici As New DTOExercici(oEmp, 0)
        For Each oPrNumero As PrNumero In oPrNumeros
            If oPrNumero.Yea <> oExercici.Year Then
                oExercici = New DTOExercici(oEmp, oPrNumero.Yea)
                oExercicis.Add(oExercici)
            End If
        Next
        Xl_Exercicis1.Load(oExercicis)
    End Sub

    Private Sub LoadYear(value As DTOExercici)
        Dim oPrNumeros As List(Of PrNumero) = _PrNumeros.FindAll(Function(x) x.Yea = value.Year)
        _ControlItems = New ControlItems
        For Each oItem As PrNumero In oPrNumeros
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = 48 ' DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect

            .DefaultCellStyle.SelectionBackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True

            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Text)
                .HeaderText = ""
                .DataPropertyName = "Text"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Thumb), DataGridViewImageColumn)
                .HeaderText = ""
                .DataPropertyName = "Thumb"
                .Width = 48
                .ImageLayout = DataGridViewImageCellLayout.Zoom
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As PrNumeros
        Dim retval As New PrNumeros
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_PrNumero As New Menu_PrNumero(SelectedItems)
            oContextMenu.Items.AddRange(oMenu_PrNumero.Range)
        End If

        Dim oMenuItem As New ToolStripMenuItem("Afegir número", Nothing, AddressOf AddNewNumero)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddNewNumero(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Dim oPrNumero As PrNumero = oControlItem.Source
            Dim oFrm As New Frm_PrNumero(oPrNumero)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_Exercicis1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Exercicis1.onItemSelected
        LoadYear(e.Argument)
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As PrNumero

        Public Property Text As String
        Public Property Thumb As Image

        Public Sub New(oPrNumero As PrNumero)
            MyBase.New()
            _Source = oPrNumero
            With oPrNumero
                _Text = GetText(oPrNumero)
                If Not oPrNumero.DocFile Is Nothing Then
                    _Thumb = oPrNumero.DocFile.Thumbnail
                End If
            End With
        End Sub

        Private Function GetText(oPrNumero As PrNumero) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Num. " & oPrNumero.Numero)
            sb.Append(DTOLang.CAT.MesAbr(oPrNumero.Mes) & " " & oPrNumero.Yea.ToString)
            Dim retval As String = sb.ToString
            Return retval
        End Function

    End Class


    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class

