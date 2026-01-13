

Public Class Frm_LiniesTelefon
    Private mSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private mBigFile As maxisrvr.BigFileNew
    Private mAllowEvents As Boolean
    Private mDisplayObsolets As Boolean = False

    Public Event AfterSelect(sender As Object, e As System.EventArgs)

    Private Enum Cols
        Guid
        Num
        Nom
        Ex
    End Enum


    Public Sub New(Optional oSelectionmode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionmode
        LoadGrid()
    End Sub

    Public Property BigFile As maxisrvr.BigFileNew
        Get
            'amagatzema temporalment un fitxer a la espera de que es seleccioni de quina linia correspon
            'i així recuperar-lo desde el programa que ho crida
            Return mBigFile
        End Get
        Set(value As maxisrvr.BigFileNew)
            mBigFile = value
        End Set
    End Property

    Public ReadOnly Property LiniaTelefon As LiniaTelefon
        Get
            Return CurrentItm()
        End Get
    End Property

    Private Sub LoadGrid()

        Dim SQL As String = ""

        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.Accounts
                SQL = "SELECT GUID,NUM,ALIAS,(CASE WHEN BAIXA IS NULL THEN 0 ELSE 1 END) AS OBSOLETO FROM LINIATELEFON "
                If Not mDisplayObsolets Then
                    SQL = SQL & " WHERE BAIXA IS NULL "
                End If
            Case Rol.Ids.SalesManager
                SQL = "SELECT GUID,NUM,ALIAS,(CASE WHEN BAIXA IS NULL THEN 0 ELSE 1 END) AS OBSOLETO FROM LINIATELEFON WHERE PRIVAT=0 "
                If Not mDisplayObsolets Then
                    SQL = SQL & " AND BAIXA IS NULL "
                End If
        End Select

        SQL = SQL & " ORDER BY NUM"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Num)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Ex)
                .Visible = False
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentItm() As LiniaTelefon
        Dim oItm As Object = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oItm = New LiniaTelefon(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As LiniaTelefon = CurrentItm()
        If oItm IsNot Nothing Then
            Select Case BLL.BLLSession.Current.User.Rol.Id
                Case Rol.Ids.SuperUser, Rol.Ids.Admin
                    oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
                    oContextMenuStrip.Items.Add(oMenuItem)
            End Select

            oMenuItem = New ToolStripMenuItem("consums", Nothing, AddressOf Consums)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.binoculares, AddressOf Delete)
            oMenuItem.Enabled = oItm.AllowDelete
            oContextMenuStrip.Items.Add(oMenuItem)

        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        'DataGridView1.ContextMenuStrip = oContextMenuStrip

        oMenuItem = New ToolStripMenuItem("inclou obsolets", Nothing, AddressOf Do_DisplayObsolets)
        oContextMenuStrip.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent AfterSelect(Me, e)
            Me.Close()
        Else
            Zoom()
        End If
    End Sub


    Private Sub Do_DisplayObsolets(sender As Object, e As System.EventArgs)
        mDisplayObsolets = Not mDisplayObsolets
        Dim oMenuItem As ToolStripMenuItem = sender
        oMenuItem.Checked = mDisplayObsolets
        RefreshRequest(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLiniaTelefon As New LiniaTelefon
        Dim oFrm As New Frm_LiniaTelefon(oLiniaTelefon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_LiniaTelefon(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Consums()
        Dim oFrm As New Frm_LiniaTelConsumsXMes(CurrentItm())
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As Object = CurrentItm()
        If oItm.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim BlSuccess As Boolean = oItm.Delete
                If Not BlSuccess Then
                    MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        Else
            MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
        Dim oFrm As New Frm_LiniaTelefon(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
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


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint

        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim iEx As Integer = CInt(oRow.Cells(Cols.Ex).Value)
        If iEx = 1 Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If

    End Sub


End Class