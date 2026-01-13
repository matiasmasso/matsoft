Public Class Xl_RepLiqNew_RepsSplit
    'Private _RepComsLiquidables As RepComsLiquidables

    Private _ControlItems As ControlItems
    Private _CancelRequest As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Check
        RepAbr
        FreeFra
        FreeFch
        Base
        Comisio
        FreeClientObs
    End Enum

    Public ReadOnly Property Reps As List(Of DTORep)
        Get
            Dim retval As New List(Of DTORep)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public WriteOnly Property NewRepLiqControlItems As NewRepLiqControlItems
        Set(value As NewRepLiqControlItems)
            _ControlItems = New ControlItems

            Dim oLastRep As DTORep = Nothing
            Dim oLastControlItem As ControlItem = Nothing
            For Each oItem As NewRepLiqControlItem In value
                If oItem.Checked Then
                    If Not oItem.Source.Rep.Equals(oLastRep) Then
                        oLastRep = oItem.Source.Rep
                        oLastControlItem = GetControlItemByRep(oLastRep)
                    End If
                    oLastControlItem.Base += oItem.Base
                    oLastControlItem.Comisio += oItem.Comisio
                End If
            Next
            LoadGrid()
        End Set
    End Property

    Private Function GetControlItemByRep(oRep As DTORep) As ControlItem
        Dim retval As ControlItem = Nothing
        For Each oControlItem As ControlItem In _ControlItems
            If oRep.Equals(oControlItem.Source) Then
                retval = oControlItem
                Exit For
            End If
        Next

        If retval Is Nothing Then
            retval = New ControlItem(oRep, DTOAmt.Empty, DTOAmt.factory)
            _ControlItems.Add(retval)
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        With DataGridView1
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewCheckBoxColumn)
            For i As Integer = Cols.RepAbr To Cols.FreeClientObs
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            .DataSource = _ControlItems
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Check)
                .HeaderText = ""
                .DataPropertyName = "Checked"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.RepAbr)
                .HeaderText = "Rep"
                .DataPropertyName = "RepAbr"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.FreeFra)
                .HeaderText = ""
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.FreeFch)
                .HeaderText = ""
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Base)
                .ReadOnly = True
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Comisio)
                .ReadOnly = True
                .HeaderText = "Comisio"
                .DataPropertyName = "Comisio"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.FreeClientObs)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oControlItem.Source)
            Dim oMenu_Rep As New Menu_Rep(oControlItem.Source, oContactMenu)
            oContextMenu.Items.AddRange(oMenu_Rep.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Function

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Await SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = Cols.Check Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Checked = Not oControlItem.Checked
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As DTORep

        Public Property Checked As Boolean
        Public Property RepAbr As String
        Public Property Base As Decimal
        Public Property Comisio As Decimal

        Public Sub New(oRep As DTORep, oBase As DTOAmt, oComisio As DTOAmt)
            MyBase.New()
            _Source = oRep
            _Checked = True
            _RepAbr = oRep.NickName
            _Base = oBase.Eur
            _Comisio = oComisio.Eur
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As ControlItem)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ControlItem
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

    End Class


End Class
