Public Class Frm_Location
    Private _Location As DTOLocation
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oLocation As DTOLocation)
        MyBase.New()
        Me.InitializeComponent()
        _Location = oLocation
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        BLL.BLLLocation.Load(_Location)
        With _Location
            TextBoxZona.Text = BLL.BLLZona.FullNom(_Location.Zona, BLL.BLLApp.Lang)
            TextBoxLocation.Text = .Nom
            If .IsNew Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxLocation.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Location
            .Nom = TextBoxLocation.Text
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLLocation.Update(_Location, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Location))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar població")
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If BLL.BLLLocation.Delete(_Location, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Location))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar població")
        End If
    End Sub

End Class