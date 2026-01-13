Public Class EdiversaInvoice
    Shared Async Function EdiFile(oEmp As DTOEmp, oInvoice As DTOInvoice, exs As List(Of DTOEdiversaException)) As Task(Of DTOEdiversaFile)
        Dim retval As DTOEdiversaFile = Nothing
        If oInvoice Is Nothing Then
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, "factura buida"))
        Else
            Dim ex2 As New List(Of Exception)
            If Not Invoice.Load(oInvoice, ex2) Then
                exs.AddRange(DTOEdiversaException.FromSystemExceptions(ex2))
            End If

            Dim oSender As New DTOEdiversaContact
            With oSender
                .Contact = oEmp.Org
                .Ean = .Contact.GLN
            End With

            Dim oReceiver As New DTOEdiversaContact
            With oReceiver
                .Contact = oInvoice.Customer
                .Ean = .Contact.GLN
            End With

            retval = New DTOEdiversaFile
            With retval
                .Source = oInvoice
                .Tag = DTOInvoice.EdiversaTag(oInvoice).ToString
                .Fch = oInvoice.Fch
                .Sender = oSender
                .Receiver = oReceiver
                .Result = DTOEdiversaFile.Results.Pending
                .ResultBaseGuid = oInvoice
                .Stream = Await EdiMessage(oEmp, oInvoice, exs)
                .IOCod = DTOEdiversaFile.IOcods.Outbox
                .Docnum = oInvoice.Num
                .Amount = oInvoice.Total
            End With
        End If
        Return retval
    End Function

    Shared Async Function EdiMessage(oEmp As DTOEmp, oInvoice As DTOInvoice, exs As List(Of DTOEdiversaException)) As Task(Of String)
        Dim retval As String = ""
        Try
            Select Case DTOInvoice.EdiversaTag(oInvoice)
                Case DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010 'Sonae
                    retval = Await Ediversa_INVOIC_D_01B_UN_EAN010.EdiversaMessage(oEmp, oInvoice, exs)
                Case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007 'El Corte Ingles, Eciga, Carrefour, ToysRUs
                    retval = Await Ediversa_INVOIC_D_93A_UN_EAN007.EdiMessage(oEmp, oInvoice, exs)
            End Select

        Catch ex As Exception
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, ex.Message))
        End Try
        Return retval
    End Function

End Class
