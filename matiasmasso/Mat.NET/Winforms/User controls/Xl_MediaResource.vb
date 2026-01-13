Public Class Xl_MediaResource
    Inherits PictureBox

    Private _MediaResource As DTOMediaResource

    Public Shadows Sub Load(oMediaResource As DTOMediaResource)
        _MediaResource = oMediaResource
        MyBase.Image = _MediaResource.Thumbnail
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _MediaResource IsNot Nothing Then
            'Dim oMenu_BancPool As New Menu_BancPool(SelectedItems.First)
            'AddHandler oMenu_BancPool.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_BancPool.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("importar", Nothing, AddressOf Do_Import)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Import()
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Title = "Importar nou recurs"
            If .ShowDialog Then
                Dim exs As New List(Of Exception)
                If BLLMediaResource.FromFile(.FileName) Then
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub
End Class
