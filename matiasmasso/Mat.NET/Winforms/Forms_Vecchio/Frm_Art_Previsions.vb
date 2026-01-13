

Public Class Frm_Art_Previsions
    Private mArt As Art
    Private mEmp as DTOEmp

    Private Enum Cols
        yea
        week
        qty
        Prv
        Clx
    End Enum

    Public WriteOnly Property Art() As Art
        Set(ByVal Value As Art)
            If Not Value Is Nothing Then
                mArt = Value
                mEmp = mArt.Emp
                Me.Text = "PREVISIONS " & mArt.Nom_ESP
                LoadGrid()
            End If
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT DELIVERY.YEA,DELIVERY.WEEK,DELIVERY.QTY,DELIVERY.PRV,CLX.CLX FROM DELIVERY INNER JOIN " _
        & "CLX ON DELIVERY.EMP=CLX.EMP AND DELIVERY.PRV=CLX.CLI " _
        & "WHERE " _
        & "DELIVERY.ArtGuid=@ArtGuid " _
        & "ORDER BY DELIVERY.YEA,DELIVERY.WEEK,CLX.CLX"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@ArtGuid", mArt.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            With .Columns(Cols.yea)
                .Width = 30
                .HeaderText = "any"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.week)
                .HeaderText = "setmana"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.qty)
                .HeaderText = "quantitat"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###;-#,###;#"
            End With
            With .Columns(Cols.Prv)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "proveidor"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentPrevisio() As Previsio
        Dim oPrevisio As Previsio = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oProveidor As New Proveidor(Contact.FromNum(mEmp, oRow.Cells(Cols.Prv).Value).Guid)
            oPrevisio = New Previsio(oProveidor, oRow.Cells(Cols.yea).Value, oRow.Cells(Cols.week).Value)
        End If
        Return oprevisio
    End Function


    Private Sub ShowPrevisio()
        Dim oFrm As New Frm_PrvPrevisions
        AddHandler oFrm.afterupdate, AddressOf refreshrequest
        With oFrm
            .Previsio = CurrentPrevisio()
            .Show()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("no hi han factures registrades!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPrevisio As Previsio = CurrentPrevisio()

        If oPrevisio IsNot Nothing Then
            Dim oMenu_Previsio As New Menu_Previsio(oPrevisio)
            AddHandler oMenu_Previsio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Previsio.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        root.ShowPrevisions(, CurrentPrevisio)
    End Sub
End Class