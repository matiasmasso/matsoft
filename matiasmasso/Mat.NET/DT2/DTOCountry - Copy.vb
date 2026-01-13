Public Class DTOCountry
    Inherits DTOArea

    Property ISO As String
    'Shadows Property nom As DTOLangText

    Property langNom As DTOLangText
    Property lang As DTOLang
    Property exportCod As DTOInvoice.ExportCods
    Property prefixeTelefonic As String
    <JsonIgnore> Property flag As Image

    Property regions As List(Of DTOAreaRegio)
    Property zonas As List(Of DTOZona)
    Property zips As List(Of DTOZip)
    Property contacts As List(Of DTOContact)

    Public Enum wellknowns
        NotSet
        spain
        portugal
        andorra
        germany
    End Enum

    Public Sub New()
        MyBase.New()
        _langNom = New DTOLangText
        _zonas = New List(Of DTOZona)
        _Regions = New List(Of DTOAreaRegio)
        _Lang = DTOLang.ENG
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _langNom = New DTOLangText
        _zonas = New List(Of DTOZona)
        _Regions = New List(Of DTOAreaRegio)
        _Lang = DTOLang.ENG
    End Sub

    Public Sub New(oGuid As Guid, siso As String)
        MyBase.New(oGuid)
        _langNom = New DTOLangText
        _ISO = siso
        _Zonas = New List(Of DTOZona)
        _Lang = DTOLang.ENG
    End Sub

    Shared Shadows Function Factory(oGuid As Guid, Optional sIso As String = "")
        Dim retval As New DTOCountry(oGuid)
        retval.ISO = sIso
        Return retval
    End Function


    Shared Function wellknown(id As DTOCountry.wellknowns) As DTOCountry
        Dim retval As DTOCountry = Nothing
        Dim sGuid As String = ""
        Dim Iso As String = ""
        Select Case id
            Case DTOCountry.wellknowns.Spain
                sGuid = "AEEA6300-DE1D-4983-9AA2-61B433EE4635"
                Iso = "ES"
            Case DTOCountry.wellknowns.Portugal
                sGuid = "631B1258-9761-4254-8ED9-25B9E42FD6D1"
                Iso = "PT"
            Case DTOCountry.wellknowns.Andorra
                sGuid = "AE3E6755-8FB7-40A5-A8B3-490ED2C44061"
                Iso = "AD"
            Case DTOCountry.wellknowns.Germany
                sGuid = "B21500BA-2891-4742-8CFF-8DD65EBB0C82"
                Iso = "DE"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOCountry(oGuid)
            retval.ISO = Iso
        End If
        Return retval
    End Function

    Shared Shadows Function IsEsp(oCountry As DTOCountry) As Boolean
        Dim retval As Boolean = False
        If oCountry.ISO = "" Then
            retval = oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain))
        ElseIf oCountry.ISO = "ES" Then
            retval = True
        End If
        Return retval
    End Function

    Shared Function IsAnd(oCountry As DTOCountry) As Boolean
        Dim retval As Boolean = oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Andorra))
        Return retval
    End Function

    Shared Function IsPt(oCountry As DTOCountry) As Boolean
        Dim retval As Boolean = oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Portugal))
        Return retval
    End Function

    Shared Function Parse(src As String, allCountries As List(Of DTOCountry)) As DTOCountry
        Dim retval = allCountries.FirstOrDefault(Function(x) DTOCountry.Match(x, src))
        Return retval
    End Function

    Shared Function Match(oCountry As DTOCountry, src As String) As Boolean
        Dim retval As Boolean
        If oCountry IsNot Nothing Then
            Select Case src
                Case ""
                Case Else
                    With oCountry
                        If String.Compare(src, .ISO, True) = 0 Then
                            retval = True
                        ElseIf String.Compare(src, .langNom.Esp, True) = 0 Then
                            retval = True
                        ElseIf String.Compare(src, .langNom.Cat, True) = 0 Then
                            retval = True
                        ElseIf String.Compare(src, .langNom.Eng, True) = 0 Then
                            retval = True
                        ElseIf String.Compare(src, .langNom.Por, True) = 0 Then
                            retval = True
                        End If
                    End With
            End Select
        End If
        Return retval
    End Function

    Public Function SuggestedLang() As DTOLang
        Dim retval As DTOLang = DTOLang.ENG
        If Me.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
            retval = DTOLang.ESP
        ElseIf Me.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Andorra)) Then
            retval = DTOLang.CAT
        ElseIf Me.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Portugal)) Then
            retval = DTOLang.POR
        End If
        Return retval
    End Function

    Shared Function NomTraduit(oCountry As DTOCountry, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCountry IsNot Nothing Then
            retval = oCountry.langNom.tradueix(oLang)
        End If
        Return retval
    End Function

    Shared Function Clon(oCountries As List(Of DTOCountry), exs As List(Of Exception)) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        For Each oCountry In oCountries
            Dim oClon As New DTOCountry
            If DTOBaseGuid.CopyPropertyValues(Of DTOCountry)(oCountry, oClon, exs) Then
                retval.Add(oClon)
            End If
        Next
        Return retval
    End Function
End Class

