Imports System.Xml
Imports Newtonsoft.Json.Linq
Public Class WtbolStocks

    Shared Async Function All(exs As List(Of Exception), oSite As DTOWtbolSite) As Task(Of List(Of DTOWtbolStock))
        Return Await Api.Fetch(Of List(Of DTOWtbolStock))(exs, "WtbolStocks", oSite.Guid.ToString())
    End Function

    Shared Async Function Merged(exs As List(Of Exception), oSite As DTOWtbolSite) As Task(Of List(Of DTOWtbolStock))
        Return Await Api.Fetch(Of List(Of DTOWtbolStock))(exs, "WtbolStocks/merged", oSite.Guid.ToString())
    End Function


    Shared Async Function Upload(oSite As DTOWtbolSite, oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim jObject = JStocks(oSite, oSheet, exs)
        If exs.Count = 0 Then
            retval = Await Api.Execute(Of JObject)(jObject, exs, "wtbol/upload/stocks")
        End If
        Return retval
    End Function

    Shared Function JStocks(oSite As DTOWtbolSite, oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As JObject
        Dim retval = New JObject
        If WtbolSite.Load(oSite, exs) Then
            Dim jSite = New JObject()
            jSite.Add("Guid", oSite.Guid.ToString())

            Dim jItems = New JArray

            retval.Add("site", jSite)
            retval.Add("items", jItems)

            For iRow As Integer = 1 To oSheet.Rows.Count - 1
                Dim oRow As MatHelper.Excel.Row = oSheet.Rows(iRow)
                If TextHelper.VbIsNumeric(oRow.cells(0).content) Then
                    Dim jItem = New JObject
                    jItem.Add("sku", oRow.cells(0).content.ToString())
                    jItem.Add("stock", oRow.cells(1).content.ToString.Replace("""", ""))
                    Dim price As Decimal = 0.0
                    If oRow.cells.Count > 2 Then
                        Decimal.TryParse(oRow.cells(2).content.ToString, price)
                    End If
                    jItem.Add("price", price)

                    jItems.Add(jItem)
                End If
            Next
        End If
        Return retval
    End Function

    Shared Async Function Xml(exs As List(Of Exception), oEmp As DTOEmp, oSite As DTOWtbolSite) As Task(Of String)
        Dim oStocks = Await WtbolStocks.Merged(exs, oSite)

        'Create XmlWriterSettings.
        'Dim settings As XmlWriterSettings = New XmlWriterSettings()
        'settings.OmitXmlDeclaration = True

        Dim memory_stream As New System.IO.MemoryStream
        Dim xml_text_writer As New XmlTextWriter(memory_stream, System.Text.Encoding.UTF8)

        ' Use indentation to make the result look nice.
        xml_text_writer.Formatting = Formatting.Indented
        xml_text_writer.Indentation = 4

        ' Write the XML declaration.
        xml_text_writer.WriteStartDocument(True)

        ' Start the Employees node.
        xml_text_writer.WriteStartElement("XML")

        If oSite.Active Then
            For Each oStock In oStocks
                ' Start the Customer element.

                With oStock

                    If .Stock > 0 AndAlso .Price IsNot Nothing Then
                        xml_text_writer.WriteStartElement("Item")

                        xml_text_writer.WriteStartElement("Brand")
                        xml_text_writer.WriteString(.Sku.Category.Brand.Nom.Esp)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("ProductCategory")
                        xml_text_writer.WriteString(.Sku.Category.Nom.Esp)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("ProductName")
                        xml_text_writer.WriteString(.Sku.NomProveidor)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("Product_URL")
                        Dim sUrl = .Uri.AbsoluteUri
                        'Bartu uses char # in the Uri which is not admitted by Hatch
                        If oSite.MerchantId = 98781 Then sUrl = sUrl.Replace("#", "?")
                        xml_text_writer.WriteString(sUrl)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("MPN")
                        xml_text_writer.WriteString(.Sku.RefProveidor)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("Stock")
                        xml_text_writer.WriteString(.Stock)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteStartElement("Price")
                        xml_text_writer.WriteString(.Price.Eur)
                        xml_text_writer.WriteEndElement()

                        xml_text_writer.WriteEndElement()
                    End If
                End With
            Next

        End If

        ' End the Employees node.

        ' End the document.
        xml_text_writer.WriteEndDocument()
        xml_text_writer.Flush()

        ' Use a StreamReader to display the result.
        Dim stream_reader As New System.IO.StreamReader(memory_stream)

        memory_stream.Seek(0, System.IO.SeekOrigin.Begin)
        Dim retval As String = stream_reader.ReadToEnd()

        ' Close the XmlTextWriter.
        xml_text_writer.Close()
        Return retval
    End Function

End Class
