Public Class Xl_TpaStps
    Private _Tpa As Tpa
    Private _Stps As Stps
    Private _DefaultStp As Stp
    Private _HeaderVisible As Boolean
    Private _MenuItemHideObsolets As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Property SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event SelectionChanged(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Private Sub Xl_Stps_Load(sender As Object, e As EventArgs) Handles Me.Load
        _MenuItemHideObsolets = New ToolStripMenuItem("Ocultar obsolets", Nothing, AddressOf Do_ShowObsolets)
        _MenuItemHideObsolets.CheckOnClick = True
        _MenuItemHideObsolets.Checked = True
    End Sub

    Public Sub LoadControl(oTpa As Tpa, Optional BlHeaderVisible As Boolean = False, Optional oDefaultStp As Stp = Nothing)
        _Tpa = oTpa
        _HeaderVisible = BlHeaderVisible
        _DefaultStp = oDefaultStp
        LoadGrid()
    End Sub

    Public ReadOnly Property Stp As Stp
        Get
            Dim retval As Stp = CurrentItem()
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        _AllowEvents = False
        _Stps = MaxiSrvr.Stps.AllFromTpa(_Tpa, _MenuItemHideObsolets.Checked)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("Nom", "Nom")
            .DataSource = _Stps
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = _HeaderVisible
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Nom)
                .HeaderText = "Categories"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        If _Stps.Count > 0 Then
            If _DefaultStp Is Nothing Then
                DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(0)
                DataGridView1.Rows(0).Selected = True
                SetContextMenu()
            Else
                For Each oRow As DataGridViewRow In DataGridView1.Rows
                    Dim oStp As Stp = oRow.DataBoundItem
                    If oStp.Equals(_DefaultStp) Then
                        DataGridView1.CurrentCell = oRow.Cells(0)
                        oRow.Selected = True
                        SetContextMenu()
                        Exit For
                    End If
                Next
            End If
        End If

        _AllowEvents = True
    End Sub


    Private Function CurrentItem() As Stp
        Dim retval As Stp = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Function FirstTpa() As Tpa
        Dim retval As Tpa = Nothing
        If _Stps.Count > 0 Then
            retval = _Stps(0).Tpa
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

        'LoadGrid()

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
        Dim oStp As Stp = CurrentItem()

        If oStp IsNot Nothing Then
            Dim oMenu_Stp As New Menu_ProductCategory(oStp)
            AddHandler oMenu_Stp.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Stp.Range)
        End If

        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add(_MenuItemHideObsolets)
        oContextMenu.Items.Add("Afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ShowObsolets(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTpa As Tpa = FirstTpa()
        If oTpa IsNot Nothing Then
            Dim oStp As New Stp(oTpa)
            Dim oFrm As New Frm_Stp(oStp, BLL.Defaults.SelectionModes.Browse)
            AddHandler oFrm.AfterUpdate, AddressOf OnNewItemAdded
            oFrm.Show()
        End If
    End Sub

    Private Sub OnNewItemAdded(sender As Object, e As EventArgs)
        Dim oItem As Stp = sender
        Dim oItems As Stps = DataGridView1.DataSource
        oItems.Add(oItem)
        'RefreshRequest(sender, e)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oStp As Stp = oRow.DataBoundItem
                If oStp.Obsoleto Then
                    e.CellStyle.BackColor = Color.LightGray
                Else
                    e.CellStyle.BackColor = Color.White
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oStp As Stp = CurrentItem()
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_Stp(oStp, BLL.Defaults.SelectionModes.Browse)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                Dim oArgs As New MatEventArgs(oStp)
                RaiseEvent ValueChanged(Me, oArgs)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            Dim oArgs As New MatEventArgs(CurrentItem)
            RaiseEvent SelectionChanged(Me, oArgs)
        End If
    End Sub


End Class
