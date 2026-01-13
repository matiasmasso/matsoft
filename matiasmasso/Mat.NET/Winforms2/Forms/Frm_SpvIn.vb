Public Class Frm_SpvIn

    Private _SpvIn As DTOSpvIn
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSpvIn)
        MyBase.New()
        Me.InitializeComponent()
        _SpvIn = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.SpvIn.Load(_SpvIn, exs) Then
            With _SpvIn
                Me.Text = String.Format("Entrada {0} de mercancía per reparar", .Id)
                TextBoxExpedicio.Text = .Expedicio
                TextBoxObs.Text = .Obs
                DateTimePicker1.Value = .Fch

                Dim oSpvs = Await FEB.Spvs.All(exs, _SpvIn)
                If exs.Count = 0 Then
                    Xl_Spvs1.Load(oSpvs)
                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew
                Else
                    UIHelper.WarnError(exs)
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxExpedicio.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SpvIn
            .Expedicio = TextBoxExpedicio.Text
            .Obs = TextBoxObs.Text
            .Fch = DateTimePicker1.Value
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim id = Await FEB.SpvIn.Update(_SpvIn, exs)
        UIHelper.ToggleProggressBar(Panel1, False)

        If exs.Count = 0 Then
            _SpvIn.Id = id
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SpvIn))
            Me.Close()
        Else
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
            If Await FEB.SpvIn.Delete(_SpvIn, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SpvIn))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


