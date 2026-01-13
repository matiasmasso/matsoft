Imports Newtonsoft.Json.Linq

Public Class SQLHelper

    Shared Event onSqlDependencyChange(sender As Object, e As SqlNotificationEventArgs)


    Shared Function GetDataReader(ByVal SQL As String, ByVal ParamArray parametres() As String) As SqlDataReader
        Dim oConn As SqlConnection = Nothing
        Dim retVal As SqlDataReader = Nothing
        Dim oCmd As SqlCommand

        oConn = New SqlConnection(Defaults.SQLConnectionString)
        oCmd = GetSQLCommand(SQL, oConn, parametres)
        oCmd.CommandTimeout = 60 'standard 30 canviat per PurchaseOrderItemLoader.PendentsDeLiquidacioRep
        oConn.Open()

        retVal = oCmd.ExecuteReader(CommandBehavior.CloseConnection)

        Return retVal
    End Function

    Shared Function GetDataReader(oConn As SqlConnection, SQL As String, ByVal ParamArray parametres() As String) As SqlDataReader
        Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
        oCmd.CommandTimeout = 60 'standard 30 canviat per PurchaseOrderItemLoader.PendentsDeLiquidacioRep
        oConn.Open()

        Dim retval As SqlDataReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
        Return retval
    End Function

    Shared Function SQLConnection(Optional BlOpenConnection As Boolean = False) As System.Data.SqlClient.SqlConnection
        Dim retval As New SqlConnection(Defaults.SQLConnectionString)
        If BlOpenConnection Then retval.Open()
        Return retval
    End Function

    Shared Function GetDataset(ByVal SQL As String, oExceptions As List(Of System.Exception), ByVal ParamArray parametres() As String) As DataSet
        Dim oConn As SqlConnection = Nothing
        Dim retVal As New DataSet
        Dim oDA As New SqlDataAdapter


        Try
            oConn = New SqlConnection(Defaults.SQLConnectionString)
            Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
            oConn.Open()

            oDA.SelectCommand = oCmd
            Dim oCB As New SqlCommandBuilder(oDA)
            oDA.Fill(retVal)

        Catch e As SqlException
            oExceptions.Add(e)
        Finally
            If oConn IsNot Nothing Then
                oConn.Close()
            End If
        End Try
        Return retVal
    End Function


    Shared Function ExecuteNonQuery(ByVal SQL As String, oTrans As SqlTransaction, ByVal ParamArray parametres() As String) As Integer
        Dim retval As Integer = 0
        Dim oConn As SqlConnection = oTrans.Connection
        If oConn.State = ConnectionState.Closed Then
            oConn.Open()
        End If

        Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
        oCmd.Transaction = oTrans

        retval = oCmd.ExecuteNonQuery()

        Return retval
    End Function

    Shared Function ExecuteNonQuery(ByVal SQL As String, oExceptions As List(Of System.Exception), ByVal ParamArray parametres() As String) As Integer
        Dim oConn As SqlConnection = Nothing
        Dim retVal As Integer = 0

        Try
            oConn = New SqlConnection(Defaults.SQLConnectionString)
            Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
            oConn.Open()

            retVal = oCmd.ExecuteNonQuery()

        Catch e As SqlException
            oExceptions.Add(e)
        Finally
            If oConn IsNot Nothing Then
                oConn.Close()
            End If
        End Try
        Return retVal
    End Function

    Shared Function ExecuteNonQuery(ByVal SQL As String, ByRef iCount As Integer) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim oConn As SqlConnection = Nothing
        iCount = 0

        Try
            oConn = New SqlConnection(Defaults.SQLConnectionString)
            Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn)
            oConn.Open()

            iCount = oCmd.ExecuteNonQuery()
            retval.Succeed()
        Catch ex As SqlException
            retval.Fail(ex, "Error al executar SQL")
        Finally
            If oConn IsNot Nothing Then
                oConn.Close()
            End If
        End Try
        Return retval
    End Function


    Shared Function GetSQLDataAdapter(ByVal SQL As String, ByVal oTransaction As SqlTransaction, ByVal ParamArray parametres() As String) As SqlDataAdapter
        Dim oConn As SqlConnection = oTransaction.Connection
        If oConn.State = ConnectionState.Closed Then
            oConn.Open()
        End If

        Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
        oCmd.Transaction = oTransaction

        Dim oDA As New SqlDataAdapter
        oDA.SelectCommand = oCmd
        Dim oCB As New SqlCommandBuilder(oDA)
        Return oDA
    End Function

    Shared Function GetSQLDataAdapter(ByVal SQL As String, ByVal oConn As SqlConnection, ByVal ParamArray parametres() As String) As SqlDataAdapter
        Dim oCmd As SqlCommand = GetSQLCommand(SQL, oConn, parametres)
        oConn.Open()
        Dim oDA As New SqlDataAdapter
        oDA.SelectCommand = oCmd
        Dim oCB As New SqlCommandBuilder(oDA)
        Return oDA
    End Function

    Shared Function GetSQLCommand(ByVal SQL As String, ByVal oConnection As SqlConnection, ByVal ParamArray parametres() As String) As SqlCommand
        Dim retval As New SqlCommand(SQL, oConnection)

        Dim i As Integer = 0
        Do While i < UBound(parametres)
            Dim sParamName As String = parametres(i)
            Dim sParamValue As String = parametres(i + 1)
            retval.Parameters.Add(New SqlParameter(sParamName, sParamValue))
            i += 2
        Loop
        'System.Diagnostics.Debug.WriteLine(Format(Now, "dd/MM/yy HH:mm:ss"))
        'System.Diagnostics.Debug.WriteLine(SQL)
        'Stop
        Return retval
    End Function

    Shared Function BeginTransaction() As SqlTransaction
        Dim oConn As New SqlConnection(Defaults.SQLConnectionString)
        oConn.Open()
        Dim retval As SqlTransaction = oConn.BeginTransaction
        Return retval
    End Function


    Shared Function IsNotZero(src As Object) As Boolean
        Dim retval As Boolean = True
        If src IsNot Nothing Then
            If Not IsDBNull(src) Then
                If IsNumeric(src) Then
                    retval = src <> 0
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function FieldValueContainsAll(fieldName As String, values As List(Of String)) As String
        Dim sb As New Text.StringBuilder
        For Each value In values
            If Not String.IsNullOrEmpty(value) Then
                sb.Append(IIf(sb.Length = 0, "(", "AND "))
                sb.AppendFormat("{0} LIKE '%{1}%' ", fieldName, value)
            End If
        Next
        If sb.Length > 0 Then sb.Append(") ")
        Return sb.ToString
    End Function

    Shared Function GetIntegerFromDataReader(src As Object) As Integer
        Dim retval As Integer
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetNullableIntegerFromDataReader(src As Object) As Nullable(Of Integer)
        Dim retval As Nullable(Of Integer) = Nothing
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetDecimalFromDataReader(src As Object) As Decimal
        Dim retval As Decimal
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetDoubleFromDataReader(src As Object) As Double
        Dim retval As Double
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetAmtFromDataReader(src As Object) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If Not IsDBNull(src) Then
            retval = DTOAmt.Factory(CDec(src))
        End If
        Return retval
    End Function

    Shared Function GetAmtCompactFromDataReader(src As Object) As DTOAmt.Compact
        Dim retval As DTOAmt.Compact = Nothing
        If Not IsDBNull(src) Then
            retval = DTOAmt.Compact.Factory(CDec(src))
        End If
        Return retval
    End Function
    Shared Function GetAmtCompactFromDataReader(oDrd As SqlDataReader, Optional DivisaField As String = "Val", Optional CurField As String = "Cur", Optional EurField As String = "Eur") As DTOAmt.Compact
        Dim retval As DTOAmt.Compact = Nothing
        Dim eur As Decimal = SQLHelper.GetIntegerFromDataReader(oDrd(EurField))
        Dim cur As String = SQLHelper.GetStringFromDataReader(oDrd(CurField))
        Dim val As Decimal = SQLHelper.GetIntegerFromDataReader(oDrd(DivisaField))
        retval = DTOAmt.Compact.Factory(eur, cur, val)
        Return retval
    End Function

    Shared Function GetAmtFromDataReader2(oDrd As SqlDataReader, Optional fieldEur As String = "Eur", Optional fieldCur As String = "Cur", Optional FieldVal As String = "Val") As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim DcEur As Decimal
        Dim DcVal As Decimal

        If FieldExists(oDrd, fieldEur) Then DcEur = GetDecimalFromDataReader(oDrd(fieldEur))
        If FieldExists(oDrd, fieldCur) Then
            Dim sCur As String = GetStringFromDataReader(oDrd(fieldCur))
            If FieldExists(oDrd, FieldVal) Then DcVal = GetDecimalFromDataReader(oDrd(FieldVal))
            retval = DTOAmt.Factory(DcEur, sCur, DcVal)
        Else
            retval = DTOAmt.Factory(DcEur)
        End If
        Return retval
    End Function

    Shared Function GetCurFromDataReader(src As Object) As DTOCur
        Dim retval As DTOCur = Nothing
        If Not IsDBNull(src) Then
            retval = DTOApp.Current.Curs.First(Function(x) x.Tag = src)
        End If
        Return retval
    End Function

    Shared Function GetEANFromDataReader(src As Object) As DTOEan
        Dim retval As DTOEan = Nothing
        If Not IsDBNull(src) Then
            If src.trim > "" Then
                retval = DTOEan.Factory(src.ToString())
            End If
        End If
        Return retval
    End Function
    Shared Function GetBaseGuidFromDataReader(src As Object) As DTOBaseGuid
        Dim retval As DTOBaseGuid = Nothing
        If Not IsDBNull(src) Then
            If GuidHelper.IsGuid(src) Then
                retval = New DTOBaseGuid(New Guid(src.ToString()))
            End If
        End If
        Return retval
    End Function

    Shared Function GetStringFromDataReader(src As Object) As String
        Dim retval As String = ""
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function


    Shared Function GetGuidFromDataReader(src As Object) As Guid
        Dim retval As Guid = Nothing
        If Not IsDBNull(src) Then
            If TypeOf src Is Guid Then
                retval = src
            ElseIf TypeOf src Is String Then
                retval = New Guid(src.ToString())
            End If
        End If
        Return retval
    End Function

    Shared Function GetHtmlFromDataReader(src As Object) As String
        Dim retval As String = ""
        If Not IsDBNull(src) Then
            retval = src.Replace(vbCrLf, "<br/>")
        End If
        Return retval
    End Function

    Shared Function GetAddressFromDataReader(oDrd As SqlDataReader,
                                             Optional AdrField As String = "Adr",
                                             Optional AdrViaNomField As String = "AdrViaNom",
                                             Optional AdrNumField As String = "AdrNum",
                                             Optional AdrPisField As String = "AdrPis",
                                             Optional LongitudField As String = "Longitud",
                                             Optional LatitudField As String = "Latitud",
                                             Optional ZipGuidField As String = "ZipGuid",
                                             Optional ZipCodField As String = "ZipCod",
                                             Optional LocationGuidField As String = "LocationGuid",
                                             Optional LocationNomField As String = "LocationNom",
                                             Optional ZonaGuidField As String = "ZonaGuid",
                                             Optional ZonaNomField As String = "ZonaNom",
                                             Optional ZonaExportField As String = "ZonaExport",
                                             Optional ProvinciaGuidField As String = "ProvinciaGuid",
                                             Optional ProvinciaNomField As String = "ProvinciaNom",
                                             Optional ProvinciaIntrastatField As String = "ProvinciaIntrastat",
                                             Optional RegioGuidField As String = "RegioGuid",
                                             Optional RegioNomField As String = "RegioNom",
                                             Optional CountryGuidField As String = "CountryGuid",
                                             Optional CountryEspField As String = "CountryEsp",
                                             Optional CountryCatField As String = "CountryCat",
                                             Optional CountryEngField As String = "CountryEng",
                                             Optional CountryPorField As String = "CountryPor",
                                             Optional CountryISOField As String = "CountryISO",
                                             Optional ExportCodField As String = "ExportCod") As DTOAddress
        Dim retval As New DTOAddress
        Dim oSchema As DataTable = oDrd.GetSchemaTable()
        Dim oSchemaRow As DataRow = oSchema.Rows(0)
        With retval
            If FieldExists(oDrd, "Cod") Then
                .Codi = GetIntegerFromDataReader(oDrd("Cod"))
            End If
            If FieldExists(oDrd, AdrField) Then
                .Text = GetStringFromDataReader(oDrd(AdrField))
            End If
            If FieldExists(oDrd, AdrViaNomField) Then
                .ViaNom = SQLHelper.GetStringFromDataReader(oDrd(AdrViaNomField))
            End If
            If FieldExists(oDrd, AdrNumField) Then
                .Num = SQLHelper.GetStringFromDataReader(oDrd(AdrNumField))
            End If
            If FieldExists(oDrd, AdrPisField) Then
                .Pis = SQLHelper.GetStringFromDataReader(oDrd(AdrPisField))
            End If
            If FieldExists(oDrd, LongitudField) And FieldExists(oDrd, LatitudField) Then
                If Not IsDBNull(oDrd(LongitudField)) AndAlso Not IsDBNull(oDrd(LatitudField)) Then
                    .Coordenadas = New GeoHelper.Coordenadas(oDrd(LatitudField), oDrd(LongitudField))
                End If
            End If
            .Zip = GetZipFromDataReader(oDrd,
                                        ZipGuidField,
                                        ZipCodField,
                                        LocationGuidField,
                                        LocationNomField,
                                        ZonaGuidField,
                                        ZonaNomField,
                                        ZonaExportField,
                                        ProvinciaGuidField,
                                        ProvinciaNomField,
                                        ProvinciaIntrastatField,
                                        RegioGuidField,
                                        RegioNomField,
                                        CountryGuidField,
                                        CountryEspField,
                                        CountryCatField,
                                        CountryEngField,
                                        CountryPorField,
                                        CountryISOField,
                                        ExportCodField)
        End With
        Return retval
    End Function

    Shared Function GetZipFromDataReader(oDrd As SqlDataReader,
                                             Optional ZipGuidField As String = "ZipGuid",
                                             Optional ZipCodField As String = "ZipCod",
                                             Optional LocationGuidField As String = "LocationGuid",
                                             Optional LocationNomField As String = "LocationNom",
                                             Optional ZonaGuidField As String = "ZonaGuid",
                                             Optional ZonaNomField As String = "ZonaNom",
                                             Optional ZonaExportField As String = "ZonaExport",
                                             Optional ProvinciaGuidField As String = "ProvinciaGuid",
                                             Optional ProvinciaNomField As String = "ProvinciaNom",
                                                  Optional ProvinciaIntrastatField As String = "ProvinciaIntrastat",
                                        Optional RegioGuidField As String = "RegioGuid",
                                             Optional RegioNomField As String = "RegioNom",
                                             Optional CountryGuidField As String = "CountryGuid",
                                             Optional CountryEspField As String = "CountryEsp",
                                             Optional CountryCatField As String = "CountryCat",
                                             Optional CountryEngField As String = "CountryEng",
                                             Optional CountryPorField As String = "CountryPor",
                                             Optional CountryISOField As String = "CountryISO",
                                             Optional ExportCodField As String = "ExportCod",
                                             Optional CountryLangField As String = "CountryLang") As DTOZip
        Dim retval As DTOZip = Nothing
        Dim oSchema As DataTable = oDrd.GetSchemaTable()
        Dim oSchemaRow As DataRow = oSchema.Rows(0)

        If FieldExists(oDrd, ZipGuidField) Then
            If Not IsDBNull(oDrd(ZipGuidField)) Then
                retval = New DTOZip(oDrd(ZipGuidField))
                If FieldExists(oDrd, ZipCodField) Then
                    retval.ZipCod = GetStringFromDataReader(oDrd(ZipCodField))
                End If

                retval.Location = GetLocationFromDataReader(oDrd,
                                              LocationGuidField,
                                              LocationNomField,
                                              ZonaGuidField,
                                              ZonaNomField,
                                              ZonaExportField,
                                              ProvinciaGuidField,
                                              ProvinciaNomField,
                                              ProvinciaIntrastatField,
                                              RegioGuidField,
                                              RegioNomField,
                                              CountryGuidField,
                                              CountryEspField,
                                              CountryCatField,
                                              CountryEngField,
                                              CountryPorField,
                                              CountryISOField,
                                              ExportCodField,
                                              CountryLangField)
            End If
        End If
        Return retval
    End Function

    Shared Function GetLocationFromDataReader(oDrd As SqlDataReader,
                                             Optional LocationGuidField As String = "LocationGuid",
                                             Optional LocationNomField As String = "LocationNom",
                                             Optional ZonaGuidField As String = "ZonaGuid",
                                             Optional ZonaNomField As String = "ZonaNom",
                                             Optional ZonaExportField As String = "ZonaExport",
                                             Optional ProvinciaGuidField As String = "ProvinciaGuid",
                                             Optional ProvinciaNomField As String = "ProvinciaNom",
                                                  Optional ProvinciaIntrastatField As String = "ProvinciaIntrastat",
                                             Optional RegioGuidField As String = "RegioGuid",
                                             Optional RegioNomField As String = "RegioNom",
                                             Optional CountryGuidField As String = "CountryGuid",
                                             Optional CountryEspField As String = "CountryEsp",
                                             Optional CountryCatField As String = "CountryCat",
                                             Optional CountryEngField As String = "CountryEng",
                                             Optional CountryPorField As String = "CountryPor",
                                             Optional CountryISOField As String = "CountryISO",
                                             Optional ExportCodField As String = "ExportCod",
                                             Optional CountryLangField As String = "CountryLang"
                                             ) As DTOLocation
        Dim retval As DTOLocation = Nothing
        Dim oSchema As DataTable = oDrd.GetSchemaTable()
        Dim oSchemaRow As DataRow = oSchema.Rows(0)

        If FieldExists(oDrd, LocationGuidField) Then
            If Not IsDBNull(oDrd(LocationGuidField)) Then

                retval = New DTOLocation(oDrd(LocationGuidField))
                If FieldExists(oDrd, LocationNomField) Then
                    retval.Nom = GetStringFromDataReader(oDrd(LocationNomField))
                End If

                retval.Zona = GetZonaFromDataReader(oDrd,
                                                  ZonaGuidField,
                                                  ZonaNomField,
                                                  ZonaExportField,
                                                  ProvinciaGuidField,
                                                  ProvinciaNomField,
                                                  ProvinciaIntrastatField,
                                                  RegioGuidField,
                                                  RegioNomField,
                                                  CountryGuidField,
                                                  CountryEspField,
                                                  CountryCatField,
                                                  CountryEngField,
                                                  CountryPorField,
                                                  CountryISOField,
                                                  ExportCodField,
                                                  CountryLangField)
            End If
        End If
        Return retval
    End Function

    Shared Function GetZonaFromDataReader(oDrd As SqlDataReader,
                                             Optional ZonaGuidField As String = "ZonaGuid",
                                             Optional ZonaNomField As String = "ZonaNom",
                                             Optional ZonaExportField As String = "ZonaExport",
                                             Optional ProvinciaGuidField As String = "ProvinciaGuid",
                                             Optional ProvinciaNomField As String = "ProvinciaNom",
                                                       Optional ProvinciaIntrastatField As String = "ProvinciaIntrastat",
                                        Optional RegioGuidField As String = "RegioGuid",
                                             Optional RegioNomField As String = "RegioNom",
                                             Optional CountryGuidField As String = "CountryGuid",
                                             Optional CountryEspField As String = "CountryEsp",
                                             Optional CountryCatField As String = "CountryCat",
                                             Optional CountryEngField As String = "CountryEng",
                                             Optional CountryPorField As String = "CountryPor",
                                             Optional CountryISOField As String = "CountryISO",
                                             Optional ExportCodField As String = "ExportCod",
                                             Optional CountryLangField As String = "CountryLang"
                                             ) As DTOZona

        Dim retval As DTOZona = Nothing
        Dim oSchema As DataTable = oDrd.GetSchemaTable()
        Dim oSchemaRow As DataRow = oSchema.Rows(0)

        If FieldExists(oDrd, ZonaGuidField) Then
            retval = New DTOZona(oDrd(ZonaGuidField))
            If FieldExists(oDrd, ZonaNomField) Then
                retval.Nom = GetStringFromDataReader(oDrd(ZonaNomField))
            End If

            'If FieldExists(oDrd, ZonaExportField) Then
            'retval.ExportCod = oDrd(ZonaExportField) 'ZonaExportField es boolean
            If FieldExists(oDrd, ExportCodField) Then
                If Not IsDBNull(oDrd(ExportCodField)) Then
                    retval.ExportCod = oDrd(ExportCodField)
                End If
            End If

            If FieldExists(oDrd, "ZonaLang") AndAlso Not IsDBNull(oDrd("ZonaLang")) Then
                retval.Lang = DTOLang.Factory(oDrd("ZonaLang"))
            End If

            If FieldExists(oDrd, ProvinciaGuidField) Then
                If Not IsDBNull(oDrd(ProvinciaGuidField)) Then
                    Dim oProvincia As New DTOAreaProvincia(oDrd(ProvinciaGuidField))
                    oProvincia.Nom = GetStringFromDataReader(oDrd(ProvinciaNomField))
                    If FieldExists(oDrd, ProvinciaIntrastatField) Then
                        oProvincia.Intrastat = GetStringFromDataReader(oDrd(ProvinciaIntrastatField))
                    End If
                    If FieldExists(oDrd, RegioGuidField) Then
                        If Not IsDBNull(oDrd(RegioGuidField)) Then
                            oProvincia.Regio = New DTOAreaRegio(oDrd(RegioGuidField))
                            oProvincia.Regio.Nom = GetStringFromDataReader(oDrd(RegioNomField))
                        End If
                    End If
                    retval.Provincia = oProvincia
                End If
            End If

            retval.Country = GetCountryFromDataReader(oDrd,
                                              CountryGuidField,
                                              CountryEspField,
                                              CountryCatField,
                                              CountryEngField,
                                              CountryPorField,
                                              CountryISOField,
                                              ExportCodField,
                                              CountryLangField)
        End If
        Return retval
    End Function

    Shared Function GetCountryFromDataReader(oDrd As SqlDataReader,
                                             Optional CountryGuidField As String = "CountryGuid",
                                             Optional CountryEspField As String = "CountryEsp",
                                             Optional CountryCatField As String = "CountryCat",
                                             Optional CountryEngField As String = "CountryEng",
                                             Optional CountryPorField As String = "CountryPor",
                                             Optional CountryISOField As String = "CountryISO",
                                             Optional ExportCodField As String = "ExportCod",
                                             Optional CountryLangField As String = "CountryLang"
                                             ) As DTOCountry
        Dim retval As DTOCountry = Nothing
        Dim oSchema As DataTable = oDrd.GetSchemaTable()
        Dim oSchemaRow As DataRow = oSchema.Rows(0)

        If FieldExists(oDrd, CountryGuidField) Then
            retval = New DTOCountry(oDrd(CountryGuidField))
            retval.LangNom = GetLangTextFromDataReader(oDrd, CountryEspField, CountryCatField, CountryEngField, CountryPorField)
            If FieldExists(oDrd, CountryISOField) Then
                retval.ISO = SQLHelper.GetStringFromDataReader(oDrd(CountryISOField))
            End If
            If FieldExists(oDrd, ExportCodField) Then
                retval.ExportCod = SQLHelper.GetIntegerFromDataReader(oDrd(ExportCodField))
            End If
            If FieldExists(oDrd, CountryLangField) Then
                retval.Lang = SQLHelper.GetLangFromDataReader(oDrd(CountryLangField))
            End If
        End If
        Return retval
    End Function

    Shared Function getIbanFromDataReader(oDrd As SqlDataReader,
                                          Optional FieldIbanGuid As String = "IbanGuid",
                                          Optional FieldIbanCod As String = "IbanCod",
                                          Optional FieldIbanCcc As String = "IbanCcc",
                                          Optional FieldBank As String = "BankGuid",
                                          Optional FieldBankNom As String = "BankNom",
                                          Optional FieldBankAlias As String = "BankAlias",
                                          Optional FieldBankSwift As String = "BankSwift",
                                          Optional FieldBankBranchGuid As String = "BankBranchGuid",
                                          Optional FieldBankBranchAdr As String = "BankBranchAdr",
                                          Optional FieldBankBranchLocationGuid As String = "BankBranchZonaGuid",
                                          Optional FieldBankBranchLocationNom As String = "BankBranchLocationNom",
                                          Optional FieldBankBranchZonaGuid As String = "BankBranchZonaGuid",
                                          Optional FieldBankBranchZonaNom As String = "BankBranchZonaNom",
                                          Optional FieldBankBranchCountryGuid As String = "BankBranchCountryGuid",
                                          Optional FieldBankBranchCountryISO As String = "BankBranchCountryISO"
                                          ) As DTOIban
        Dim retval As DTOIban = Nothing
        If FieldExists(oDrd, FieldIbanGuid) Then
            If Not IsDBNull(oDrd(FieldIbanGuid)) Then
                retval = New DTOIban(oDrd(FieldIbanGuid))

                Dim oCountry As DTOCountry = Nothing
                If FieldExists(oDrd, FieldBankBranchCountryGuid) Then
                    If Not IsDBNull(oDrd(FieldBankBranchCountryGuid)) Then
                        oCountry = New DTOCountry(DirectCast(oDrd(FieldBankBranchCountryGuid), Guid))
                        If FieldExists(oDrd, FieldBankBranchCountryISO) Then
                            oCountry.ISO = SQLHelper.GetStringFromDataReader(oDrd(FieldBankBranchCountryISO))
                        End If
                    End If
                End If

                Dim oZona As DTOZona = Nothing
                If FieldExists(oDrd, FieldBankBranchZonaGuid) Then
                    If Not IsDBNull(oDrd(FieldBankBranchZonaGuid)) Then
                        oZona = New DTOZona(DirectCast(oDrd(FieldBankBranchZonaGuid), Guid))
                        oZona.Nom = GetStringFromDataReader(oDrd(FieldBankBranchZonaNom))
                        oZona.Country = oCountry
                    End If
                End If

                Dim oLocation As DTOLocation = Nothing
                If FieldExists(oDrd, FieldBankBranchLocationGuid) Then
                    If Not IsDBNull(oDrd(FieldBankBranchLocationGuid)) Then
                        oLocation = New DTOLocation(DirectCast(oDrd(FieldBankBranchLocationGuid), Guid))
                        oLocation.Nom = GetStringFromDataReader(oDrd(FieldBankBranchLocationNom))
                        oLocation.Zona = oZona
                    End If
                End If

                Dim oBank As DTOBank = Nothing
                If FieldExists(oDrd, FieldBank) Then
                    If Not IsDBNull(oDrd(FieldBank)) Then
                        oBank = New DTOBank(DirectCast(oDrd(FieldBank), Guid))
                        oBank.RaoSocial = GetStringFromDataReader(oDrd(FieldBankNom))
                        oBank.NomComercial = GetStringFromDataReader(oDrd(FieldBankAlias))
                        oBank.Swift = GetStringFromDataReader(oDrd(FieldBankSwift))
                    End If
                End If

                Dim oBankBranch As DTOBankBranch = Nothing
                If FieldExists(oDrd, FieldBankBranchGuid) Then
                    If Not IsDBNull(oDrd(FieldBankBranchGuid)) Then
                        oBankBranch = New DTOBankBranch(oDrd(FieldBankBranchGuid))
                        With oBankBranch
                            .Bank = oBank
                            .Location = oLocation
                            .Address = SQLHelper.GetStringFromDataReader(oDrd(FieldBankBranchAdr))
                        End With
                    End If
                End If

                With retval
                    .BankBranch = oBankBranch
                    If FieldExists(oDrd, FieldIbanCcc) Then
                        .Digits = SQLHelper.GetStringFromDataReader(oDrd(FieldIbanCcc))
                    End If
                End With
            End If
        End If
        Return retval
    End Function



    Shared Function FieldExists(oDrd As IDataReader, sFieldName As String) As Boolean
        Dim retval As Boolean
        For i = 0 To oDrd.FieldCount - 1
            If (oDrd.GetName(i).Equals(sFieldName, StringComparison.InvariantCultureIgnoreCase)) Then
                retval = True
                Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function FieldExists(oRow As DataRow, sFieldName As String) As Boolean
        Dim retval As Boolean
        If oRow.Table.Columns(sFieldName) IsNot Nothing Then
            retval = True
        End If
        Return retval
    End Function

