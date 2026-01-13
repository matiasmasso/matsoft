Public Class Frm_Plantilla
    Private _Plantilla As DTOPlantilla
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPlantilla)
        MyBase.New()
        Me.InitializeComponent()
        _Plantilla = value
    End Sub

    Private Async Sub Frm_Plantilla_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Plantilla.Load(exs, _Plantilla) Then
            With _Plantilla
                If .DocFile IsNot Nothing Then
                    TextBoxNom.Text = .DocFile.Nom
                    Await Xl_DocFile1.Load(.DocFile)
                End If
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
        TextBoxNom.TextChanged,
         Xl_DocFile1.AfterUpdate
        If _AllowEvents And Xl_DocFile1.Value IsNot Nothing Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Plantilla
            .DocFile = Xl_DocFile1.Value
            .DocFile.Nom = TextBoxNom.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Plantilla.Upload(exs, _Plantilla, Current.Session.Emp) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Plantilla))
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
            If Await FEB.Plantilla.Delete(exs, _Plantilla) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Plantilla))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


