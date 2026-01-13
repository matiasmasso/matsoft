Public Class Frm_Banner

    Private _Banner As DTOBanner
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBanner)
        MyBase.New()
        Me.InitializeComponent()
        _Banner = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Banner
            TextBoxNom.Text = .Nom
            TextBoxNavigateTo.Text = .NavigateTo
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo <> Nothing Then
                CheckBoxFchTo.Checked = True
                DateTimePickerFchTo.Value = .FchTo
            End If

            Xl_Image1.Bitmap = .Image
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged, _
         TextBoxNavigateTo.TextChanged, _
         DateTimePickerFchFrom.ValueChanged, _
          DateTimePickerFchTo.ValueChanged, _
           CheckBoxFchTo.CheckedChanged, _
            Xl_Image1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _Banner
            .Nom = TextBoxNom.Text
            .NavigateTo = TextBoxNavigateTo.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            End If
            .Image = Xl_Image1.Bitmap
        End With
        If BannerLoader.Update(_Banner, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Banner))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BannerLoader.Delete(_Banner, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Banner))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


