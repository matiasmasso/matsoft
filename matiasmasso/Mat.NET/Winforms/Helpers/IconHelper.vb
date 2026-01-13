Public Class IconHelper
    Shared Function GetIconFromMimeCod(oMimeCod As MimeCods) As Image
        Dim retval As Image = Nothing
        Select Case oMimeCod
            Case MimeCods.Pdf
                retval = My.Resources.pdf
            Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Ai, MimeCods.Bmp, MimeCods.Eps, MimeCods.Png, MimeCods.Tif, MimeCods.Tiff
                retval = My.Resources.img_16
            Case MimeCods.Xls, MimeCods.Xlsx
                retval = My.Resources.Excel
            Case MimeCods.Doc, MimeCods.Docx
                retval = My.Resources.word
        End Select
        Return retval
    End Function

    Shared Function PrintModeIcon(oPrintMode As DTOInvoice.PrintModes) As System.Drawing.Image
        Dim retval As System.Drawing.Image = Nothing
        Select Case oPrintMode
            Case DTOInvoice.PrintModes.Pending
                retval = My.Resources.Waiting_clock
            Case DTOInvoice.PrintModes.NoPrint
                retval = My.Resources.aspa
            Case DTOInvoice.PrintModes.Printer
                retval = My.Resources.printer
            Case DTOInvoice.PrintModes.Email
                retval = My.Resources.MailSobreGroc
            Case DTOInvoice.PrintModes.Edi
                retval = My.Resources.edi
        End Select
        Return retval
    End Function

    Shared Function PurchaseSrcIcon(ByVal oSrc As DTOPurchaseOrder.Sources) As Image
        Select Case oSrc
            Case DTOPurchaseOrder.Sources.no_Especificado
                Return My.Resources.Unknown
            Case DTOPurchaseOrder.Sources.telefonico
                Return My.Resources.tel
            Case DTOPurchaseOrder.Sources.fax
                Return My.Resources.fax
            Case DTOPurchaseOrder.Sources.eMail
                Return My.Resources.MailSobreGroc
            Case DTOPurchaseOrder.Sources.representante
                Return My.Resources.People_Blue
            Case DTOPurchaseOrder.Sources.representante_por_Web
                Return My.Resources.People_Orange
            Case DTOPurchaseOrder.Sources.cliente_por_Web
                Return My.Resources.iExplorer
            Case DTOPurchaseOrder.Sources.matPocket
                Return My.Resources.pda
            Case DTOPurchaseOrder.Sources.fira
                Return My.Resources.star
            Case DTOPurchaseOrder.Sources.cliente_XML
                Return My.Resources.xml
            Case DTOPurchaseOrder.Sources.edi
                Return My.Resources.edi
            Case DTOPurchaseOrder.Sources.iPhone
                Return My.Resources.pda
            Case DTOPurchaseOrder.Sources.cliente_por_WebApi
                Return My.Resources.Api_16
            Case Else
                Return My.Resources.Unknown
        End Select
    End Function

    Shared Function SiiResult(value As DTOSiiLog) As Image
        Dim retval As Image = Nothing
        If value IsNot Nothing Then
            Select Case value.Result
                Case DTOSiiLog.Results.Correcto
                    retval = My.Resources.vb
                Case DTOSiiLog.Results.Parcialmente_Correcto
                    retval = My.Resources.warn
                Case DTOSiiLog.Results.Incorrecto
                    retval = My.Resources.wrong
                Case DTOSiiLog.Results.Error_De_Comunicacion
                    retval = My.Resources.aspa
            End Select
        End If
        Return retval
    End Function

    Shared Function TaskResult(oTaskLog As DTOTaskLog) As Image
        Dim retval As Image = Nothing
        Select Case oTaskLog.resultCod
            Case DTOTask.ResultCods.running
                retval = My.Resources.Waiting_clock
            Case DTOTask.ResultCods.success
                retval = My.Resources.vb
            Case DTOTask.ResultCods.empty
                retval = My.Resources.NoPark
            Case DTOTask.ResultCods.doneWithErrors
                retval = My.Resources.warning
            Case DTOTask.ResultCods.failed
                retval = My.Resources.aspa
        End Select
        Return retval
    End Function

End Class
