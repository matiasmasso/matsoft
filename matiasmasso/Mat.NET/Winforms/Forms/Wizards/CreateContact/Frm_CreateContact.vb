Public Class Frm_CreateContact
    Inherits Frm__Wizard

    Private _Contact As DTOContact
    Private _WizardMode As WizardSteps
    Private _Incoterms As List(Of DTOIncoterm)
    'Private _exs As List(Of Exception)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum WizardSteps
        Channel
        Standard
        Customer
        Proveidor
        Rep
        Staff
        Tels
        Finish
    End Enum

    Public Sub New()
        MyBase.New
        _Contact = DTOContact.Factory(Current.Session.Emp)
        SetProperties(iTabsCount:=4)
    End Sub

    Private Async Sub Frm__Wizard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Incoterms = Await FEB2.Incoterms.All(exs)
        If exs.Count > 0 Then UIHelper.WarnError(exs)

        LoadStepChannel()
    End Sub


    Private Sub loadNextStep(sender As Object, e As MatEventArgs) Handles MyBase.requestForNextStep
        Dim exs As New List(Of Exception)
        Dim oCallingStep As IWizardStep = e.Argument
        Select Case oCallingStep.WizardStep
            Case WizardSteps.Channel
                Dim oXl As Xl_CreateContact_StepChannel = oCallingStep
                Dim oContactClass As DTOContactClass = oXl.ContactClass
                _Contact.ContactClass = oContactClass

                If oContactClass.DistributionChannel Is Nothing Then
                    If oContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Staff)) Then
                        _Contact = DTOStaff.FromContact(_Contact)
                        LoadStepStaff()
                    ElseIf oContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Rep)) Then
                        _Contact = DTORep.FromContact(_Contact)
                        _Contact.IsNew = True
                        LoadStepRep()
                    ElseIf oContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Proveidor)) Then
                        _Contact = DTOProveidor.FromContact(_Contact)
                        _Contact.IsNew = True
                        LoadStepProveidor()
                    Else
                        LoadStepStandard()
                    End If
                Else
                    _Contact = FEB2.Customer.Factory(exs, _Contact)
                    If exs.Count = 0 Then
                        LoadStepCustomer()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If

            Case WizardSteps.Standard
                LoadContactFromStep(_Contact, oCallingStep)
                LoadStepTels()
            Case WizardSteps.Customer
                LoadCustomerFromStep(_Contact, oCallingStep)
                LoadStepTels()
            Case WizardSteps.Proveidor
                LoadProveidorFromStep(_Contact, oCallingStep)
                LoadStepTels()
            Case WizardSteps.Staff
                LoadStaffFromStep(_Contact, oCallingStep)
                LoadStepTels()
            Case WizardSteps.Rep
                LoadRepFromStep(_Contact, oCallingStep)
                LoadStepTels()

            Case WizardSteps.Tels
                Dim oXl As Xl_CreateContact_StepTels = oCallingStep
                With _Contact
                    .Tels = oXl.Tels
                    .Emails = oXl.Emails
                End With
                LoadStepFinish()
        End Select
    End Sub

    Private Sub loadPreviousStep(sender As Object, e As MatEventArgs) Handles MyBase.requestForPreviousStep
        Dim oCallingStep As IWizardStep = e.Argument

        Select Case oCallingStep.WizardStep
            Case WizardSteps.Standard
                LoadContactFromStep(_Contact, oCallingStep)
                LoadStepChannel()
            Case WizardSteps.Customer
                LoadCustomerFromStep(_Contact, oCallingStep)
                LoadStepChannel()
            Case WizardSteps.Proveidor
                LoadProveidorFromStep(_Contact, oCallingStep)
                LoadStepChannel()
            Case WizardSteps.Staff
                LoadStaffFromStep(_Contact, oCallingStep)
                LoadStepChannel()
            Case WizardSteps.Rep
                LoadRepFromStep(_Contact, oCallingStep)
                LoadStepChannel()

            Case WizardSteps.Tels
                Dim oXl As Xl_CreateContact_StepTels = oCallingStep
                With _Contact
                    .Tels = oXl.Tels
                    .Emails = oXl.Emails
                End With

                Select Case _WizardMode
                    Case WizardSteps.Standard
                        LoadStepStandard()
                    Case WizardSteps.Customer
                        LoadStepCustomer()
                    Case WizardSteps.Proveidor
                        LoadStepProveidor()
                    Case WizardSteps.Staff
                        LoadStepStaff()
                    Case WizardSteps.Rep
                        LoadStepRep()
                End Select

            Case WizardSteps.Finish
                MyBase.SetFinishButtons(finish:=False)
                LoadStepTels()
        End Select
    End Sub

    Private Async Sub Finish() Handles MyBase.requestToFinish
        Dim exs As New List(Of Exception)
        _Contact.Lang = _Contact.Address.SuggestedLang
        _Contact.FullNom = DTOContact.GenerateFullNom(_Contact)
        '_exs = New List(Of Exception)

        Select Case _WizardMode
            Case WizardSteps.Standard
                _Contact.rol = New DTORol(DTORol.Ids.Guest)
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim id As Integer = Await FEB2.Contact.Update(exs, _Contact)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    _Contact.id = id
                    onUpdate(True)
                Else
                    UIHelper.WarnError(exs)
                End If

            Case WizardSteps.Customer
                Dim oCustomer As DTOCustomer = _Contact
                With oCustomer
                    .Rol = New DTORol(DTORol.Ids.CliFull)
                    .TarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, oCustomer.ContactClass.DistributionChannel, Today)
                    For Each oUser As DTOUser In .Emails
                        oUser.Rol = New DTORol(DTORol.Ids.CliFull)
                    Next
                    .ExportCod = .Address.ExportCod
                    If .IsEstrangerResident Then .ExportCod = DTOInvoice.ExportCods.nacional
                    .Incoterm = DTOIncoterm.Factory("DAP")
                End With

                UIHelper.ToggleProggressBar(Panel1, True)
                Dim id = Await FEB2.Contact.Update(exs, oCustomer)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    _Contact.id = id
                    Dim success = Await FEB2.Customer.Update(exs, oCustomer)
                    onUpdate(success)
                Else
                    UIHelper.WarnError(exs)
                End If

            Case WizardSteps.Proveidor
                Dim oProveidor As DTOProveidor = _Contact
                oProveidor.Rol = New DTORol(DTORol.Ids.Manufacturer)
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim id = Await FEB2.Contact.Update(exs, oProveidor)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    _Contact.id = id
                    onUpdate(Await FEB2.Proveidor.Update(oProveidor, exs))
                Else
                    UIHelper.WarnError(exs)
                End If

            Case WizardSteps.Staff
                Dim oStaff As DTOStaff = _Contact
                oStaff.Rol = New DTORol(DTORol.Ids.Operadora)
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim id = Await FEB2.Contact.Update(exs, oStaff)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count Then
                    _Contact.id = id
                    onUpdate(Await FEB2.Staff.Update(exs, oStaff))
                Else
                    UIHelper.WarnError(exs)
                End If

            Case WizardSteps.Rep
                Dim oRep As DTORep = _Contact
                oRep.Rol = New DTORol(DTORol.Ids.Rep)
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim id = Await FEB2.Contact.Update(exs, oRep)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    _Contact.id = id
                    onUpdate(Await FEB2.Rep.Update(exs, oRep))
                Else
                    UIHelper.WarnError(exs)
                End If

            Case Else
                onUpdate(True)
        End Select

    End Sub


    Private Sub LoadContactFromStep(ByRef oContact As DTOContact, oXl As Xl_CreateContact_StepStandard)
        With oContact
            .Nom = oXl.RaoSocial
            .NomComercial = oXl.NomComercial
            .Nifs = DTONif.Collection.Factory(oXl.Nif.Value, oXl.Nif.Cod)
            .Address = oXl.Address
        End With
    End Sub

    Private Sub LoadCustomerFromStep(ByRef oCustomer As DTOCustomer, oXl As Xl_CreateContact_StepCustomer)
        With oCustomer
            .Nom = oXl.RaoSocial
            .NomComercial = oXl.NomComercial
            If oXl.Nif IsNot Nothing Then
                .Nifs = DTONif.Collection.Factory(oXl.Nif.Value, oXl.Nif.Cod)
            End If
            .Address = oXl.Address
            .Incoterm = oXl.Incoterm
            .Iva = oXl.IVA
            .Req = oXl.Req
        End With
    End Sub

    Private Sub LoadProveidorFromStep(ByRef oProveidor As DTOProveidor, oXl As Xl_CreateContact_StepProveidor)
        With oProveidor
            .Nom = oXl.RaoSocial
            .NomComercial = oXl.NomComercial
            .Nifs = DTONif.Collection.Factory(oXl.Nif.Value, oXl.Nif.Cod)
            .Address = oXl.Address
            .IncoTerm = oXl.Incoterm
        End With
    End Sub

    Private Sub LoadStaffFromStep(ByRef oStaff As DTOStaff, oXl As Xl_CreateContact_StepStaff)
        With oStaff
            .Nom = oXl.RaoSocial
            .Address = oXl.Address
            .Abr = oXl.Nickname
            .Nifs = DTONif.Collection.Factory(oXl.Nif.Value, oXl.Nif.Cod)
            .NumSs = oXl.NumSegSocial
            .Birth = oXl.Birth
            .Alta = oXl.Alta
            .Sex = oXl.Sex
            .StaffPos = oXl.StaffPos
            .Iban = oXl.Iban
        End With
    End Sub

    Private Sub LoadRepFromStep(ByRef oRep As DTORep, oXl As Xl_CreateContact_StepRep)
        With oRep
            .Nom = oXl.RaoSocial
            .NickName = oXl.Nickname
            .Nifs = DTONif.Collection.Factory(oXl.Nif.Value, oXl.Nif.Cod)
            .Address = oXl.Address
            .FchAlta = oXl.FchAlta
            .ComisionStandard = oXl.ComStd
            .ComisionReducida = oXl.ComRed
            .RepProducts = oXl.RepProducts
        End With
    End Sub

    Private Async Sub LoadStepChannel()
        Dim exs As New List(Of Exception)
        Dim oValues = Await FEB2.ContactClasses.All(exs)
        If exs.Count = 0 Then
            Dim oXl As New Xl_CreateContact_StepChannel(WizardSteps.Channel, _Contact, oValues)
            AddHandler oXl.requestForNextStep, AddressOf MyBase.PushNextStep
            MyBase.LoadStepControl(oXl)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadStepStandard()
        _WizardMode = WizardSteps.Standard
        Dim oXl As New Xl_CreateContact_StepStandard(WizardSteps.Standard, _Contact)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepCustomer()
        _WizardMode = WizardSteps.Customer
        Dim oXl As New Xl_CreateContact_StepCustomer(WizardSteps.Customer, _Contact, _Incoterms)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepProveidor()
        _WizardMode = WizardSteps.Proveidor
        Dim oXl As New Xl_CreateContact_StepProveidor(WizardSteps.Proveidor, _Contact, _Incoterms)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepStaff()
        _WizardMode = WizardSteps.Staff
        Dim oXl As New Xl_CreateContact_StepStaff(WizardSteps.Staff, _Contact)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepRep()
        _WizardMode = WizardSteps.Rep
        Dim oXl As New Xl_CreateContact_StepRep(WizardSteps.Rep, _Contact)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepTels()
        Dim oXl As New Xl_CreateContact_StepTels(WizardSteps.Tels, _Contact)
        MyBase.LoadStepControl(oXl)
    End Sub

    Private Sub LoadStepFinish()
        Dim oXl As New Xl_CreateContact_StepFinish(WizardSteps.Finish, _Contact)
        MyBase.LoadStepControl(oXl)
        MyBase.SetFinishButtons(finish:=True)
    End Sub

    Private Async Sub onUpdate(success As Boolean)
        Dim exs As New List(Of Exception)
        Select Case GlobalVariables.Emp.Id
            Case DTOEmp.Ids.MatiasMasso, DTOEmp.Ids.Tatita
                If Await NotifySubscriptors(exs) Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "Contacte desat pero no s'ha pogut avisar a la Victoria")
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
                    Me.Close()
                End If
            Case Else
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contact))
                Me.Close()

        End Select
    End Sub

    Public Async Function NotifySubscriptors(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oRecipients = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.AltaClient)
        If exs.Count = 0 AndAlso oRecipients.Count > 0 Then
            Dim sUser As String = DTOUser.NicknameOrElse(Current.Session.User)
            Dim sSubject = String.Format("Nova fitxa de contacte num.{0} creada per {1}", _Contact.Id, sUser)
            Dim oMailMessage = DTOMailMessage.Factory(oRecipients, sSubject)

            retval = Await FEB2.MailMessage.Send(exs, Current.Session.User, oMailMessage)
        End If
        Return retval
    End Function

End Class
