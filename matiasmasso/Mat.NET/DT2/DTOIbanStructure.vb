Public Class DTOIbanStructure
    Property Country As DTOCountry

    Property BankPosition As Integer
    Property BankLength As Integer
    Property BankFormat As Formats

    Property BranchPosition As Integer
    Property BranchLength As Integer
    Property BranchFormat As Formats

    Property CheckDigitsPosition As Integer
    Property CheckDigitsLength As Integer
    Property CheckDigitsFormat As Formats

    Property AccountPosition As Integer
    Property AccountLength As Integer
    Property AccountFormat As Formats


    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Enum Formats
        Numeric
        Alfanumeric
    End Enum

    Shared Function Factory(oCountry As DTOCountry) As DTOIbanStructure
        Dim retval As New DTOIbanStructure
        With retval
            .Country = oCountry
        End With
        Return retval
    End Function

    Public Function GetBankId(sDigits As String) As String
        Dim retval As String = ""
        If sDigits > "" Then
            Dim sCleanString As String = CleanCcc(sDigits)
            retval = sCleanString.Substring(_BankPosition, _BankLength)
        End If
        Return retval
    End Function

    Public Function GetBranchId(sDigits As String) As String
        Dim retval As String = ""
        If sDigits > "" Then
            Dim sCleanString As String = CleanCcc(sDigits)
            retval = sCleanString.Substring(_BranchPosition, _BranchLength)
        End If
        Return retval
    End Function

    Shared Function BankDigits(oStructure As DTOIbanStructure, sIBANDigits As String) As String
        Dim iPos As Integer = oStructure.BankPosition
        Dim iLen As Integer = oStructure.BankLength
        Dim retval As String = ""
        If sIBANDigits.Length >= iPos + iLen Then
            retval = sIBANDigits.Substring(iPos, iLen)
        End If
        Return retval
    End Function

    Shared Function BranchDigits(oStructure As DTOIbanStructure, sIBANDigits As String) As String
        Dim iPos As Integer = oStructure.BranchPosition
        Dim iLen As Integer = oStructure.BranchLength
        Dim retval As String = ""
        If sIBANDigits.Length >= iPos + iLen Then
            retval = sIBANDigits.Substring(iPos, iLen)
        End If
        Return retval
    End Function

    Shared Function CleanCcc(src As String) As String
        Dim retval As String = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper
        Return retval
    End Function

    Shared Function IbanDigitsFromEspCcc(ByVal NumeroCuenta As String) As String
        Dim retval As String = DTOIban.CleanCcc(NumeroCuenta)

        NumeroCuenta = DTOIban.CleanCcc(NumeroCuenta)
        If NumeroCuenta.Length = 20 And IsNumeric(NumeroCuenta) Then

            Dim ParteCuenta As String
            Dim ProximosNumeros As Integer

            'Módulo de los primeros 9 digitos
            ParteCuenta = String.Format("{0:00}", CInt(NumeroCuenta.Substring(0, 9)) Mod 97)

            ' Cogemos otro grupo de digitos de la cuenta
            NumeroCuenta = NumeroCuenta.Substring(9, NumeroCuenta.Length - 9)

            ' Recorremos la cuenta hasta el final
            While NumeroCuenta <> ""

                If CInt(ParteCuenta) < 10 Then
                    ProximosNumeros = 8
                Else
                    ProximosNumeros = 7
                End If

                If NumeroCuenta.Length < ProximosNumeros Then
                    ParteCuenta = ParteCuenta & NumeroCuenta
                    NumeroCuenta = ""
                Else
                    ParteCuenta = ParteCuenta & NumeroCuenta.Substring(0, ProximosNumeros)
                    NumeroCuenta = NumeroCuenta.Substring(ProximosNumeros, NumeroCuenta.Length - ProximosNumeros)
                End If

                ParteCuenta = String.Format("{0:00}", ParteCuenta Mod 97)
            End While

            retval = "ES" & String.Format("{0:00}", 98 - ParteCuenta)
        End If

        Return retval
    End Function

    Shared Function GetCountryISO(sIbanDigits As String) As String
        Dim src As String = DTOIban.CleanCcc(sIbanDigits)
        If IsNumeric(src) And src.Length = 20 Then
            src = DTOIbanStructure.IbanDigitsFromEspCcc(src)
        End If
        Dim retval As String = ""
        If src.Length >= 2 And Not IsNumeric(src) Then
            retval = src.Substring(0, 2)
        End If
        Return retval
    End Function

    Public Function OverallLength() As Integer
        Dim retval As Integer = 4 + _BankLength + _BranchLength + _CheckDigitsLength + _AccountLength
        Return retval
    End Function
End Class