#Region "SQLDependency"
    Shared Function DirtyTables() As List(Of DirtyTableModel)
        Dim retval As New List(Of DirtyTableModel)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT OBJECT_NAME(OBJECT_ID) AS TableName ")
        sb.AppendLine(", MAX(last_user_update) AS LastUpdate ")
        sb.AppendLine("FROM sys.dm_db_index_usage_stats ")
        sb.AppendLine("WHERE database_id = DB_ID( 'Maxi') AND last_user_update IS NOT NULL ")
        sb.AppendLine("GROUP BY OBJECT_NAME(OBJECT_ID) ")
        sb.AppendLine("ORDER BY LastUpdate DESC ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item = DirtyTableModel.Factory(SQLHelper.GetStringFromDataReader(oDrd("TableName")), SQLHelper.GetFchFromDataReader(oDrd("LastUpdate")))
            If item IsNot Nothing Then
                retval.Add(item)
            End If
        Loop
        Return retval
    End Function

    Shared Sub InitSQLDependency()
        ' Create a dependency connection.
        Dim ConnectionString = Defaults.SQLConnectionString
        SqlDependency.Start(ConnectionString)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT OBJECT_NAME(OBJECT_ID) AS TableName ")
        sb.AppendLine(", MAX(last_user_update) AS LastUpdate ")
        sb.AppendLine("FROM sys.dm_db_index_usage_stats ")
        sb.AppendLine("WHERE database_id = DB_ID( 'Maxi') AND last_user_update IS NOT NULL ")
        sb.AppendLine("GROUP BY OBJECT_NAME(OBJECT_ID) ")
        sb.AppendLine("ORDER BY LastUpdate DESC ")
        Dim SQL = sb.ToString
        Dim oConn As New SqlConnection(Defaults.SQLConnectionString)
        oConn.Open()
        Using SqlCommand As New SqlCommand(SQL, oConn)
            'Create a dependency And associate it with the SqlCommand.
            Dim dependency As New SqlDependency(SqlCommand)
            AddHandler dependency.OnChange, New OnChangeEventHandler(AddressOf OnDependencyChange)
            SqlCommand.ExecuteReader()
        End Using
    End Sub


    ' Handler method
    Shared Sub OnDependencyChange(ByVal sender As Object, ByVal e As SqlNotificationEventArgs)
        ' Handle the event (for example, invalidate this cache entry).
        RaiseEvent onSqlDependencyChange(sender, e)
    End Sub

    Shared Sub StopSQLDependency()
        ' Release the dependency
        If Not String.IsNullOrEmpty(Defaults.SQLConnectionString) Then
            SqlDependency.Stop(Defaults.SQLConnectionString, "MatQueue")
        End If
    End Sub
#End Region

    Shared Function GetNifsFromDataReader(oDrd As IDataReader, Optional fieldNif As String = "Nif", Optional fieldNifCod As String = "NifCod", Optional fieldNif2 As String = "Nif2", Optional fieldNif2Cod As String = "Nif2Cod") As DTONif.Collection
        Dim nif = IIf(FieldExists(oDrd, fieldNif), GetStringFromDataReader(oDrd(fieldNif)), "")
        Dim cod = IIf(FieldExists(oDrd, fieldNifCod), GetIntegerFromDataReader(oDrd(fieldNifCod)), DTONif.Cods.Unknown)
        Dim nif2 = IIf(FieldExists(oDrd, fieldNif2), GetStringFromDataReader(oDrd(fieldNif2)), "")
        Dim cod2 = IIf(FieldExists(oDrd, fieldNif2Cod), GetIntegerFromDataReader(oDrd(fieldNif2Cod)), DTONif.Cods.Unknown)
        Dim retval = DTONif.Collection.Factory(nif, cod, nif2, cod2)
        Return retval
    End Function

    Shared Sub SetNullableNifs(ByRef oRow As DataRow, oNifs As DTONif.Collection)
        If oNifs IsNot Nothing Then
            If oNifs.Count > 0 Then
                Dim oNif = oNifs.First
                If oNif.Value.isNotEmpty Then
                    oRow("Nif") = oNif.Value
                    oRow("NifCod") = oNif.Cod
                End If
                If oNifs.Count > 1 Then
                    oNif = oNifs(1)
                    If oNifs(1).Value.isNotEmpty AndAlso oNifs.First.Value <> oNifs(1).Value Then
                        oRow("Nif2") = oNif.Value
                        oRow("Nif2Cod") = oNif.Cod
                    End If
                End If
            End If
        End If
    End Sub

    Shared Function GetLangTextFromDataRow(oRow As DataRow, fieldEsp As String, fieldCat As String, fieldEng As String, fieldPor As String) As DTOLangText
        Dim retval As New DTOLangText
        If FieldExists(oRow, fieldEsp) Then
            If Not IsDBNull(oRow(fieldEsp)) Then
                retval.Esp = oRow(fieldEsp)
            End If
        End If
        If FieldExists(oRow, fieldCat) Then
            If Not IsDBNull(oRow(fieldCat)) Then
                retval.Cat = oRow(fieldCat)
            End If
        End If
        If FieldExists(oRow, fieldEng) Then
            If Not IsDBNull(oRow(fieldEng)) Then
                retval.Eng = oRow(fieldEng)
            End If
        End If
        If FieldExists(oRow, fieldPor) Then
            If Not IsDBNull(oRow(fieldPor)) Then
                retval.Por = oRow(fieldPor)
            End If
        End If
        Return retval
    End Function

    Shared Function GetLangFromDataReader(oLangTag As Object) As DTOLang
        Dim retval As DTOLang = Nothing
        If Not IsDBNull(oLangTag) Then
            Dim sTag As String = oLangTag.ToString
            retval = DTOLang.Factory(sTag)
        End If
        Return retval
    End Function

    Shared Function GetLangTextFromDataReader(odrd As SqlDataReader, Optional fieldEsp As String = "Esp", Optional fieldCat As String = "Cat", Optional fieldEng As String = "Eng", Optional fieldPor As String = "Por") As DTOLangText
        Dim retval As New DTOLangText
        If FieldExists(odrd, fieldEsp) Then
            If Not IsDBNull(odrd(fieldEsp)) Then
                retval.Esp = odrd(fieldEsp)
            End If
        End If
        If fieldCat > "" Then
            If FieldExists(odrd, fieldCat) Then
                If Not IsDBNull(odrd(fieldCat)) Then
                    retval.Cat = odrd(fieldCat)
                End If
            End If
        End If
        If fieldEng > "" Then
            If FieldExists(odrd, fieldEng) Then
                If Not IsDBNull(odrd(fieldEng)) Then
                    retval.Eng = odrd(fieldEng)
                End If
            End If
        End If
        If fieldPor > "" Then
            If FieldExists(odrd, fieldPor) Then
                If Not IsDBNull(odrd(fieldPor)) Then
                    retval.Por = odrd(fieldPor)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Sub LoadLangTextFromDataReader(ByRef oLangText As DTOLangText, odrd As SqlDataReader, Optional fieldEsp As String = "Esp", Optional fieldCat As String = "Cat", Optional fieldEng As String = "Eng", Optional fieldPor As String = "Por")
        If FieldExists(odrd, fieldEsp) Then
            If Not IsDBNull(odrd(fieldEsp)) Then
                oLangText.Esp = odrd(fieldEsp)
            End If
        End If
        If fieldCat > "" Then
            If FieldExists(odrd, fieldCat) Then
                If Not IsDBNull(odrd(fieldCat)) Then
                    oLangText.Cat = odrd(fieldCat)
                End If
            End If
        End If
        If fieldEng > "" Then
            If FieldExists(odrd, fieldEng) Then
                If Not IsDBNull(odrd(fieldEng)) Then
                    oLangText.Eng = odrd(fieldEng)
                End If
            End If
        End If
        If fieldPor > "" Then
            If FieldExists(odrd, fieldPor) Then
                If Not IsDBNull(odrd(fieldPor)) Then
                    oLangText.Por = odrd(fieldPor)
                End If
            End If
        End If
    End Sub

    Shared Function LangTextResultFromDataReader(oLang As DTOLang, odrd As SqlDataReader, Optional fieldEsp As String = "Esp", Optional fieldCat As String = "Cat", Optional fieldEng As String = "Eng", Optional fieldPor As String = "Por") As String
        Dim oLangText As New DTOLangText()
        If FieldExists(odrd, fieldEsp) Then
            If Not IsDBNull(odrd(fieldEsp)) Then
                oLangText.Esp = odrd(fieldEsp)
            End If
        End If
        Select Case oLang.id
            Case DTOLang.Ids.CAT
                If fieldCat > "" Then
                    If FieldExists(odrd, fieldCat) Then
                        If Not IsDBNull(odrd(fieldCat)) Then
                            oLangText.Cat = odrd(fieldCat)
                        End If
                    End If
                End If
            Case DTOLang.Ids.ENG
                If fieldEng > "" Then
                    If FieldExists(odrd, fieldEng) Then
                        If Not IsDBNull(odrd(fieldEng)) Then
                            oLangText.Eng = odrd(fieldEng)
                        End If
                    End If
                End If
            Case DTOLang.Ids.POR
                If fieldPor > "" Then
                    If FieldExists(odrd, fieldPor) Then
                        If Not IsDBNull(odrd(fieldPor)) Then
                            oLangText.Por = odrd(fieldPor)
                        End If
                    End If
                End If
        End Select
        Dim retval As String = oLangText.Tradueix(oLang)
        Return retval
    End Function


    Shared Function GetAreaFromDataReader(oDrd) As DTOArea
        Dim retval As DTOArea = Nothing
        If FieldExists(oDrd, "CountryGuid") Then
            If Not IsDBNull(oDrd("CountryGuid")) Then
                Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNomEsp", "CountryNomCat", "CountryNomEng", "CountryNomPor")
                End With
                If FieldExists(oDrd, "ZonaGuid") AndAlso Not IsDBNull(oDrd("ZonaGuid")) Then
                    retval = New DTOZona(oDrd("ZonaGuid"))
                    With DirectCast(retval, DTOZona)
                        .Country = oCountry
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    End With
                ElseIf IsDBNull(oDrd("ZonaGuid")) Then
                    retval = oCountry
                Else
                    Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                    With oZona
                        If FieldExists(oDrd, "ZonaNom") Then
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                        End If
                    End With

                    If FieldExists(oDrd, "LocationGuid") Then
                        If IsDBNull(oDrd("LocationGuid")) Then
                            retval = oZona
                        Else
                            Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
                            With oLocation
                                If FieldExists(oDrd, "LocationNom") Then
                                    .Nom = GetStringFromDataReader(oDrd("LocationNom"))
                                End If
                                .Zona = oZona
                            End With
                            retval = oLocation
                            If FieldExists(oDrd, "ZipGuid") Then
                                If IsDBNull(oDrd("ZipGuid")) Then
                                    retval = oLocation
                                Else
                                    Dim oZip As New DTOZip(oDrd("ZipGuid"))
                                    With oZip
                                        If FieldExists(oDrd, "ZipCod") Then
                                            .ZipCod = GetStringFromDataReader(oDrd("ZipCod"))
                                        End If
                                        .Location = oLocation
                                    End With
                                    retval = oZip
                                End If
                            Else
                                retval = oLocation
                            End If
                        End If
                    Else
                        retval = oZona
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function GetProductFromDataReader(oDrd As SqlDataReader,
                                             Optional brandGuidField As String = "BrandGuid",
                                             Optional brandNomField As String = "BrandNom",
                                             Optional categoryGuidField As String = "CategoryGuid",
                                             Optional categoryNomField As String = "CategoryNom",
                                             Optional skuGuidField As String = "SkuGuid",
                                             Optional skuNomField As String = "SkuNom",
                                             Optional skuNomLlargField As String = "SkuNomLlarg",
                                             Optional skuNomLlargEspField As String = "SkuNomLlargEsp"
                                             ) As DTOProduct
        Dim retval As DTOProduct = Nothing

        If FieldExists(oDrd, brandGuidField) Then
            If Not IsDBNull(oDrd(brandGuidField)) Then


                Dim oBrand As New DTOProductBrand(oDrd(brandGuidField))
                With oBrand
                    .SourceCod = DTOProduct.SourceCods.Brand
                    If FieldExists(oDrd, "Emp") Then
                        .Emp = New DTOEmp(oDrd("Emp"))
                    End If
                    If FieldExists(oDrd, brandNomField) Then
                        SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, brandNomField, brandNomField, brandNomField, brandNomField)
                    End If
                    If FieldExists(oDrd, "Proveidor") Then
                        If Not IsDBNull(oDrd("Proveidor")) Then
                            .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                        End If
                    End If
                    If FieldExists(oDrd, "BrandObsoleto") Then
                        .obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("BrandObsoleto"))
                    End If
                End With

                If FieldExists(oDrd, "DeptGuid") AndAlso Not IsDBNull(oDrd("DeptGuid")) Then
                    retval = New DTODept(oDrd("DeptGuid"))
                    With DirectCast(retval, DTODept)
                        .Brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "DeptNom", "DeptNom", "DeptNom", "DeptNom")
                    End With
                ElseIf IsDBNull(oDrd(categoryGuidField)) Then
                    retval = oBrand
                Else
                    Dim oCategory As New DTOProductCategory(oDrd(categoryGuidField))
                    With oCategory
                        .SourceCod = DTOProduct.SourceCods.Category
                        If FieldExists(oDrd, categoryNomField) Then
                            SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, categoryNomField, categoryNomField, categoryNomField, categoryNomField)
                        ElseIf FieldExists(oDrd, "CategoryNomEsp") Then
                            SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                        End If
                        If FieldExists(oDrd, "CategoryCodi") Then
                            .Codi = GetStringFromDataReader(oDrd("CategoryCodi"))
                        End If
                        .Brand = oBrand
                        If FieldExists(oDrd, "CategoryDimensionL") Then
                            .DimensionL = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryDimensionL"))
                        End If
                        If FieldExists(oDrd, "CategoryDimensionW") Then
                            .DimensionW = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryDimensionW"))
                        End If
                        If FieldExists(oDrd, "CategoryKg") Then
                            .KgBrut = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryKg"))
                        End If
                        If FieldExists(oDrd, "CategoryKgNet") Then
                            .KgNet = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryKgNet"))
                        End If
                        If FieldExists(oDrd, "CategoryDimensionH") Then
                            .DimensionH = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryDimensionH"))
                        End If
                        If FieldExists(oDrd, "CategoryMoq") Then
                            .InnerPack = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryMoq"))
                        End If
                        If FieldExists(oDrd, "CategoryForzarMoq") Then
                            .ForzarInnerPack = SQLHelper.GetDecimalFromDataReader(oDrd("CategoryForzarMoq"))
                        End If
                        If FieldExists(oDrd, "CategoryHideUntil") Then
                            .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("CategoryHideUntil"))
                        End If
                        If FieldExists(oDrd, "CategoryObsoleto") Then
                            .obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("CategoryObsoleto"))
                        End If

                    End With

                    If FieldExists(oDrd, skuGuidField) Then
                        If IsDBNull(oDrd(skuGuidField)) Then
                            retval = oCategory
                        Else
                            Dim oSku As New DTOProductSku(oDrd(skuGuidField))
                            With oSku
                                .SourceCod = DTOProduct.SourceCods.Sku
                                If FieldExists(oDrd, skuNomField) Then
                                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, skuNomField, "SkuNomCat", "SkuNomEng", "SkuNomPor")
                                ElseIf FieldExists(oDrd, "SkuNomEsp") Then
                                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                                End If
                                If FieldExists(oDrd, skuNomLlargField) Then
                                    SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, skuNomLlargField, "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                                ElseIf FieldExists(oDrd, skuNomLlargEspField) Then
                                    SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                                End If
                                If FieldExists(oDrd, "SkuRef") Then
                                    .RefProveidor = GetStringFromDataReader(oDrd("SkuRef"))
                                End If
                                If FieldExists(oDrd, "SkuPrvNom") Then
                                    .NomProveidor = GetStringFromDataReader(oDrd("SkuPrvNom"))
                                End If

                                If FieldExists(oDrd, "SkuId") Then
                                    .Id = GetStringFromDataReader(oDrd("SkuId"))
                                End If
                                If FieldExists(oDrd, "EAN13") Then
                                    .Ean13 = GetEANFromDataReader(oDrd("EAN13"))
                                End If
                                If FieldExists(oDrd, "HeredaDimensions") Then
                                    .HeredaDimensions = SQLHelper.GetBooleanFromDatareader(oDrd("HeredaDimensions"))
                                End If
                                If FieldExists(oDrd, "SkuDimensionL") Then
                                    .DimensionL = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionL"))
                                End If
                                If FieldExists(oDrd, "SkuDimensionW") Then
                                    .DimensionW = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionW"))
                                End If
                                If FieldExists(oDrd, "SkuDimensionH") Then
                                    .DimensionH = SQLHelper.GetDecimalFromDataReader(oDrd("SkuDimensionH"))
                                End If
                                If FieldExists(oDrd, "SkuKg") Then
                                    .KgBrut = SQLHelper.GetDecimalFromDataReader(oDrd("SkuKg"))
                                End If
                                If FieldExists(oDrd, "SkuKgNet") Then
                                    .KgNet = SQLHelper.GetDecimalFromDataReader(oDrd("SkuKgNet"))
                                End If
                                If FieldExists(oDrd, "SkuMoq") Then
                                    .InnerPack = SQLHelper.GetDecimalFromDataReader(oDrd("SkuMoq"))
                                End If
                                If FieldExists(oDrd, "SkuForzarMoq") Then
                                    .ForzarInnerPack = SQLHelper.GetDecimalFromDataReader(oDrd("SkuForzarMoq"))
                                End If

                                If FieldExists(oDrd, "SkuNoStk") Then
                                    Dim oNoStk = SQLHelper.GetBooleanFromDatareader(oDrd("SkuNoStk"))
                                    .NoStk = oNoStk
                                End If
                                If FieldExists(oDrd, "SecurityStock") Then
                                    .SecurityStock = SQLHelper.GetIntegerFromDataReader(oDrd("SecurityStock"))
                                End If
                                If FieldExists(oDrd, "LastProduction") Then
                                    .LastProduction = SQLHelper.GetBooleanFromDatareader(oDrd("LastProduction"))
                                End If
                                If FieldExists(oDrd, "SkuHideUntil") Then
                                    .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("SkuHideUntil"))
                                End If
                                If FieldExists(oDrd, "isBundle") Then
                                    .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                                End If
                                If FieldExists(oDrd, "Obsoleto") Then
                                    .obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
                                End If
                                If FieldExists(oDrd, "FchObsoleto") Then
                                    .FchObsoleto = SQLHelper.GetFchFromDataReader(oDrd("FchObsoleto"))
                                End If
                                If FieldExists(oDrd, "ObsoletoConfirmed") Then
                                    .ObsoletoConfirmed = SQLHelper.GetFchFromDataReader(oDrd("ObsoletoConfirmed"))
                                End If
                                .Category = oCategory
                                If FieldExists(oDrd, "Madein") Then
                                    If Not IsDBNull(oDrd("MadeIn")) Then
                                        .MadeIn = New DTOCountry(oDrd("MadeIn"))
                                    End If
                                End If
                                If FieldExists(oDrd, "CodiMercancia") Then
                                    If Not IsDBNull(oDrd("CodiMercancia")) Then
                                        .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia"))
                                    End If
                                End If
                            End With
                            retval = oSku
                        End If
                    Else
                        retval = oCategory
                    End If
                End If
            End If
        End If

        Return retval
    End Function

    Shared Function GetProductBrandUrlCanonicasFromDataReader(oDrd As SqlDataReader,
                                                 Optional UrlBrandEspField As String = "UrlBrandEsp",
                                                 Optional UrlBrandCatField As String = "UrlBrandCat",
                                                 Optional UrlBrandEngField As String = "UrlBrandEng",
                                                 Optional UrlBrandPorField As String = "UrlBrandPor"
                                                    ) As DTOUrl

        Dim retval As DTOUrl = Nothing

        Dim oBrandUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlBrandEspField) AndAlso Not IsDBNull(oDrd(UrlBrandEspField)) Then
            oBrandUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlBrandEspField, UrlBrandCatField, UrlBrandEngField, UrlBrandPorField)
        End If

        If oBrandUrl Is Nothing Then
        Else
            retval = DTOUrl.Factory(oBrandUrl)
        End If

        Return retval
    End Function



    Shared Function GetProductDeptUrlCanonicasFromDataReader(oDrd As SqlDataReader,
                                                 Optional UrlBrandEspField As String = "UrlBrandEsp",
                                                 Optional UrlBrandCatField As String = "UrlBrandCat",
                                                 Optional UrlBrandEngField As String = "UrlBrandEng",
                                                 Optional UrlBrandPorField As String = "UrlBrandPor",
                                                 Optional UrlDeptEspField As String = "UrlDeptEsp",
                                                 Optional UrlDeptCatField As String = "UrlDeptCat",
                                                 Optional UrlDeptEngField As String = "UrlDeptEng",
                                                 Optional UrlDeptPorField As String = "UrlDeptPor"
                                                    ) As DTOUrl

        Dim retval As DTOUrl = Nothing

        Dim oBrandUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlBrandEspField) AndAlso Not IsDBNull(oDrd(UrlBrandEspField)) Then
            oBrandUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlBrandEspField, UrlBrandCatField, UrlBrandEngField, UrlBrandPorField)
        End If

        Dim oDeptUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlDeptEspField) AndAlso Not IsDBNull(oDrd(UrlDeptEspField)) Then
            oDeptUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlDeptEspField, UrlDeptCatField, UrlDeptEngField, UrlDeptPorField)
        End If

        If oDeptUrl Is Nothing Then
            If oBrandUrl Is Nothing Then
            Else
                retval = DTOUrl.Factory(oBrandUrl)
            End If
        Else
            retval = DTOUrl.Factory(oBrandUrl, oDeptUrl)
        End If

        Return retval
    End Function


    Shared Function GetProductCategoryUrlCanonicasFromDataReader(oDrd As SqlDataReader,
                                                 Optional UrlBrandEspField As String = "UrlBrandEsp",
                                                 Optional UrlBrandCatField As String = "UrlBrandCat",
                                                 Optional UrlBrandEngField As String = "UrlBrandEng",
                                                 Optional UrlBrandPorField As String = "UrlBrandPor",
                                                 Optional UrlDeptEspField As String = "UrlDeptEsp",
                                                 Optional UrlDeptCatField As String = "UrlDeptCat",
                                                 Optional UrlDeptEngField As String = "UrlDeptEng",
                                                 Optional UrlDeptPorField As String = "UrlDeptPor",
                                                 Optional UrlCategoryEspField As String = "UrlCategoryEsp",
                                                 Optional UrlCategoryCatField As String = "UrlCategoryCat",
                                                 Optional UrlCategoryEngField As String = "UrlCategoryEng",
                                                 Optional UrlCategoryPorField As String = "UrlCategoryPor",
                                                 Optional IncludeDeptOnUrlField As String = "IncludeDeptOnUrl"
                                                    ) As DTOUrl

        Dim retval As DTOUrl = Nothing

        Dim oBrandUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlBrandEspField) AndAlso Not IsDBNull(oDrd(UrlBrandEspField)) Then
            oBrandUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlBrandEspField, UrlBrandCatField, UrlBrandEngField, UrlBrandPorField)
        End If

        Dim oDeptUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlDeptEspField) AndAlso Not IsDBNull(oDrd(UrlDeptEspField)) Then
            oDeptUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlDeptEspField, UrlDeptCatField, UrlDeptEngField, UrlDeptPorField)
        End If

        Dim oCategoryUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlCategoryEspField) AndAlso Not IsDBNull(oDrd(UrlCategoryEspField)) Then
            oCategoryUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlCategoryEspField, UrlCategoryCatField, UrlCategoryEngField, UrlCategoryPorField)
        End If

        If oCategoryUrl Is Nothing Then
            If oDeptUrl Is Nothing Then
                If oBrandUrl Is Nothing Then
                Else
                    retval = DTOUrl.Factory(oBrandUrl)
                End If
            Else
                retval = DTOUrl.Factory(oBrandUrl, oDeptUrl)
            End If
        Else
            If SQLHelper.GetBooleanFromDatareader(oDrd(IncludeDeptOnUrlField)) Then
                retval = DTOUrl.Factory(oBrandUrl, oDeptUrl, oCategoryUrl)
            Else
                retval = DTOUrl.Factory(oBrandUrl, oCategoryUrl)
            End If
        End If

        Return retval
    End Function

    Shared Function GetProductUrlCanonicasFromDataReader(oDrd As SqlDataReader,
                                                 Optional UrlBrandEspField As String = "UrlBrandEsp",
                                                 Optional UrlBrandCatField As String = "UrlBrandCat",
                                                 Optional UrlBrandEngField As String = "UrlBrandEng",
                                                 Optional UrlBrandPorField As String = "UrlBrandPor",
                                                 Optional UrlDeptEspField As String = "UrlDeptEsp",
                                                 Optional UrlDeptCatField As String = "UrlDeptCat",
                                                 Optional UrlDeptEngField As String = "UrlDeptEng",
                                                 Optional UrlDeptPorField As String = "UrlDeptPor",
                                                 Optional UrlCategoryEspField As String = "UrlCategoryEsp",
                                                 Optional UrlCategoryCatField As String = "UrlCategoryCat",
                                                 Optional UrlCategoryEngField As String = "UrlCategoryEng",
                                                 Optional UrlCategoryPorField As String = "UrlCategoryPor",
                                                 Optional UrlSkuEspField As String = "UrlSkuEsp",
                                                 Optional UrlSkuCatField As String = "UrlSkuCat",
                                                 Optional UrlSkuEngField As String = "UrlSkuEng",
                                                 Optional UrlSkuPorField As String = "UrlSkuPor",
                                                 Optional IncludeDeptOnUrlField As String = "IncludeDeptOnUrl"
                                                    ) As DTOUrl

        Dim retval As DTOUrl = Nothing

        Dim oBrandUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlBrandEspField) AndAlso Not IsDBNull(oDrd(UrlBrandEspField)) Then
            oBrandUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlBrandEspField, UrlBrandCatField, UrlBrandEngField, UrlBrandPorField)
        End If

        Dim oDeptUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlDeptEspField) AndAlso Not IsDBNull(oDrd(UrlDeptEspField)) Then
            oDeptUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlDeptEspField, UrlDeptCatField, UrlDeptEngField, UrlDeptPorField)
        End If

        Dim oCategoryUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlCategoryEspField) AndAlso Not IsDBNull(oDrd(UrlCategoryEspField)) Then
            oCategoryUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlCategoryEspField, UrlCategoryCatField, UrlCategoryEngField, UrlCategoryPorField)
        End If

        Dim oSkuUrl As DTOLangText = Nothing
        If FieldExists(oDrd, UrlSkuEspField) AndAlso Not IsDBNull(oDrd(UrlSkuEspField)) Then
            oSkuUrl = SQLHelper.GetLangTextFromDataReader(oDrd, UrlSkuEspField, UrlSkuCatField, UrlSkuEngField, UrlSkuPorField)
        End If

        If oSkuUrl Is Nothing Then
            If oCategoryUrl Is Nothing Then
                If oDeptUrl Is Nothing Then
                    If oBrandUrl Is Nothing Then
                    Else
                        retval = DTOUrl.Factory(oBrandUrl)
                    End If
                Else
                    retval = DTOUrl.Factory(oBrandUrl, oDeptUrl)
                End If
            Else
                If SQLHelper.GetBooleanFromDatareader(oDrd(IncludeDeptOnUrlField)) Then
                    retval = DTOUrl.Factory(oBrandUrl, oDeptUrl, oCategoryUrl)
                Else
                    retval = DTOUrl.Factory(oBrandUrl, oCategoryUrl)
                End If
            End If
        Else
            If SQLHelper.GetBooleanFromDatareader(oDrd(IncludeDeptOnUrlField)) Then
                retval = DTOUrl.Factory(oBrandUrl, oDeptUrl, oCategoryUrl, oSkuUrl)
            Else
                retval = DTOUrl.Factory(oBrandUrl, oCategoryUrl, oSkuUrl)
            End If
        End If

        Return retval
    End Function


    Shared Function getUsrLogFromDataReader(odrd As SqlDataReader,
                                            Optional UsrCreatedField As String = "UsrCreated",
                                            Optional UsrCreatedEmailAddressField As String = "UsrCreatedEmailAddress",
                                            Optional UsrCreatedNickNameField As String = "UsrCreatedNickName",
                                            Optional UsrLastEditedField As String = "UsrLastEdited",
                                            Optional UsrLastEditedEmailAddressField As String = "UsrLastEditedEmailAddress",
                                            Optional UsrLastEditedNickNameField As String = "UsrLastEditedNickName",
                                            Optional FchCreatedField As String = "FchCreated",
                                            Optional FchLastEditedField As String = "FchLastEdited"
                                            ) As DTOUsrLog
        Dim retval As New DTOUsrLog
        With retval
            If FieldExists(odrd, FchCreatedField) Then
                .FchCreated = GetFchFromDataReader(odrd(FchCreatedField))
            End If

            If FieldExists(odrd, UsrCreatedField) Then
                If Not IsDBNull(odrd(UsrCreatedField)) Then
                    .UsrCreated = New DTOUser(DirectCast(odrd(UsrCreatedField), Guid))
                    If FieldExists(odrd, UsrCreatedEmailAddressField) Then
                        .UsrCreated.EmailAddress = SQLHelper.GetStringFromDataReader(odrd(UsrCreatedEmailAddressField))
                    End If
                    If FieldExists(odrd, UsrCreatedNickNameField) Then
                        .UsrCreated.NickName = SQLHelper.GetStringFromDataReader(odrd(UsrCreatedNickNameField))
                    End If
                End If
            End If

            If FieldExists(odrd, FchLastEditedField) Then
                .FchLastEdited = GetFchFromDataReader(odrd(FchLastEditedField))
            End If
            If FieldExists(odrd, UsrLastEditedField) Then
                If Not IsDBNull(odrd(UsrLastEditedField)) Then
                    .UsrLastEdited = New DTOUser(DirectCast(odrd(UsrLastEditedField), Guid))
                    If FieldExists(odrd, UsrLastEditedEmailAddressField) Then
                        .UsrLastEdited.EmailAddress = SQLHelper.GetStringFromDataReader(odrd(UsrLastEditedEmailAddressField))
                    End If
                    If FieldExists(odrd, UsrLastEditedNickNameField) Then
                        .UsrLastEdited.NickName = SQLHelper.GetStringFromDataReader(odrd(UsrLastEditedNickNameField))
                    End If
                End If
            End If
        End With
        Return retval
    End Function

    Shared Function getUsrLog2FromDataReader(odrd As SqlDataReader,
                                            Optional UsrCreatedField As String = "UsrCreated",
Optional UsrCreatedEmailAddressField As String = "UsrCreatedEmailAddress",
                                            Optional UsrCreatedNickNameField As String = "UsrCreatedNickName",
                                            Optional UsrLastEditedField As String = "UsrLastEdited",
                                            Optional UsrLastEditedEmailAddressField As String = "UsrLastEditedEmailAddress",
                                            Optional UsrLastEditedNickNameField As String = "UsrLastEditedNickName",
                                            Optional FchCreatedField As String = "FchCreated",
                                            Optional FchLastEditedField As String = "FchLastEdited"
                                            ) As DTOUsrLog2
        Dim retval As New DTOUsrLog2
        With retval
            If FieldExists(odrd, FchCreatedField) Then
                .FchCreated = GetFchFromDataReader(odrd(FchCreatedField))
            End If

            If FieldExists(odrd, UsrCreatedField) Then
                If Not IsDBNull(odrd(UsrCreatedField)) Then
                    .UsrCreated = New DTOGuidNom(CType(odrd(UsrCreatedField), Guid))
                    If FieldExists(odrd, UsrCreatedNickNameField) Then
                        .UsrCreated.Nom = SQLHelper.GetStringFromDataReader(odrd(UsrCreatedNickNameField))
                    End If
                    If String.IsNullOrEmpty(.UsrCreated.Nom) AndAlso FieldExists(odrd, UsrCreatedEmailAddressField) Then
                        .UsrCreated.Nom = SQLHelper.GetStringFromDataReader(odrd(UsrCreatedEmailAddressField))
                    End If
                End If
            End If

            If FieldExists(odrd, FchLastEditedField) Then
                .FchLastEdited = GetFchFromDataReader(odrd(FchLastEditedField))
            End If

            If FieldExists(odrd, UsrLastEditedField) Then
                If Not IsDBNull(odrd(UsrLastEditedField)) Then
                    .UsrLastEdited = New DTOGuidNom(CType(odrd(UsrLastEditedField), Guid))
                    If FieldExists(odrd, UsrLastEditedNickNameField) Then
                        .UsrLastEdited.Nom = SQLHelper.GetStringFromDataReader(odrd(UsrLastEditedNickNameField))
                    End If
                    If String.IsNullOrEmpty(.UsrLastEdited.Nom) AndAlso FieldExists(odrd, UsrLastEditedEmailAddressField) Then
                        .UsrLastEdited.Nom = SQLHelper.GetStringFromDataReader(odrd(UsrLastEditedEmailAddressField))
                    End If
                End If
            End If
        End With
        Return retval
    End Function

    Shared Function GetSiiLogFromDataReader(oDrd As SqlDataReader,
                                            Optional FieldResult As String = "SiiResult",
                                            Optional FieldFch As String = "SiiFch",
                                            Optional FieldCsv As String = "SiiCsv",
                                            Optional FieldErr As String = "SiiErr") As DTOSiiLog
        Dim retval As New DTOSiiLog
        With retval
            If FieldExists(oDrd, FieldResult) Then
                .Result = SQLHelper.GetIntegerFromDataReader(oDrd(FieldResult))
            End If
            If FieldExists(oDrd, FieldFch) Then
                .Fch = SQLHelper.GetFchFromDataReader(oDrd(FieldFch))
            End If
            If FieldExists(oDrd, FieldCsv) Then
                .Csv = SQLHelper.GetStringFromDataReader(oDrd(FieldCsv))
            End If
            If FieldExists(oDrd, FieldErr) Then
                .ErrMsg = SQLHelper.GetStringFromDataReader(oDrd(FieldErr))
            End If
        End With
        Return retval
    End Function

    Shared Function GetDocFileFromDataReaderHash(src As Object) As DTODocFile
        Dim retval As DTODocFile = Nothing
        If Not IsDBNull(src) Then
            retval = New DTODocFile(src)
        End If
        Return retval
    End Function

    Shared Function GetWeekdaysFromDataReader(src As Object) As List(Of Boolean)
        Dim retval As List(Of Boolean)
        If IsDBNull(src) OrElse String.IsNullOrEmpty(src) Then
            retval = {False, False, False, False, False, False, False}.ToList()
        Else
            retval = New List(Of Boolean)
            retval.Add(src.Substring(0, 1) = "1")
            retval.Add(src.Substring(1, 1) = "1")
            retval.Add(src.Substring(2, 1) = "1")
            retval.Add(src.Substring(3, 1) = "1")
            retval.Add(src.Substring(4, 1) = "1")
            retval.Add(src.Substring(5, 1) = "1")
            retval.Add(src.Substring(6, 1) = "1")
        End If
        Return retval
    End Function

    Shared Function GetDocFileFromDataReader(oDrd As SqlDataReader) As DTODocFile
        Dim retval As DTODocFile = Nothing
        If FieldExists(oDrd, "DocfileHash") Then
            If Not IsDBNull(oDrd("DocfileHash")) Then
                retval = New DTODocFile(oDrd("DocfileHash"))
                If FieldExists(oDrd, "DocfileLength") Then retval.Length = oDrd("DocfileLength")
                If FieldExists(oDrd, "DocfileMime") Then retval.Mime = oDrd("DocfileMime")
                If FieldExists(oDrd, "DocfilePags") Then retval.Pags = oDrd("DocfilePags")
                If FieldExists(oDrd, "DocfileVRes") Then retval.VRes = oDrd("DocfileVRes")
                If FieldExists(oDrd, "DocfileHRes") Then retval.HRes = oDrd("DocfileHRes")
                If FieldExists(oDrd, "DocfileWidth") AndAlso FieldExists(oDrd, "DocfileHeight") Then
                    retval.Size = New System.Drawing.Size(oDrd("DocfileWidth"), oDrd("DocfileHeight"))
                End If
                If FieldExists(oDrd, "DocfileNom") Then retval.Nom = SQLHelper.GetStringFromDataReader(oDrd("DocfileNom"))
                If FieldExists(oDrd, "DocfileFch") Then retval.Fch = oDrd("DocfileFch")
                If FieldExists(oDrd, "DocfileFchCreated") Then retval.FchCreated = oDrd("DocfileFchCreated")
                If FieldExists(oDrd, "DocfileThumbnail") Then retval.Thumbnail = oDrd("DocfileThumbnail")
            End If
        End If
        Return retval
    End Function

    Shared Function GetIncotermFromDataReader(src As Object) As DTOIncoterm
        Dim retval As DTOIncoterm = Nothing
        If Not IsDBNull(src) Then
            retval = DTOIncoterm.Factory(src.ToString)
        End If
        Return retval
    End Function


    Shared Function NullableIncoterm(oIncoterm As DTOIncoterm) As Object
        Dim retval As Object = System.DBNull.Value
        If oIncoterm IsNot Nothing AndAlso oIncoterm.Id.Trim > "" Then
            retval = oIncoterm.Id.ToString
        End If
        Return retval
    End Function

    Shared Function NullableWeekdays(src As List(Of Boolean)) As String
        Dim sb As New Text.StringBuilder
        sb.Append(IIf(src(0), "1", "0"))
        sb.Append(IIf(src(1), "1", "0"))
        sb.Append(IIf(src(2), "1", "0"))
        sb.Append(IIf(src(3), "1", "0"))
        sb.Append(IIf(src(4), "1", "0"))
        sb.Append(IIf(src(5), "1", "0"))
        sb.Append(IIf(src(6), "1", "0"))
        Return sb.ToString()
    End Function

    Shared Function GetFchFromDataReader(src As Object) As Date
        Dim retval As Date = Nothing
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetDateTimeOffsetFromDataReader(src As Object) As DateTimeOffset
        Dim retval As DateTimeOffset = Nothing
        If Not IsDBNull(src) Then
            If TypeOf src Is DateTime Then
                Dim DtFch As DateTime = src
                Dim oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time")
                retval = New DateTimeOffset(DtFch, oTimeZoneBcn.GetUtcOffset(DtFch))
            Else
                retval = src
            End If
        End If
        Return retval
    End Function

    Shared Function GetNullableFchFromDataReader(src As Object) As Nullable(Of Date)
        Dim retval As Nullable(Of Date) = Nothing
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function



    Shared Function GetBytesFromDatareader(src As Object) As Byte()
        Dim retval As Byte() = Nothing
        If Not IsDBNull(src) Then
            retval = src
        End If
        Return retval
    End Function

    Shared Function GetBooleanFromDatareader(src As Object) As Boolean
        Dim retval As Boolean
        If Not IsDBNull(src) Then
            retval = src <> 0
        End If
        Return retval
    End Function

    Shared Function NullableInteger(src As Integer) As Object
        Dim retval As Object
        If src = 0 Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function

    Shared Function NullableString(src As String, Optional maxLength As Integer = 0) As Object
        Dim retval As Object
        If String.IsNullOrEmpty(src) Then
            retval = System.DBNull.Value
        ElseIf maxLength > 0 AndAlso src.Trim().Length > maxLength Then
            retval = src.Trim().Substring(0, maxLength)
        Else
            retval = src.Trim()
        End If
        Return retval
    End Function

    Shared Function NullableLang(oLang As DTOLang) As Object
        Dim retval As Object = System.DBNull.Value
        If oLang IsNot Nothing Then
            retval = oLang.Tag
        End If
        Return retval
    End Function

    Shared Function NullableLangText(src As DTOLangText, oLang As DTOLang) As Object
        Dim retval As Object = System.DBNull.Value
        If src IsNot Nothing Then
            Select Case oLang.id
                Case DTOLang.Ids.ESP
                    If src.Esp > "" Then retval = src.Esp
                Case DTOLang.Ids.CAT
                    If src.Cat > "" Then retval = src.Cat
                Case DTOLang.Ids.ENG
                    If src.Eng > "" Then retval = src.Eng
                Case DTOLang.Ids.POR
                    If src.Por > "" Then retval = src.Por
            End Select
        End If
        Return retval
    End Function

    Shared Sub SetNullableLangText(oLangText As Object, oRow As DataRow, sFieldEsp As String, Optional sFieldCat As String = "", Optional sFieldEng As String = "", Optional sFieldPor As String = "")
        If oLangText IsNot Nothing Then
            Dim esp As String = DirectCast(oLangText, DTOLangText).Esp
            Dim cat As String = DirectCast(oLangText, DTOLangText).Cat
            Dim eng As String = DirectCast(oLangText, DTOLangText).Eng
            Dim por As String = DirectCast(oLangText, DTOLangText).Por

            oRow(sFieldEsp) = IIf(esp = "", System.DBNull.Value, esp)
            If sFieldCat > "" Then oRow(sFieldCat) = IIf(cat = "", System.DBNull.Value, cat)
            If sFieldEng > "" Then oRow(sFieldEng) = IIf(eng = "", System.DBNull.Value, eng)
            If sFieldPor > "" Then oRow(sFieldPor) = IIf(por = "", System.DBNull.Value, por)
        End If
    End Sub

    Shared Function NullableEan(oEan As DTOEan) As Object
        Dim retval As Object = System.DBNull.Value
        If oEan IsNot Nothing Then
            If oEan.Value > "" Then
                retval = oEan.Value
            End If
        End If
        Return retval
    End Function

    Shared Function NullableAmt(src As DTOAmt) As Object
        Dim retval As Object
        If src Is Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src.Eur
        End If
        Return retval
    End Function

    Shared Sub SetNullableAmt(src As DTOAmt, oRow As DataRow, Optional EurField As String = "Eur", Optional CurField As String = "Cur", Optional ValField As String = "Val")
        If src Is Nothing Then
            If FieldExists(oRow, EurField) Then oRow(EurField) = System.DBNull.Value
            If FieldExists(oRow, ValField) Then oRow(ValField) = System.DBNull.Value
            If FieldExists(oRow, CurField) Then oRow(CurField) = System.DBNull.Value
        Else
            With src
                If FieldExists(oRow, EurField) Then oRow(EurField) = .Eur
                If FieldExists(oRow, ValField) Then oRow(ValField) = .Val
                If FieldExists(oRow, CurField) Then
                    If .Cur IsNot Nothing Then
                        oRow(CurField) = .Cur.Tag
                    End If
                End If
            End With
        End If
    End Sub

    Shared Function NullableInt(src As Integer) As Object
        Dim retval As Object
        If src = 0 Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function

    Shared Function NullableDecimal(src As Decimal) As Object
        Dim retval As Object
        If src = 0 Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function



    Shared Function NullableDateTimeOffset(src As DateTimeOffset) As Object
        Dim retval As Object
        If src = Nothing Or src = DateTimeOffset.MinValue Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function
    Shared Function NullableFch(src As Date) As Object
        Dim retval As Object
        If src = Nothing Or src = Date.MinValue Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function

    Shared Function NullableFch2(src As Nullable(Of Date)) As Object
        Dim retval As Object
        If src Is Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function

    Shared Function NullableGuid(src As Guid) As Object
        Dim retval As Object
        If src = Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src
        End If
        Return retval
    End Function

    Shared Function NullableBaseGuid(Optional src As DTOBaseGuid = Nothing) As Object
        Dim retval As Object
        If src Is Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src.Guid
        End If
        Return retval
    End Function

    ' Shared Function NullableBaseGuid(src As DTOBaseGuid2) As Object
    ' Dim retval As Object
    ' If src Is Nothing Then
    '        retval = System.DBNull.Value
    'Else
    '       retval = src.Guid
    'End If
    'Return retval
    'End Function

    Shared Sub SetUsrLog(oUsrLog As DTOUsrLog, ByRef oRow As DataRow,
                         Optional UsrCreatedField As String = "UsrCreated",
                         Optional UsrLastEditedField As String = "UsrLastEdited",
                         Optional FchCreatedField As String = "FchCreated",
                         Optional FchLastEditedField As String = "FchLastEdited"
                         )
        With oUsrLog
            .FchLastEdited = DTO.GlobalVariables.Now()
            If .FchCreated = Nothing Then .FchCreated = .FchLastEdited
            If FieldExists(oRow, UsrCreatedField) Then oRow(UsrCreatedField) = SQLHelper.NullableBaseGuid(.UsrCreated)
            If FieldExists(oRow, UsrLastEditedField) Then oRow(UsrLastEditedField) = SQLHelper.NullableBaseGuid(.UsrLastEdited)
            If FieldExists(oRow, FchCreatedField) Then oRow(FchCreatedField) = SQLHelper.NullableFch(.FchCreated)
            If FieldExists(oRow, FchLastEditedField) Then oRow(FchLastEditedField) = DTO.GlobalVariables.Now()
        End With
    End Sub

    Shared Sub SetUsrLog(oUsrLog As DTOUsrLog2, ByRef oRow As DataRow,
                         Optional UsrCreatedField As String = "UsrCreated",
                         Optional UsrLastEditedField As String = "UsrLastEdited",
                         Optional FchCreatedField As String = "FchCreated",
                         Optional FchLastEditedField As String = "FchLastEdited"
                         )
        With oUsrLog
            .FchLastEdited = DTO.GlobalVariables.Now()
            If .FchCreated = Nothing Then .FchCreated = .FchLastEdited
            If FieldExists(oRow, UsrCreatedField) Then oRow(UsrCreatedField) = SQLHelper.NullableBaseGuid(.UsrCreated)
            If FieldExists(oRow, UsrLastEditedField) Then oRow(UsrLastEditedField) = SQLHelper.NullableBaseGuid(.UsrLastEdited)
            If FieldExists(oRow, FchCreatedField) Then oRow(FchCreatedField) = SQLHelper.NullableFch(.FchCreated)
            If FieldExists(oRow, FchLastEditedField) Then oRow(FchLastEditedField) = DTO.GlobalVariables.Now()
        End With
    End Sub

    Shared Sub SetSiiLog(oSiiLog As DTOSiiLog, ByRef oRow As DataRow,
                         Optional FieldResult As String = "SiiResult",
                         Optional FieldFch As String = "SiiFch",
                         Optional FieldCsv As String = "SiiCsv",
                         Optional FieldErr As String = "SiiErr"
                         )
        If oSiiLog IsNot Nothing Then
            With oSiiLog
                If FieldExists(oRow, FieldResult) Then oRow(FieldResult) = .Result
                If FieldExists(oRow, FieldFch) Then oRow(FieldFch) = SQLHelper.NullableFch(.Fch)
                If FieldExists(oRow, FieldCsv) Then oRow(FieldCsv) = SQLHelper.NullableString(.Csv)
                If FieldExists(oRow, FieldErr) Then oRow(FieldErr) = SQLHelper.NullableString(.ErrMsg)
            End With
        End If
    End Sub

    Shared Function NullableDocFile(src As DTODocFile) As Object
        Dim retval As Object
        If src Is Nothing Then
            retval = System.DBNull.Value
        Else
            retval = src.Hash
        End If
        Return retval
    End Function

    Shared Function FormatDatetime(Optional DtFch As Date = Nothing) As String
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Now()
        Dim retval As String = Format(DtFch, "yyyy-MM-ddTHH:mm:ss.fffZ")
        Return retval
    End Function
    Shared Function FormatDatetime2(Optional DtFch As Date = Nothing) As String
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Now()
        Dim retval As String = Format(DtFch, "yyyy-MM-ddTHH:mm:ss")
        Return retval
    End Function
End Class
