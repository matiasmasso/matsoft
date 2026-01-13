Public Class Frm_DefaultImage
    Private _allowEvents As Boolean

    Private Sub Frm_DefaultImage_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBoxId.Items.AddRange([Enum].GetNames(GetType(DTO.Defaults.ImgTypes)))
        _allowEvents = True
    End Sub

    Private Function CurrentType() As DTO.Defaults.ImgTypes
        Dim retval As DTO.Defaults.ImgTypes = DTO.Defaults.ImgTypes.NotSet
        If ComboBoxId.SelectedIndex >= 0 Then
            retval = [Enum].Parse(GetType(DTO.Defaults.ImgTypes), ComboBoxId.Text)
        End If
        Return retval
    End Function

    Private Sub ComboBoxId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxId.SelectedIndexChanged
        If _allowEvents Then
            Dim oType As DTO.Defaults.ImgTypes = CurrentType()
            Dim oImage As Image = BLL.BLLDefaultImage.Find(oType)
            If oImage Is Nothing Then
                ButtonDel.Enabled = False
            Else
                Xl_Image1.Bitmap = oImage
                ButtonDel.Enabled = True
            End If
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oType As DTO.Defaults.ImgTypes = CurrentType()
        Dim oImage As Image = Xl_Image1.Bitmap
        If oType > DTO.Defaults.ImgTypes.NotSet And oImage IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If BLL.BLLDefaultImage.Update(oType, oImage, exs) Then
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al desar la imatge")
            End If
        End If
    End Sub

    Private Sub Xl_Image1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta imatge?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim oType As DTO.Defaults.ImgTypes = CurrentType()
            Dim oImage As Image = BLL.BLLDefaultImage.Find(oType)
            Dim exs As New List(Of Exception)
            If BLL.BLLDefaultImage.Update(oType, oImage, exs) Then
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al desar la imatge")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class