

Public Class Frm_PqHdrs

    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Id
        Fch
        albs
        bultos
        kgs
    End Enum


    Public WriteOnly Property Mgz() As Mgz
        Set(ByVal Value As Mgz)
            LoadMgzs(Value)
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PQhdr.Id, PQhdr.fch, COUNT(ALB.alb) AS ALBS, SUM(ALB.Bultos) AS BULTOS, SUM(ALB.Kgs) AS KGS " _
         & "FROM PQhdr INNER JOIN " _
         & " ALB ON PQhdr.emp = ALB.Emp AND PQhdr.yea = ALB.Pq_Yea AND PQhdr.Id = ALB.Pq_Id " _
         & "WHERE PQHDR.EMP=" & mEmp.Id & " AND " _
         & "PQHDR.YEA=" & CurrentYea() & " AND " _
         & "PQHDR.MGZ=" & CurrentMgz.Id & " " _
         & "GROUP BY PQhdr.Id, PQhdr.fch " _
         & "ORDER BY PQhdr.ID DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Width = 50
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.albs)
                .HeaderText = "albarans"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.bultos)
                .HeaderText = "bultos"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.kgs)
                .HeaderText = "kilos"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Return Today.Year
    End Function

    Private Function CurrentMgz() As Mgz
        Dim oMgz As Mgz = MaxiSrvr.Mgz.FromNum(mEmp, ComboBoxMgz.SelectedValue)
        Return oMgz
    End Function

    Private Function CurrentPqHdr() As PqHdr
        Dim oPqHdr As PqHdr = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = oRow.Cells(Cols.Id).Value
            oPqHdr = New PqHdr(CurrentMgz, CurrentYea, LngId)
        End If
        Return oPqHdr
    End Function

    Private Sub LoadMgzs(ByVal oMgz As Mgz)
        Dim SQL As String = "SELECT PQHDR.MGZ, MGZ.NOM FROM PQHDR LEFT OUTER JOIN " _
        & "MGZ ON PQHDR.EMP=MGZ.EMP AND PQHDR.MGZ=MGZ.CLI " _
        & "WHERE PQHDR.EMP=" & mEmp.Id & " AND " _
        & "PQHDR.YEA=" & CurrentYea() & " " _
        & "GROUP BY PQHDR.MGZ, MGZ.NOM " _
        & "ORDER BY MGZ.NOM"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With ComboBoxMgz
            .DisplayMember = "NOM"
            .ValueMember = "MGZ"
            .DataSource = oTb
            .SelectedValue = oMgz.Id
        End With
    End Sub

    Private Sub ComboBoxMgz_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        root.ShowPqHdr(CurrentPqHdr)
    End Sub
End Class
