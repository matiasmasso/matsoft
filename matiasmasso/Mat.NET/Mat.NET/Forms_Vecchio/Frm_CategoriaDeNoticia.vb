Public Class Frm_CategoriaDeNoticia

    Private _CategoriaDeNoticia As CategoriaDeNoticia
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As CategoriaDeNoticia)
        MyBase.New()
        Me.InitializeComponent()
        _CategoriaDeNoticia = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _CategoriaDeNoticia
            TextBoxNom.Text = .Nom
            TextBoxExcerpt.Text = .Excerpt
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _CategoriaDeNoticia
            .Nom = TextBoxNom.Text
            .Excerpt = TextBoxExcerpt.Text
        End With
        If CategoriaDeNoticiaLoader.Update(_CategoriaDeNoticia, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CategoriaDeNoticia))
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
            If CategoriaDeNoticiaLoader.Delete(_CategoriaDeNoticia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CategoriaDeNoticia))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TextBoxExcerpt_TextChanged(sender As Object, e As EventArgs) Handles TextBoxExcerpt.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
            Dim len As Integer = 160 - TextBoxExcerpt.Text.Length
            LabelDsc.Text = "Descripció (queden " & len & " caracters)"
        End If
    End Sub
End Class


