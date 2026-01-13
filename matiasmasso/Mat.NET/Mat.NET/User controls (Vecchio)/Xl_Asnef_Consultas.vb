

Public Class Xl_Asnef_Consultas
    Private mEmp as DTOEmp
    Private mLang As DTOLang = root.usuari.Lang

    Private mAllowEvents As Boolean = False

    Private Enum Cols1
        Yea
        MesId
        MesNom
        Count
    End Enum

    Private Enum Cols2
        Guid
        Fch
        Login
        Cod
        Ico
        Clx
    End Enum

    Public WriteOnly Property Emp() as DTOEmp
        Set(ByVal value as DTOEmp)
            mEmp = value
            LoadGrid1()
            If DataGridView1.CurrentRow IsNot Nothing Then
                LoadGrid2()
                SetContextMenu()
            End If
        End Set
    End Property

    Private Sub LoadGrid1()
        Dim SQL As String = "SELECT DATEPART(yy,FCHCREATED) AS YEA, DATEPART(mm,FCHCREATED) AS MESID, '' AS MESNOM, " _
        & "COUNT(GUID) AS ITMCOUNT " _
        & "FROM ASNEF_CONSULTAS " _
        & "WHERE EMP=@EMP " _
        & "GROUP BY DATEPART(yy,FCHCREATED), DATEPART(mm,FCHCREATED) " _
        & "ORDER BY DATEPART(yy,FCHCREATED) DESC, DATEPART(mm,FCHCREATED) DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        mAllowEvents = False
        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols1.MesId)
                .Visible = False
            End With
            With .Columns(Cols1.Yea)
                .HeaderText = "any"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols1.MesNom)
                .HeaderText = "mes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols1.Count)
                .HeaderText = "consultes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid2()
        Dim oGrid1 As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid1.CurrentRow
        If oRow Is Nothing Then Exit Sub

        Dim iYea As Integer = oRow.Cells(Cols1.Yea).Value
        Dim iMes As Integer = oRow.Cells(Cols1.MesId).Value

        Dim SQL As String = "SELECT A.GUID,A.FCHCREATED,U.LOGIN,A.COD,C.CLX " _
        & "FROM ASNEF_CONSULTAS A INNER JOIN " _
        & "CLX C ON A.EMP=C.EMP AND A.CLI=C.CLI LEFT OUTER JOIN " _
        & "EMPUSR E ON A.USRCREATED=E.CLI LEFT OUTER JOIN " _
        & "USR U ON E.UsrGuid=U.Guid " _
        & "WHERE A.EMP=@EMP AND YEAR(FCHCREATED)=@YEA AND MONTH(FCHCREATED)=@MES " _
        & "ORDER BY A.FCHCREATED DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", iYea, "@MES", iMes)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono 
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols2.Ico)

        mAllowEvents = False
        With DataGridView2
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols2.Guid)
                .Visible = False
            End With
            With .Columns(Cols2.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols2.Login)
                .HeaderText = "usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
            End With
            With .Columns(Cols2.Cod)
                .Visible = False
            End With
            With .Columns(Cols2.Ico)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols2.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = Cols1.MesNom Then
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            Dim iMes As Integer = oRow.Cells(Cols1.MesId).Value
            e.Value = mLang.MesAbr(iMes)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            LoadGrid2()
        End If
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
        oMenuItem.Enabled = CurrentItem() IsNot Nothing
        oContextMenu.Items.Add(oMenuItem)

        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentItem() As AsnefConsulta
        Dim oRetVal As AsnefConsulta = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New AsnefConsulta(New Guid(oRow.Cells(Cols2.Guid).Value.ToString))
        End If
        Return oRetVal
    End Function

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As AsnefConsulta = CurrentItem()
        If oItem IsNot Nothing Then
            Dim oFrm As New Frm_ASNEF_Consulta(oItem)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols2.Clx
        Dim oGrid As DataGridView = DataGridView2
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid2()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case Cols2.Ico
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim oCod As AsnefConsulta.Cods = oRow.Cells(Cols2.Cod).Value
                Select Case oCod
                    Case AsnefConsulta.Cods.Clean
                        e.Value = My.Resources.Ok
                    Case AsnefConsulta.Cods.Dirty
                        e.Value = My.Resources.warn
                End Select
        End Select
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Zoom(sender, e)
    End Sub

End Class
