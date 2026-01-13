

Public Class Xl_Cur_Old
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private mCur As DTOCur
    Private mLocked As Boolean
    Private COLOR_DEFAULT As System.Drawing.Color = System.Drawing.Color.White
    Private COLOR_FOREIGN As System.Drawing.Color = System.Drawing.Color.Yellow

    Public Property Cur() As DTOCur
        Get
            Return mCur
        End Get
        Set(ByVal Value As DTOCur)
            mCur = Value
            Display()
        End Set
    End Property

    Private Sub Display()
        If Not mCur Is Nothing Then
            TextBox1.Text = mCur.Id.ToString
            TextBox1.Tag = mCur.Id.ToString
            If mCur.Id = MaxiSrvr.DefaultCur.Id Then
                TextBox1.BackColor = COLOR_DEFAULT
            Else
                TextBox1.BackColor = COLOR_FOREIGN
            End If
        End If
    End Sub

    Public Property Locked() As Boolean
        Get
            Return mLocked
        End Get
        Set(ByVal Value As Boolean)
            mLocked = Value
            CanviarToolStripMenuItem.Enabled = Not mLocked
            If mLocked Then
                Try
                    TextBox1.BackColor = Me.BackColor ' System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control)
                Catch ex As Exception
                    TextBox1.BackColor = System.Drawing.Color.lightgray
                End Try
            Else
                TextBox1.BackColor = System.Drawing.Color.White
            End If
        End Set
    End Property

    Private Sub Xl_Cur_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        MyBase.Height = TextBox1.Height
        MyBase.Width = TextBox1.Width
    End Sub

    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        Zoom()
    End Sub

    Private Sub CanviarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CanviarToolStripMenuItem.Click
        SelectNew()
    End Sub

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        If mLocked Then
            Zoom()
        Else
            SelectNew()
        End If
    End Sub

    Private Sub Zoom()
        'root.ShowCur(mCur)
    End Sub

    Private Sub SelectNew()
        'Dim oFrm As New Frm_Curs(bll.dEFAULTS.SelectionModes.Selection, mCur)
        'AddHandler oFrm.onItemSelected, AddressOf onItemSelected
        'oFrm.Show()
    End Sub

    Private Sub onItemSelected(sender As Object, e As MatEventArgs)
        mCur = e.Argument
        Display()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(mCur))
    End Sub
End Class
