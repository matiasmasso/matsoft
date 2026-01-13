Public Class Xl_AreaAlbs
    Private _Area As area
    Private _Albs As Albs
    Private _HeaderVisible As Boolean
    Private _AllowEvents As Boolean

    Public Property SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event SelectionChanged(sender As Object, e As EventArgs)
    Public Event ValueChanged(sender As Object, e As EventArgs)

    Private Enum Cols
        alb
        fch
        nom
    End Enum


    Public Sub LoadControl(oArea As area, Optional BlHeaderVisible As Boolean = False)
        _Area = oArea
        _Albs = Albs.FromArea(_Area)
        _HeaderVisible = BlHeaderVisible
        LoadGrid()
    End Sub

    Public ReadOnly Property Alb As Alb
        Get
            Dim retval As Alb = CurrentItem()
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        _AllowEvents = False
        _Albs = MaxiSrvr.Albs.FromArea(_Area)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("Alb", "Alb")
            .Columns.Add("Fch", "Fch")
            .Columns.Add("Nom", "Nom")
            .DataSource = _Albs
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = _HeaderVisible
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.alb)
                .HeaderText = "Albará"
                .DataPropertyName = "Id"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            With .Columns(Cols.fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns(Cols.nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        _AllowEvents = True
    End Sub


    Private Function CurrentItem() As Alb
        Dim retval As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlb As Alb = CurrentItem()

        If oAlb IsNot Nothing Then
            Dim oMenu_Alb As New Menu_Alb(oAlb)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Alb.Range)
        End If

        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oAlb As Alb = CurrentItem()
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_AlbNew2(oAlb)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                RaiseEvent ValueChanged(oAlb, EventArgs.Empty)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            RaiseEvent SelectionChanged(CurrentItem, EventArgs.Empty)
        End If
    End Sub


End Class

