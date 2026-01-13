Imports System.Text.RegularExpressions
Imports System.Globalization
Public Class NumericHelper

    Shared Function RandomNumber(ByVal MaxNumber As Integer, Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function


    Shared Function ParseDouble(candidate As String, exs As List(Of Exception)) As Double
        Dim retval As Double
        ' This way you can remove unwanted characters (anything that is not a digit, and the following symbols: ".", "-", ",")
        Dim fixedInput As String = Regex.Replace(candidate, "[^\d-,\.]", "")

        Dim indexOfDot As Integer = fixedInput.IndexOf(".")
        Dim indexOfComma As Integer = fixedInput.IndexOf(",")
        Dim cultureTestOrder As List(Of CultureInfo) = New List(Of CultureInfo)
        Dim parsingResult As Double?
        Try
            If indexOfDot > 0 And indexOfComma > 0 Then
                ' There are both the dot and the comma..let's check their order
                If indexOfDot > indexOfComma Then
                    ' The dot comes after the comma. It should be en-US like Culture
                    parsingResult = Double.Parse(fixedInput, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))
                Else
                    ' The dot comes after the comma. It should be es-ES like Culture
                    parsingResult = Double.Parse(fixedInput, NumberStyles.Number, CultureInfo.GetCultureInfo("es-ES"))
                End If
            ElseIf indexOfDot >= 0 Then
                ' There is only the dot! And it is followed by exactly two digits..it should be en-US like Culture
                parsingResult = Double.Parse(fixedInput, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"))
            ElseIf indexOfComma >= 0 Then
                ' There is only the comma! And it is followed by exactly two digits..it should be es-ES like Culture
                parsingResult = Double.Parse(fixedInput, NumberStyles.Number, CultureInfo.GetCultureInfo("es-ES"))
            End If
        Catch
        End Try

        If Not parsingResult.HasValue Then
            Try
                ' There is no dot or comma, or the parsing failed for some reason. Let's try a less specific parsing.
                parsingResult = Double.Parse(fixedInput, NumberStyles.Any, NumberFormatInfo.InvariantInfo)
            Catch ex As Exception
                exs.Add(ex)
            End Try
        End If

        If Not parsingResult.HasValue Then
            ' Conversion not possible, throw exception or do something else
            exs.Add(New Exception(String.Format("Error al convertir la cadena '{0}' a un numero", candidate)))
        Else
            ' Use parsingResult.Value
            retval = parsingResult
        End If
        Return retval
    End Function

End Class
