Public Class DTOEan
    Property value As String

    Public Enum ValidationResults
        NotSet
        Ok
        Empty
        WrongLength
        WrongCheckDigit
    End Enum



    Public Sub New(sDigits As String)
        MyBase.New()
        _Value = sDigits
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Shared Function Factory(sDigits As String) As DTOEan
        Dim retval As New DTOEan
        retval.Value = DTOEan.CleanDigits(sDigits)
        Return retval
    End Function

    Shared Function EanValue(oEan As DTOEan) As String
        Dim retval As String = ""
        If oEan IsNot Nothing Then
            retval = oEan.Value
        End If
        Return retval
    End Function

    Public Function RemoveControlDigit() As String
        Dim retval As String = _Value.Substring(0, 12)
        Return retval
    End Function

    Shared Function CleanDigits(src As String) As String
        Dim retval As String = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            If TypeOf oCandidate Is DTOEan Then
                If CType(oCandidate, DTOEan).Value = _Value Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function


    Shared Function ValidationResultString(oValidationResult As DTOEan.ValidationResults, ByVal oLang As DTOLang) As String
        Dim s As String = ""
        Select Case oValidationResult
            Case DTOEan.ValidationResults.Empty
                s = oLang.tradueix("casilla vacía", "casella buida", "empty textbox")
            Case DTOEan.ValidationResults.WrongLength
                s = oLang.tradueix("longitud invalida", "longitud invalida", "wrong length")
            Case DTOEan.ValidationResults.WrongCheckDigit
                s = oLang.tradueix("digito de control invalido", "digit de control invalid", "unvalid check digit")
            Case DTOEan.ValidationResults.Ok
                oLang.tradueix("codigo validado", "codi validat", "EAN validated")
            Case Else
                oLang.tradueix("error desconocido", "error desconegut", "unknown error")
        End Select
        Return s
    End Function

    Shared Function isValid(oEan As DTOEan) As Boolean
        Dim oValidationResult As DTOEan.ValidationResults = validate(oEan)
        Dim retval As Boolean = (oValidationResult = DTOEan.ValidationResults.Ok)
        Return retval
    End Function

    Shared Function validate(oEan As DTOEan) As DTOEan.ValidationResults
        Dim retval As DTOEan.ValidationResults = DTOEan.ValidationResults.NotSet
        If oEan Is Nothing Then
            retval = DTOEan.ValidationResults.Empty
        Else
            Select Case Len(oEan.Value)
                Case 0
                    retval = DTOEan.ValidationResults.Empty
                Case 13
                    Dim iLastDigit As Integer = Right(oEan.Value, 1)
                    Dim iCheckDigit As Integer = DTOEan.CheckDigit(oEan)
                    If iLastDigit = iCheckDigit Then
                        retval = DTOEan.ValidationResults.Ok
                    Else
                        retval = DTOEan.ValidationResults.WrongCheckDigit
                    End If
                Case Else
                    retval = DTOEan.ValidationResults.WrongLength
            End Select
        End If

        Return retval
    End Function

    Shared Function CheckDigit(oEan As DTOEan) As Integer
        Dim iOddSum As Integer
        Dim iEvenSum As Integer
        Dim iCheckDigit As Integer
        Dim sDigits As String = oEan.Value

        For i As Integer = 0 To 11
            If i Mod 2 Then
                iOddSum = iOddSum + sDigits.Substring(i, 1)
            Else
                iEvenSum = iEvenSum + sDigits.Substring(i, 1)
            End If
        Next

        'check digit is some number + (iEvenSum + iOddSum * 3) = value evenly divisible by 10
        iCheckDigit = 10 - ((iEvenSum + 3 * iOddSum) Mod 10)
        If iCheckDigit = 10 Then iCheckDigit = 0

        Return iCheckDigit
    End Function

    'Public Function Bitmap(Optional ByVal BlDrawDigits As Boolean = True, Optional iHeight As Integer = 40, Optional oBgColor As Color = Nothing) As Bitmap
    'deprecated switched to winforms.ean13helper
    'Dim retval As Bitmap = Ean13Helper.Bitmap(_Value, BlDrawDigits, iHeight, oBgColor)
    'Return Nothing
    'End Function
End Class
