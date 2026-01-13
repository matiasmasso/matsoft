Public Class GeonamesHelper
    Public Const username As String = "matiasmasso"
    Public Const rootUrl As String = "http://api.geonames.org/"

    Shared Function postalCodesUrl(sCountryIso As String, sZipCod As String) As String
        Dim retval = String.Format("{0}postalCodeLookupJSON?postalcode={2}&country={3}&username={1}", rootUrl, username, sZipCod, sCountryIso)
        Return retval
    End Function

    Public Class request
        Public Property postalCodes As List(Of postalCode)
    End Class

    Public Class postalCode
        Property countryCode As String
        Property postalCode As String
        Property placeName As String

        Property adminCode1 As String
        Property adminName1 As String
        Property adminCode2 As String
        Property adminName2 As String
        Property adminCode3 As String
        Property adminName3 As String
        Property lat As Decimal
        Property lng As Decimal
    End Class

#Region "ExcelImport"


    'http://download.geonames.org/export/zip/
    'The data format is tab-delimited text in utf8 encoding, with the following fields :

    'country code      : iso country code, 2 characters
    'postal code       : varchar(20)
    'place name        : varchar(180)
    'admin name1       : 1. order subdivision (state) varchar(100)
    'admin code1       : 1. order subdivision (state) varchar(20)
    'admin name2       : 2. order subdivision (county/province) varchar(100)
    'admin code2       : 2. order subdivision (county/province) varchar(20)
    'admin name3       : 3. order subdivision (community) varchar(100)
    'admin code3       : 3. order subdivision (community) varchar(20)
    'latitude          : estimated latitude (wgs84)
    'longitude         : estimated longitude (wgs84)
    'accuracy          : accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape

    Shared Function ReadFromExcel(oSheet As ExcelHelper.Sheet) As List(Of GeonamesHelper.Zip)
        Dim retval As New List(Of GeonamesHelper.Zip)
        For Each oRow In oSheet.Rows
            Dim item As New Zip
            With item
                .CountryISO = oRow.Cells(0).Content
                .ZipCod = oRow.Cells(1).Content
                .LocationNom = oRow.Cells(2).Content
                .AddArea(oRow, 3, 4)
                .AddArea(oRow, 5, 6)
                .AddArea(oRow, 7, 8)
                .longitud = oRow.Cells(9).Content
                .latitud = oRow.Cells(10).Content
                .Accuracy = oRow.Cells(11).Content
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    Public Class Zip
        Property CountryISO As String
        Property ZipCod As String
        Property LocationNom As String
        Property Areas As List(Of Area)
        Property latitud As Decimal
        Property longitud As Decimal
        Property Accuracy As Accuracies

        Public Enum Accuracies
            estimated = 1
            geonameid = 4
            shape = 6
        End Enum

        Public Sub New()
            MyBase.New
            _Areas = New List(Of Area)
        End Sub

        Public Sub AddArea(oRow As MatHelper.ExcelHelper.Row, NomCellIdx As Integer, CodCellIdx As Integer)
            Dim oArea As New Area
            With oArea
                .Level = _Areas.Count + 1
                .Nom = oRow.Cells(NomCellIdx).Content
                .Cod = oRow.Cells(CodCellIdx).Content
            End With
            _Areas.Add(oArea)
        End Sub
    End Class

    Public Class Area
        Property Nom As String
        Property Cod As String
        Property Level As Integer

    End Class
#End Region
End Class
