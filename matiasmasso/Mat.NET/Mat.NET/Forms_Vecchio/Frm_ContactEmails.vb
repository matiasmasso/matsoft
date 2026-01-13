

Public Class Frm_ContactEmails
    Private mDs As DataSet = Nothing

    Private Enum Cols
        Id
        Clx
        Email
        Obs
    End Enum

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        PictureBox1.Image = Xl_Contact1.Contact.Img48
        ButtonAdd.Enabled = True
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        If mDs Is Nothing Then
            mDs = CreateDataSource()
            With DataGridView1
                .DataSource = mDs.Tables(0)
                .ColumnHeadersVisible = False
                .RowHeadersVisible = False
                .RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True
                With .RowTemplate
                    .Height = DataGridView1.Font.Height * 1.3
                End With
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                With .Columns(Cols.Id)
                    .Visible = False
                End With
                With .Columns(Cols.Clx)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                With .Columns(Cols.Email)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                With .Columns(Cols.Obs)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
            End With
        End If

        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oContact As contact = Xl_Contact1.Contact

        For Each oEmail As Email In oContact.Emails
            oRow = oTb.NewRow
            oRow(Cols.Id) = oContact.Id
            oRow(Cols.Clx) = oContact.Clx
            oRow(Cols.Email) = oEmail.Adr
            oRow(Cols.Obs) = oEmail.Nom
            oTb.Rows.InsertAt(oRow, 0)
        Next

        ButtonAdd.Enabled = False
        ButtonExcel.Enabled = True
    End Sub

    Private Function CreateDataSource() As DataSet
        Dim oDs As New DataSet
        Dim oTable As New DataTable
        With oTable.Columns
            .Add(New DataColumn("Id", System.Type.GetType("System.String")))
            .Add(New DataColumn("Clx", System.Type.GetType("System.String")))
            .Add(New DataColumn("Email", System.Type.GetType("System.String")))
            .Add(New DataColumn("Obs", System.Type.GetType("System.String")))
        End With
        oDs.Tables.Add(oTable)
        Return oDs
    End Function

    Private Sub ButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub DataGridView1_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        ButtonExcel.Enabled = mDs.Tables(0).Rows.Count > 0
    End Sub
End Class