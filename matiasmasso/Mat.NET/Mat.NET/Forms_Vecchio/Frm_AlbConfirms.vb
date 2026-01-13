

Public Class Frm_AlbConfirms
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mMgz As Mgz = New Mgz(BLL.BLLApp.Mgz.Guid)
    Private mDefaultAlb As Alb = Nothing
    Private mAllowevents As Boolean = False

    Private Enum Cols1
        Yea
        Id
        Fch
    End Enum

    Private Enum Cols2
        Yea
        Alb
        Fch
        Bultos
        Kg
        TrpNom
        Zona
        Expedicio
        Cubicatje
        Eur
    End Enum

    Public Sub New(Optional ByVal oAlb As Alb = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mDefaultAlb = oAlb
        Me.Show()
        Cursor = Cursors.WaitCursor
        LoadgridHeader()
        SetContextMenu1()
        LoadGridDetall()
        SetContextMenu2()
        Cursor = Cursors.Default
        mAllowevents = True
    End Sub

    Private Sub LoadgridHeader()
        Dim SQL As String = "SELECT YEA,ID,FCH FROM ALB_CFM_HEADER WHERE EMP=@EMP AND MGZ=@MGZ ORDER BY YEA DESC, ID DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MGZ", mMgz.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols1.Yea)
                .Visible = False
            End With
            With .Columns(Cols1.Id)
                .HeaderText = "fitxer"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols1.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With

        If mDefaultAlb IsNot Nothing Then
            Dim oConfirm As AlbConfirm = mDefaultAlb.Confirmacio.Parent
            If oConfirm IsNot Nothing Then
                For Each oRow As DataGridViewRow In DataGridView1.Rows
                    If oRow.Cells(Cols1.Yea).Value = oConfirm.Yea Then
                        If oRow.Cells(Cols1.Id).Value = oConfirm.Id Then
                            DataGridView1.CurrentCell = oRow.Cells(Cols1.Id)
                            Exit For
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Function CurrentHeader() As AlbConfirm
        Dim oRetVal As AlbConfirm = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New AlbConfirm(mMgz, CInt(oRow.Cells(Cols1.Yea).Value), CInt(oRow.Cells(Cols1.Id).Value))
        End If
        Return oRetVal
    End Function

    Private Sub LoadGridDetall()
        Dim SQL As String = "SELECT D.AlbYea, D.AlbId, A.FCH, D.Bultos, D.Kg, T.Abr, Zona.Nom, D.Expedicio, D.Cubicatje, D.Eur " _
        & "FROM ALB_CFM_DETAIL D INNER JOIN " _
        & "ALB A ON D.EMP=A.EMP AND D.ALBYEA=A.YEA AND D.ALBID=A.ALB LEFT OUTER JOIN " _
        & "TRP T ON D.Emp = T.emp AND D.Transportista = T.Id LEFT OUTER JOIN " _
        & "Zip ON A.Zip=Zip.Guid INNER JOIN " _
        & "Location ON Zip.Location=Location.Guid INNER JOIN " _
        & "Zona ON Location.Zona=Zona.Guid " _
        & "WHERE D.EMP=@EMP AND D.MGZ=@MGZ AND D.PARENTYEA=@YEA AND D.PARENTID=@ID " _
        & "ORDER BY D.ALBYEA, D.ALBID"

        Dim oParent As AlbConfirm = CurrentHeader()
        If oParent IsNot Nothing Then

            Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oParent.Mgz.Emp.Id, "@MGZ", oParent.Mgz.Id, "@YEA", oParent.Yea, "@ID", oParent.Id)
            Dim oTb As DataTable = oDs.Tables(0)

            'afegeix icono PDF
            'Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
            'oCol.SetOrdinal(Cols2.Ico)

            mAllowevents = False
            With DataGridView2
                .RowTemplate.Height = .Font.Height * 1.3
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False

                With .Columns(Cols2.Yea)
                    .Visible = False
                End With
                With .Columns(Cols2.Alb)
                    .HeaderText = "albará"
                    .Width = 45
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.Fch)
                    .HeaderText = "data"
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
                With .Columns(Cols2.Bultos)
                    .HeaderText = "bultos"
                    .Width = 45
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.Kg)
                    .HeaderText = "Kg"
                    .Width = 45
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.TrpNom)
                    .HeaderText = "transportista"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
                With .Columns(Cols2.Zona)
                    .HeaderText = "destinació"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
                With .Columns(Cols2.Expedicio)
                    .HeaderText = "expedició"
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
                With .Columns(Cols2.Cubicatje)
                    .HeaderText = "cubicatje"
                    .Width = 45
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.Eur)
                    .HeaderText = "cost"
                    .Width = 45
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End With

            If mDefaultAlb IsNot Nothing Then
                Dim oConfirm As AlbConfirm = mDefaultAlb.Confirmacio.Parent
                If oConfirm IsNot Nothing Then
                    If oConfirm.Id = CurrentHeader.Id Then
                        For Each oRow As DataGridViewRow In DataGridView2.Rows
                            If oRow.Cells(Cols2.Yea).Value = mDefaultAlb.Yea Then
                                If oRow.Cells(Cols2.Alb).Value = mDefaultAlb.Id Then
                                    DataGridView2.CurrentCell = oRow.Cells(Cols2.Alb)
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            mAllowevents = True
        End If
    End Sub

    Private Function CurrentDetall() As AlbConfirmItem
        Dim oRetVal As AlbConfirmItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim oAlb As Alb = MaxiSrvr.Alb.FromNum(mEmp, CInt(oRow.Cells(Cols2.Yea).Value), CInt(oRow.Cells(Cols2.Alb).Value))
            oRetVal = New AlbConfirmItem(oAlb)
        End If
        Return oRetVal
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom_Header(sender, e)
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Zoom_Detall(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowevents Then
            SetContextMenu1()
            LoadGridDetall()
            SetContextMenu2()
        End If
    End Sub

    Private Sub SetContextMenu1()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom_header)
        oMenuItem.Enabled = (CurrentHeader() IsNot Nothing)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowevents Then
            SetContextMenu2()
        End If
    End Sub

    Private Sub SetContextMenu2()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim BlExists As Boolean = (CurrentDetall() IsNot Nothing)
        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom_Detall)
        oMenuItem.Enabled = (BlExists)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom_Detall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As AlbConfirmItem = CurrentDetall()
        If oItem IsNot Nothing Then
            Dim oFrm As New Frm_AlbNew2(oItem.Alb)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest_Detall
            oFrm.Show()
        End If
    End Sub


    Private Sub Zoom_Header(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub


    Private Sub RefreshRequest_Header(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols1.Fch
        Dim oGrid As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadgridHeader()

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

    Private Sub RefreshRequest_Detall(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols2.TrpNom
        Dim oGrid As DataGridView = DataGridView2
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridDetall()

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
End Class