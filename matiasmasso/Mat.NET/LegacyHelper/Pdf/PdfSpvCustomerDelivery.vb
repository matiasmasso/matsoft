Public Class PdfSpvCustomerDelivery
    Private _Lang As DTOLang

    Public Enum exemplars
        Transportista
        Client
    End Enum

    Shared Function Factory(exs As List(Of Exception), value As DTOSpv, oTrp As DTOTransportista, oTaller As DTOTaller) As Byte()
        Dim retval As Byte() = Nothing
        Dim oPdf As New _PdfBase()
        DrawContent(oPdf, value, oTrp, oTaller, exemplars.Transportista)
        DrawSplitLine(oPdf)
        DrawContent(oPdf, value, oTrp, oTaller, exemplars.Client)

        retval = oPdf.Stream()

        Return retval
    End Function

    Shared Sub DrawContent(oPdf As _PdfBase, oSpv As DTOSpv, oTrp As DTOTransportista, oTaller As DTOTaller, oExemplar As exemplars)
        Dim oLang = oSpv.Customer.Lang
        Dim exs As New List(Of Exception)

        oPdf.SetMargins(0, 100, 50)

        Select Case oExemplar
            Case exemplars.Transportista
                oPdf.Y = 50
            Case exemplars.Client
                oPdf.Y = 50 + (oPdf.MarginsRectangle.Bottom - oPdf.MarginsRectangle.Top) / 2
        End Select

        oPdf.DrawString(oSpv.Customer.Nom)
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(oSpv.Address.Text)
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(DTOZip.FullNom(oSpv.Address.Zip, oSpv.Customer.Lang))
        oPdf.Y += LineHeight(oPdf.Font)

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        Dim s As String = String.Format("{0}, {1}", DTOAddress.Location(oSpv.address).Nom, DTO.GlobalVariables.Today().ToShortDateString)
        Dim iWidth As Integer = oPdf.MeasureStringWidth(s)
        oPdf.X = (oPdf.MarginsRectangle.Right - iWidth - 100)
        oPdf.DrawString(s)


        oPdf.Y += 2 * LineHeight(oPdf.Font)
        s = oLang.Tradueix("Albarán de entrega", "Albarà d'entrega", "Shipping advice")
        iWidth = oPdf.MeasureStringWidth(s)
        oPdf.X = (oPdf.MarginsRectangle.Right - oPdf.MarginsRectangle.Left - iWidth) / 2
        oPdf.DrawString(s)

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawString(oLang.Tradueix("Entregamos a:", "Entreguem a", "We deliver to:"))
        oPdf.X = 100
        oPdf.DrawString(oTrp.NomComercialOrDefault())

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawString(oLang.Tradueix("Bultos:", "Bultos:", "Packages:"))
        oPdf.X = 100
        oPdf.DrawString(1)

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawString(oLang.Tradueix("Con destino a:", "Amb destinació:", "Destination:"))
        oPdf.X = 100
        oPdf.DrawString(oTaller.Nom)
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(oTaller.Address.Text)
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(DTOZip.FullNom(oTaller.Address.Zip, oSpv.Customer.Lang))

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawString(oLang.Tradueix("Referencia:", "Referencia:", "Ref:"))
        oPdf.X = 100
        oPdf.DrawString(oSpv.Id)

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawString(oLang.Tradueix("Contenido:", "Contingut:", "Content:"))
        oPdf.X = 100
        oPdf.DrawString(oSpv.product.FullNom() & " " & oLang.Tradueix("para su reparación", "per reparar", "to repair"))

        oPdf.Y += 4 * LineHeight(oPdf.Font)
        oPdf.X = 0
        oPdf.DrawRectangle(oPdf.X, oPdf.Y, 250, 100)
        oPdf.X = 10
        oPdf.DrawString(oLang.Tradueix("Firma y sello del transportista", "Signatura i segell del transportista", "Carrier signature and stamp"))


        LabelExemplar(oExemplar, oPdf, oLang)
    End Sub

    Private Shared Function LineHeight(oFont As Font) As Integer
        Dim factor As Decimal = 0.75
        Dim retval As Integer = oFont.Height * factor
        Return retval
    End Function

    Shared Sub LabelExemplar(oExemplar As exemplars, oPdf As _PdfBase, oLang As DTOLang)
        Dim s As String = ""
        Dim Y As Integer
        Select Case oExemplar
            Case exemplars.Transportista
                s = oLang.Tradueix("Ejemplar para el transportista", "Exemplar per el transportista", "forwarder copy")
                Y = 50
            Case exemplars.Client
                s = oLang.Tradueix("Ejemplar para el remitente", "Exemplar per el remitent", "sender copy")
                Y = 50 + (oPdf.MarginsRectangle.Bottom - oPdf.MarginsRectangle.Top) / 2
        End Select

        Dim oFont As New Font("Arial", 16, FontStyle.Bold)
        Dim iX As Integer = oPdf.MarginsRectangle.Left - 50
        Dim oRc As New RectangleF(iX, Y, 16, (oPdf.MarginsRectangle.Bottom - oPdf.MarginsRectangle.Top))
        Dim sF As New StringFormat()
        sF.Alignment = StringAlignment.Near
        sF.FormatFlags = StringFormatFlags.DirectionVertical
        oPdf.DrawString(s, oFont, Brushes.Red, oRc, sF)
    End Sub

    Shared Sub DrawSplitLine(oPdf As _PdfBase)
        Dim Ypos As Integer = oPdf.Pdf.PageRectangle.Height / 2
        Dim oPen2 As New Pen(Brushes.Black)
        oPen2.DashPattern = New Single() {8.0F, 2.0F, 1.0F, 2.0F} '.DashStyle = Drawing2D.DashStyle.DashDotDot
        oPdf.Pdf.DrawLine(oPen2, 0, Ypos, oPdf.Pdf.PageRectangle.Right, Ypos)

        Dim oScissors As Image = My.Resources.scissors24
        Dim rcScissors As New RectangleF(2, Ypos - oScissors.Height - 1, oScissors.Width, oScissors.Height)
        oPdf.DrawImage(oScissors, rcScissors)
    End Sub

End Class
