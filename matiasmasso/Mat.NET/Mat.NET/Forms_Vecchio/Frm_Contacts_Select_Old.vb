

Public Class Frm_Contacts_Select_Old
    Private mIdx As Integer
    Private mContactKeys As ContactKeys
    Private mTb As DataTable
    Private mStartPos As Point
    Private mArrastrando As Boolean
    Public Event SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Ico
        Nom
        Ex
        Codi
        Botiga
    End Enum

    Public ReadOnly Property ContactKey() As ContactKey
        Get
            Return CurrentContactKey()
        End Get
    End Property

    Public WriteOnly Property ContactKeys() As ContactKeys
        Set(ByVal Value As ContactKeys)
            mContactKeys = Value
            LoadGrid()
        End Set
    End Property

    Private Function CurrentContactKey() As ContactKey
        Dim oContactKey As ContactKey = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Return mContactKeys(oRow.Index)
    End Function

    Private Sub LoadGrid()
        Dim oKeys As MaxiSrvr.ContactKeys = mContactKeys
        Dim oGraphics As Graphics = DataGridView1.CreateGraphics()
        mTb = New DataTable("KEYS")
        mTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        mTb.Columns.Add("TEXT", System.Type.GetType("System.String"))
        mTb.Columns.Add("EX", System.Type.GetType("System.Boolean"))
        mTb.Columns.Add("COD", System.Type.GetType("System.Int32"))
        mTb.Columns.Add("BOTIGA", System.Type.GetType("System.Boolean"))
        Dim oKey As MaxiSrvr.ContactKey
        Dim oRow As DataRow
        Dim iFirstObsoleto As Integer = -1
        Dim idx As Integer = 0
        Dim iWidth As Integer = 0
        Dim iMaxWidth As Integer = 0
        For Each oKey In oKeys
            iWidth = DataGridViewCell.MeasureTextWidth(oGraphics, oKey.Clx, DataGridView1.Font, DataGridView1.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
            oRow = mTb.NewRow()
            oRow(Cols.Nom) = oKey.Clx
            oRow(Cols.Ex) = oKey.Obsoleto
            oRow(Cols.Codi) = oKey.Codi
            oRow(Cols.Botiga) = oKey.Botiga
            mTb.Rows.Add(oRow)

            If iFirstObsoleto = -1 Then
                If oKey.Obsoleto Then
                    iFirstObsoleto = idx
                End If
            End If
            idx += 1

        Next

        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If mTb.Rows.Count <= MaxVisibleRows Then
            VisibleRows = mTb.Rows.Count
        Else
            If iFirstObsoleto >= 3 And iFirstObsoleto <= MaxVisibleRows Then
                VisibleRows = iFirstObsoleto - 1
            Else
                VisibleRows = MaxVisibleRows
            End If
        End If

        With DataGridView1
            .DataSource = mTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            With .Columns(Cols.Ico)
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Ex)
                .Visible = False
            End With
            With .Columns(Cols.Codi)
                .Visible = False
            End With
            With .Columns(Cols.Botiga)
                .Visible = False
            End With
        End With

        Me.Height = DataGridView1.RowTemplate.Height * VisibleRows + 3
        Dim FormWidth As Integer = Me.Width
        Dim ClxWidth As Integer = DataGridView1.Columns(Cols.Nom).Width
        Dim NewFormWidth As Integer = FormWidth + iMaxWidth - ClxWidth
        Me.Width = NewFormWidth
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Botiga).Value Then
                    e.Value = My.Resources.Basket
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Ex).Value Then
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
                e.Handled = True
        End Select
    End Sub

    Private Sub Sortida()
        RaiseEvent SelectedItemChanged(CurrentContactKey, New System.EventArgs)
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