Public Class DTOCliApertura
    Inherits DTOBaseGuid

    Property Nom As String
    Property RaoSocial As String
    Property NomComercial As String
    Property Nif As String
    Property Adr As String
    Property Zip As String
    Property Cit As String
    Property Zona As DTOZona
    Property Tel As String
    Property Email As String
    Property Web As String
    Property ContactClass As DTOContactClass
    Property CodSuperficie As CodsSuperficie = CodsSuperficie.NotSet
    Property CodVolumen As CodsVolumen = CodsVolumen.NotSet
    Property SharePuericultura As Integer
    Property OtherShares As String
    Property CodSalePoint As CodsSalePoint = CodsSalePoint.NotSet
    Property Associacions As String
    Property CodAntiguedad As CodsAntiguedad = CodsAntiguedad.NotSet
    Property FchApertura As Date
    Property CodExperiencia As CodsExperiencia = CodsExperiencia.NotSet
    Property Brands As List(Of DTOGuidNom)
    Property OtherBrands As String
    Property Obs As String
    Property FchCreated As Date
    Property CodTancament As CodsTancament = CodsTancament.StandBy
    Property Contact As DTOContact
    Property RepObs As String

    Public Enum CodsSuperficie
        NotSet
        lt50
        from50to100
        from100to200
        from200to300
        gt300
    End Enum

    Public Enum CodsVolumen
        NotSet
        lt50K
        from50to100
        from100to300
        from300to600
        gt600k
    End Enum

    Public Enum CodsSalePoint
        NotSet
        [Single]
        Twin
        ThreeOrMore
    End Enum

    Public Enum CodsAntiguedad
        NotSet
        EnEstudio
        EnEjecucion
        Lt1year
        From1to3Years
        Gt3Years
    End Enum

    Public Enum CodsExperiencia
        NotSet
        PrimeraExperiencia
        VieneDeOtroSector
        HaTrabajadoYaEnElSector
    End Enum

    Public Enum CodsTancament
        StandBy
        Visitat
        ClientNou
        Cancelled
    End Enum

    Public Sub New()
        MyBase.New()
        _Brands = New List(Of DTOGuidNom)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function Url(Optional absoluteUrl As Boolean = False) As String
        Return DTOWebDomain.Default(absoluteUrl).Url("apertura", MyBase.Guid.ToString)
    End Function

    Public Function StatusLabel(oLang As DTOLang) As String
        Return DTOCliApertura.StatusLabel(_CodTancament, oLang)
    End Function

    Shared Function StatusLabel(oStatus As DTOCliApertura.CodsTancament, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oStatus
            Case DTOCliApertura.CodsTancament.Cancelled
                retval = oLang.Tradueix("Cancelado", "Cancel·lat", "Cancelled")
            Case DTOCliApertura.CodsTancament.ClientNou
                retval = oLang.Tradueix("Completado", "Complert", "Completed")
            Case DTOCliApertura.CodsTancament.StandBy
                retval = oLang.Tradueix("A la espera", "A la espera", "Waiting")
            Case DTOCliApertura.CodsTancament.Visitat
                retval = oLang.Tradueix("Visitado", "Visitat", "Visited")
        End Select
        Return retval
    End Function

    Shared Function CodSuperficieText(oCodSuperficie As DTOCliApertura.CodsSuperficie, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oCodSuperficie
            Case DTOCliApertura.CodsSuperficie.lt50
                retval = oLang.Tradueix("menos de 50 m2", "menys de 50 m2", "less than 50m2", "menos de 50 m2")
            Case DTOCliApertura.CodsSuperficie.from50to100
                retval = oLang.Tradueix("entre 50 y 100 m2", "entre 50 i 100 m2", "from 50 to 100 m2", "entre 50 e 100 m2")
            Case DTOCliApertura.CodsSuperficie.from100to200
                retval = oLang.Tradueix("entre 100 y 200 m2", "entre 100 i 200 m2", "from 100 to 200 m2", "entre 100 e 200 m2")
            Case DTOCliApertura.CodsSuperficie.from200to300
                retval = oLang.Tradueix("entre 200 y 300 m2", "entre 200 i 300 m2", "from 200 to 300 m2", "entre 200 e 300 m2")
            Case DTOCliApertura.CodsSuperficie.gt300
                retval = oLang.Tradueix("más de 300 m2", "més de 300 m2", "more than 300 m2", "mais de 300 m2")
        End Select
        Return retval
    End Function

    Public Function CodSuperficieText(oLang As DTOLang) As String
        Return CodSuperficieText(_CodSuperficie, oLang)
    End Function

    Shared Function CodVolumenText(oCodVolumen As DTOCliApertura.CodsVolumen, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oCodVolumen
            Case DTOCliApertura.CodsVolumen.lt50K
                retval = oLang.Tradueix("menos de 50.000,00 €", "menys de 50.000,00 €", "less than 50.000,00 €", "menos de 50.000,00 €")
            Case DTOCliApertura.CodsVolumen.from50to100
                retval = oLang.Tradueix("entre 50.000,00 € y 100.000,00 €", "entre 50.000,00 € i 100.000,00 €", "from 50.000,00 € to 100.000,00 €", "entre 50.000,00 € e 100.000,00 €")
            Case DTOCliApertura.CodsVolumen.from100to300
                retval = oLang.Tradueix("entre 100.000,00 € y 300.000,00 €", "entre 100.000,00 € i 300.000,00 €", "from 100.000,00 € to 300.000,00 €", "entre 100.000,00 € e 300.000,00 €")
            Case DTOCliApertura.CodsVolumen.from300to600
                retval = oLang.Tradueix("entre 300.000,00 € y 600.000,00 €", "entre 300.000,00 € i 600.000,00 €", "from 300.000,00 € to 600.000,00 €", "entre 300.000,00 € e 600.000,00 €")
            Case DTOCliApertura.CodsVolumen.gt600k
                retval = oLang.Tradueix("más de 600.000,00 €", "més de 600.000,00 €", "more than 600.000,00 €", "mais de 600.000,00 €")
        End Select
        Return retval
    End Function

    Public Function CodVolumenText(oLang As DTOLang) As String
        Return CodVolumenText(_CodVolumen, oLang)
    End Function


    Shared Function CodAntiguedadText(oCodAntiguedad As DTOCliApertura.CodsAntiguedad, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oCodAntiguedad
            Case DTOCliApertura.CodsAntiguedad.EnEstudio
                retval = oLang.Tradueix("en proyecto. Estoy estudiando aun su viabilidad", "en projecte. Estic estudiant la seva viabilitat", "In project. Still evaluating viability", "em projeto. Estou a estudar a viabilidade")
            Case DTOCliApertura.CodsAntiguedad.EnEjecucion
                retval = oLang.Tradueix("en ejecución. Con fecha de inauguración en un plazo concreto", "en execució, amb inauguració en termini fixat", "on execution, with fixed launching date", "em execução. Com data de inauguração num prazo concreto")
            Case DTOCliApertura.CodsAntiguedad.Lt1year
                retval = oLang.Tradueix("menos de un año en funcionamiento", "menys de un any en funcionament", "less than 1 year activity", "menos de um ano em funcionamento")
            Case DTOCliApertura.CodsAntiguedad.From1to3Years
                retval = oLang.Tradueix("entre uno y tres años", "entre un i tres anys", "between 1 and 3 years", "entre um e três anos")
            Case DTOCliApertura.CodsAntiguedad.Gt3Years
                retval = oLang.Tradueix("más de 3 años", "més de tres anys", "more than 3 years", "mais de 3 anos")
        End Select
        Return retval
    End Function

    Public Function CodAntiguedadText(oLang As DTOLang) As String
        Return CodAntiguedadText(_CodAntiguedad, oLang)
    End Function


    Shared Function CodSalePointText(oCodSalePoint As DTOCliApertura.CodsSalePoint, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oCodSalePoint
            Case DTOCliApertura.CodsSalePoint.Single
                retval = oLang.Tradueix("uno solo", "un solsament", "just one", "só um")
            Case DTOCliApertura.CodsSalePoint.Twin
                retval = oLang.Tradueix("dos", "dos", "two", "dois")
            Case DTOCliApertura.CodsSalePoint.ThreeOrMore
                retval = oLang.Tradueix("tres o más", "tres o més", "three or more", "três ou mais")
        End Select
        Return retval
    End Function

    Public Function CodSalePointText(oLang As DTOLang) As String
        Return CodSalePointText(_CodSalePoint, oLang)
    End Function

    Shared Function CodExperienciaText(oCodExperiencia As DTOCliApertura.CodsExperiencia, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oCodExperiencia
            Case DTOCliApertura.CodsExperiencia.PrimeraExperiencia
                retval = oLang.Tradueix("es mi primera experiencia comercial", "es la meva primera experiencia comercial", "this is my first retail experience", "é a minha primeira experiência comercial")
            Case DTOCliApertura.CodsExperiencia.VieneDeOtroSector
                retval = oLang.Tradueix("tengo experiencia comercial de otros sectores", "tinc experiencia comercial en altres sectors", "I've got commercial experience on alternative fields", "tenho experiência comercial de outros sectores")
            Case DTOCliApertura.CodsExperiencia.HaTrabajadoYaEnElSector
                retval = oLang.Tradueix("he trabajado anteriormente en el sector de la puericultura", "he treballat anteriorment al sector de la puericultura", "I've already been working on child care field", "trabalhei anteriormente no sector da puericultura")
        End Select
        Return retval
    End Function

    Public Function CodExperienciaText(oLang As DTOLang) As String
        Return CodExperienciaText(_CodExperiencia, oLang)
    End Function

    Public Function FullLocation() As String
        Dim sb As New Text.StringBuilder
        If Not String.IsNullOrEmpty(_Cit) Then
            sb.Append(_Cit)
        End If
        If _Zona IsNot Nothing Then
            If _Cit <> _Zona.Nom Then
                sb.Append(" (" & _Zona.Nom & ")")
            End If
        End If
        If Not String.IsNullOrEmpty(_Adr) Then
            sb.Append(" - " & _Adr)
        End If
        Dim textInfo = (New System.Globalization.CultureInfo("es-ES", False)).TextInfo
        Dim retval As String = textInfo.ToTitleCase(sb.ToString)
        Return retval
    End Function

    Public Function FullNom() As String
        Dim sb As New Text.StringBuilder
        If Not String.IsNullOrEmpty(_Nom) Then
            sb.Append(_Nom)
        End If
        If Not String.IsNullOrEmpty(_RaoSocial) Then
            If _RaoSocial <> _Nom Then
                sb.Append(" " & _RaoSocial)
            End If
        End If
        If Not String.IsNullOrEmpty(_NomComercial) Then
            If _NomComercial <> _RaoSocial And _NomComercial <> _Nom Then
                sb.Append(" " & _NomComercial)
            End If
        End If
        Dim textInfo = (New System.Globalization.CultureInfo("es-ES", False)).TextInfo
        Dim retval As String = textInfo.ToTitleCase(sb.ToString)
        Return retval
    End Function

    Public Class Collection
        Inherits List(Of DTOCliApertura)

        Public Function Open() As Collection
            Dim retval As New Collection
            retval.AddRange(MyBase.Where(Function(x) x.CodTancament = CodsTancament.StandBy))
            Return retval
        End Function
        Public Function Closed() As Collection
            Dim retval As New Collection
            retval.AddRange(MyBase.Where(Function(x) x.CodTancament <> CodsTancament.StandBy))
            Return retval
        End Function
    End Class

End Class
