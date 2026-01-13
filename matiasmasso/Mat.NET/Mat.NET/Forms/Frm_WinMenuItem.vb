Public Class Frm_WinMenuItem
    Private _WinMenuItem As DTOWinMenuItem
    Private _DirtyRols As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWinMenuItem)
        MyBase.New()
        Me.InitializeComponent()
        _WinMenuItem = value
        BLL.BLLWinMenuItem.Load(_WinMenuItem)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _WinMenuItem
            If .Parent Is Nothing Then
                LabelParent.Text = "(arrel)"
            Else
                LabelParent.Text = .Parent.Nom
            End If
            TextBoxNom.Text = .Nom
            ComboBoxCod.SelectedIndex = .Cod
            TextBoxAction.Text = .Action
            TextBoxAction.Enabled = (.Cod = MaxiSrvr.MMC.Cods.Item)
            If Not .Icon Is Nothing Then Xl_ImageBig.Bitmap = .Icon
            Xl_Rols_Allowed1.Load(.Rols)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
         Xl_Rols_Allowed1.AfterUpdate, _
          TextBoxNom.TextChanged, _
           ComboBoxCod.SelectedIndexChanged, _
            Xl_ImageBig.AfterUpdate, _
             TextBoxAction.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WinMenuItem
            .Nom = TextBoxNom.Text
            .Cod = ComboBoxCod.SelectedIndex
            .Action = TextBoxAction.Text
            .Icon = Xl_ImageBig.Bitmap
            If _DirtyRols Then .Rols = Xl_Rols_Allowed1.Rols
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLWinMenuItem.Update(_WinMenuItem, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WinMenuItem))
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
            If BLL.BLLWinMenuItem.Delete(_WinMenuItem, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WinMenuItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_Rols_Allowed1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_Rols_Allowed1.AfterUpdate
        _DirtyRols = True
    End Sub
End Class


