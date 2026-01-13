Public Class Frm_PostComment
    Private _Value As PostComment
    Private _AllowEvents As Boolean


    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oValue As PostComment)
        MyBase.New()
        Me.InitializeComponent()
        _Value = oValue
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        With _Value
            DateTimePickerFch.Value = .Fch
            Xl_Usuari1.Load(.User)

            Dim sParentText As String = ""
            Select Case .ParentSource
                Case PostComment.ParentSources.Noticia
                    Dim oNoticia As Noticia = NoticiaLoader.Find(.Parent)
                    Xl_PostCommentParent1.Load(oNoticia)
            End Select

            TextBoxComment.Text = .Text
            If .FchApproved = Nothing Then
                CheckBoxAproved.Checked = False
                DateTimePickerAproved.Visible = False
            Else
                CheckBoxAproved.Checked = True
                DateTimePickerAproved.Visible = True
                DateTimePickerAproved.Value = .FchApproved
            End If
            If .FchDeleted = Nothing Then
                CheckBoxDeleted.Checked = False
                DateTimePickerDeleted.Visible = False
            Else
                CheckBoxDeleted.Checked = True
                DateTimePickerDeleted.Visible = True
                DateTimePickerDeleted.Value = .FchDeleted
            End If

        End With
    End Sub

    Private Sub Save()
        With _Value
            .Fch = DateTimePickerFch.Value
            .Text = TextBoxComment.Text
            If CheckBoxAproved.Checked Then
                .FchApproved = DateTimePickerAproved.Value
            Else
                .FchApproved = Nothing
            End If
            If CheckBoxDeleted.Checked Then
                .FchDeleted = DateTimePickerDeleted.Value
            Else
                .FchDeleted = Nothing
            End If
            .Update()
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Save()
        RaiseEvent AfterUpdate(_Value, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonOkAndAnswer_Click(sender As Object, e As EventArgs) Handles ButtonOkAndAnswer.Click
        CheckBoxAproved.Checked = True
        DateTimePickerAproved.Value = Now
        Save()
        Dim oAnswer As New PostComment(_Value, TextBoxAnswer.Text)
        oAnswer.Update()
        Dim exs as New List(Of exception)
        If BLL_Comment.MailToUser(oAnswer, exs) Then
            RaiseEvent AfterUpdate(_Value, EventArgs.Empty)
            Me.Close()
        Else
            MsgBox( BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePickerFch.ValueChanged, _
         TextBoxComment.TextChanged, _
          CheckBoxAproved.CheckedChanged, _
           DateTimePickerAproved.ValueChanged, _
            CheckBoxDeleted.CheckedChanged, _
             DateTimePickerDeleted.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonAnswer_Click(sender As Object, e As EventArgs) Handles ButtonAnswer.Click
        TextBoxComment.Height = TextBoxAnswer.Top - TextBoxComment.Top - 5
        ButtonAnswer.Visible = False
    End Sub
End Class
