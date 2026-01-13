Public Class Frm_PgcEpg0

    Private _Epg As DTOPgcEpg0
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPgcEpg0)
        MyBase.New()
        Me.InitializeComponent()
        _Epg = value
        BLL_PgcEpg0.Load(_Epg)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Epg
            TextBoxEsp.Text = .NomEsp
            TextBoxCat.Text = .NomCat
            TextBoxEng.Text = .NomEng
            NumericUpDownOrd.Value = .Ordinal

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged, _
         TextBoxCat.TextChanged, _
          TextBoxEng.TextChanged, _
           NumericUpDownOrd.ValueChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _Epg
            .NomEsp = TextBoxEsp.Text
            .NomCat = TextBoxCat.Text
            .NomEng = TextBoxEng.Text
            .Ordinal = NumericUpDownOrd.Value
        End With

        If BLL_PgcEpg0.Update(_Epg, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Epg))
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
            If BLL_PgcEpg0.Delete(_Epg, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Epg))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


