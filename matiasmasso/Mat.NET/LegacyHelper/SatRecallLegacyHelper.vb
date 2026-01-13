Public Class SatRecallLegacyHelper
    Shared Function FillForm(_SatRecall As DTOSatRecall, ByRef targetFilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim TemplateFilename As String = DTOSatRecall.TemplatePath(_SatRecall, exs)
        Dim bookmarks As New List(Of WordHelper.WordBookmark)
        bookmarks.Add(New WordHelper.WordBookmark("DocDate", Format(Today, "dd/MM/yy")))
        If _SatRecall.Incidencia IsNot Nothing Then
            Dim oIncidencia As DTOIncidencia = _SatRecall.Incidencia

            bookmarks.Add(New WordHelper.WordBookmark("Incidencia", oIncidencia.AsinOrNum()))
            bookmarks.Add(New WordHelper.WordBookmark("CustomerPhone", _SatRecall.Tel))
            bookmarks.Add(New WordHelper.WordBookmark("CustomerContactPerson", _SatRecall.ContactPerson))
            bookmarks.Add(New WordHelper.WordBookmark("Defect", _SatRecall.Defect))

            If oIncidencia.Customer IsNot Nothing Then
                Dim oCustomer As DTOCustomer = oIncidencia.Customer
                If _SatRecall.Address Is Nothing Then _SatRecall.Address = oCustomer.address
                bookmarks.Add(New WordHelper.WordBookmark("CustomerName", oCustomer.NomComercialOrDefault()))
                If _SatRecall.Address IsNot Nothing Then
                    bookmarks.Add(New WordHelper.WordBookmark("CustomerAddress", _SatRecall.Address.Text))
                    bookmarks.Add(New WordHelper.WordBookmark("CustomerZipLocation", DTOAddress.ZipyCit(_SatRecall.Address)))
                End If
            End If

            If _SatRecall.Incidencia.FchCompra.HasValue Then
                bookmarks.Add(New WordHelper.WordBookmark("PurchaseDate", Format(oIncidencia.FchCompra, "dd/MM/yy")))
            End If

            If _SatRecall.Incidencia.Product IsNot Nothing AndAlso TypeOf oIncidencia.Product Is DTOProductSku Then
                Dim oSku As DTOProductSku = oIncidencia.Product
                bookmarks.Add(New WordHelper.WordBookmark("SkuCod", oSku.RefProveidor))
                bookmarks.Add(New WordHelper.WordBookmark("SkuName", oSku.NomProveidor))
            End If

            If Not String.IsNullOrEmpty(_SatRecall.Incidencia.SerialNumber) Then
                bookmarks.Add(New WordHelper.WordBookmark("SerialNumber", _SatRecall.Incidencia.SerialNumber))
            End If

            'WordHelper.FillBookmarks(TemplateFilename, targetFilename, bookmarks, exs)
        End If
        retval = exs.Count = 0
        Return retval
    End Function

End Class
