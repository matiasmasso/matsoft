Imports System.Globalization

Public Class IncidenciaQuery

    Shared Function UrlExcelDoc(oUser As DTOUser, year As Integer) As String
        Dim retval = UrlHelper.Factory(False, "doc", DTODocFile.Cods.incidenciesexcel, oUser.Guid.ToString(), year)
        Return retval
    End Function

    Shared Function ExcelSheet(oModel As Models.IncidenciesModel, oCulture As CultureInfo, oLang As DTOLang) As ExcelHelper.Sheet
        Dim sTitle As String = DTOLang.ENG.Tradueix("M+O Incidencias Servicio Técnico", "M+O Incidències Servei Tècnic", "M+O Support Incidences")
        Dim retval As New ExcelHelper.Sheet(sTitle, sTitle)
        If oModel.Items.Count = 0 Then
            Dim oRow As ExcelHelper.Row = retval.AddRow()
            oRow.AddCell(oLang.Tradueix("No existen incidencias abiertas en estos momentos", "No hi han incidencies obertes en aquests moments", "No open support incidences available"))
        Else
            With retval
                .AddColumn(oLang.Tradueix("Registro", "Registre", "Report"))
                .AddColumn(oLang.Tradueix("Fecha", "Data", "Date"))
                .AddColumn(oLang.Tradueix("Cliente", "Client", "Customer"))
                '.AddColumn(oLang.Tradueix("Marca", "Marca", "Brand"))
                '.AddColumn(oLang.Tradueix("Categoria", "Categoria", "Category"))
                .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"))
                .AddColumn(oLang.Tradueix("Nº de serie", "Num.de serie", "Serial number"))
                .AddColumn(oLang.Tradueix("Fecha fabricación", "Datat fabricació", "Manufacturing date"))
                .AddColumn(oLang.Tradueix("Concepto", "Concepte", "Concept"))
                .AddColumn(oLang.Tradueix("Cierre", "Tancament", "Closed"))
            End With

            For Each item In oModel.Items
                Dim oRow As ExcelHelper.Row = retval.AddRow()
                oRow.AddCell(item.Num.ToString, FEB2.UrlHelper.Factory(True, "incidencia", item.Guid.ToString))
                oRow.AddCell(item.Fch.ToString("d", oCulture))

                oRow.AddCell(oModel.Customers.FirstOrDefault(Function(x) x.Guid.Equals(item.CustomerGuid)).Nom)
                If item.ProductGuid = Nothing Then
                    oRow.AddCell()
                    'oRow.AddCell()
                    'oRow.AddCell()
                Else
                    oRow.AddCell(oModel.Catalog.ProductFullNom(item.ProductGuid))
                    'oRow.AddCell(DTOProduct.CategoryNom(oIncidencia.Product))
                    'oRow.AddCell(DTOProduct.GetNom(oIncidencia.Product))
                End If
                oRow.AddCell(item.SerialNumber)
                oRow.AddCell(item.ManufactureDate)
                If item.CodApertura = Nothing Then
                    oRow.AddCell()
                Else
                    Dim oCodApertura = oModel.CodisApertura.FirstOrDefault(Function(x) x.Guid.Equals(item.CodApertura))
                    If oCodApertura Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(oCodApertura.Nom)
                    End If
                End If
                If item.FchClose = Nothing Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(item.FchClose.ToString("d", oCulture))
                End If
            Next
        End If
        Return retval
    End Function


End Class
