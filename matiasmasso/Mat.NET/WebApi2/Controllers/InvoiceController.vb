Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InvoiceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Invoice/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Invoice.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la factura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Invoice/pdf/{guid}")>
    Public Function Pdf(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oInvoice As New DTOInvoice(guid)
            Dim value = BEBL.Invoice.Pdf(oInvoice)
            retval = MyBase.HttpPdfResponseMessage(value, "M+O-factura")
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al baixar el pdf de la factura")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Invoice/fromNum/{emp}/{year}/{num}")>
    Public Function Find(emp As DTOEmp.Ids, year As Integer, num As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Invoice.FromNum(oEmp, year, num)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la factura")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Invoice")>
    Public Function Update() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value As DTOInvoice = ApiHelper.Client.DeSerialize(Of DTOInvoice)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la factura")
            Else
                If value.Cca IsNot Nothing Then
                    If value.Cca.DocFile IsNot Nothing Then
                        value.Cca.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                        value.Cca.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                    End If
                End If

                'Rebuild circular refs
                If value.Cca IsNot Nothing Then
                    Dim oPnds = value.Cca.Pnds
                    If oPnds IsNot Nothing Then
                        For Each oPnd In oPnds
                            oPnd.Cca = value.Cca
                            oPnd.Invoice = value
                        Next
                    End If
                End If


                If BEBL.Invoice.Update(exs, value) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.InvoiceLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.TemplateLoader")
        End Try

        Return result

    End Function

    <HttpPost>
    <Route("api/Invoice/delete")>
    Public Function Delete(<FromBody> value As DTOInvoice) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Invoice.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la factura")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la factura")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Invoice/logSii")>
    Public Function LogSii(<FromBody> value As DTOInvoice) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Invoice.LogSii(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar la factura al Sii")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar la factura al Sii")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Invoice/logPrint/{invoice}/{printmode}/{winuser}/{destuser}/{fch}")>
    Public Function LogPrint(invoice As Guid, printMode As DTOInvoice.PrintModes, winuser As Guid, destuser As Guid, fch As Date) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oInvoice As New DTOInvoice(invoice)
            Dim oWinUser As New DTOUser(winuser)
            Dim oDestUser As DTOUser = Nothing
            If Not destuser.Equals(Guid.Empty) Then oDestUser = New DTOUser(destuser)
            If BEBL.Invoice.LogPrint(exs, oInvoice, printMode, oWinUser, oDestUser, fch) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar la factura al Sii")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar la factura al Sii")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Invoices/ClearPrintLog")>
    Public Function clearPrintLog(<FromBody> oInvoices As List(Of DTOInvoice)) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.Invoices.ClearPrintLog(exs, oInvoices) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir printlog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir printlog")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Invoice/SendToEmail/{user}")>
    Public Async Function SendToEmail(user As Guid, <FromBody> oInvoice As DTOInvoice) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            Dim oEmp = oUser.Emp
            Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
            Dim oToSubscriptors = BEBL.Subscription.Subscriptors(oSubscription, oInvoice.Customer)
            oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CopiaFactura)
            Dim oCcSubscriptors = BEBL.Subscription.Subscriptors(oSubscription)

            Dim oMailMessage = BEBL.Invoice.MailMessage(oInvoice, oToSubscriptors, oCcSubscriptors)
            If oMailMessage.To.Count = 0 Then
                retval = MyBase.HttpErrorResponseMessage(exs, String.Format("factura {0} sense destinataris per enviar", oInvoice.Num))
            Else
                If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                    BEBL.Invoice.LogPrint(exs, oInvoice, DTOInvoice.PrintModes.Email, oUser, oToSubscriptors.First, oInvoice.FchLastPrinted)
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, String.Format("error al enviar la factura {0}", oInvoice.Num))
                End If

            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la factura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoice/ediOrderFiles/{invoice}")>
    Public Function ediOrderFiles(invoice As Guid) As HttpResponseMessage 'per verificar fitxers Edi
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oInvoice As New DTOInvoice(invoice)
            Dim values = BEBL.Invoice.ediOrderFiles(oInvoice)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del mes")
        End Try
        Return retval
    End Function


End Class

Public Class InvoicesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/invoices/{emp}/{year}/{mes}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer, mes As Integer) As HttpResponseMessage 'per declaració Iva
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Invoices.All(oExercici, mes)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del mes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/FromCustomer/{customer}")>
    Public Function FromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.Invoices.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del client")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invoices/Headers/{emp}/{year}/{month}")>
    Public Function Headers(emp As DTOEmp.Ids, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oYearMonth As New DTOYearMonth(year, month)
            Dim values = BEBL.Invoices.Headers(oEmp, oYearMonth)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del mes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/compact/fromUser/{user}")>
    Public Function CompactFromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Invoices.Compact(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del mes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/compact/fromCustomer/{customer}")>
    Public Function CompactFromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.Invoices.Compact(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures del mes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invoices/LlibreDeFactures/{emp}/{year}/{toFch}")>
    Public Function LlibreDeFactures(emp As DTOEmp.Ids, year As Integer, tofch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Invoices.LlibreDeFactures(oExercici, tofch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invoices/Printed/{customer}")>
    Public Function Printed(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.Invoices.Printed(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures enviades")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invoices/PrintPending/{emp}/{fch}")>
    Public Function PrintPending(emp As DTOEmp.Ids, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Invoices.PrintPending(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures pendents d'enviar")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invoices/PrintedCollection/{emp}")>
    Public Function PrintedCollection(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Invoices.PrintedCollection(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/invoices/SetNoPrint/{user}")>
    Public Function SetNoPrint(user As Guid, <FromBody> oInvoices As List(Of DTOInvoice)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser As New DTOUser(user)
            If BEBL.Invoices.SetNoPrint(exs, oInvoices, oUser) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar les factures")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar les factures")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/invoices/availableNums/{emp}/{year}/{serie}")>
    Public Function availableNums(emp As DTOEmp.Ids, year As Integer, serie As DTOInvoice.Series) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Invoices.AvailableNums(oExercici, serie)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els números de factura disponibles")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/last/{emp}/{year}/{serie}")>
    Public Function Last(emp As DTOEmp.Ids, year As Integer, serie As DTOInvoice.Series) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.Invoices.Last(oExercici, serie)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'ultim número de factura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/lastNum/{emp}/{year}/{serie}")>
    Public Function LastNum(emp As DTOEmp.Ids, year As Integer, serie As DTOInvoice.Series) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oInvoice = BEBL.Invoices.Last(oExercici, serie)
            Dim value As Integer = oInvoice.Num
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'ultim número de factura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/LastEachSerie/{emp}/{year}")>
    Public Function LastEachSerie(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Invoices.LastEachSerie(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'ultim número de factura")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/invoices/IntrastatPending/{emp}/{year}/{month}")>
    Public Function IntrastatPending(emp As Integer, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oYearMonth As New DTOYearMonth(year, month)
            Dim values = BEBL.Invoices.IntrastatPending(oEmp, oYearMonth)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans pendents de transmetre")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/invoices/summary/{emp}")>
    Public Function Summary(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Invoices.Summary(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les factures")
        End Try
        Return retval
    End Function


End Class
