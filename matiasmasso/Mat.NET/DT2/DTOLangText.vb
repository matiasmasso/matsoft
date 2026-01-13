Public Class DTOLangText
    Property srcType As SrcTypes
    Property src As DTOBaseGuid
    Property esp As String = ""
    Property cat As String = ""
    Property eng As String = ""
    Property por As String = ""

    Public Enum SrcTypes
        notset
        WebMenuGroup
        WebMenuItem
        WinMenuItem
        Category
        Sku
        Noticia
        SepaText
    End Enum

    Public Sub New()
        MyBase.New
        _src = New DTOBaseGuid()
    End Sub

    Public Sub New(Optional esp As String = "", Optional cat As Object = Nothing, Optional eng As Object = Nothing, Optional por As Object = Nothing)
        MyBase.New()
        _esp = esp
        If cat IsNot Nothing Then
            If Not IsDBNull(cat) Then _Cat = cat
        End If
        If eng IsNot Nothing Then
            If Not IsDBNull(eng) Then _Eng = eng
        End If
        If por IsNot Nothing Then
            If Not IsDBNull(por) Then _Por = por
        End If
        _Src = New DTOBaseGuid()
    End Sub

    Public Function Contains(searchKey As String) As Boolean
        Dim retval As Boolean
        Dim src As String = searchKey.ToLower
        If _Esp.ToLower.Contains(src) Then
            retval = True
        ElseIf _Esp.ToLower.Contains(src) Then
            retval = True
        ElseIf _Esp.ToLower.Contains(src) Then
            retval = True
        ElseIf _Esp.ToLower.Contains(src) Then
            retval = True
        End If
        Return retval
    End Function

    Public Sub SetSrc(oSrcType As SrcTypes, oSrc As DTOBaseGuid)
        _SrcType = oSrcType
        _Src = oSrc
    End Sub

    Public Function Text(oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oLang.Id
            Case DTOLang.Ids.ESP
                retval = _Esp
            Case DTOLang.Ids.CAT
                retval = _Cat
            Case DTOLang.Ids.ENG
                retval = _Eng
            Case DTOLang.Ids.POR
                retval = _Por
        End Select
        Return retval
    End Function

    Public Sub SetText(oLang As DTOLang, text As String)
        Select Case oLang.Id
            Case DTOLang.Ids.ESP
                _Esp = text
            Case DTOLang.Ids.CAT
                _Cat = text
            Case DTOLang.Ids.ENG
                _Eng = text
            Case DTOLang.Ids.POR
                _Por = text
        End Select
    End Sub

    Public Function tradueix(oLang As DTOLang) As String
        Dim retval As String = oLang.tradueix(_Esp, _Cat, _Eng, _Por)
        Return retval
    End Function

    Public Function IsEmpty() As Boolean
        Dim retval As Boolean
        If _Esp = "" AndAlso _Cat = "" AndAlso _Eng = "" AndAlso _Por = "" Then
            retval = True
        End If
        Return retval
    End Function

    Public Function IsMultiLang() As Boolean
        Dim retval As Boolean = _Cat & _Eng & _Por > ""
        Return retval
    End Function

    Shared Function Nom(oLangText As DTOLangText) As String
        Dim retval As String = ""
        Select Case oLangText.SrcType
            Case DTOLangText.SrcTypes.WebMenuGroup
                Dim oWebMenuGroup As DTOWebMenuGroup = oLangText.Src
                retval = oWebMenuGroup.LangText.Esp
            Case DTOLangText.SrcTypes.WebMenuItem
                Dim oWebMenuItem As DTOWebMenuItem = oLangText.Src
                retval = oWebMenuItem.LangText.Esp
            Case DTOLangText.SrcTypes.WinMenuItem
                Dim oWinMenuItem As DTOWinMenuItem = oLangText.Src
                retval = oWinMenuItem.LangText.Esp
            Case DTOLangText.SrcTypes.Category
                Dim oCategory As DTOProductCategory = oLangText.Src
                retval = oCategory.Brand.Nom & " " & oCategory.Nom
            Case DTOLangText.SrcTypes.Sku
                Dim oSku As DTOProductSku = oLangText.Src
                retval = oSku.Category.Brand.Nom & " " & oSku.Category.Nom & " " & oSku.NomCurt
        End Select
        Return retval
    End Function

    Shared Sub SetText(oLangText As DTOLangText, oLang As DTOLang, text As String)
        If oLangText Is Nothing Then oLangText = New DTOLangText
        Select Case oLang.Id
            Case DTOLang.Ids.ESP
                oLangText.Esp = text
            Case DTOLang.Ids.CAT
                oLangText.Cat = text
            Case DTOLang.Ids.ENG
                oLangText.Eng = text
            Case DTOLang.Ids.POR
                oLangText.Por = text
        End Select
    End Sub

    Shared Function Replace(olangText As DTOLangText, SearchString As String, ReplacementString As String) As DTOLangText
        Dim retval As New DTOLangText
        With retval
            If olangText.Esp > "" Then
                .Esp = olangText.Esp.Replace(SearchString, ReplacementString)
            End If
            If olangText.Cat > "" Then
                .Cat = olangText.Cat.Replace(SearchString, ReplacementString)
            End If
            If olangText.Eng > "" Then
                .Eng = olangText.Eng.Replace(SearchString, ReplacementString)
            End If
            If olangText.Por > "" Then
                .Por = olangText.Por.Replace(SearchString, ReplacementString)
            End If
        End With
        Return retval
    End Function

    Public Shared Function RemoveAccents(oLangText As DTOLangText) As DTOLangText
        ' the normalization to FormD splits accented letters in accents+letters
        Dim retval As New DTOLangText
        With retval
            If oLangText.Esp > "" Then
                .Esp = TextHelper.RemoveAccents(oLangText.Esp)
            End If
            If oLangText.Cat > "" Then
                .Cat = TextHelper.RemoveAccents(oLangText.Cat)
            End If
            If oLangText.Eng > "" Then
                .Eng = TextHelper.RemoveAccents(oLangText.Eng)
            End If
            If oLangText.Por > "" Then
                .Por = TextHelper.RemoveAccents(oLangText.Por)
            End If
        End With
        Return retval
    End Function

    Public Function ToMultiline() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(_esp)
        If _cat > "" Then sb.AppendLine(_cat)
        If _eng > "" Then sb.AppendLine(_eng)
        If _por > "" Then sb.AppendLine(_por)
        Return sb.ToString
    End Function


End Class
