Imports System.Xml

Public Class WtbolStocks
    Shared Function Update(oSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolStocksLoader.Update(oSite, exs)
        Return retval
    End Function

    Shared Function All(oSite As DTOWtbolSite) As List(Of DTOWtbolStock)
        Dim retval = WtbolStocksLoader.All(oSite)
        Return retval
    End Function

    Shared Function Merged(oSite As DTOWtbolSite) As List(Of DTOWtbolStock)
        Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso)
        Dim retval = WtbolStocksLoader.Merged(oSite, oEmp.Mgz)
        Return retval
    End Function

    Shared Function Xml(oSite As DTOWtbolSite) As String
        Dim oStocks = BEBL.WtbolStocks.Merged(oSite)

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

        For Each oStock In oStocks
            ' Start the Customer element.

            With oStock

                If .Stock > 0 AndAlso .Price IsNot Nothing Then
                    xml_text_writer.WriteStartElement("Item")

                    xml_text_writer.WriteStartElement("Brand")
                    xml_text_writer.WriteString(.Sku.category.brand.nom.Esp)
                    xml_text_writer.WriteEndElement()

                    xml_text_writer.WriteStartElement("ProductCategory")
                    xml_text_writer.WriteString(.Sku.category.nom.Esp)
                    xml_text_writer.WriteEndElement()

                    xml_text_writer.WriteStartElement("ProductName")
                    xml_text_writer.WriteString(.Sku.NomProveidor)
                    xml_text_writer.WriteEndElement()

                    xml_text_writer.WriteStartElement("Product_URL")
                    xml_text_writer.WriteString(.Uri.AbsoluteUri)
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

