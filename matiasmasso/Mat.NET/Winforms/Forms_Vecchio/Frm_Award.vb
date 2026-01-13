Public Class Frm_Award
    Private _Award As Award
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As Award)
        MyBase.New()
        Me.InitializeComponent()
        _Award = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Award
            Xl_Contact1.Contact = .Organisation
            TextBoxNom.Text = .ToString
            TextBoxYear.Text = .Year
            TextBoxRating.Text = .Rating
            TextBoxUrl.Text = .Url
            Xl_LookupProduct1.Product = .Product
            Xl_Image1.Bitmap = .Image
            If .Caduca <> Nothing Then
                CheckBoxCaduca.Checked = True
                DateTimePickerCaduca.Visible = True
                DateTimePickerCaduca.Value = .Caduca
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact1.AfterUpdate, _
        TextBoxNom.TextChanged, _
         TextBoxYear.TextChanged, _
          TextBoxRating.TextChanged, _
          TextBoxUrl.TextChanged, _
           Xl_LookupProduct1.AfterUpdate, _
           CheckBoxCaduca.CheckedChanged, _
            DateTimePickerCaduca.ValueChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Award
            .Organisation = Xl_Contact1.Contact
            .Nom = TextBoxNom.Text
            .Year = TextBoxYear.Text
            .Rating = TextBoxRating.Text
            .Url = TextBoxUrl.Text
            .Image = Xl_Image1.Bitmap
            .Product = Xl_LookupProduct1.Product
            If CheckBoxCaduca.Checked Then
                .Caduca = DateTimePickerCaduca.Value
            Else
                .Caduca = Nothing
            End If
        End With

        Dim exs As New List(Of exception)
        If AwardLoader.Update(_Award, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Award))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar el premi")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Awardloader.Delete(_Award, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Award))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar el premi")
            End If
        End If
    End Sub
End Class


