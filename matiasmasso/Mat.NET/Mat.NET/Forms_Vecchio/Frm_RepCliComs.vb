

Public Class Frm_RepCliComs
    Private mRep As Rep
    Private mAllowEvents As Boolean

    Private Enum Cols
        Com
        Cli
        Nom
        Desde
        Hasta
    End Enum

    Public Sub New(oRep As Rep)
        MyBase.New()
        Me.InitializeComponent()
        mRep = oRep
        Me.Text = "Excepcions de comisions de " & oRep.Abr
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT COM.COM,COM.CLI,CLX.CLX,COM.DESDE,COM.HASTA FROM COM INNER JOIN " _
                            & "CLX ON COM.EMP=CLX.EMP AND COM.CLI=CLX.CLI " _
                            & "WHERE COM.EMP=@EMP AND COM.REP=@REP " _
                            & "ORDER BY (CASE WHEN COM.HASTA IS NULL THEN 0 ELSE 1 END), CLX.CLX"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mRep.Emp.Id, "@REP", mRep.Id)
        Dim oTb As DataTable = oDs.Tables(0)

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

            With .Columns(Cols.Com)
                .HeaderText = "comisió"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "# %;-# %;#"
            End With

            With .Columns(Cols.Cli)
                .Visible = False
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Desde)
                .HeaderText = "des de"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
                .Width = 80
            End With

            With .Columns(Cols.Hasta)
                .HeaderText = "fins"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
                .Width = 80
            End With

        End With
    End Sub

    Private Function CurrentItm() As RepCliCom
        Dim oItm As RepCliCom = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCli As Client = MaxiSrvr.Client.FromNum(mRep.Emp, CInt(oRow.Cells(Cols.Cli).Value))
            oItm = New RepCliCom(mRep, oCli)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Object = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRepCliCom As New RepCliCom(mRep, Nothing)
        Dim oFrm As New Frm_RepCliCom(oRepCliCom)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_RepCliCom(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As Object = CurrentItm()
        If oItm.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim BlSuccess As Boolean = oItm.Delete
                If BlSuccess Then
                    RefreshRequest(Nothing, EventArgs.Empty)
                Else
                    MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        Else
            MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

 
End Class