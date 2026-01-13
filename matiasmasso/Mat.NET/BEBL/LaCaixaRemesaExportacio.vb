Public Class LaCaixaRemesaExportacio
    Shared Function Text(ByRef oCsa As DTOCsa, exs As List(Of Exception)) As String
        Dim sb As New System.Text.StringBuilder
        Try

            sb.AppendLine(Registro_CabeceraRemesa(oCsa).FullText)
            For Each item As DTOCsb In oCsa.Items
                sb.AppendLine(Registro_individualPrincipal(item).FullText)
                sb.AppendLine(Registro_individualSecundario(item).FullText)
                If DTOCsb.Country(item).Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra)) Then
                    sb.AppendLine(Registro_AuxAndorra(item).FullText)
                End If
            Next
            sb.AppendLine(Registro_totales(oCsa).FullText)

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Shared Function Registro_CabeceraRemesa(oCsa As DTOCsa) As DTOAsciiRow
        Dim sCodRegistro_CabeceraRemesa = "10"
        Dim sCedentExport As String = If(String.IsNullOrEmpty(oCsa.Banc.NormaRMECedent), "0", oCsa.Banc.NormaRMECedent)
        Dim sNuestraRefRemesa As String = oCsa.formattedId()
        Dim sISOPaisDestino As String = DTOCsa.Country(oCsa).ISO
        Dim sIsoDivisa As String = DTOCsa.TotalNominal(oCsa).Cur.Tag
        Dim sConceptoEstadisticoBalanzaPagos As String = "960000"
        Dim sCodAnticipos As String = IIf(oCsa.Descomptat, "S", "N")

        Dim retval As New DTOAsciiRow
        With retval
            .AddInt(sCodRegistro_CabeceraRemesa, 2)
            .AddDec(sCedentExport, 15)
            .AddFch(oCsa.Fch, DTOAsciiRow.FchFormats.YYYYMMDD)
            .AddTxt(sNuestraRefRemesa, 16)
            .AddTxt(sISOPaisDestino, 2)
            .AddTxt(sIsoDivisa, 3)
            .AddTxt(sConceptoEstadisticoBalanzaPagos, 6)
            .AddTxt(sCodAnticipos, 1)
            .AddPlaceHolder(295)
        End With
        Return retval
    End Function

    Private Shared Function Registro_individualPrincipal(oCsb As DTOCsb) As DTOAsciiRow
        'oAEB.AddItm(itm.Formated, itm.Vto, itm.Amt.Eur, itm.Iban, itm.txt, itm.Client.NIF, itm.Client.Nom, itm.Client.Address.Text, itm.Client.Address.Zip.Location.Nom)
        Dim sCodRegistro_individualPrincipal = "20"
        Dim idxEfecte As Integer = oCsb.Csa.Items.IndexOf(oCsb) + 1
        Dim sRefEfecto As String = oCsb.FormattedId()
        Dim sTipoDeEfecto As String = "REC" 'recibos
        Dim oCountry As DTOCountry = DTOCsb.Country(oCsb)
        Dim sAceptacion As String = IIf(oCountry.ISO = "FR" Or oCountry.ISO = "DE", "N", " ") 'no informar en paises distintos de Francia y Alemania
        Dim sConceptoEstadisticoBalanzaPagos As String = "960000"
        Dim sNuestraFra As String = oCsb.Txt
        Dim sCodAnticipos As String = IIf(oCsb.Csa.Descomptat, "S", "N")

        Dim retval As New DTOAsciiRow
        With retval
            .AddInt(sCodRegistro_individualPrincipal, 2)
            .AddInt(idxEfecte, 4)
            .AddTxt(sRefEfecto, 35)
            .AddTxt(sTipoDeEfecto, 3)
            .AddFch(oCsb.Vto, DTOAsciiRow.FchFormats.YYYYMMDD)
            .AddDec(oCsb.Amt.Eur, 9, 2)
            .AddTxt(sAceptacion, 1)
            .AddTxt(sConceptoEstadisticoBalanzaPagos, 6)
            .AddTxt(sNuestraFra, 16)
            .AddFch(oCsb.Csa.Fch, DTOAsciiRow.FchFormats.YYYYMMDD) 'en realitat, data factura
            .AddTxt(sCodAnticipos, 1)
            .AddPlaceHolder(253)
        End With
        Return retval
    End Function

    Private Shared Function Registro_individualSecundario(oCsb As DTOCsb) As DTOAsciiRow
        Dim sCodRegistro_individualSecundario = "21"
        Dim idxEfecte As Integer = oCsb.Csa.Items.IndexOf(oCsb) + 1

        'suggerit per el departament de informatica de La Caixa: treure el "PT" del començament del NIF per Portugal
        Dim sNIFLibrado As String = oCsb.Contact.PrimaryNifValue()
        Dim oCountry As DTOCountry = DTOCsb.Country(oCsb)
        If oCountry.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal)) Then
            sNIFLibrado = sNIFLibrado.Replace("PT", "")
        End If

        Dim sFormatoCta As String = "I" 'formato IBAN
        Dim sZipBank As String = "" 'no recullim aquesta info a la nostre base de dades

        Dim retval As New DTOAsciiRow
        With retval
            .AddInt(sCodRegistro_individualSecundario, 2)
            .AddInt(idxEfecte, 4)
            .AddTxt(sNIFLibrado, 14)
            .AddTxt(sFormatoCta, 1)
            .AddTxt(oCsb.Iban.Digits, 35)
            .AddTxt(oCsb.Contact.Nom, 35)
            .AddTxt(oCsb.Contact.Address.Text, 35)
            .AddTxt(oCsb.Contact.Address.Zip.Location.Nom, 35)
            .AddTxt(oCsb.Contact.Address.Zip.ZipCod, 35)
            .AddTxt(DTOIban.Swift(oCsb.Iban), 12)
            .AddTxt(oCsb.Iban.BankBranch.Bank.RaoSocial, 35)
            .AddTxt(oCsb.Iban.BankBranch.Address, 35)
            .AddTxt(oCsb.Iban.BankBranch.Location.Nom, 35)
            .AddTxt(sZipBank, 35)
        End With
        Return retval
    End Function

    Private Shared Function Registro_AuxAndorra(oCsb As DTOCsb) As DTOAsciiRow
        Dim sCodRegistro_AuxAndorra = "23"
        Dim idxEfecte As Integer = oCsb.Csa.Items.IndexOf(oCsb) + 1

        Dim sTxt2 As String = "" 'no obligatorio
        Dim sTxt3 As String = "" 'no obligatorio
        Dim sTxt4 As String = "" 'no obligatorio
        Dim sNomLibrado As String = "" 'no obligatorio en Andorra

        Dim retval As New DTOAsciiRow
        With retval
            .AddInt(sCodRegistro_AuxAndorra, 2)
            .AddInt(idxEfecte, 4)
            .AddTxt(oCsb.Txt, 78)
            .AddTxt(sTxt2, 78)
            .AddTxt(sTxt3, 78)
            .AddTxt(sTxt4, 78)
            .AddPlaceHolder(30)
        End With
        Return retval
    End Function


    Private Shared Function Registro_totales(oCsa As DTOCsa) As DTOAsciiRow
        Dim sCodRegistro_TotalesRemesa As Integer = 30
        Dim DcTotalImportes As Decimal = DTOCsa.TotalNominal(oCsa).Eur
        Dim retval As New DTOAsciiRow
        With retval
            .AddInt(sCodRegistro_TotalesRemesa, 2)
            .AddInt(oCsa.Items.Count, 9)
            .AddDec(DcTotalImportes, 9, 2)
            .AddPlaceHolder(326)
        End With
        Return retval
    End Function
End Class