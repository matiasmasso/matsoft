

Public Class Frm_PgcEpg

    Private mPgcEpg As PgcEpg
    Private mGrups As PgcGrups
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
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
            mPgcEpg = value
            LoadPlans()
            Refresca()
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mPgcEpg
            If .Exists Then
                Me.Text = "EPIGRAF #" & .Id
            Else
                Me.Text = "NOU EPIGRAF"
            End If
            Dim sErr As String = ""
            ButtonDel.Enabled = mPgcEpg.AllowDelete(sErr)
            TextBoxCod.Text = [Enum].GetName(GetType(PgcEpg.BalCods), .BalCod)
            TextBoxLevel.Text = .Level.Nom
            TextBoxEsp.Text = .Esp
            TextBoxCat.Text = .Cat
            TextBoxEng.Text = .Eng
            LoadCtas()
            mAllowEvents = True
        End With
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxEsp.TextChanged, _
     TextBoxCat.TextChanged, _
      TextBoxEng.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Graba()
        Me.Close()
    End Sub

    Private Sub Graba()
        With mPgcEpg
            If Not .Exists Then
                .Ord = .Siblings.Count + 1
            End If
            .Esp = TextBoxEsp.Text
            .Cat = TextBoxCat.Text
            .Eng = TextBoxEng.Text
            .Update()
        End With
        RaiseEvent AfterUpdate(mPgcEpg, New System.EventArgs)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest epigraf?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If mPgcEpg.Delete(sErr) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox(sErr, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

   
    
    Private Sub ButtonEditCtas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEditCtas.Click
        If mPgcEpg.Id = 0 Then
            Dim rc As MsgBoxResult = MsgBox("Aquest epigraf no ha estat registrat encara. El grabem?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc <> MsgBoxResult.Ok Then Exit Sub
            Graba()
        End If
        Dim oFrm As New Frm_PgcEpgCtas_Select
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        With oFrm
            .DefaultPlan = CurrentPlan()
            .Epg = mPgcEpg
            .Show()
        End With
    End Sub

    Private Sub RefreshRequestCtas(ByVal sender As Object, ByVal e As System.EventArgs)
        mPgcEpg.SetGrups(sender)
        LoadCtas()
    End Sub

    Private Sub LoadCtas()
        Dim SQL As String = "SELECT  G.Id, G.Esp " _
        & "FROM PGCGRUP AS G INNER JOIN " _
        & "PGCEPGCTAS AS E ON E.PGCPLAN=G.PGCPLAN AND E.Cta LIKE G.Id + '%' " _
        & "WHERE E.Epg =" & mPgcEpg.Id & " AND " _
        & "E.PgcPlan=" & CurrentPlan.Id.ToString & " " _
        & "GROUP BY G.Id, G.Esp " _
        & "ORDER BY G.Id"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
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
            .ReadOnly = True

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Quadre de Comptes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowGrup(CurrentGrup)
    End Sub

    Private Sub ShowGrup(ByVal oGrup As PgcGrup)
        Dim oFrm As New Frm_PgcGrup
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCtas
        With oFrm
            .PgcGrup = oGrup
            .Show()
        End With
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