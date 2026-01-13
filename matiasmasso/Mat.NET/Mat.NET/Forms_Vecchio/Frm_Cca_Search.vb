

Public Class Frm_Cca_Search
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet

    Private Enum Cols
        Cca
        Fch
        Txt
        Cta
        Clx
        Cli
    End Enum

    Public Sub New()
        MyBase.new()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(ByVal iYea As Integer, ByVal DcEur As Decimal)
        MyBase.new()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Yea = iYea
        Xl_AmtCur1.Amt = New maxisrvr.Amt(DcEur)
        LoadGrid()
    End Sub


    Public WriteOnly Property Yea() As Integer
        Set(ByVal Value As Integer)
            With NumericUpDownYea
                .Maximum = IIf(Value < Today.Year, Today.Year, Value)
                .Value = Value
                .Minimum = Value - 10
            End With
        End Set
    End Property

    Private Sub Frm_Cca_Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If NumericUpDownYea.Value = 0 Then Me.Yea = Today.Year
        Xl_AmtCur1.Amt = New maxisrvr.Amt()
    End Sub

    Private Sub LoadGrid()
        Me.Cursor = Cursors.WaitCursor
        Try

            Dim SQL As String = "SELECT  CCA.CCA, CCA.fch, CCA.txt, CCB.cta, clx.clx, CCB.cli " _
             & "FROM CCB INNER JOIN " _
             & "CCA ON Ccb.CcaGuid = Cca.Guid LEFT OUTER JOIN " _
             & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli " _
             & "where CCB.EMP=" & mEmp.Id & " AND " _
             & "CCB.YEA=" & CurrentYea() & " AND " _
             & "CCB.PTS=" & CStr(Xl_AmtCur1.Amt.Val).Replace(",", ".") & " AND " _
             & "CCB.CUR LIKE '" & Xl_AmtCur1.Amt.Cur.Id & "' " _
             & "ORDER BY CCA.CCA DESC"
            mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
            Dim oTb As DataTable = mDs.Tables(0)
            If oTb.Rows.Count = 0 Then
                PictureBoxNotFound.Visible = True
                DataGridView1.Visible = False
            Else
                PictureBoxNotFound.Visible = False
                DataGridView1.Visible = True
                With DataGridView1
                    With .RowTemplate
                        .Height = DataGridView1.Font.Height * 1.3
                    End With
                    .DataSource = oTb
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .ColumnHeadersVisible = True
                    .RowHeadersVisible = False
                    .MultiSelect = False
                    .AllowUserToResizeRows = False
                    .AllowDrop = False

                    With .Columns(Cols.Cca)
                        .HeaderText = "registre"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        .Width = 45
                        .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                        .DefaultCellStyle.Format = "#"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                    With .Columns(Cols.Fch)
                        .HeaderText = "Data"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        .Width = 65
                        .DefaultCellStyle.Format = "dd/MM/yy"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    End With
                    With .Columns(Cols.Txt)
                        .HeaderText = "concepte"
                        .Width = 50
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    End With
                    With .Columns(Cols.Cta)
                        .HeaderText = "compte"
                        .Width = 50
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    End With
                    With .Columns(Cols.Clx)
                        .HeaderText = "subcompte"
                        .Width = 100
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    End With
                    With .Columns(Cols.Cli)
                        .Visible = False
                    End With

                End With

            End If
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function CurrentCca() As Cca
        Dim oCca As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim CcaId As Long = oRow.Cells(Cols.Cca).Value
            oCca = MaxiSrvr.Cca.FromNum(mEmp, CurrentYea, CcaId)
        End If
        Return oCca
    End Function

    Private Function CurrentCcas() As Ccas
        Dim oCcas As New Ccas
        Dim IntYea As Integer
        Dim LngId As Integer
        Dim oCca As Cca

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                IntYea = CurrentYea()
                LngId = oRow.Cells(Cols.Cca).Value
                oCca = MaxiSrvr.Cca.FromNum(mEmp, IntYea, LngId)
                oCcas.Add(oCca)
            Next
        Else
            oCcas.Add(CurrentCca)
        End If
        Return oCcas
    End Function

    Private Sub Xl_AmtCur1_AfterUpdateValue(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCur1.AfterUpdateValue
        PictureBoxNotFound.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Cca

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If mDs.Tables(0).Rows.Count = 0 Then
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
        Dim oCcas As Ccas = CurrentCcas()

        If oCcas.count > 0 Then
            Dim oMenu_Cca As New Menu_Cca(oCcas)
            AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Cca.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Cca(CurrentCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ButtonSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
        LoadGrid()
    End Sub

    Private Sub NumericUpDownYea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownYea.Click
        'PictureBoxNotFound.Visible = False
        'DataGridView1.Visible = True
    End Sub

    Private Sub NumericUpDownYea_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownYea.ValueChanged
        PictureBoxNotFound.Visible = False
        DataGridView1.Visible = False
    End Sub
End Class