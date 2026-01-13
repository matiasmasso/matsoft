Public Class Frm_JsonSchema

    Private _JsonSchema As DTOJsonSchema
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOJsonSchema)
        MyBase.New()
        Me.InitializeComponent()
        _JsonSchema = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.JsonSchema.Load(_JsonSchema, exs) Then
            With _JsonSchema
                TextBoxNom.Text = .Nom
                TextBoxJson.Text = .Json
                Xl_UsrLog1.Load(.UsrLog)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            TextBoxNom.TextChanged,
             TextBoxJson.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _JsonSchema
            .Nom = TextBoxNom.Text
            .Json = TextBoxJson.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.JsonSchema.Update(_JsonSchema, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_JsonSchema))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.JsonSchema.Delete(_JsonSchema, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_JsonSchema))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


