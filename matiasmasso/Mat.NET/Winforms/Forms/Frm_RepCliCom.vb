

Public Class Frm_RepCliCom

    Private _RepCliCom As DTORepCliCom
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRepCliCom As DTORepCliCom)
        MyBase.New()
        Me.InitializeComponent()
        _RepCliCom = oRepCliCom
    End Sub

    Private Sub Frm_RepCliCom_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.RepCliCom.Load(exs, _RepCliCom) Then
            Refresca()
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Refresca()
        With _RepCliCom
            TextBoxRep.Text = .Rep.NickName
            Xl_Contact1.Contact = .Customer
            DateTimePickerFch.Value = .Fch
            Xl_Contact1.Contact = .Customer
            ComboBoxComCod.SelectedIndex = .ComCod
            TextBoxObs.Text = .Obs

            If .IsNew Then
                LabelUsr.Text = "Nou registre creat per " & DTOUser.NicknameOrElse(.UsrCreated)
            Else
                LabelUsr.Text = String.Format("Creat per {0} el {1:dd/MM/yy} a les {1:HH:mm}", DTOUser.NicknameOrElse(.UsrCreated), .FchCreated)
            End If

            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Contact1.AfterUpdate,
          DateTimePickerFch.ValueChanged,
           Xl_Contact1.AfterUpdate,
            ComboBoxComCod.SelectedIndexChanged,
             TextBoxObs.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _RepCliCom
            .Fch = DateTimePickerFch.Value
            .Customer = New DTOCustomer(Xl_Contact1.Contact.Guid)
            .ComCod = ComboBoxComCod.SelectedIndex
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.RepCliCom.Update(exs, _RepCliCom) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepCliCom))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.RepCliCom.Delete(exs, _RepCliCom) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class