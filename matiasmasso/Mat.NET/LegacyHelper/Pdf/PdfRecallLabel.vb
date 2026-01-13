Public Class PdfRecallLabel

    Shared Function Factory(value As DTOSatRecall) As Byte()
        Dim oPdf As New _PdfBase()

        Dim bulto As Integer = 1
        Dim ref = value.Incidencia.AsinOrNum()

        Dim Ypos As Integer = 0
        DrawEtiqueta(oPdf, 0, value)

        Ypos = oPdf.Pdf.PageRectangle.Height / 2
        DrawSplitLine(oPdf)

        DrawEtiqueta(oPdf, Ypos, value)

        Dim retval As Byte() = oPdf.Stream()
        Return retval
    End Function

    Shared Function Factory(value As DTORecallCli) As Byte()
        Dim oPdf As New _PdfBase()

        Dim bulto As Integer = 1
        For Each product In value.Products
            If value.Products.IndexOf(product) > 0 Then
                oPdf.NewPage()
            End If

            Dim Ypos As Integer = 0
            DrawEtiqueta(oPdf, 0, value, bulto)

            Ypos = oPdf.Pdf.PageRectangle.Height / 2
            DrawSplitLine(oPdf)

            DrawEtiqueta(oPdf, Ypos, value, bulto)
            bulto += 1
        Next

        Dim retval As Byte() = oPdf.Stream()
        Return retval
    End Function

    Shared Sub DrawSplitLine(oPdf As _PdfBase)
        Dim Ypos As Integer = oPdf.Pdf.PageRectangle.Height / 2
        Dim oPen2 As New Pen(Brushes.Black)
        oPen2.DashPattern = New Single() {8.0F, 2.0F, 1.0F, 2.0F} '.DashStyle = Drawing2D.DashStyle.DashDotDot
        oPdf.Pdf.DrawLine(oPen2, 0, Ypos, oPdf.Pdf.PageRectangle.Right, Ypos)

        Dim oScissors As Image = My.Resources.scissors24
        Dim rcScissors As New RectangleF(2, Ypos - oScissors.Height - 1, oScissors.Width, oScissors.Height)
        oPdf.DrawImage(oScissors, rcScissors)
    End Sub

    Shared Sub DrawEtiqueta(oPdf As _PdfBase, Ypos As Integer, value As DTOSatRecall)
        Dim margin As Integer = 40
        Dim padding As Integer = 10
        Dim oFramePen As New Pen(Brushes.Black, 2)
        Dim oFrame As New RectangleF(margin, Ypos + margin, oPdf.Pdf.PageRectangle.Width - 2 * margin, oPdf.Pdf.PageRectangle.Height / 2 - 2 * margin)
        oPdf.Pdf.DrawRectangle(oFramePen, oFrame)

        oPdf.Font = New Font("Helvetica", 24, FontStyle.Bold)
        oPdf.X = oFrame.Left + 50
        oPdf.Y = oFrame.Top + 20
        oPdf.DrawString("MATIAS MASSO, S.A.")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("STACI")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("Can Montcau, 9-17 Nave C3")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("muelles 25-33")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("08186 Lliçà d'Amunt (Barcelona)")

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        Dim s As String = String.Format("ref.: Incidencia {0}", value.Incidencia.AsinOrNum())
        Dim iWidth As Integer = oPdf.MeasureStringWidth(s)
        oPdf.X = oFrame.Right - padding - iWidth
        oPdf.DrawString(s)


        Dim oHLine1 As New RectangleF(oFrame.Left, oFrame.Bottom - 100, oFrame.Width, 0)
        oPdf.Pdf.DrawRectangle(oFramePen, oHLine1)

        Dim oHLine2 As New RectangleF(oFrame.Left, oHLine1.Top + 24, oFrame.Width, 0)
        oPdf.Pdf.DrawRectangle(oFramePen, oHLine2)

        Dim oVLine As New RectangleF(oFrame.Right - 110, oHLine1.Top, 0, oFrame.Bottom - oHLine1.Top)
        oPdf.Pdf.DrawRectangle(oFramePen, oVLine)


        oPdf.Font = New Font("Helvetica", 16, GraphicsUnit.Point)
        oPdf.Y = oHLine1.Top
        oPdf.X = oFrame.Left + padding
        oPdf.DrawString("remite")

        oPdf.X = oVLine.Left + padding
        oPdf.DrawString("bulto / bultos")


        oPdf.Font = oPdf.GetAdjustedFont(value.Address.Text, oVLine.Left - oFrame.Left - 2 * padding, 16, 6)
        oPdf.Y = oHLine2.Top + padding
        oPdf.X = oFrame.Left + padding
        oPdf.DrawString(value.Incidencia.Customer.NomComercialOrDefault())

        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(value.Address.Text.toSingleLine)

        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(DTOAddress.ZipyCit(value.Address))

        'oPdf.Font = New Font("Helvetica", 24, FontStyle.Bold)
        's = String.Format("{0}/{1}", bulto, bulto)
        'iWidth = oPdf.MeasureStringWidth(s)
        'oPdf.Y = oHLine2.Top + 24
        'oPdf.X = oVLine.Left + (oFrame.Right - oVLine.Left - iWidth) / 2
        'oPdf.DrawString(s)
    End Sub


    Shared Sub DrawEtiqueta(oPdf As _PdfBase, Ypos As Integer, value As DTORecallCli, bulto As Integer)
        Dim margin As Integer = 40
        Dim padding As Integer = 10
        Dim oFramePen As New Pen(Brushes.Black, 2)
        Dim oFrame As New RectangleF(margin, Ypos + margin, oPdf.Pdf.PageRectangle.Width - 2 * margin, oPdf.Pdf.PageRectangle.Height / 2 - 2 * margin)
        oPdf.Pdf.DrawRectangle(oFramePen, oFrame)

        oPdf.Font = New Font("Helvetica", 24, FontStyle.Bold)
        oPdf.X = oFrame.Left + 50
        oPdf.Y = oFrame.Top + 20
        oPdf.DrawString("MATIAS MASSO, S.A.")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("STACI")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("Can Montcau, 9-17 Nave C3")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("muelles 25-33")
        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString("08186 Lliçà d'Amunt (Barcelona)")

        oPdf.Y += 2 * LineHeight(oPdf.Font)
        Dim s As String = String.Format("ref.: {0} recall", value.Recall.Nom)
        Dim iWidth As Integer = oPdf.MeasureStringWidth(s)
        oPdf.X = oFrame.Right - padding - iWidth
        oPdf.DrawString(s)


        Dim oHLine1 As New RectangleF(oFrame.Left, oFrame.Bottom - 100, oFrame.Width, 0)
        oPdf.Pdf.DrawRectangle(oFramePen, oHLine1)

        Dim oHLine2 As New RectangleF(oFrame.Left, oHLine1.Top + 24, oFrame.Width, 0)
        oPdf.Pdf.DrawRectangle(oFramePen, oHLine2)

        Dim oVLine As New RectangleF(oFrame.Right - 110, oHLine1.Top, 0, oFrame.Bottom - oHLine1.Top)
        oPdf.Pdf.DrawRectangle(oFramePen, oVLine)


        oPdf.Font = New Font("Helvetica", 16, GraphicsUnit.Point)
        oPdf.Y = oHLine1.Top
        oPdf.X = oFrame.Left + padding
        oPdf.DrawString("remite")

        oPdf.X = oVLine.Left + padding
        oPdf.DrawString("bulto / bultos")


        oPdf.Font = oPdf.GetAdjustedFont(value.Address, oVLine.Left - oFrame.Left - 2 * padding, 16, 6)
        oPdf.Y = oHLine2.Top + padding
        oPdf.X = oFrame.Left + padding
        oPdf.DrawString(value.Customer.NomComercialOrDefault())

        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(value.Address)

        oPdf.Y += LineHeight(oPdf.Font)
        oPdf.DrawString(DTORecallCli.RemiteLocation(value))

        oPdf.Font = New Font("Helvetica", 24, FontStyle.Bold)
        s = String.Format("{0}/{1}", bulto, value.Products.Count)
        iWidth = oPdf.MeasureStringWidth(s)
        oPdf.Y = oHLine2.Top + 24
        oPdf.X = oVLine.Left + (oFrame.Right - oVLine.Left - iWidth) / 2
        oPdf.DrawString(s)
    End Sub

    Private Shared Function LineHeight(oFont As Font) As Integer
        Dim factor As Decimal = 0.75
        Dim retval As Integer = oFont.Height * factor
        Return retval
    End Function


End Class
