Public Class DTOLang
    Property id As Ids

    Public Enum Ids
        NotSet
        ESP
        CAT
        ENG
        POR
        EUS
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oId As Ids)
        MyBase.New()
        _Id = oId
    End Sub

    Shared Function Factory(oId As Ids) As DTOLang
        Dim retval As New DTOLang
        retval.Id = oId
        Return retval
    End Function

    Shared Function Factory(sTag As String) As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        Dim oId As Ids = Ids.NotSet
        If [Enum].TryParse(sTag.ToUpper, oId) Then
            retval = New DTOLang(oId)
        End If
        Return retval
    End Function

    Shared Function Nom(oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oLang.Id
            Case DTOLang.Ids.ESP
                retval = oLang.Tradueix("Español", "Espanyol", "Spanish", "Espanhol")
            Case DTOLang.Ids.CAT
                retval = oLang.Tradueix("Catalán", "Catalá", "Catalan", "Catalão")
            Case DTOLang.Ids.ENG
                retval = oLang.Tradueix("Inglés", "Anglès", "English", "Inglês")
            Case DTOLang.Ids.POR
                retval = oLang.Tradueix("Portugués", "Portuguès", "Portuguese", "Português")
        End Select
        Return retval
    End Function


    Shared Function Mes(oLang As DTOLang, ByVal IntMes As Integer) As String
            Dim retval As String
            Select Case oLang.Id
                Case DTOLang.Ids.CAT
                    retval = Choose(IntMes, "Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre")
                Case DTOLang.Ids.ENG
                    retval = Choose(IntMes, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")
                Case Else
                    retval = Choose(IntMes, "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre")
            End Select
            Return retval
        End Function

        Shared Function MesAbr(oLang As DTOLang, ByVal IntMes As Integer) As String
            Dim retval As String
            Select Case oLang.Id
                Case DTOLang.Ids.CAT
                    retval = Choose(IntMes, "Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Des")
                Case DTOLang.Ids.ENG
                    retval = Choose(IntMes, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")
                Case Else
                    retval = Choose(IntMes, "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic")
            End Select
            Return retval
        End Function

        Shared Function WeekDay(oLang As DTOLang, ByVal IntDay As Integer) As String
            If IntDay = 0 Then IntDay = 7
            Dim retval As String
            Select Case oLang.Id
                Case DTOLang.Ids.CAT
                    retval = Choose(IntDay, "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte", "Diumenge")
                Case DTOLang.Ids.ENG
                    retval = Choose(IntDay, "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")
                Case Else
                    retval = Choose(IntDay, "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo")
            End Select
            Return retval
        End Function

        Shared Function WeekDay(oLang As DTOLang, ByVal DtFch As Date) As String
            Dim iDay As Integer = DtFch.DayOfWeek
            Return WeekDay(oLang, iDay)
        End Function



    Shared Function FromCulture(sCulture As String) As DTOLang
            Dim retval As DTOLang = Nothing
            Select Case sCulture
                Case "ca"
                    retval = DTOLang.CAT
                Case "en"
                    retval = DTOLang.ENG
                Case "pt"
                    retval = DTOLang.POR
                Case Else
                    retval = DTOLang.ESP
            End Select
            Return retval
        End Function




    Shared Function FactoryByLocale(value As String) As DTOLang
        Dim retval As DTOLang = Nothing
        If value.Length = 2 Then
            Select Case value
                Case "ca"
                    retval = DTOLang.CAT
                Case "en"
                    retval = DTOLang.ENG
                Case "pt"
                    retval = DTOLang.POR
                Case "es"
                    retval = DTOLang.ESP
                Case Else
                    retval = DTOLang.ESP
            End Select

        Else
            Select Case Left(value, 3)
                Case "ca_"
                    retval = DTOLang.CAT
                Case "en_"
                    retval = DTOLang.ENG
                Case "pt_"
                    retval = DTOLang.POR
                Case "es_"
                    retval = DTOLang.ESP
                Case Else
                    retval = DTOLang.ESP
            End Select
        End If
        Return retval
    End Function
    Shared Function Locale(oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oLang.Id
            Case DTOLang.Ids.CAT
                retval = "ca_ES"
            Case DTOLang.Ids.ENG
                retval = "en_US"
            Case DTOLang.Ids.POR
                retval = "pt_PT"
            Case Else
                retval = "es_ES"
        End Select
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            If TypeOf oCandidate Is DTOLang Then
                If CType(oCandidate, DTOLang).Id = _Id Then retval = True
            End If
        End If
        Return retval
    End Function

    Public ReadOnly Property Tag As String
        Get
            Dim retval As String = _Id.ToString
            Return retval
        End Get
    End Property

    Public ReadOnly Property NomEsp As String
        Get
            Dim retval As String = Tradueix("Español", "Catalán", "Inglés", "Portugués")
            Return retval
        End Get
    End Property

    Public ReadOnly Property NomCat As String
        Get
            Dim retval As String = Tradueix("Espanyol", "Català", "Anglés", "Portuguès")
            Return retval
        End Get
    End Property

    Public ReadOnly Property NomEng As String
        Get
            Dim retval As String = Tradueix("Spanish", "Catalan", "English", "Portuguese")
            Return retval
        End Get
    End Property

    Public ReadOnly Property NomPor As String
        Get
            Dim retval As String = Tradueix("Espanhol", "Catalão", "Inglês", "Português")
            Return retval
        End Get
    End Property

    Public Function Tradueix(ByVal Esp As String, Optional ByVal Cat As String = "", Optional ByVal Eng As String = "", Optional ByVal Por As String = "") As String
        Dim Str As String = Esp
        Select Case _Id
            Case Ids.CAT
                If Cat > "" Then Str = Cat
            Case Ids.ENG
                If Eng > "" Then Str = Eng
            Case Ids.POR
                If Por > "" Then Str = Por
        End Select
        Return Str
    End Function

    Public Function Format(ByVal Esp As String, ByVal Cat As String, ByVal Eng As String, ByVal Por As String, ParamArray Params() As String) As String
        Dim retval As String = String.Format(Tradueix(Esp, Cat, Eng, Por), Params)
        Return retval
    End Function

    Public Function Mes(ByVal IntMes As Integer) As String
        Dim retval As String
        Select Case _Id
            Case Ids.CAT
                retval = Choose(IntMes, "Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre")
            Case Ids.ENG
                retval = Choose(IntMes, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December")
            Case Else
                retval = Choose(IntMes, "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre")
        End Select
        Return retval
    End Function

    Public Function MesAbr(ByVal IntMes As Integer) As String
        Dim retval As String
        Select Case _Id
            Case Ids.CAT
                retval = Choose(IntMes, "Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Des")
            Case Ids.ENG
                retval = Choose(IntMes, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")
            Case Else
                retval = Choose(IntMes, "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic")
        End Select
        Return retval
    End Function

    Public Function WeekDay(ByVal IntDay As Integer) As String
        If IntDay = 0 Then IntDay = 7
        Dim retval As String
        Select Case _Id
            Case Ids.CAT
                retval = Choose(IntDay, "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte", "Diumenge")
            Case Ids.ENG
                retval = Choose(IntDay, "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")
            Case Else
                retval = Choose(IntDay, "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo")
        End Select
        Return retval
    End Function

    Public Function WeekDay(ByVal DtFch As Date) As String
        Dim iDay As Integer = DtFch.DayOfWeek
        Return WeekDay(iDay)
    End Function

    Shared Function ESP() As DTOLang
        Dim retval As New DTOLang(DTOLang.Ids.ESP)
        Return retval
    End Function

    Shared Function CAT() As DTOLang
        Dim retval As New DTOLang(DTOLang.Ids.CAT)
        Return retval
    End Function

    Shared Function ENG() As DTOLang
        Dim retval As New DTOLang(DTOLang.Ids.ENG)
        Return retval
    End Function

    Shared Function POR() As DTOLang
        Dim retval As New DTOLang(DTOLang.Ids.POR)
        Return retval
    End Function

    Shared Function PortugueseOrEsp(oLang As DTOLang) As DTOLang
        'si no es portugués, retorna espanyol
        Dim retval As DTOLang = IIf(oLang.Id = DTOLang.Ids.POR, oLang, DTOLang.ESP)
        Return retval
    End Function

    Shared Function All() As List(Of DTOLang)
        Dim retval As New List(Of DTOLang)
        With retval
            .Add(New DTOLang(Ids.ESP))
            .Add(New DTOLang(Ids.CAT))
            .Add(New DTOLang(Ids.ENG))
            .Add(New DTOLang(Ids.POR))
        End With
        Return retval
    End Function


End Class
