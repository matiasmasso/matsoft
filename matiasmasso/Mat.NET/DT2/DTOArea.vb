Public Class DTOArea
    Inherits DTOGuidNom

    Property Cod As Cods

    Public Enum Cods
        NotSet
        Country
        Zona
        Location
        Zip
        Adr
        Contact
        Comarca
    End Enum

    Public Enum SelectModes
        Browse
        SelectAny
        SelectCountry
        SelectZona
        SelectLocation
        SelectZip
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid, Optional sNom As String = "")
        MyBase.New(oGuid, sNom)
    End Sub

    Shared Function FromObject(oObject As Object) As DTOArea
        Dim retval As DTOArea = Nothing
        If oObject IsNot Nothing Then
            If oObject.GetType().IsSubclassOf(GetType(DTOArea)) Then
                retval = oObject
            Else
                Dim oArea As DTOArea = oObject.toobject(Of DTOArea)
                Select Case oArea.Cod
                    Case DTOArea.Cods.Contact
                        retval = oObject.toobject(Of DTOContact)
                    Case DTOArea.Cods.Adr
                        retval = oObject.toobject(Of DTOAddress)
                    Case DTOArea.Cods.Zip
                        retval = oObject.toobject(Of DTOZip)
                    Case DTOArea.Cods.Location
                        retval = oObject.toobject(Of DTOLocation)
                    Case DTOArea.Cods.Zona
                        retval = oObject.toobject(Of DTOZona)
                    Case DTOArea.Cods.Comarca
                        retval = oObject.toobject(Of DTOComarca)
                    Case DTOArea.Cods.Country
                        retval = oObject.toobject(Of DTOCountry)
                    Case Else
                        retval = oArea
                End Select
            End If
        End If
        Return retval
    End Function
    Public Function GetCod() As Cods
        Dim retval As Cods = Cods.NotSet
        If _Cod = Cods.NotSet Then
            If TypeOf Me Is DTOCountry Then
                retval = Cods.Country
            ElseIf TypeOf Me Is DTOZona Then
                retval = Cods.Zona
            ElseIf TypeOf Me Is DTOLocation Then
                retval = Cods.Location
            ElseIf TypeOf Me Is DTOContact Then
                retval = Cods.Contact
            End If
        Else
            retval = _Cod
        End If
        Return retval
    End Function

    Shared Shadows Function Factory(oGuid As Guid, oCod As DTOArea.Cods, Optional sNom As String = "") As DTOArea
        Dim retval As New DTOArea(oGuid)
        With retval
            .Cod = oCod
            .nom = sNom
        End With
        Return retval
    End Function

    Shared Function NomOrDefault(oArea As DTOArea) As String
        Dim retval As String = ""
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOCountry Then
                retval = CType(oArea, DTOCountry).langNom.Esp
            ElseIf TypeOf oArea Is DTOZona Then
                Dim oZona As DTOZona = oArea
                retval = oZona.Nom
            ElseIf TypeOf oArea Is DTOLocation Then
                retval = CType(oArea, DTOLocation).Nom
            ElseIf TypeOf oArea Is DTOContact Then
                retval = CType(oArea, DTOContact).FullNom
            End If
        End If
        Return retval
    End Function

    Shared Function FullNom(oArea As DTOArea, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang
        Dim retval As String = ""
        If TypeOf oArea Is DTOCountry Then
            retval = CType(oArea, DTOCountry).langNom.Esp
        ElseIf TypeOf oArea Is DTOZona Then
            Dim oZona As DTOZona = oArea
            retval = oZona.FullNom(oLang)
        ElseIf TypeOf oArea Is DTOLocation Then
            Dim oLocation As DTOLocation = oArea
            retval = oLocation.FullNom(oLang)
        ElseIf TypeOf oArea Is DTOZip Then
            Dim oZip As DTOZip = oArea
            retval = DTOZip.FullNomSegmented(oZip, oLang)
        ElseIf TypeOf oArea Is DTOContact Then
            retval = CType(oArea, DTOContact).FullNom
        End If
        Return retval
    End Function

    Shared Function FullNomSegmented(oArea As DTOArea, oLang As DTOLang) As String
        Dim retval As String = ""
        If TypeOf oArea Is DTOCountry Then
            Dim oCountry As DTOCountry = oArea
            retval = DTOCountry.NomTraduit(oCountry, oLang)
        ElseIf TypeOf oArea Is DTOZona Then
            Dim oZona As DTOZona = oArea
            retval = oZona.FullNomSegmented(oLang)
        ElseIf TypeOf oArea Is DTOLocation Then
            Dim oLocation As DTOLocation = oArea
            retval = DTOLocation.FullNomSegmented(oLocation, oLang)
        ElseIf TypeOf oArea Is DTOZip Then
            Dim oZip As DTOZip = oArea
            retval = DTOZip.FullNomSegmented(oZip, oLang)
        ElseIf TypeOf oArea Is DTOContact Then
            retval = CType(oArea, DTOContact).FullNom
        Else
            retval = oArea.Nom
        End If
        Return retval
    End Function

    Shared Function FullNomSegmentedReversed(oArea As DTOArea, oLang As DTOLang) As String
        Dim retval As String = ""
        If TypeOf oArea Is DTOCountry Then
            Dim oCountry As DTOCountry = oArea
            retval = DTOCountry.NomTraduit(oCountry, oLang)
        ElseIf TypeOf oArea Is DTOZona Then
            Dim oZona As DTOZona = oArea
            retval = oZona.FullNomSegmentedReversed(oLang)
        ElseIf TypeOf oArea Is DTOLocation Then
            Dim oLocation As DTOLocation = oArea
            retval = DTOLocation.FullNomSegmentedReversed(oLocation, oLang)
        ElseIf TypeOf oArea Is DTOZip Then
            Dim oZip As DTOZip = oArea
            retval = DTOZip.FullNomSegmentedReversed(oZip, oLang)
        ElseIf TypeOf oArea Is DTOContact Then
            retval = CType(oArea, DTOContact).FullNom
        End If
        Return retval
    End Function

    Shared Function IsEsp(oArea As DTOArea) As Boolean

        Dim retval As Boolean
        If oArea IsNot Nothing Then
            Dim oEsp As DTOCountry = DTOCountry.wellknown(DTOCountry.wellknowns.Spain)

            If TypeOf oArea Is DTOCountry Then
                retval = CType(oArea, DTOCountry).Equals(oEsp)
            ElseIf TypeOf oArea Is DTOZona Then
                Dim oZona As DTOZona = oArea
                retval = IsEsp(oZona.Country)
            ElseIf TypeOf oArea Is DTOLocation Then
                Dim oLocation As DTOLocation = oArea
                retval = IsEsp(oLocation.Zona)
            ElseIf TypeOf oArea Is DTOZip Then
                Dim oZip As DTOZip = oArea
                retval = IsEsp(oZip.Location)
            End If
        End If

        Return retval
    End Function

    Shared Function Country(oArea As DTOArea) As DTOCountry
        Dim retval As DTOCountry = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOCountry Then
                retval = oArea
            ElseIf TypeOf oArea Is DTOZona Then
                Dim oZona As DTOZona = oArea
                retval = oZona.Country
            ElseIf TypeOf oArea Is DTOLocation Then
                Dim oLocation As DTOLocation = oArea
                If oLocation.Zona IsNot Nothing Then
                    retval = oLocation.Zona.Country
                End If
            ElseIf TypeOf oArea Is DTOZip Then
                Dim oZip As DTOZip = oArea
                If oZip.Location IsNot Nothing Then
                    If oZip.Location.Zona IsNot Nothing Then
                        retval = oZip.Location.Zona.Country
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Provincia(oArea As DTOArea) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOAreaProvincia Then
                retval = oArea
            ElseIf TypeOf oArea Is DTOZona Then
                Dim oZona As DTOZona = oArea
                retval = oZona.Provincia
            ElseIf TypeOf oArea Is DTOLocation Then
                Dim oLocation As DTOLocation = oArea
                If oLocation.Zona IsNot Nothing Then
                    retval = oLocation.Zona.Provincia
                End If
            ElseIf TypeOf oArea Is DTOZip Then
                Dim oZip As DTOZip = oArea
                If oZip.Location IsNot Nothing Then
                    If oZip.Location.Zona IsNot Nothing Then
                        retval = oZip.Location.Zona.Provincia
                    End If
                End If
            End If
        End If
        Return retval
    End Function


    Shared Function Zona(oArea As DTOArea) As DTOZona
        Dim retval As DTOZona = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOZona Then
                retval = oArea
            ElseIf TypeOf oArea Is DTOLocation Then
                Dim oLocation As DTOLocation = oArea
                retval = oLocation.Zona
            ElseIf TypeOf oArea Is DTOZip Then
                Dim oZip As DTOZip = oArea
                If oZip.Location IsNot Nothing Then
                    retval = oZip.Location.Zona
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Location(oArea As DTOArea) As DTOLocation
        Dim retval As DTOLocation = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOLocation Then
                retval = oArea
            ElseIf TypeOf oArea Is DTOZip Then
                Dim oZip As DTOZip = oArea
                retval = oZip.Location
            End If
        End If
        Return retval
    End Function

    Shared Function Zip(oArea As DTOArea) As DTOZip
        Dim retval As DTOZip = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOZip Then
                retval = oArea
            End If
        End If
        Return retval
    End Function

    Shared Function ToArea(oArea As DTOArea) As DTOArea
        Dim retval As DTOArea = Nothing
        If oArea IsNot Nothing Then
            If TypeOf oArea Is DTOCountry Then
                retval = New DTOArea(oArea.Guid, CType(oArea, DTOCountry).langNom.Esp)
                retval.Cod = Cods.Country
            ElseIf TypeOf oArea Is DTOZona Then
                retval = New DTOArea(oArea.Guid, CType(oArea, DTOZona).Nom)
                retval.Cod = Cods.Zona
            ElseIf TypeOf oArea Is DTOLocation Then
                retval = New DTOArea(oArea.Guid, CType(oArea, DTOLocation).Nom)
                retval.Cod = Cods.Location
            ElseIf TypeOf oArea Is DTOZip Then
                retval = New DTOArea(oArea.Guid, CType(oArea, DTOZip).ZipCod)
                retval.Cod = Cods.Zip
            Else
                retval = oArea
            End If
        End If
        Return retval
    End Function

    Public Function UrlCustomersSegment() As String
        Return MyBase.UrlSegment("area/areacustomers")
    End Function
End Class
