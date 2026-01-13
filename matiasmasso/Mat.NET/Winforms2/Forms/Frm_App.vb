Public Class Frm_App
    Private _App As DTOApp
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOApp)
        MyBase.New()
        Me.InitializeComponent()
        _App = value
    End Sub

    Private Sub Frm_App_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        UIHelper.LoadComboFromEnum(ComboBoxId, GetType(DTOApp.AppTypes))
        If FEB.App.Load(exs, _App) Then
            With _App
                ComboBoxId.SelectedIndex = .Id
                TextBoxNom.Text = .Nom
                TextBoxLastVersion.Text = .LastVersion
                TextBoxMinVersion.Text = .MinVersion
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         ComboBoxId.SelectedIndexChanged,
          TextBoxLastVersion.TextChanged,
           TextBoxMinVersion.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _App
            .Id = ComboBoxId.SelectedIndex
            .Nom = TextBoxNom.Text
            .LastVersion = TextBoxLastVersion.Text
            .MinVersion = TextBoxMinVersion.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.App.Update(exs, _App) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_App))
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
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.App.Delete(exs, _App) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_App))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


