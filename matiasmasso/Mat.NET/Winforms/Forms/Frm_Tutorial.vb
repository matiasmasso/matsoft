Public Class Frm_Tutorial

    Private _Tutorial As DTOTutorial
    Private _AllRols As List(Of DTORol)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOTutorial)
        MyBase.New()
        Me.InitializeComponent()
        _Tutorial = value
        _AllRols = BLLRols.All(Current.Session.Lang)
        BLL.BLLTutorial.Load(_Tutorial)

    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Tutorial
            If .Parent IsNot Nothing Then
                TextBoxParent.Text = .Parent.Guid.ToString
            End If
            DateTimePicker1.Value = .Fch
            TextBoxTitle.Text = .Title
            TextBoxExcerpt.Text = .Excerpt
            TextBoxUrl.Text = .YouTubeId

            Xl_RolsAllowed1.Load(_AllRols, .Rols)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxParent.TextChanged,
         DateTimePicker1.ValueChanged,
          TextBoxTitle.TextChanged,
           TextBoxExcerpt.TextChanged,
            TextBoxUrl.TextChanged,
             Xl_RolsAllowed1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Tutorial
            .Parent = New DTOBaseGuid(New Guid(TextBoxParent.Text))
            .Title = TextBoxTitle.Text
            .Fch = DateTimePicker1.Value
            .Excerpt = TextBoxExcerpt.Text
            .YouTubeId = TextBoxUrl.Text
            .Rols = Xl_RolsAllowed1.CheckedValues
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLTutorial.Update(_Tutorial, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tutorial))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLTutorial.Delete(_Tutorial, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tutorial))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class