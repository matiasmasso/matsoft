Public Class Amazon

    Public Const VENDORCODE = "MATJ1"


    Shared Function SendInvRpt(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            BEBL.Emp.Load(oEmp)
            BEBL.Contact.Load(oEmp.Org)
            Dim oEdiversa As DTOEdiversaFile = InvRptEdiFile(oEmp)
            If EdiversaFile.Update(oEdiversa, exs) Then
                oEdiversa.LoadSegments()
                ' Dim iCount As Integer = oEdiversa.Segments.Where(Function(x) x.Fields(0) = "LIN").Count
                'retval.Succeed("enviat inventari a Amazon amb {0:N0} referencies", iCount)
            Else
                exs.Add(New Exception("BEBL.Amazon.SendInvRpt: Error al desar el fitxer de inventari per Amazon"))
            End If
        Catch ex As Exception
            exs.Add(New Exception("BEBL.Amazon.SendInvRpt: Error al redactar inventari per Amazon"))
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Shared Function InvRptEdiFile(oEmp As DTOEmp) As DTOEdiversaFile
        Dim retval As New DTOEdiversaFile()
        With retval
            .Fch = DTO.GlobalVariables.Today()
            .Stream = InvRptEdiSrc(oEmp)
            .IOCod = DTOEdiversaFile.IOcods.Outbox
            .Tag = DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008.ToString
            .Sender = New DTOEdiversaContact()
            .Sender.Ean = oEmp.Org.GLN
            .Receiver = New DTOEdiversaContact
            .receiver.Ean = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.amazon).GLN
        End With
        Return retval
    End Function

    Shared Function InvRptEdiSrc(oEmp As DTOEmp) As String
        Dim oAmz As DTOCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.amazon)
        BEBL.Contact.Load(oAmz)
        Dim retval As String = BEBL.EdiversaInvRpt.EdiSrc(oEmp, oAmz)
        Return retval
    End Function

    Shared Function InvRptExcel(oEmp As DTOEmp) As MatHelper.Excel.Sheet
        BEBL.Emp.Load(oEmp)
        BEBL.Contact.Load(oEmp.Org)

        Dim retval As New MatHelper.Excel.Sheet("InvRpt", InvRptFilename)
        With retval
            .AddColumn("linia", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("ref", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("marca", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("nom", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("stock", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("preu", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim lineNumber As Integer
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.amazon)
        Dim oSkus As List(Of DTOProductSku) = BEBL.Mgz.InvRpt(oCustomer, oEmp.Mgz)
        For Each oSku As DTOProductSku In oSkus
            lineNumber += 1

            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(lineNumber)
            oRow.AddCell(oSku.Ean13.Value)
            oRow.AddCell(oSku.Id)
            oRow.AddCell(oSku.Category.Brand.Nom.Esp)
            oRow.AddCell(oSku.NomLlarg.Esp)
            oRow.AddCell(oSku.Stock)
            oRow.AddCellAmt(oSku.Price)

        Next
        Return retval

    End Function

    Shared Function InvRptFilename() As String
        Dim retval As String = String.Format("Amazon Cost & Inventory feed {0:yyyyMMddHHmmss}", DTO.GlobalVariables.Now())
        Return retval
    End Function


    Private Shared Function NumFormat(DcSrc As Decimal) As String
        Dim retval As String = Format(DcSrc, "#0.00").Replace(",", ".")
        Return retval
    End Function

End Class

