Public Class Frm_WebLogBrowse

    Private _WebLogBrowse As DTOWebLogBrowse
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWebLogBrowse)
        MyBase.New()
        Me.InitializeComponent()
        _WebLogBrowse = value
        BLL.BLLWebLogBrowse.Load(_WebLogBrowse)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _WebLogBrowse
            DateTimePicker1.Value = .Fch
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WebLogBrowse
            .Fch = DateTimePicker1.Value
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLWebLogBrowse.Update(_WebLogBrowse, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebLogBrowse))
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
            If BLL.BLLWebLogBrowse.Delete(_WebLogBrowse, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebLogBrowse))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


