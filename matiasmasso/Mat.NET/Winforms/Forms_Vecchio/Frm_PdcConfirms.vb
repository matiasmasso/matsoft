

Public Class Frm_PdcConfirms
    Private mContact As contact
    Private mAllowEvents As Boolean = False

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
        Me.Text = "CONFIRMACIONS DE COMANDES PENDENTS DE " & oContact.Nom_o_NomComercial
        LoadGrid()
    End Sub

    Private Enum Cols
        Guid
        Fch
        Ref
        ETA
        ETD
    End Enum

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CF.GUID,CF.FCH,CF.REF,CF.ETA,CF.ETD FROM PDCCONFIRM CF INNER JOIN " _
        & "PNC ON CF.GUID LIKE PNC.PDCCONFIRM " _
        & "WHERE CF.EMP=@EMP AND CF.CONTACT=@CONTACT AND PNC.PN2>0 " _
        & "GROUP BY CF.GUID,CF.FCH,CF.REF,CF.ETA,CF.ETD " _
        & "ORDER BY CF.FCH"

        mAllowEvents = False
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mContact.Emp.Id, "@CONTACT", mContact.Id)
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

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Ref)
                .HeaderText = "numero"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.ETD)
                .HeaderText = "ETD"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.ETA)
                .HeaderText = "ETA"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentItem() As PdcConfirm
        Dim oPdcConfirm As PdcConfirm = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New System.Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oPdcConfirm = New PdcConfirm(oGuid)
        End If
        Return oPdcConfirm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Zoom()
    End Sub

    Private Sub Zoom()
        Dim oPdcConfirm As PdcConfirm = CurrentItem()
        Dim oFrm As New Frm_PdcConfirm(oPdcConfirm)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        ofrm.show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            SetContextMenu()
        End If
    End Sub
End Class