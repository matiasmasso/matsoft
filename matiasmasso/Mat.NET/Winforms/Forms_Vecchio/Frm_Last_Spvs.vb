

Public Class Frm_Last_Spvs

    Private mEmp As DTO.DTOEmp = BLL.BLLApp.Emp
    Private mClient As Client
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Yea
        Id
        Fch
        Llegit
        Arribat
        Sortit
        Art
        Clx
    End Enum

    Public WriteOnly Property Client() As Client
        Set(ByVal value As Client)
            mClient = value
            If mClient IsNot Nothing Then
                CheckBoxOldClis.Visible = True
                CheckBoxOldClis.Enabled = (mClient.ContactAnterior IsNot Nothing)
            End If
        End Set
    End Property

    Private Sub Frm_LastSpvs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False

        Dim SQL As String = "SELECT SPV.Guid, SPV.yea, SPV.Id, SPV.fchAvis, " _
        & "CAST((CASE WHEN SPV.fchRead IS NULL THEN 0 ELSE 1 END) AS BIT)  AS LLEGIT, " _
        & "CAST((CASE WHEN SPVIN IS NULL THEN 0 ELSE 1 END) AS BIT) AS ARRIBAT, " _
        & "CAST((CASE WHEN ALBGUID IS NULL THEN 0 ELSE 1 END) AS BIT) AS SORTIT, " _
        & "Product.NOM, " _
        & "CLX.clx " _
        & "FROM  SPV " _
        & "INNER JOIN CLX ON SPV.CliGuid = Clx.Guid " _
        & "LEFT OUTER JOIN Product ON SPV.ProductGuid = Product.Guid " _
        & "WHERE SPV.EMP=" & BLLApp.Emp.Id

        If mClient IsNot Nothing Then
            If CheckBoxOldClis.Checked Then
                SQL = SQL & " AND (SPV.CliGuid='" & mClient.Guid.ToString & "' " & SQLOldClis() & ") "
            Else
                SQL = SQL & " AND SPV.CliGuid='" & mClient.Guid.ToString & "' "
            End If
        End If

        If Not CheckBoxRead.Checked Then
            SQL = SQL & " AND SPV.fchRead < '1/1/2000' "
        End If
        If Not CheckBoxNotRead.Checked Then
            SQL = SQL & " AND SPV.fchRead >= '1/1/2000' "
        End If

        If Not CheckBoxArrived.Checked Then
            SQL = SQL & " AND SPV.SPVIN IS NULL "
        End If
        If Not CheckBoxNotArrived.Checked Then
            SQL = SQL & " AND SPV.SPVIN IS NOT NULL "
        End If

        If Not CheckBoxLeft.Checked Then
            SQL = SQL & " AND (Spv.AlbGuid IS NULL AND Spv.UsrOutOfSpvOutGuid IS NULL) "
        End If
        If Not CheckBoxNotLeft.Checked Then
            SQL = SQL & " AND (Spv.AlbGuid IS NOT NULL OR Spv.UsrOutOfSpvOutGuid IS NOT NULL) "
        End If


        SQL = SQL & "ORDER BY SPV.YEA DESC, SPV.Id DESC "
        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Full"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "registrat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Llegit)
                .HeaderText = "llegit"
                .Width = 35
            End With
            With .Columns(Cols.Arribat)
                .HeaderText = "arribat"
                .Width = 35
            End With
            With .Columns(Cols.Sortit)
                .HeaderText = "sortit"
                .Width = 35
            End With
            With .Columns(Cols.Art)
                .HeaderText = "article"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With

        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function SQLOldClis() As String
        Dim SQL As String = ""
        Dim oCli As Contact = mClient
        Do
            If oCli.ContactAnterior.Id = 0 Then
                Exit Do
            Else
                oCli = oCli.ContactAnterior
                SQL = SQL & " OR SPV.CliGuid='" & oCli.Guid.ToString & "' "
            End If
        Loop
        Return SQL
    End Function

    Private Function CurrentSpv() As DTOSpv
        Dim retval As DTOSpv = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = New DTOSpv(oRow.Cells(Cols.Guid).Value)
            With retval
                .FchAvis = oRow.Cells(Cols.Fch).Value
                .Id = oRow.Cells(Cols.Id).Value
            End With
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSpv As DTOSpv = CurrentSpv()

        If oSpv IsNot Nothing Then
            Dim oMenu_Spv As New Menu_Spv(oSpv)
            AddHandler oMenu_Spv.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Spv.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Spv(CurrentSpv)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxNotRead.CheckedChanged, _
        CheckBoxRead.CheckedChanged, _
        CheckBoxNotArrived.CheckedChanged, _
        CheckBoxArrived.CheckedChanged, _
        CheckBoxNotLeft.CheckedChanged, _
        CheckBoxLeft.CheckedChanged, _
        CheckBoxOldClis.CheckedChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub PictureBoxSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxSearch.Click
        Dim iYea As Integer = NumericUpDownYea.Value
        Dim iSpv As Integer = NumericUpDownSpv.Value
        Dim oSpv As DTOSpv = BLLSpv.FromId(iYea, iSpv)
        Dim oFrm As New Frm_Spv(oSpv)

        oFrm.Show()
    End Sub
End Class