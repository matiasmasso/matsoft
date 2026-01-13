Public Class Frm_DocfileSrc
    Private _DocfileSrc As DTODocFileSrc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTODocFileSrc)
        MyBase.New()
        Me.InitializeComponent()
        _DocfileSrc = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.DocFileSrc.Load(_DocfileSrc, exs) Then
            With _DocfileSrc
                If .Docfile Is Nothing Then
                    TextBoxNom.Text = .Nom
                Else
                    If .Docfile.Fch > DateTimePicker1.MinDate Then
                        DateTimePicker1.Value = .Docfile.Fch
                    End If
                    TextBoxNom.Text = .Docfile.Nom
                    Await Xl_DocFile1.Load(.Docfile)
                End If
                ButtonOk.Enabled = True ' .IsNew
                ButtonDel.Enabled = True 'Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _DocfileSrc
            If Xl_DocFile1.IsDirty Then
                .Docfile = Xl_DocFile1.Value
            End If
            If .Docfile IsNot Nothing Then
                .Docfile.nom = TextBoxNom.Text
                .Docfile.fch = DateTimePicker1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.DocFileSrc.Update(_DocfileSrc, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocfileSrc))
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
            If Await FEB.DocFileSrc.Delete(_DocfileSrc, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocfileSrc))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

End Class