Public Class Frm_Cod

    Private _Cod As DTOCod
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCod)
        MyBase.New()
        Me.InitializeComponent()
        _Cod = value
    End Sub

    Private Sub Frm_Cod_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Cod.Load(exs, _Cod) Then
            With _Cod
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                TextBoxPor.Text = .Nom.Por
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Cod
            .Nom.Esp = TextBoxEsp.Text
            .Nom.Cat = TextBoxCat.Text
            .Nom.Eng = TextBoxEng.Text
            .Nom.Por = TextBoxPor.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.Cod.Update(exs, _Cod) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cod))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest codi?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.Cod.Delete(exs, _Cod) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cod))
                Me.Close()
                Else
                    UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar el codi")
            End If
            End If
        End Sub
    End Class


