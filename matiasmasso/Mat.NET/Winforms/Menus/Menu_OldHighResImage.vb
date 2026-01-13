Public Class Menu_OldHighResImage
    Private _HighResImage As HighResImage

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oHighResImage As HighResImage)
        MyBase.New()
        _HighResImage = oHighResImage
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_CopyLink()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_OldHighResImage(_HighResImage)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, _HighResImage.Url)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


