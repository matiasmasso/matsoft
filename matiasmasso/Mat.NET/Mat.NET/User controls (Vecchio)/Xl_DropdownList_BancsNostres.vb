

Public Class Xl_DropdownList_BancsNostres
    Private mBanc As Banc
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
        Img
    End Enum

    Public Property Banc() As Banc
        Get
            Return CurrentBanc()
        End Get
        Set(ByVal oBanc As Banc)
            If oBanc IsNot Nothing Then
                For Each oRow As DataGridViewRow In DataGridView1.Rows
                    If oRow.Cells(Cols.Id).Value = oBanc.Id Then
                        DataGridView1.CurrentCell = oRow.Cells(Cols.Img)
                        Exit Property
                    End If
                Next
            End If
        End Set
    End Property

    Private Sub Xl_DropdownList_Banc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadBancs()
    End Sub

    Private Sub LoadBancs()
        Dim SQL As String = "SELECT CLI,ABR FROM CliBnc WHERE EMP=1 ORDER BY ABR"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("IMG", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Img)

        Dim oBanc As Banc
        For Each oRow As DataRow In oTb.Rows
            oBanc = MaxiSrvr.Banc.FromNum(mEmp, oRow(Cols.Id))
            oRow(Cols.Img) = MaxiSrvr.GetByteArrayFromImg(BLL.BLLIban.Img(oBanc.Iban.Digits))
        Next

        With DataGridView1
            .DataSource = oTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .RowTemplate.Height = 50
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .Visible = False
            End With
            With .Columns(Cols.Img)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        SetContextMenu()

    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        'Dim oMenu_Banc As New Menu_Banc(CurrentBanc)
        'oContextMenu.Items.AddRange(oMenu_Banc.Range)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentBanc() As Banc
        Dim oBanc As Banc = Nothing
        If DataGridView1.FirstDisplayedScrollingRowIndex > 0 Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.FirstDisplayedScrollingRowIndex)
            If oRow IsNot Nothing Then
                oBanc = MaxiSrvr.Banc.FromNum(mEmp, oRow.Cells(Cols.Id).Value)
            End If
        End If
        Return oBanc
    End Function

    Private Sub Do_Zoom()
        Dim oFrm As New Frm_Contact(CurrentBanc)
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oBanc As Banc = MaxiSrvr.Banc.FromNum(mEmp, oRow.Cells(Cols.Id).Value)
        e.ToolTipText = oBanc.Abr
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        mBanc = CurrentBanc()
        RaiseEvent AfterUpdate(mBanc, e)
    End Sub

End Class
