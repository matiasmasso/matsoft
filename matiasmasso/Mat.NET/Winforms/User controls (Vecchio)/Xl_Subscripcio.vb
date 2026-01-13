
Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class Xl_Subscripcio
    Private _Subscription As DTOSubscription
    Private mDirtyRols As Boolean
    Private mAllowEvents As Boolean
    Private _BindingLeft As BindingSource
    Private _BindingRight As BindingSource

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        Nom
        Dsc
        Rols
        Subscriptors
    End Enum

    Private Enum Cols
        Adr
        Fch
    End Enum

    Public Shadows Async Function Load(value As DTOSubscription) As Task
        _Subscription = value
        If _Subscription IsNot Nothing Then
            Await Refresca()
        End If
        mAllowEvents = True
    End Function

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        If FEB2.Subscription.Load(exs, _Subscription) Then
            With _Subscription
                Me.Text = _Subscription.Nom.Tradueix(Current.Session.User.lang)
                Xl_LangsTextNom.Load(.Nom)
                Xl_LangsTextDsc.Load(.Dsc)
                Await LoadRols()
                Await LoadSubscriptors()
                RadioButtonAll.Checked = Not .Reverse
                RadioButtonNone.Checked = .Reverse
                ButtonOk.Enabled = False
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadRols() As Task
        Dim oSubscripcioRols As List(Of DTORol) = _Subscription.Rols()
        Dim exs As New List(Of Exception)
        Dim oAllRols = Await FEB2.Rols.All(exs)
        If exs.Count = 0 Then
            Dim oLeftRols As New List(Of DTORol)
            Dim oRightRols As New List(Of DTORol)
            For Each oRol In oAllRols
                Dim oRolId = oRol.id
                If oSubscripcioRols.Any(Function(x) x.id = oRolId) Then
                    oRightRols.Add(oRol)
                Else
                    oLeftRols.Add(oRol)
                End If
            Next

            _BindingLeft = New BindingSource
            _BindingLeft.DataSource = oLeftRols
            With ListBoxRolSource
                .DisplayMember = "NomEsp"
                .ValueMember = "Id"
                .DataSource = _BindingLeft
            End With

            _BindingRight = New BindingSource
            _BindingRight.DataSource = oRightRols
            With ListBoxRolAssigned
                .DisplayMember = "NomEsp"
                .ValueMember = "Id"
                .DataSource = _BindingRight
            End With
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        With _Subscription
            .Nom = Xl_LangsTextNom.Value
            .Dsc = Xl_LangsTextDsc.Value

            If mDirtyRols Then
                .Rols = _BindingRight.List
            End If

            .Reverse = RadioButtonNone.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Subscription.Update(exs, _Subscription) Then
            UIHelper.ToggleProggressBar(Panel1, False)
            RaiseEvent AfterUpdate(_Subscription, e)
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonAll.CheckedChanged,
        RadioButtonNone.CheckedChanged,
        Xl_LangsTextNom.AfterUpdate,
        Xl_LangsTextDsc.AfterUpdate,
        RadioButtonAll.CheckedChanged,
        RadioButtonNone.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonAddRol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRol.Click
        AddRol()
    End Sub

    Private Sub ButtonRemoveRol_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemoveRol.Click
        RemoveRol()
    End Sub

    Private Sub ListBoxRolSource_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxRolSource.DoubleClick
        AddRol()
    End Sub

    Private Sub ListBoxRolAssigned_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxRolAssigned.DoubleClick
        RemoveRol()
    End Sub

    Private Sub AddRol()
        Dim oSrcItem As DTORol = ListBoxRolSource.SelectedItem

        _BindingLeft.Remove(oSrcItem)
        _BindingRight.Add(oSrcItem)

        mDirtyRols = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub RemoveRol()
        Dim oDestItem As ListItem = ListBoxRolAssigned.SelectedItem
        ListBoxRolSource.Items.Add(oDestItem)
        ListBoxRolAssigned.Items.Remove(oDestItem)
        mDirtyRols = True
        ButtonOk.Enabled = True
    End Sub

    Private Async Function LoadSubscriptors() As Task
        Dim exs As New List(Of Exception)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("adr", "email")
            .Columns.Add("FchCreated", "data")
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Adr)
                .HeaderText = "email"
                .DataPropertyName = "EmailAddress"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .DataPropertyName = "FchCreated"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With

        Dim oSubscriptors = Await FEB2.Subscriptors.All(exs, _Subscription)
        If exs.Count = 0 Then
            DataGridView1.DataSource = oSubscriptors
            SetContextMenu()
            RadioButtonAll.Enabled = (oSubscriptors.Count = 0)
            RadioButtonNone.Enabled = (oSubscriptors.Count = 0)
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Function


    Private Function CurrentItem() As DTOSubscriptor
        Dim retval As DTOSubscriptor = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Function CurrentItems() As List(Of DTOSubscriptor)
        Dim retval As New List(Of DTOSubscriptor)
        If DataGridView1.SelectedRows.Count = 0 Then
            If CurrentItem() IsNot Nothing Then
                retval.Add(CurrentItem)
            End If
        Else
            For Each oRow In DataGridView1.SelectedRows
                retval.Add(oRow.DataBoundItem)
            Next
        End If
        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Contact_Email(CurrentItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.FirstDisplayedCell.ColumnIndex

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        Await LoadSubscriptors()

        Select Case DataGridView1.Rows.Count
            Case 0
            Case Is < i
                If DataGridView1.Rows.Count > iFirstRow Then
                    DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
                    DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
                End If
            Case Else
                DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oSubscriptors As List(Of DTOSubscriptor) = CurrentItems()

        If oSubscriptors.Count > 0 Then
            Dim oMenu_Subscriptor As New Menu_Subscriptor(oSubscriptors)
            AddHandler oMenu_Subscriptor.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Subscriptor.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim oSubscriptors = Await FEB2.Subscriptors.All(exs, _Subscription)
        If exs.Count = 0 Then
            Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
            For Each oSubscriptor In oSubscriptors
                Dim oRow = oSheet.addRow
                oRow.addCell(oSubscriptor.emailAddress)
            Next

            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class
