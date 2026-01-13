

Public Class frm_arts_select
    Private mIdx As Integer
    Private mArts As Arts
    Private mTb As DataTable
    Private mArt As Art
    Private mEmp as DTOEmp

    Private mStartPos As Point
    Private mArrastrando As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    'Public Event SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Img
        Nom
        Epg
        Stk
        Color
        Obsoleto
    End Enum

    Public ReadOnly Property Art() As Art
        Get
            Return mArt
        End Get
    End Property

    Public WriteOnly Property Arts() As Arts
        Set(ByVal Value As Arts)
            mArts = Value
            memp = mArts(0).Emp
            mTb = CreateTable(mArts)
            With DataGridView1
                .DataSource = mTb
                .ColumnHeadersVisible = False
                .RowHeadersVisible = False
                .RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True
                .RowTemplate.Height = 48
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                With .Columns(Cols.Id)
                    .Visible = False
                End With
                With .Columns(Cols.Img)
                    .Width = 42
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With
                With .Columns(Cols.Nom)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                With .Columns(Cols.Epg)
                    .DefaultCellStyle.SelectionForeColor = Color.Black
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With
                With .Columns(Cols.Stk)
                    '.DefaultCellStyle.SelectionBackColor = Color.Transparent
                    .DefaultCellStyle.SelectionForeColor = Color.Black
                    .Width = 30
                    .CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Color)
                    .Visible = False
                End With
                With .Columns(Cols.Obsoleto)
                    .Visible = False
                End With
            End With


        End Set
    End Property

 
    Private Function CreateTable(ByVal oKeys As MaxiSrvr.Arts) As DataTable
        Dim oMgz As Mgz = New Mgz(BLL.BLLApp.Mgz.Guid)
        Dim oTb As New DataTable("KEYS")
        oTb.Columns.Add("ID", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("IMG", System.Type.GetType("System.Byte[]"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        oTb.Columns.Add("EPG", System.Type.GetType("System.String"))
        oTb.Columns.Add("STK", System.Type.GetType("System.String"))
        oTb.Columns.Add("COLOR", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("OBSOLETO", System.Type.GetType("System.Int32"))
        Dim oArt As MaxiSrvr.Art
        Dim oRow As DataRow
        Dim oColor As System.Drawing.Color
        Dim iStk As Integer
        Dim iPn2 As Integer
        Dim oImg As Image
        Dim oArtStream As IO.MemoryStream
        Dim iFirstObsoleto As Integer = -1
        Dim Idx As Integer = 0

        For Each oArt In mArts
            iStk = oArt.Stk
            iPn2 = oArt.Pn2
            oColor = oArt.BackColor(iStk, iPn2)


            oImg = maxisrvr.GetThumbnail(oArt.Image, 48, 48)
            If oImg Is Nothing Then oImg = My.Resources.empty
            oArtStream = New IO.MemoryStream
            oImg.Save(oArtStream, System.Drawing.Imaging.ImageFormat.Jpeg)

            oRow = oTb.NewRow()
            oRow(Cols.Id) = oArt.Id
            oRow(Cols.Img) = oArtStream.ToArray
            'oRow(Cols.Nom) = oArt.Stp.Nom & vbCrLf & oArt.NomCurt
            oRow(Cols.Nom) = oArt.Id & vbCrLf & oArt.Nom_ESP
            oRow(Cols.Epg) = "stock:" & vbCrLf & "pendents:"
            oRow(Cols.Stk) = iStk & vbCrLf & iPn2
            oRow(Cols.Color) = oColor.ToArgb
            oRow(Cols.Obsoleto) = IIf(oArt.Obsoleto, 1, 0)
            oTb.Rows.Add(oRow)

            If iFirstObsoleto = -1 Then
                If oArt.Obsoleto Then
                    iFirstObsoleto = Idx
                End If
            End If
            Idx += 1
        Next

        Dim MaxVisibleRows As Integer = 7
        Dim VisibleRows As Integer = 0
        If oTb.Rows.Count <= MaxVisibleRows Then
            VisibleRows = oTb.Rows.Count
        Else
            If iFirstObsoleto >= 3 And iFirstObsoleto <= MaxVisibleRows Then
                VisibleRows = iFirstObsoleto - 1
            Else
                VisibleRows = MaxVisibleRows
            End If
        End If
        Me.Height = DataGridView1.Top + (48 + 2) * VisibleRows '+ 38


        Return oTb
    End Function

   

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Epg, Cols.Stk
                Dim oBackColor As Color = Color.FromArgb(mTb.Rows(e.RowIndex)(Cols.Color))
                e.CellStyle.BackColor = oBackColor
                ' e.CellStyle.SelectionBackColor() = oBackColor
                e.CellStyle.SelectionBackColor() = e.CellStyle.BackColor
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Obsoleto).Value = 1 Then
                    e.CellStyle.BackColor = Color.LightGray
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Sortida()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Sortida()
        End Select
    End Sub

    Private Sub Sortida()
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim ArtId As Integer = oRow.Cells(Cols.Id).Value
        mArt = MaxiSrvr.Art.FromNum(mEmp, ArtId)
        RaiseEvent AfterUpdate(mArt, New System.EventArgs)
        Me.Close()
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        mArrastrando = True
        mStartPos = New Point(e.X, e.Y)
        Me.Cursor = Cursors.IBeam
    End Sub


    Private Sub DataGridView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseUp
        If mArrastrando Then
            Dim OldPoint As Point = Me.Location
            Dim NewLeft As Integer = OldPoint.X + (e.X - mStartPos.X)
            Dim NewTop As Integer = OldPoint.Y + (e.Y - mStartPos.Y)
            Dim NewPoint As New Point(NewLeft, NewTop)
            Me.Location = NewPoint
            mStartPos = New Point(e.X, e.Y)
            Me.Cursor = Cursors.Default
            mArrastrando = False
        End If
    End Sub
End Class