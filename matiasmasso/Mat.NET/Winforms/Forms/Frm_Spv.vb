

Public Class Frm_Spv

    Private _Spv As DTOSpv
    Private _AllowEvents As Boolean
    Private mDirtyOutSpvIn As Boolean
    Private mDirtyOutSpvOut As Boolean
    Private mDirtyAlb As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Yea
        Id
        ArtNom
        Clx
    End Enum

    Private Enum Tabs
        General
        Entrada
        Sortida
    End Enum

    Public Sub New(ByVal oSpv As DTOSpv)
        MyBase.New()
        Me.InitializeComponent()
        _Spv = oSpv
    End Sub

    Private Sub Frm_Spv_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Spv.IsNew Then
            Me.Text = "Nou full de reparació"
            If _Spv.Incidencia IsNot Nothing Then
                Me.Text = Me.Text & " per la incidencia " & _Spv.Incidencia.Num
            End If
        Else
            If FEB2.Spv.Load(_Spv, exs) Then
                Me.Text = "Full de reparació num." & _Spv.Id.ToString
                If _Spv.Incidencia IsNot Nothing Then
                    Me.Text = Me.Text & " (incidencia " & _Spv.Incidencia.Num & ")"
                End If
                ButtonDel.Enabled = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        Refresca()
        _AllowEvents = True
    End Sub

    Private Async Sub Refresca()
        Dim exs As New List(Of Exception)
        With _Spv
            Xl_Contact1.Contact = .Customer
            If .Nom > "" Or .Address IsNot Nothing Then
                CheckBoxAdr.Checked = True
                TextBoxNom.Text = .Nom
                Xl_Adr1.Load(.Address)
            Else
                TextBoxNom.Text = _Spv.Customer.NomComercialOrDefault()
                Xl_Adr1.Load(FEB2.Customer.ShippingAddressOrDefault(_Spv.Customer))
            End If
            TextBoxReg.Text = DTOSpv.TextRegistre(_Spv, DTOApp.current.lang)
            TextBoxContacto.Text = .Contacto
            TextBoxsRef.Text = .sRef
            Xl_Product1.Product = .Product
            CheckBoxSolicitaGarantia.Checked = .SolicitaGarantia
            TextBoxCliObs.Text = .ObsClient
            Xl_AmtSpvJob.Amt = .ValJob
            Xl_AmtSpvMaterial.Amt = .ValMaterial
            Xl_AmtSpvEmbalatje.Amt = .ValEmbalatje
            Xl_AmtSpvTransport.Amt = .ValPorts

            If .FchRead <= Date.MinValue Then
                CheckBoxRead.Checked = False
                TextBoxFchRead.Text = ""
            Else
                CheckBoxRead.Checked = True
                TextBoxFchRead.Text = .FchRead.ToShortDateString & " " & .FchRead.ToShortTimeString
            End If


            If .SpvIn Is Nothing Then
                If .UsrOutOfSpvIn IsNot Nothing Then
                    CheckBoxOutSpvIn.Checked = True
                    TextBoxObsOutOfSpvIn.Visible = True
                    TextBoxObsOutOfSpvIn.Text = .ObsOutOfSpvIn
                    TextBoxOutSpvIn.Text = DTOSpv.textOutSpvIn(_Spv)

                    DateTimePickerSpvIn.Enabled = False
                    TextBoxExp.Enabled = False
                    TextBoxBultos.Enabled = False
                    TextBoxObs.Enabled = False
                    TextBoxKg.Enabled = False
                    ButtonNoSpvIn.Enabled = False
                End If
            Else
                With .SpvIn
                    CheckBoxOutSpvIn.Enabled = False
                    TextBoxExp.Text = .expedicio
                    If .fch > DateTimePickerSpvIn.MinDate Then DateTimePickerSpvIn.Value = .fch
                    TextBoxBultos.Text = .Bultos
                    TextBoxKg.Text = .Kg
                    TextBoxObs.Text = .Obs
                End With

                Dim oSpvs = Await FEB2.Spvs.All(exs, .SpvIn)
                If exs.Count = 0 Then
                    Xl_Spvs1.Load(oSpvs)
                    TextBoxOutSpvIn.Visible = False
                    ButtonNoSpvIn.Enabled = False
                Else
                    UIHelper.WarnError(exs)
                End If
            End If

            If .UsrOutOfSpvOut IsNot Nothing Then
                CheckBoxOutSpvOut.Checked = True
                TextBoxObsOutOfSpvOut.Visible = True
                TextBoxObsOutOfSpvOut.Text = .ObsOutOfSpvOut
                TextBoxOutSpvOut.Text = DTOSpv.textOutSpvOut(_Spv)
            End If

            If .Delivery Is Nothing Then
                ButtonShowAlb.Enabled = False
            Else
                CheckBoxOutSpvOut.Enabled = False
                TextBoxOutSpvOut.Visible = False
                TextBoxOutSpvOut.Visible = False
                TextBoxAlbNum.Text = "albará " & .Delivery.Id & " del " & .Delivery.Fch.ToShortDateString
                CheckBoxGarantiaConfirmada.Checked = .Garantia
                TextBoxObsTecnic.Text = .ObsTecnic
                GetAmtsFromAlb(Xl_AmtJob.Amt, Xl_AmtMaterial.Amt, Xl_AmtEmbalatje.Amt, Xl_AmtTransport.Amt)
                CheckBoxFacturable.Checked = .Delivery.Facturable
                GroupBoxPressupost.Enabled = False
            End If

            If .Id = 0 Then
                Await LoadEmailsForLabel()
            Else
                ComboBoxEmailLabelTo.Visible = False
                If .LabelEmailedTo > "" Then
                    LabelEmailTo.Text = "etiqueta enviada a " & .LabelEmailedTo
                Else
                    LabelEmailTo.Text = "no consta envio de etiqueta per email"
                End If
            End If

            If .IsNew Then ButtonOk.Enabled = True
        End With
        _AllowEvents = True
    End Sub

    Private Async Function LoadEmailsForLabel() As Task
        Dim exs As New List(Of Exception)
        Dim oUsers = Await FEB2.Users.All(exs, _Spv.Customer)
        If exs.Count = 0 Then
            ComboBoxEmailLabelTo.Items.Clear()

            For Each oUser In oUsers
                ComboBoxEmailLabelTo.Items.Add(oUser.EmailAddress)
            Next
            ComboBoxEmailLabelTo.Items.Add("(altres)")
            Select Case ComboBoxEmailLabelTo.Items.Count
                Case 1
                    ComboBoxEmailLabelTo.BackColor = Color.Salmon
                Case 2
                    ComboBoxEmailLabelTo.SelectedIndex = 0
                Case Else
                    ComboBoxEmailLabelTo.SelectedIndex = 0
                    ComboBoxEmailLabelTo.BackColor = Color.Yellow
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub SetAmtsToAlb()
        Dim oSku As DTOProductSku = Nothing
        Dim oDelivery As DTODelivery = _Spv.delivery
        If oDelivery IsNot Nothing Then
            For i As Integer = oDelivery.items.Count - 1 To 0 Step -1
                oSku = oDelivery.items(i).sku
                If oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObraSinCargo)) Or
                        oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObra)) Or
                        oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.MaterialEmpleado)) Or
                        oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Embalaje)) Or
                        oSku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)) Then
                    oDelivery.items.RemoveAt(i)
                End If
            Next
        End If

        If Xl_AmtJob.Amt IsNot Nothing Then
            oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObra)
        Else
            oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObraSinCargo)
        End If
        Dim item As DTODeliveryItem = DTODeliveryItem.Factory(_Spv, oSku, 1, Xl_AmtJob.Amt)
        oDelivery.Items.Add(item)

        If Xl_AmtMaterial.Amt IsNot Nothing Then
            oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.MaterialEmpleado)
            item = DTODeliveryItem.Factory(_Spv, oSku, 1, Xl_AmtMaterial.Amt)
            oDelivery.Items.Add(item)
        End If

        If Xl_AmtEmbalatje.Amt IsNot Nothing Then
            oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Embalaje)
            item = DTODeliveryItem.Factory(_Spv, oSku, 1, Xl_AmtEmbalatje.Amt)
            oDelivery.Items.Add(item)
        End If

        If Xl_AmtTransport.Amt IsNot Nothing Then
            oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)
            item = DTODeliveryItem.Factory(_Spv, oSku, 1, Xl_AmtTransport.Amt)
            oDelivery.Items.Add(item)
        End If

    End Sub

    Private Sub GetAmtsFromAlb(ByRef oJob As DTOAmt, ByRef oMaterial As DTOAmt, ByRef oEmbalatje As DTOAmt, ByRef oTransport As DTOAmt)
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = _Spv.Delivery
        If FEB2.Delivery.Load(oDelivery, exs) Then
            For Each item In oDelivery.Items
                If item.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObraSinCargo)) Then
                    oJob = item.Import
                ElseIf item.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObra)) Then
                    oJob = item.Import
                ElseIf item.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.MaterialEmpleado)) Then
                    oMaterial = item.Import
                ElseIf item.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Embalaje)) Then
                    oEmbalatje = item.Import
                ElseIf item.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)) Then
                    oTransport = item.Import
                End If
            Next
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonShowAlb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonShowAlb.Click
        Dim oDelivery = _Spv.Delivery
        Dim oCustomer = _Spv.Customer
        Dim exs As New List(Of Exception)
        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oFrm As New Frm_AlbNew2(oDelivery)
            oFrm.Show()
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub


    Private Async Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Application.DoEvents()

        If FEB2.Contact.Load(Xl_Contact1.Customer, exs) Then
            With _Spv
                .Nom = TextBoxNom.Text
                If CheckBoxAdr.Checked Then
                    .Address = Xl_Adr1.Address
                Else
                    .Address = FEB2.Customer.ShippingAddressOrDefault(Xl_Contact1.Customer)
                End If
                .Product = Xl_Product1.Product

                .Customer = Xl_Contact1.Customer
                .SolicitaGarantia = CheckBoxSolicitaGarantia.Checked
                .ValJob = Xl_AmtSpvJob.Amt
                .ValMaterial = Xl_AmtSpvMaterial.Amt
                .ValEmbalatje = Xl_AmtSpvEmbalatje.Amt
                .ValPorts = Xl_AmtSpvTransport.Amt
                .Garantia = CheckBoxGarantiaConfirmada.Checked
                .ObsClient = TextBoxCliObs.Text
                .Contacto = TextBoxContacto.Text
                .ObsTecnic = TextBoxObsTecnic.Text
                .sRef = TextBoxsRef.Text

                If _Spv.IsNew Then
                    If ComboBoxEmailLabelTo.SelectedIndex >= 0 Then
                        Dim s As String = ComboBoxEmailLabelTo.SelectedItem.ToString
                        Do While s = "" Or s = "(altres)"
                            s = InputBox("e-mail:", "ENVIAMENT DE CORREO REPARACIO")
                        Loop
                        .LabelEmailedTo = s
                    End If

                    .UsrRegister = Current.Session.User
                End If

                If CheckBoxRead.Checked Then
                    If .FchRead > Date.MinValue Then
                    Else
                        .FchRead = Today
                    End If
                Else
                    .FchRead = Date.MinValue
                End If

                If mDirtyOutSpvIn Then
                    .UsrOutOfSpvIn = Current.Session.User
                    .FchOutOfSpvIn = Now
                    If CheckBoxOutSpvIn.Checked Then
                        .SpvIn = Nothing
                        .ObsOutOfSpvIn = TextBoxObsOutOfSpvIn.Text
                    Else
                        .SpvIn = DTOSpvIn.Factory()
                    End If
                End If
                If mDirtyOutSpvOut Then
                    If CheckBoxOutSpvOut.Checked Then
                        .Delivery = Nothing
                        .UsrOutOfSpvOut = Current.Session.User
                        .FchOutOfSpvOut = Now
                        .ObsOutOfSpvOut = TextBoxObsOutOfSpvOut.Text
                    Else
                        .UsrOutOfSpvOut = Nothing
                        .FchOutOfSpvOut = Nothing
                        .ObsOutOfSpvOut = ""
                        .Delivery = Nothing
                    End If
                End If

                If mDirtyAlb Then
                    .ValJob = Xl_AmtJob.Amt
                    .ValMaterial = Xl_AmtMaterial.Amt
                    .ValEmbalatje = Xl_AmtEmbalatje.Amt
                    .ValPorts = Xl_AmtTransport.Amt
                    If .Delivery IsNot Nothing Then
                        With .Delivery
                            .cod = DTOPurchaseOrder.Codis.reparacio
                            .mgz = DTOMgz.FromContact(Current.Session.Emp.Taller)
                            If CheckBoxAdr.Checked Then
                                .nom = TextBoxNom.Text
                                .address = Xl_Adr1.Address
                            Else
                                TextBoxNom.Text = _Spv.Customer.NomComercialOrDefault()
                                Xl_Adr1.Load(FEB2.Customer.ShippingAddressOrDefault(_Spv.Customer))
                            End If
                            .tel = _Spv.Tel
                            If CheckBoxFacturable.Checked Then
                                .facturable = True
                                .cashCod = FEB2.Customer.CcxOrMe(exs, .customer).cashCod
                            End If
                            SetAmtsToAlb()

                        End With
                        exs = New List(Of Exception)
                        Dim id As Integer = Await FEB2.Delivery.Update(exs, .Delivery)
                        If exs.Count = 0 Then
                            .Delivery.id = id
                        Else
                            UIHelper.WarnError(exs, "Error al desar l'albarà")
                        End If
                    End If
                End If

                exs = New List(Of Exception)
                Dim oSpv = Await FEB2.Spv.Update(_Spv, exs)
                If exs.Count = 0 Then
                    _Spv = oSpv
                    If _Spv.Incidencia IsNot Nothing Then
                        Dim oIncidencia = _Spv.Incidencia
                        If FEB2.Incidencia.Load(exs, oIncidencia) Then
                            With oIncidencia
                                .Description = .Description & vbCrLf & "reparacion " & _Spv.Id
                                .Spv = _Spv
                                .UsrLog.usrLastEdited = Current.Session.User
                            End With
                            oIncidencia = Await FEB2.Incidencia.Update(exs, oIncidencia)
                            If exs.Count = 0 Then
                            Else
                                UIHelper.ToggleProggressBar(Panel1, False)
                                UIHelper.WarnError(exs, "error al actualitzar la incidencia")
                            End If
                        Else
                            UIHelper.ToggleProggressBar(Panel1, False)
                            UIHelper.WarnError(exs)
                        End If
                    End If

                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Spv))

                    If _Spv.IsNew Then
                        MsgBox("nova reparació " & _Spv.Id, MsgBoxStyle.Information, "MAT.NET")

                        Dim oMailMessage = Await SpvMailMessageHelper.MailMessage(GlobalVariables.Emp, _Spv, exs)
                        If exs.Count = 0 Then
                            If Await OutlookHelper.Send(oMailMessage, exs) Then
                                Me.Close()
                            Else
                                UIHelper.ToggleProggressBar(Panel1, False)
                                UIHelper.WarnError(exs)
                            End If
                        Else
                            UIHelper.ToggleProggressBar(Panel1, False)
                            UIHelper.WarnError(exs, "error al redactar el missatge")
                        End If
                    Else
                        Me.Close()
                    End If


                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs, "error al desar la reparació")
                End If

            End With
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxOutSpvIn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOutSpvIn.CheckedChanged
        If _AllowEvents Then
            mDirtyOutSpvIn = True
            TextBoxObsOutOfSpvIn.Visible = CheckBoxOutSpvIn.Checked
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxOutSpvOut_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOutSpvOut.CheckedChanged
        If _AllowEvents Then
            mDirtyOutSpvOut = True
            TextBoxObsOutOfSpvOut.Visible = CheckBoxOutSpvOut.Checked
            SetDirty()
        End If
    End Sub


    Private Sub CheckBoxFacturable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxFacturable.CheckedChanged
        If _AllowEvents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub

    Private Sub Pressupost_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtSpvEmbalatje.AfterUpdate, Xl_AmtSpvTransport.AfterUpdate, Xl_AmtSpvMaterial.AfterUpdate, Xl_AmtSpvJob.AfterUpdate
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtTransport.AfterUpdate, Xl_AmtEmbalatje.AfterUpdate, Xl_AmtMaterial.AfterUpdate, Xl_AmtJob.AfterUpdate
        If _AllowEvents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la reparació num." & _Spv.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Spv.Delete(_Spv, exs) Then
                MsgBox("Reparació num." & _Spv.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(sender, MatEventArgs.Empty)
                Me.Close()
            Else
                MsgBox("Operació cancelada." & vbCrLf & "La reparació ja ha sortit." & vbCrLf & "Cal eliminar primer l'albará", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub CheckBoxSolicitaGarantia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxSolicitaGarantia.CheckedChanged,
    CheckBoxGarantiaConfirmada.CheckedChanged,
    TextBoxCliObs.TextChanged,
    TextBoxsRef.TextChanged,
    TextBoxContacto.TextChanged
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxAdr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAdr.CheckedChanged
        TextBoxNom.Enabled = CheckBoxAdr.Checked
        Xl_Adr1.Enabled = CheckBoxAdr.Checked
        SetDirty()
        If CheckBoxAdr.Checked And TextBoxNom.Text = "" And Xl_Adr1.Address Is Nothing Then
            TextBoxNom.Text = _Spv.Customer.NomComercialOrDefault()
            Xl_Adr1.Load(FEB2.Customer.ShippingAddressOrDefault(_Spv.Customer))
        End If
    End Sub

    Private Sub CheckBoxRead_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxRead.CheckedChanged
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub ButtonNoSpvIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNoSpvIn.Click
        _Spv.SpvIn = Nothing
        SetDirty()
        ButtonNoSpvIn.Enabled = False
    End Sub




    Private Sub Xl_Product1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Product1.AfterUpdate
        SetDirty()
    End Sub

    Private Sub TextBoxObsTecnic_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxObsTecnic.TextChanged
        If _AllowEvents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub


End Class