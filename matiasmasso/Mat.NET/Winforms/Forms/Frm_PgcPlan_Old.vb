

Public Class Frm_PgcPlan_Old

    Private mPgcPlan As PgcPlan
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property PgcPlan() As PgcPlan
        Set(ByVal value As PgcPlan)
            mPgcPlan = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        LoadLastPlans()
        With mPgcPlan
            If .Exists Then
                Me.Text = "PLA DE COMPTABILITAT"
                ButtonDel.Enabled = True
                TextBoxId.Text = .Id
            Else
                Me.Text = "NOU PLA DE COMPTABILITAT"
                ButtonDel.Enabled = False
            End If
            TextBoxNom.Text = .Nom
            ComboBoxLastPgcPlan.SelectedValue = .LastPlan.Id
            TextBoxNextPgc.Text = .NextPlan.Nom
            TextBoxYearFrom.Text = IIf(.YearFrom > 0, .YearFrom, "")
            TextBoxYearTo.Text = IIf(.YearTo > 0, .YearTo, "")
        End With
    End Sub

    Private Sub LoadLastPlans()
        Dim SQL As String = "SELECT ID,NOM FROM PGCPLAN ORDER BY YEARFROM DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow("ID") = 0
        oRow("NOM") = "(cap)"
        oTb.Rows.InsertAt(oRow, 0)
        With ComboBoxLastPgcPlan
            .DataSource = oDs.Tables(0)
            .ValueMember = "ID"
            .DisplayMember = "NOM"
            If oDs.Tables(0).Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With

    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged,
     ComboBoxLastPgcPlan.SelectedIndexChanged,
      TextBoxYearFrom.TextChanged,
       TextBoxYearTo.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPgcPlan
            .Nom = TextBoxNom.Text
            .LastPlan = New PgcPlan(ComboBoxLastPgcPlan.SelectedValue)
            If IsNumeric(TextBoxYearFrom.Text) Then
                .YearFrom = TextBoxYearFrom.Text
            End If
            If IsNumeric(TextBoxYearTo.Text) Then
                .YearTo = TextBoxYearTo.Text
            End If
            .Update()
        End With
        RaiseEvent AfterUpdate(mPgcPlan, e)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest pla?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If mPgcPlan.Delete(sErr) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox(sErr, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Class