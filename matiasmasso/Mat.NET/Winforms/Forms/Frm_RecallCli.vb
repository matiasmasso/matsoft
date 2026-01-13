Public Class Frm_RecallCli
    Private _RecallCli As DTORecallCli
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTORecallCli)
        MyBase.New()
        Me.InitializeComponent()
        _RecallCli = value
        _RecallCli.IsLoaded = False
    End Sub

    Private Sub Frm_RecallCli_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.RecallCli.Load(exs, _RecallCli) Then
            refresca()

            With _RecallCli
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        With _RecallCli
            TextBoxRecall.Text = .Recall.Nom & " del " & .Recall.Fch.ToShortDateString
            TextBoxFchCreated.Text = Format(.UsrLog.FchCreated, "dd/MM/yy HH:mm")
            TextBoxContactNom.Text = .ContactNom
            TextBoxContactEmail.Text = .ContactEmail
            TextBoxContactTel.Text = .ContactTel
            TextBoxCustomer.Text = .Customer.NomComercialOrDefault
            TextBoxAddress.Text = .Address
            TextBoxZip.Text = .Zip
            TextBoxLocation.Text = .Location
            Xl_LookupCountry1.Country = .Country
            If .FchVivace = Nothing Then
                TextBoxFchVivace.Visible = False
            Else
                CheckBoxFchVivace.Checked = True
                TextBoxFchVivace.Text = .FchVivace
            End If
            TextBoxRegMuelle.Text = .RegMuelle
            If .PurchaseOrder IsNot Nothing Then
                CheckBoxPdc.Checked = True
                Xl_LookupPurchaseOrder1.Visible = True
                Xl_LookupPurchaseOrder1.PurchaseOrder = .PurchaseOrder
            End If
            If .Delivery IsNot Nothing Then
                CheckBoxAlb.Checked = True
                Xl_LookupDelivery1.Visible = True
                Xl_LookupDelivery1.Delivery = .Delivery
            End If

            Xl_RecallProducts1.Load(_RecallCli.Products)
        End With
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
         TextBoxContactNom.TextChanged,
          TextBoxContactEmail.TextChanged,
           TextBoxContactTel.TextChanged,
            TextBoxAddress.TextChanged,
             TextBoxZip.TextChanged,
              TextBoxLocation.TextChanged,
               Xl_LookupCountry1.AfterUpdate,
                 TextBoxRegMuelle.TextChanged,
                  Xl_LookupPurchaseOrder1.AfterUpdate,
                   Xl_LookupDelivery1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _RecallCli
            .ContactNom = TextBoxContactNom.Text
            .ContactEmail = TextBoxContactEmail.Text
            .ContactTel = TextBoxContactTel.Text
            .Address = TextBoxAddress.Text
            .Zip = TextBoxZip.Text
            .Location = TextBoxLocation.Text
            .Country = Xl_LookupCountry1.Country
            If CheckBoxFchVivace.Checked Then
                If .FchVivace = Nothing Then
                    .FchVivace = Now
                End If
            Else
                .FchVivace = Nothing
            End If
            .RegMuelle = TextBoxRegMuelle.Text
            If CheckBoxPdc.Checked Then
                .PurchaseOrder = Xl_LookupPurchaseOrder1.PurchaseOrder
            Else
                .PurchaseOrder = Nothing
            End If
            If CheckBoxAlb.Checked Then
                .Delivery = Xl_LookupDelivery1.Delivery
            Else
                .Delivery = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.RecallCli.Update(exs, _RecallCli) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RecallCli))
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
            If Await FEB2.RecallCli.Delete(exs, _RecallCli) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_RecallCli))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        refresca()
    End Sub

    Private Sub Xl_RecallProducts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RecallProducts1.RequestToRefresh
        refresca()
    End Sub

    Private Sub CheckBoxFchVivace_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchVivace.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
            TextBoxFchVivace.Visible = CheckBoxFchVivace.Checked
            If CheckBoxFchVivace.Checked Then
                If _RecallCli.FchVivace = Nothing Then
                    TextBoxFchVivace.Text = Format(Now, "dd/MM/yy HH:mm")
                Else
                    TextBoxFchVivace.Text = Format(_RecallCli.FchVivace, "dd/MM/yy HH:mm")
                End If
            End If
        End If
    End Sub


    Private Sub CheckBoxPdc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPdc.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
            Xl_LookupPurchaseOrder1.Visible = CheckBoxPdc.Checked
        End If
    End Sub

    Private Sub CheckBoxAlb_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAlb.CheckedChanged
        If _AllowEvents Then
            If _AllowEvents Then
                ButtonOk.Enabled = True
                Xl_LookupDelivery1.Visible = CheckBoxAlb.Checked
            End If
        End If
    End Sub
End Class


