Public Class ImportPrevisions
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of List(Of DTOImportPrevisio))
        Return Await Api.Fetch(Of List(Of DTOImportPrevisio))(exs, "ImportPrevisions", oSku.Guid.ToString())
    End Function

    Shared Async Function Load(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of DTOImportacio)
        Dim retval = Await Api.Fetch(Of DTOImportacio)(exs, "ImportPrevisions/load", oImportacio.Guid.ToString())
        If retval IsNot Nothing Then
            For Each item As DTOImportPrevisio In retval.previsions
                item.Importacio = oImportacio
            Next
        End If
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of Boolean)
        Return Await Api.Update(Of DTOImportacio)(oImportacio, exs, "ImportPrevisions")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oImportPrevisions As List(Of DTOImportPrevisio)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOImportPrevisio))(oImportPrevisions, exs, "ImportPrevisions")
    End Function

    Shared Async Function UploadExcel(exs As List(Of Exception), oImportacio As DTOImportacio, oSheet As MatHelper.Excel.Sheet) As Task(Of Boolean)
        Return Await Api.Execute(Of MatHelper.Excel.Sheet, Boolean)(oSheet, exs, "ImportPrevisions/UploadExcel", oImportacio.Guid.ToString())
    End Function

    Shared Function AvisCamionXml(exs As List(Of Exception), Items As List(Of DTOImportPrevisio), Optional fakeConfirmation As Boolean = False) As String
        Dim oLang = DTOLang.ESP
        Dim retval As String = ""
        If Items Is Nothing Then
            exs.Add(New Exception("no hi ha mercancia per comunicar"))
        Else
            If Items.Count = 0 Then
                exs.Add(New Exception("no hi ha mercancia per comunicar"))
            Else
                If Items.Exists(Function(x) x.Sku Is Nothing) Then
                    exs.Add(New Exception("detectats articles sense registrar al sistema"))
                Else
                    Dim oImportacio As DTOImportacio = Items.First.Importacio
                    Contact.Load(oImportacio.proveidor, exs)

                    Dim sNum As String = Guid.NewGuid().ToString.Substring(1, 4)

                    Dim oXmlDocument As New DTOXmlDocument(True)

                    Dim oRootSegment As DTOXmlSegment = oXmlDocument.AddSegment("DOCUMENT")
                    oRootSegment.AddAttributes("TYPE", "AVISOCAMION", "DATE", TextHelper.VbFormat(DTO.GlobalVariables.Today(), "dd/MM/yyyy"), "NUMERO", sNum, "REMESA", oImportacio.Guid.ToString("N").ToUpper())

                    Dim oRemite As DTOXmlSegment = oRootSegment.AddSegment("REMITE")
                    oRemite.AddAttributes("NIF", "A58007857", "NOM", "MATIAS MASSO, S.A.")

                    Dim oDestino As DTOXmlSegment = oRootSegment.AddSegment("DESTINO")
                    oDestino.AddAttributes("NIF", "A62572342", "NOM", "VIVACE LOGISTICA, S.A.")

                    Dim oExpedicion As DTOXmlSegment = oRootSegment.AddSegment("EXPEDICION")
                    oExpedicion.AddAttribute("NUMPROVEEDOR", oImportacio.proveidor.Id.ToString())
                    oExpedicion.AddAttribute("PROCEDENCIA", Net.WebUtility.HtmlEncode(oImportacio.proveidor.Nom))
                    If oImportacio.transportista IsNot Nothing Then
                        oExpedicion.AddAttribute("TRANSITARIO", Net.WebUtility.HtmlEncode(oImportacio.transportista.FullNom))
                    End If
                    oExpedicion.AddAttribute("LLEGADA", oImportacio.fchETA.ToShortDateString)
                    oExpedicion.AddAttribute("BULTOS", "")
                    oExpedicion.AddAttribute("KG", "")
                    oExpedicion.AddAttribute("M3", "")

                    Dim oItems As DTOXmlSegment = oRootSegment.AddSegment("ITEMS")
                    For Each item As DTOImportPrevisio In Items
                        Dim oItem As DTOXmlSegment = oItems.AddSegment("ITEM")
                        oItem.AddAttribute("QTY", item.Qty)
                        oItem.AddAttribute("REF", item.Sku.Id)
                        oItem.AddAttribute("DSC", Net.WebUtility.HtmlEncode(item.Sku.RefYNomLlarg.Tradueix(oLang)))
                        oItem.AddAttribute("BRAND", Net.WebUtility.HtmlEncode(item.Sku.Category.Brand.Nom.Tradueix(oLang)))
                        If item.InvoiceReceivedItem IsNot Nothing Then
                            oItem.AddAttribute("LIN", item.InvoiceReceivedItem.Guid.ToString("N").ToUpper())
                        End If
                        If fakeConfirmation Then
                            oItem.AddAttribute("CONFIRMADO", item.Qty)
                        End If
                    Next

                    retval = DTOXmlDocument.ToString(oXmlDocument)
                End If
            End If
        End If


        Return retval
    End Function


End Class
