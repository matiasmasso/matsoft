
Imports System.Data.SqlClient

Public Class Xl_CliImgs
    Private mContact As Contact
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Img
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            mContact = value
            refresca()
        End Set
    End Property

    Private Sub Refresca()
        Dim SQL As String = "SELECT ID,IMAGE FROM CLIIMG WHERE EMP=@EMP AND CLI=@CLI ORDER BY ID"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mContact.Emp.Id, "@CLI", mContact.Id)

        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            .DataSource = oTb
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Img)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .AllowDrop = True
        End With

        SetContextMenu()

    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function CurrentItem() As CliImg
        Dim oCliImg As CliImg = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim ImgId As Integer = oRow.Cells(Cols.Id).Value
            oCliImg = New CliImg(ImgId)
        End If
        Return oCliImg
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCliImg As CliImg = CurrentItem()
        Dim oMenuItem As ToolStripMenuItem
        Dim BlImgExist As Boolean = (CurrentItem() IsNot Nothing)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "zoom"
            .Image = My.Resources.prismatics
            .Enabled = BlImgExist
        End With
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "importar"
            .Image = My.Resources.clip
            .Enabled = Not BlImgExist
        End With
        AddHandler oMenuItem.Click, AddressOf Do_Importar
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "sustituir"
            .Image = My.Resources.REDO
            .Enabled = BlImgExist
        End With
        AddHandler oMenuItem.Click, AddressOf Do_Importar
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom()
        Dim oCliImg As CliImg = CurrentItem()
        MsgBox("no implementat", MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Private Sub Do_Importar()
        Dim oDlg As New Windows.Forms.OpenFileDialog
        Dim oResult As Windows.Forms.DialogResult
        Dim sFilter As String = "*.jpg|*.jpg;*.jpeg|tots els arxius|*.*"
        With oDlg
            .Title = "IMPORTAR IMATTGE CONTACTE"
            .Filter = sFilter

            oResult = .ShowDialog
            Select Case oResult
                Case Windows.Forms.DialogResult.OK
                    ImportaFile(.FileName)
            End Select

        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Img
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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



    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        e.Effect = DragDropEffects.Copy
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
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell


            Try
                If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                    Dim oObj As Object = e.Data.GetData(DataFormats.FileDrop)
                    Dim sFileName As String = oObj(0)
                    ImportaFile(sFileName)

                ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                    MsgBox("ATTACHMENTS NO IMPLEMENTATS")
                Else
                    MsgBox("format desconegut")
                End If
            Catch ex As Exception
                MsgBox("Error in DragDrop function: " + ex.Message)
            End Try

        End If
        Exit Sub

    End Sub

    Private Sub ImportaFile(ByVal sFilename As String)
        Dim oCliImg As CliImg = CurrentItem()
        If oCliImg Is Nothing Then
            oCliImg = New CliImg(mContact)
        End If
        oCliImg.Image = maxisrvr.GetImgFromFileSelection("IMPORTAR IMATGE DE " & mContact.Clx)
        oCliImg.Update()

        RaiseEvent AfterUpdate(oCliImg, New System.EventArgs)
        RefreshRequest(oCliImg, New System.EventArgs)
    End Sub

End Class
