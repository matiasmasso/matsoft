

Public Class Xl_Asnef_CliLogs
    Private mContact As Contact
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        LinCod
        Fch
        Cod
        Ico
        Obs
        Guid
    End Enum

    Private Enum LinCods
        NotSet
        Consulta
        Impagat
    End Enum

    Private Enum Cods
        NotSet
        Net
        Brut
        Actiu
        Cancelat
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            mContact = value
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property


    Private Sub LoadGrid()

        Dim SQL As String = "SELECT U.LINCOD, U.FCH, U.COD, U.OBS, U.GUID FROM " _
        & "(" _
        & "SELECT 1 AS LINCOD, " _
        & "C.FCHCREATED AS FCH, " _
        & "C.COD, " _
        & "C.OBS, " _
        & "C.GUID " _
        & "FROM ASNEF_CONSULTAS C WHERE C.EMP=@EMP AND C.CLI=@CLI " _
        & "UNION ALL " _
        & "SELECT 2 as LINCOD," _
        & "I.ASNEFALTA AS FCH, " _
        & "(CASE WHEN I.ASNEFBAIXA IS NULL THEN 3 ELSE 4 END) AS COD, " _
        & "'Fra.'+CAST(B.FRA AS VARCHAR)+' per '+CAST(B.EUR AS VARCHAR) AS OBS, " _
        & "I.GUID " _
        & "FROM IMPAGATS I INNER JOIN " _
        & "CSB B ON I.EMP=B.EMP AND I.YEA=B.YEA AND I.CSA=B.CSB AND I.CSB=B.DOC " _
        & "WHERE B.EMP=@EMP AND B.CLI=@CLI " _
        & ") AS U " _
        & "ORDER BY U.FCH DESC, U.LINCOD, U.COD"

        Dim oDs As DataSet = Nothing
        oDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", BLLApp.Emp.Id, "@CLI", mContact.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono 
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

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

            With .Columns(Cols.LinCod)
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
            With .Columns(Cols.Cod)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Guid)
                .Visible = False
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
        oMenuItem.Enabled = CurrentItem() IsNot Nothing
        oContextMenu.Items.Add(oMenuItem)


        oMenuItem = New ToolStripMenuItem("afegir consulta", Nothing, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentItem() As Object
        Dim oRetVal As Object = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New System.Guid(oRow.Cells(Cols.Guid).Value.ToString)
            Select Case CType(oRow.Cells(Cols.LinCod).Value, LinCods)
                Case LinCods.Consulta
                    oRetVal = New AsnefConsulta(oGuid)
                Case LinCods.Impagat
                    oRetVal = New DTOImpagat(oGuid)
            End Select
        End If
        Return oRetVal
    End Function

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As Object = CurrentItem()
        If TypeOf (oItem) Is AsnefConsulta Then
            Dim oConsulta As AsnefConsulta = DirectCast(oItem, AsnefConsulta)
            Dim oFrm As New Frm_ASNEF_Consulta(oConsulta)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (oItem) Is DTOImpagat Then
            Dim oFrm As New Frm_Impagat(oItem)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oConsulta As New AsnefConsulta(mContact)
        With oConsulta
            .UsrCreated = BLLSession.Current.User
            .FchCreated = Now
        End With
        Dim oFrm As New Frm_ASNEF_Consulta(oConsulta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Obs
        Dim oGrid As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
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
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As Cods = oRow.Cells(Cols.Cod).Value
                Select Case oCod
                    Case Cods.Net
                        e.Value = My.Resources.search_16
                    Case Cods.Brut
                        e.Value = My.Resources.NoPark
                    Case Cods.Actiu
                        e.Value = My.Resources.warn
                    Case Cods.Cancelat
                        e.Value = My.Resources.UNDO
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


End Class
