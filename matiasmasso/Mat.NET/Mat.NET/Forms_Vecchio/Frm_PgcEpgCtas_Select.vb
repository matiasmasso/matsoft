

Public Class Frm_PgcEpgCtas_Select
    Private mEpg As PgcEpg
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Check
        Ico
        Id
        Nom
        Epg
        Children
    End Enum

    Private Enum CheckCods
        empty
        checked
        unchecked
        grayed
    End Enum

    Public WriteOnly Property DefaultPlan() As PgcPlan
        Set(ByVal value As PgcPlan)
            mAllowEvents = False
            LoadPlans()
            ComboBoxPlan.SelectedValue = value.Id
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Epg() As PgcEpg
        Set(ByVal value As PgcEpg)
            mAllowEvents = False
            mEpg = value
            LoadPlans()
            LoadCtas()
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Grups() As PgcGrups
        Get
            Dim oGrups As New PgcGrups
            Dim oGrup As PgcGrup = Nothing
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.Rows
                If CType(oRow.Cells(Cols.Check).Value, CheckCods) = CheckCods.checked Then
                    oGrup = New PgcGrup(CurrentPlan, oRow.Cells(Cols.Id).Value)
                    oGrups.Add(oGrup)
                End If
            Next
            Return oGrups
        End Get
    End Property

    Private Sub LoadCtas()
        Dim SQL As String = "SELECT 0 as CHCK, G.Id, G.Esp, E.Epg, COUNT(DISTINCT X.Id) AS IDS " _
        & "FROM PGCGRUP G LEFT OUTER JOIN PGCEPGCTAS E " _
        & "ON G.PGCPLAN=E.PGCPLAN AND G.ID LIKE E.CTA LEFT OUTER JOIN " _
        & "PGCGRUP X ON G.PGCPLAN=X.PGCPLAN AND X.ID LIKE G.ID+'%' " _
        & "WHERE G.PGCPLAN=" & CurrentPlan.Id & " " _
        & "GROUP BY E.EPG, G.ID, G.ESP " _
        & "ORDER BY G.ID"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        For Each oRow As DataRow In oTb.Rows
            If oRow(Cols.Children) > 1 Then
                oRow(Cols.Check) = CheckCods.empty
            ElseIf IsDBNull(oRow(Cols.Epg)) Then
                oRow(Cols.Check) = CheckCods.unchecked
            ElseIf oRow(Cols.Epg) = mEpg.Id Then
                oRow(Cols.Check) = CheckCods.checked
            Else
                oRow(Cols.Check) = CheckCods.grayed
            End If
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Check)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Epg)
                .Visible = False
            End With
            With .Columns(Cols.Children)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        'SetCtaContextMenu()
    End Sub

    Private Function CurrentGrup() As PgcGrup
        Dim oGrup As PgcGrup = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Id).Value) Then
                oGrup = New PgcGrup(CurrentPlan, oRow.Cells(Cols.Id).Value)
            End If
        End If
        Return oGrup
    End Function

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        mEpg.SetGrups(Me.Grups)
        RaiseEvent AfterUpdate(Me.Grups, New System.EventArgs)
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As CheckCods = oRow.Cells(Cols.Check).Value
                Select Case oCod
                    Case CheckCods.empty
                        e.Value = My.Resources.empty
                    Case CheckCods.unchecked
                        e.Value = My.Resources.UnChecked13
                    Case CheckCods.checked
                        e.Value = My.Resources.Checked13
                    Case CheckCods.grayed
                        e.Value = My.Resources.CheckedGrayed13
                End Select
            Case Cols.Nom
                Dim oGrid As DataGridView = sender
                Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
                Dim sId As String = oRow.Cells(Cols.Id).Value
                Try
                    Dim iLevel As Integer = sId.Length
                    Dim sPad As New String(" ", 4 * iLevel)
                    e.Value = sPad & sId & " " & e.Value
                    Select Case iLevel
                        Case 1
                            e.CellStyle.BackColor = Color.LightGray
                            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                        Case 2
                            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                        Case Is >= 3
                    End Select

                Catch ex As Exception

                End Try


        End Select

    End Sub


    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As CheckCods = CType(oRow.Cells(Cols.Check).Value, CheckCods)
                Select Case oCod
                    Case CheckCods.checked
                        oRow.Cells(Cols.Check).Value = CheckCods.unchecked
                        ButtonOk.Enabled = True
                    Case CheckCods.unchecked
                        oRow.Cells(Cols.Check).Value = CheckCods.checked
                        ButtonOk.Enabled = True
                End Select
        End Select
    End Sub


    Private Function CurrentPlan() As PgcPlan
        Dim oPlan As PgcPlan = Nothing
        Dim PlanId As Integer = ComboBoxPlan.SelectedValue
        oPlan = New PgcPlan(PlanId)
        Return oPlan
    End Function

    Private Sub LoadPlans()
        If ComboBoxPlan.Items.Count > 0 Then Exit Sub

        Dim SQL As String = "SELECT ID, NOM FROM PGCPLAN ORDER BY YEARFROM DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With ComboBoxPlan
            .DataSource = oTb
            .DisplayMember = "NOM"
            .ValueMember = "ID"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub ComboBoxPlan_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPlan.SelectedValueChanged
        If mAllowEvents Then
            LoadCtas()
        End If
    End Sub
End Class