

Public Class Xl_Art
    Private mArt As Art
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Art() As Art
        Get
            Return mArt
        End Get
        Set(ByVal value As Art)
            If value IsNot Nothing Then
                RefreshRequest(value, New System.EventArgs)
            End If
        End Set
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If mArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(mArt)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

 
    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Zoom()
    End Sub

    Private Function IsDirty() As Boolean
        If mArt Is Nothing Then
            Return TextBox1.Text > ""
        Else
            Return TextBox1.Text <> mArt.Nom_ESP
        End If
    End Function

    Private Sub Zoom()
        Dim oFrm As New Frm_Art(mArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Dim sKey As String = TextBox1.Text

        Select Case e.KeyCode
            Case Keys.Tab
                If Not IsDirty() Then Exit Sub
            Case Keys.Enter
                If Not IsDirty() Then
                    Zoom()
                    Exit Sub
                End If
            Case Else
                Exit Sub
        End Select


        If sKey = "" Then
            mArt = Nothing
            ArtFound(mArt, New System.EventArgs)
        Else
            Dim oSku As ProductSku = Finder.FindSku(BLL.BLLApp.Mgz, sKey)
            If oSku Is Nothing Then
                mArt = Nothing
            Else
                mArt = New Art(oSku.Guid)
            End If
        End If
    End Sub

    Private Sub ArtFound(ByVal sender As Object, ByVal e As System.EventArgs)
        RefreshRequest(sender, e)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mArt = CType(sender, Art)
        If mArt Is Nothing Then
            TextBox1.Text = ""
        Else
            TextBox1.Text = mArt.Nom_ESP
        End If
        SetContextMenu()
    End Sub


End Class
