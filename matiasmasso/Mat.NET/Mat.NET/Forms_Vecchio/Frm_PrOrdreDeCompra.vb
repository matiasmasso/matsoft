

Public Class Frm_PrOrdreDeCompra
    Private mOrdreDeCompra As PrOrdreDeCompra = Nothing
    Private mGuid As System.Guid
    Private mBigFile As BigFileSrc = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Insercions
    End Enum

    Private Enum Cols
        Guid
        Num
        Eur
    End Enum

    Public Sub New(ByVal oOrdreDeCompra As PrOrdreDeCompra)
        MyBase.New()
        Me.InitializeComponent()
        mOrdreDeCompra = oOrdreDeCompra
        refresca()
    End Sub

    Private Sub Refresca()
        If mOrdreDeCompra.Exists Then
            Me.Text = "ORDRE DE COMPRA"
            ButtonDel.Enabled = True
            mGuid = mOrdreDeCompra.Guid
        Else
            Me.Text = "NOVA ORDRE DE COMPRA"
            ButtonDel.Enabled = False
            mGuid = Guid.NewGuid
        End If
        With mOrdreDeCompra
            If .Editorial IsNot Nothing Then
                Xl_Contact1.Contact = .Editorial
                PictureBoxLogo.Image = .Editorial.Img48
            End If
            DateTimePicker1.Value = .Fch
            TextBoxNum.Text = .Num
            Xl_DocFile1.Load(.docfile)
            Xl_BigFile1.BigFile = .BigFile.BigFile
        End With
        mAllowEvents = True
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Save()
        Me.Close()
    End Sub

    Private Sub Save()
        With mOrdreDeCompra
            .Editorial = New PrEditorial(Xl_Contact1.Contact.Guid)
            .Fch = DateTimePicker1.Value
            .Num = TextBoxNum.Text
            '.Insercions = GetInsercionsFromGrid()
            If Xl_BigFile1.IsDirty Then
                .BigFile = New BigFileSrc(DTODocFile.Cods.PrOrdreDeCompra, .Guid, Xl_BigFile1.BigFile)
            End If
            .Update(mGuid)
        End With
        RaiseEvent AfterUpdate(mOrdreDeCompra, EventArgs.Empty)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_BigFile1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_BigFile1.AfterUpdate
        Dim oBigFile As maxisrvr.BigFileNew = CType(sender, maxisrvr.BigFileNew)
        mBigFile = New BigFileSrc(DTODocFile.Cods.PrOrdreDeCompra, mOrdreDeCompra.Guid, oBigFile)
        ButtonOk.Enabled = True
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Insercions
                Static BlDoneInsercions As Boolean
                If Not BlDoneInsercions Then
                    LoadGrid()
                    SetContextMenu()
                    BlDoneInsercions = True
                End If
        End Select
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT GUID,'' as NUMTEXT,COST " _
        & "FROM PRINSERCIO " _
        & "WHERE ORDREDECOMPRA LIKE @GUID " _
        & "ORDER BY NUMERO, PAGINA"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mOrdreDeCompra.Guid.ToString)
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
            .AllowDrop = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Num)
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

    End Sub


    Private Function GetInsercionsFromGrid() As PrInsercions
        Dim oItms As New PrInsercions
        Dim oItm As PrInsercio = Nothing
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If IsDBNull(oRow.Cells(Cols.Guid).Value) Then
            Else
                oItm = New PrInsercio(New Guid(oRow.Cells(Cols.Guid).Value.ToString))
                oItms.Add(oItm)
            End If
        Next
        Return oItms
    End Function


    Private Function CurrentInsercio() As PrInsercio
        Dim oRetVal As PrInsercio = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Try
                oRetVal = New PrInsercio(New Guid(oRow.Cells(Cols.Guid).Value.ToString))
            Catch ex As Exception
            End Try
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
        oMenuItem.Enabled = (CurrentInsercio() IsNot Nothing)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("Afegir nova", Nothing, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oInsercio As PrInsercio = CurrentInsercio()
        Dim oFrm As New Frm_PrInsercio(oInsercio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not mOrdreDeCompra.Exists Then Save()

        Dim oNumero As New PrNumero(mOrdreDeCompra.Revista)
        Dim oInsercio As New PrInsercio(oNumero)
        oInsercio.OrdreDeCompra = mOrdreDeCompra

        Dim oFrm As New Frm_PrInsercio(oInsercio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Num
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
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
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Num
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow IsNot Nothing Then
                    Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                    Dim oInsercio As New PrInsercio(oGuid)
                    e.Value = oInsercio.Numero.FullText
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePicker1.ValueChanged, TextBoxNum.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(GetType(PrInsercio))) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            'Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            'DataGridView1.CurrentCell = oclickedCell
        ElseIf hit.Type = DataGridViewHitTestType.None Then
            Dim oInsercio As PrInsercio = e.Data.GetData(GetType(PrInsercio))
            oInsercio.OrdreDeCompra = mOrdreDeCompra
            Dim exs as New List(Of exception)
            If PrInsercioLoader.Update(oInsercio, exs) Then
                RefreshRequest(sender, e)
            Else
                UIHelper.WarnError( exs, "error al registrar la inserció")
            End If
        End If
    End Sub
End Class