Public Class Frm_Menu
    Private _Menu As DTOMenu
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oMenu As DTOMenu)
        MyBase.New()
        Me.InitializeComponent()
        _Menu = oMenu
    End Sub

    Private Sub Frm_Menu_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = BLL.BLLMenu.Nom(_Menu, Current.Session.Lang)
        With _Menu
            TextBoxNomEsp.Text = .NomEsp
            TextBoxNomCat.Text = .NomCat
            TextBoxNomEng.Text = .NomEng
            TextBoxAction.Text = .Action
            TextBoxParent.Text = BLL.BLLMenu.Nom(.Parent, Current.Session.Lang)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
    TextBoxNomEsp.TextChanged, _
     TextBoxNomCat.TextChanged, _
      TextBoxNomEng.TextChanged, _
       TextBoxAction.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Menu
            .NomEsp = TextBoxNomEsp.Text
            .NomCat = TextBoxNomCat.Text
            .NomEng = TextBoxNomEng.Text
            .Action = TextBoxAction.Text
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLMenu.Update(_Menu, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Menu))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLMenu.Delete(_Menu, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Menu))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class