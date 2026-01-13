

Public Class Frm_UsrEvents

    Private mUsr As Contact
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsAlbs As DataSet

    Private Enum Cols
        Id
        Nom
    End Enum

    Public WriteOnly Property Usr() As contact
        Set(ByVal value As contact)
            If value IsNot Nothing Then mUsr = value
        End Set
    End Property

    Private Sub Frm_UsrEvents_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If mUsr Is Nothing Then mUsr = root.Usuari
        Me.Text = Me.Text & " " & mUsr.Nom
        refresca()
    End Sub

    Private Sub Refresca()
        LoadGridAlbs()
    End Sub

    Private Sub LoadGridAlbs()
        Dim SQL As String = "SELECT CLI,CLX FROM USREVT INNER JOIN " _
        & "CLX ON USREVT.EMP=CLX.EMP AND USREVT.ID=CLX.CLI " _
        & "WHERE " _
        & "USREVT.EMP=" & mEmp.Id & " AND " _
        & "USREVT.USR=" & mUsr.Id & " AND " _
        & "USREVT.COD=" & CInt(UsrEvent.Cods.Albara) & " " _
        & "ORDER BY CLX"
        mDsAlbs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = mDsAlbs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_ContactAlbs_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactAlbs.AfterUpdate
        ButtonAlbsAdd.Enabled = True
    End Sub

    Private Sub ButtonAlbsAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlbsAdd.Click
        Dim oTb As DataTable = mDsAlbs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        Dim oContact As Contact = Xl_ContactAlbs.Contact
        If oContact.Id > 0 Then
            oRow(Cols.Id) = oContact.Id
            oRow(Cols.Nom) = oContact.Clx
            oTb.Rows.Add(oRow)
            ButtonAlbsAdd.Enabled = False

            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim SQL As String = "DELETE USREVT WHERE " _
        & "EMP=" & mUsr.Emp.Id & " AND " _
        & "USR=" & mUsr.Id & " AND " _
        & "COD=" & CInt(UsrEvent.Cods.Albara)
        maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

        Dim oTb As DataTable = mDsAlbs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            SQL = "INSERT INTO USREVT (EMP,USR,COD,ID) VALUES (" _
            & mUsr.Emp.Id & "," _
            & mUsr.Id & "," _
            & CInt(UsrEvent.Cods.Albara) & "," _
            & oRow(Cols.Id) & ")"
            maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

        Next
        Me.Close()
    End Sub

    Private Sub ButtonAlbsDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlbsDel.Click
        DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
    End Sub


End Class