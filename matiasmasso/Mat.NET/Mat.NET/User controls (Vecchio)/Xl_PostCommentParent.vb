Public Class Xl_PostCommentParent
    Private _Noticia As Noticia



    Public ReadOnly Property Value As Noticia
        Get
            Return _Noticia
        End Get
    End Property

    Public Shadows Sub Load(value As Noticia)
        _Noticia = Value
        If value IsNot Nothing Then
            refresca()
        End If
    End Sub

    Private Sub refresca()
        TextBox1.Text = _Noticia.Fch.ToString & " - " & _Noticia.Title
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu As New Menu_Noticia(_Noticia)
        oContextMenu.Items.AddRange(oMenu.Range)
        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick
        Dim oFrm As New Frm_Noticia(_Noticia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class

