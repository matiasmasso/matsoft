
Imports System.Data.SqlClient

Public Class Xl_Subscripcio
    Private mSubscripcio As DTOSubscription
    Private mDirtyRols As Boolean
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        Esp
        Cat
        Eng
        Rols
        Subscriptors
    End Enum

    Private Enum Cols
        Adr
        Fch
    End Enum

    Public Property Subscripcio As DTOSubscription
        Get
            Return mSubscripcio
        End Get
        Set(ByVal value As DTOSubscription)
            mSubscripcio = value
            If mSubscripcio IsNot Nothing Then
                Refresca()
            End If
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mSubscripcio
            Me.Text = BLL.BLLSubscription.Nom(mSubscripcio, BLL.BLLSession.Current.User.Lang)

            TextBoxNomEsp.Text = .Nom_ESP
            TextBoxNomCat.Text = .Nom_CAT
            TextBoxNomEng.Text = .Nom_ENG
            TextBoxDscEsp.Text = .Dsc_ESP
            TextBoxDscCat.Text = .Dsc_CAT
            TextBoxDscEng.Text = .Dsc_ENG
            LoadRols()
            LoadSubscriptors()
            RadioButtonAll.Checked = Not .Reverse
            RadioButtonNone.Checked = .Reverse
            ButtonOk.Enabled = False
        End With
    End Sub

    Private Sub LoadRols()
        ListBoxRolAssigned.Items.Clear()
        ListBoxRolSource.Items.Clear()

        Dim oSubscripcioRols As List(Of DTORol) = mSubscripcio.Rols()
        For Each oId As DTORol.Ids In [Enum].GetValues(GetType(Rol.Ids))
            Dim BlFound As Boolean = False
            For Each oRol As DTORol In oSubscripcioRols
                If oRol.Id = oId Then
                    BlFound = True
                    Exit For
                End If
            Next

            Dim oItem As New MaxiSrvr.ListItem(CInt(oId), oId.ToString)
            If BlFound Then
                ListBoxRolAssigned.Items.Add(oItem)
            Else
                ListBoxRolSource.Items.Add(oItem)
            End If
        Next
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        ButtonOk.Enabled = False

        With mSubscripcio
            .Nom_ESP = TextBoxNomEsp.Text
            .Nom_CAT = TextBoxNomCat.Text
            .Nom_ENG = TextBoxNomEng.Text
            .Dsc_ESP = TextBoxDscEsp.Text
            .Dsc_CAT = TextBoxDscCat.Text
            .Dsc_ENG = TextBoxDscEng.Text

            If mDirtyRols Then
                .Rols = New Rols
                For Each oItem As maxisrvr.ListItem In ListBoxRolAssigned.Items
                    .Rols.Add(New DTORol(oItem.Value))
                Next
            End If

            .Reverse = RadioButtonNone.Checked

        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLSubscription.Update(mSubscripcio, exs) Then
            RaiseEvent AfterUpdate(mSubscripcio, e)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNomEsp.TextChanged, _
         TextBoxNomCat.TextChanged, _
          TextBoxNomEng.TextChanged, _
           TextBoxDscEsp.TextChanged, _
            TextBoxDscCat.TextChanged, _
             TextBoxDscEng.TextChanged, _
              RadioButtonAll.CheckedChanged, _
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
        Dim oSrcItem As maxisrvr.ListItem = ListBoxRolSource.SelectedItem
        ListBoxRolAssigned.Items.Add(oSrcItem)
        ListBoxRolSource.Items.Remove(oSrcItem)
        mDirtyRols = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub RemoveRol()
        Dim oDestItem As maxisrvr.ListItem = ListBoxRolAssigned.SelectedItem
        ListBoxRolSource.Items.Add(oDestItem)
        ListBoxRolAssigned.Items.Remove(oDestItem)
        mDirtyRols = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub LoadSubscriptors()
        Dim oSubscriptors As Subscriptors = MaxiSrvr.Subscriptors.FromSubscripcio(mSubscripcio)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("adr", "email")
            .Columns.Add("FchCreated", "data")
            .DataSource = oSubscriptors
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Adr)
                .HeaderText = "email"
                .DataPropertyName = "Adr"
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
        SetContextMenu()


        RadioButtonAll.Enabled = (oSubscriptors.Count = 0)
        RadioButtonNone.Enabled = (oSubscriptors.Count = 0)

        mAllowEvents = True
    End Sub


    Private Function CurrentItem() As Subscriptor
        Dim retval As Subscriptor = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Contact_Email(CurrentItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.FirstDisplayedCell.ColumnIndex

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadSubscriptors()

        Select Case DataGridView1.Rows.Count
            Case 0
            Case Is < i
                DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
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

        Dim oSubscriptor As Subscriptor = CurrentItem()
        If oSubscriptor IsNot Nothing Then
            Dim oMenu_Subscriptor As New Menu_Subscriptor(oSubscriptor)
            AddHandler oMenu_Subscriptor.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Subscriptor.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

End Class
