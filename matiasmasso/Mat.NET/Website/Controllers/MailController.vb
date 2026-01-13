Imports MatHelperStd
Public Class MailController
    Inherits _MatController

    Function Index() As ActionResult
        Return View()
    End Function

    Async Function Password(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = Await FEB.User.Find(id, exs)
        If oUser IsNot Nothing Then
            retval = View(oUser)
        End If
        Return retval
    End Function

    Async Function ActivationRequest(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            'Dim oUsuari As DTOUsuari = UsuariLoader.Find(oGuid)
            'If oUsuari IsNot Nothing Then
            'retval = View(oUsuari)
            'End If
            Dim oUser = Await FEB.User.Find(oGuid, exs)
            If oUser IsNot Nothing Then
                retval = View(oUser)
            End If
        End If
        Return retval
    End Function


    Async Function Newsletter(id As Guid, Optional lang As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)

        Dim oNewsletter As DTONewsletter = Await FEB.Newsletter.Find(id, exs)
        Dim oLangId As DTOLang.Ids = DTOLang.Ids.ESP
        oNewsletter.Lang = DTOLang.ESP
        If [Enum].TryParse(Of DTOLang.Ids)(lang.ToUpper, oLangId) Then
            oNewsletter.Lang = DTOLang.Factory(oLangId)
        End If
        ViewBag.BajaUrl = "$unsubUrl$"
        Return View("Newsletter", oNewsletter)
    End Function

    Async Function Transmisio(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oTransmisio = Await FEB.Transmisio.Find(id, exs)
        Return View("Transmisio", oTransmisio)
    End Function

    Async Function Factura(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model = Await FEB.Invoice.Find(id, exs)
        Return View("Factura", Model)
    End Function


    Async Function Noticia(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oNoticia = Await FEB.Noticia.Find(exs, oGuid)
            If oNoticia IsNot Nothing Then
                ViewBag.ViewInBrowser = FEB.Noticia.UrlFriendly(oNoticia, True)
                ViewBag.BajaUrl = "$unsubUrl$"
                retval = View(oNoticia)
            End If
        End If
        Return retval
    End Function


    Public Async Function BankTransferReminder(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oCustomer = Await FEB.Contact.Find(id, exs)
        Dim oAllPnds = Await FEB.Pnds.All(exs, Website.GlobalVariables.Emp, oCustomer, DTOPnd.Codis.Deutor)
        Dim oPndsToCome = oAllPnds.Where(Function(x) x.Vto > DTO.GlobalVariables.Today()).ToList()
        If oPndsToCome.Count > 0 Then
            Dim firstVtoToCome As Date = oPndsToCome.Min(Function(y) y.Vto)
            Dim oBancs = Await FEB.Bancs.BancsToReceiveTransfer(exs, Website.GlobalVariables.Emp)
            Dim model As New BankTransferReminderModel
            With model
                .Pnds = oAllPnds.Where(Function(x) x.Vto = firstVtoToCome).ToList
                If oBancs.Count > 0 Then
                    .Iban = oBancs.First.Iban
                End If
                .Lang = oCustomer.Lang
                .Beneficiario = Website.GlobalVariables.Emp.Org.Nom
                .CliNum = oCustomer.Id
            End With

            If exs.Count = 0 Then
                retval = View(model)
            Else
                retval = Await MyBase.ErrorResult(exs)
            End If

        End If
        Return retval
    End Function


    Public Async Function RemittanceAdvice(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oCca As New DTOCca(oGuid)
            Dim oModel = Await FEB.RemittanceAdvice.FromCca(exs, oCca)
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function RemittanceAdvice2(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim Model = Await FEB.BancTransferBeneficiari.Find(oGuid, exs)
            If exs.Count = 0 Then
                FEB.BancTransferPool.Load(Model.Parent, exs)
                If FEB.Cca.Load(Model.Parent.Cca, exs) Then
                    FEB.Contact.Load(Model.Contact, exs)
                    retval = View(Model)
                End If
            Else
                If Debugger.IsAttached Then Stop
                retval = View("Error")
            End If
        End If
        Return retval
    End Function

    Public Async Function Incidencia(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.Incidencia.Find(exs, oGuid)
            FEB.Contact.Load(oModel.Customer, exs)
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function MailVenciment(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.Csb.Find(oGuid, exs)
            FEB.Contact.Load(oModel.Contact, exs) 'per Contact.Lang
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function RaffleParticipation(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel = Await FEB.RaffleParticipant.Find(exs, id)
        If exs.Count = 0 Then
            retval = View(oModel)
        Else
            retval = View("error")
        End If
        Return retval
    End Function


    Public Async Function RaffleReminder(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            ViewBag.BajaUrl = "$unsubUrl$"
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.Raffle.Find(oGuid, exs)
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function RaffleWinnerCongrats(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.RaffleParticipant.Find(exs, oGuid)
            FEB.Raffle.Load(oModel.Raffle, exs)
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function RaffleDistributorNotification(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oModel = Await FEB.Raffle.Find(id, exs)
        FEB.Contact.Load(oModel.Winner.Distribuidor, exs)
        retval = View(oModel)
        Return retval
    End Function

    Public Async Function CommentPendingModeration(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.PostComment.Find(exs, oGuid)
            If oModel IsNot Nothing Then
                retval = View(oModel)
            End If
        End If
        Return retval
    End Function

    Public Async Function CommentAnswered(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.PostComment.Find(exs, oGuid)
            If oModel IsNot Nothing Then
                ViewBag.Lang = oModel.User.Lang
                retval = View(oModel)
            End If
        End If
        Return retval
    End Function


    Public Async Function DeliveryConfirmationRequest(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oModel = Await FEB.Delivery.Find(id, exs)
            If oModel IsNot Nothing Then
                Select Case oModel.CashCod
                    Case DTOCustomer.CashCodes.credit, DTOCustomer.CashCodes.diposit
                        Select Case oModel.PortsCod
                            Case DTOCustomer.PortsCodes.reculliran
                                retval = View("Delivery_Confirmation_Pickup", oModel)
                            Case Else
                                retval = View("Delivery_Confirmation_Shipment", oModel)
                        End Select
                    Case Else
                        retval = View("Delivery_Confirmation_Cash", oModel)
                End Select
            End If
        End If
        Return retval
    End Function

    Public Function CliApertura(id As String) As ActionResult
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim exs As New List(Of Exception)
            Dim Model As DTOCliApertura = FEB.CliApertura.FindSync(New Guid(id), exs)
            retval = View(Model)
        End If
        Return retval
    End Function


    Public Function EmailAddressVerification(id As String) As ActionResult
        ViewBag.email = id
        Return View()
    End Function

    Public Async Function RepPurchaseOrder(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If GuidHelper.IsGuid(id) Then
            Dim oGuid As New Guid(id)
            Dim oModel = Await FEB.PurchaseOrder.Find(oGuid, exs)
            FEB.Contact.Load(oModel.Customer, exs)
            retval = View(oModel)
        End If
        Return retval
    End Function

    Public Async Function CustomerPurchaseOrder(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oModel = Await FEB.PurchaseOrder.Find(id, exs)
        FEB.Contact.Load(oModel.Customer, exs)
        retval = View(oModel)
        Return retval
    End Function

    Public Function StoreLocator() As ActionResult
        Dim retval As ActionResult = View()
        Return retval
    End Function

    Public Async Function Descatalogados(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = Await FEB.User.Find(id, exs)
        Return View(oUser)
    End Function

End Class