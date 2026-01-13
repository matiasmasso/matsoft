Public Class IbanStructure

    Shared Function Find(oCountry As DTOCountry) As DTOIban.Structure
        Dim retval As DTOIban.Structure = IbanStructureLoader.Find(oCountry)
        Return retval
    End Function

    Shared Function Find(ISO As String) As DTOIban.Structure
        Dim oCountry As DTOCountry = CountryLoader.Find(ISO)
        Dim retval As DTOIban.Structure = IbanStructureLoader.Find(oCountry)
        Return retval
    End Function

    Shared Function FindOrDefault(oCountry As DTOCountry) As DTOIban.Structure
        Dim retval As DTOIban.Structure = IbanStructureLoader.Find(oCountry)
        If retval Is Nothing Then
            retval = New DTOIban.Structure
            With retval
                .Country = oCountry
                .IsNew = True
            End With
        End If
        Return retval
    End Function

    Shared Function Load(oIbanStructure As DTOIban.Structure) As Boolean
        Dim retval As Boolean = IbanStructureLoader.Load(oIbanStructure)
        Return retval
    End Function

    Shared Function Update(oIbanStructure As DTOIban.Structure, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IbanStructureLoader.Update(oIbanStructure, exs)
        Return retval
    End Function

    Shared Function Delete(oIbanStructure As DTOIban.Structure, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IbanStructureLoader.Delete(oIbanStructure, exs)
        Return retval
    End Function

    Shared Function Formatted(oIban As DTOIban) As String
        Dim retval As String
        IbanLoader.Load(oIban)
        Dim sCleanCcc As String = DTOIban.Structure.CleanCcc(oIban.Digits)
        If sCleanCcc.Length >= 4 Then
            Dim oIbanStructure As DTOIban.Structure = oIban.IbanStructure
            If oIbanStructure Is Nothing Then
                retval = sCleanCcc
            Else
                Dim Segments As New List(Of String)
                Segments.Add(sCleanCcc.Substring(0, 4))
                If sCleanCcc.Length >= oIbanStructure.BankPosition + oIbanStructure.BankLength Then
                    Segments.Add(sCleanCcc.Substring(oIbanStructure.BankPosition, oIbanStructure.BankLength))
                    If sCleanCcc.Length >= oIbanStructure.BranchPosition + oIbanStructure.BranchLength Then
                        If oIbanStructure.BranchLength > 0 Then
                            Segments.Add(sCleanCcc.Substring(oIbanStructure.BranchPosition, oIbanStructure.BranchLength))
                        End If
                        If oIbanStructure.CheckDigitsLength > 0 Then
                            If oIbanStructure.AccountPosition > oIbanStructure.CheckDigitsPosition Then
                                If sCleanCcc.Length >= oIbanStructure.CheckDigitsPosition + oIbanStructure.CheckDigitsLength Then
                                    Segments.Add(sCleanCcc.Substring(oIbanStructure.CheckDigitsPosition, oIbanStructure.CheckDigitsLength))
                                    If sCleanCcc.Length >= oIbanStructure.AccountPosition + oIbanStructure.AccountLength Then
                                        Segments.Add(sCleanCcc.Substring(oIbanStructure.AccountPosition, oIbanStructure.AccountLength))
                                    Else
                                        Segments.Add(sCleanCcc.Substring(oIbanStructure.AccountPosition))
                                    End If
                                Else
                                    Segments.Add(sCleanCcc.Substring(oIbanStructure.CheckDigitsPosition))
                                End If
                            Else
                                If sCleanCcc.Length >= oIbanStructure.AccountPosition + oIbanStructure.AccountLength Then
                                    Segments.Add(sCleanCcc.Substring(oIbanStructure.AccountPosition, oIbanStructure.AccountLength))
                                    If sCleanCcc.Length >= oIbanStructure.CheckDigitsPosition + oIbanStructure.CheckDigitsLength Then
                                        Segments.Add(sCleanCcc.Substring(oIbanStructure.CheckDigitsPosition, oIbanStructure.CheckDigitsLength))
                                    Else
                                        Segments.Add(sCleanCcc.Substring(oIbanStructure.CheckDigitsPosition))
                                    End If
                                Else
                                    Segments.Add(sCleanCcc.Substring(oIbanStructure.AccountPosition))
                                End If
                            End If
                        Else
                            Segments.Add(sCleanCcc.Substring(oIbanStructure.AccountPosition))
                        End If
                    Else
                        Segments.Add(sCleanCcc.Substring(oIbanStructure.BranchPosition))
                    End If
                Else
                    Segments.Add(sCleanCcc.Substring(oIbanStructure.BankPosition))
                End If
                retval = String.Join(".", Segments)
            End If
        Else
            retval = sCleanCcc
        End If
        Return retval
    End Function


    Shared Function Validate(oIban As DTOIban) As Boolean
        Return Validate(oIban.Digits)
    End Function

    Shared Function Validate(src As String) As Boolean
        Dim retval As Boolean
        src = DTOIban.Structure.CleanCcc(src)

        If src > "" Then
            If src.Length > 2 Then
                Dim sISO As String = src.Substring(0, 2).ToUpper
                Select Case sISO
                    Case "ES"
                        If src.Length = 24 Then
                            Dim peso(10) As Integer, i%, c%, t%, s$
                            Dim Str As String
                            Dim iCheckDigit1 As Integer
                            Dim iCheckDigit2 As Integer


                            peso(1) = 6
                            peso(2) = 3
                            peso(3) = 7
                            peso(4) = 9
                            peso(5) = 10
                            peso(6) = 5
                            peso(7) = 8
                            peso(8) = 4
                            peso(9) = 2
                            peso(10) = 1

                            'calcula el primer digit segons banc i agencia
                            Dim sBankCod As String = src.Substring(4, 4)
                            Dim sBranchCod As String = src.Substring(8, 4)
                            Str = sBankCod & sBranchCod
                            t = 0
                            For i% = 1 To 8
                                s = Mid$(Str, 9 - i%, 1)
                                If s < "0" Or s > "9" Then Return False
                                c = Val(s)
                                t = t + c * peso(i%)
                            Next i%

                            iCheckDigit1 = (11 - (t Mod 11))
                            If iCheckDigit1 = 10 Then iCheckDigit1 = 1
                            If iCheckDigit1 = 11 Then iCheckDigit1 = 0

                            'calcula el segon digit segons numero de compte
                            Str = src.Substring(14, 10)
                            t = 0
                            For i% = 1 To 10
                                c = Val(Mid$(Str, 11 - i%, 1))
                                t = t + c * peso(i%)
                            Next i%

                            iCheckDigit2 = (11 - (t Mod 11))
                            If iCheckDigit2 = 10 Then iCheckDigit2 = 1
                            If iCheckDigit2 = 11 Then iCheckDigit2 = 0

                            'verifica que els digits calculats son iguals que els existents
                            Dim sCalculatedCheckDigits As String = CStr(iCheckDigit1) & CStr(iCheckDigit2)
                            Dim CurrentDigits As String = src.Substring(12, 2)
                            retval = (CurrentDigits = sCalculatedCheckDigits)
                        End If
                    Case Else
                        Dim oCountry As New DTOCountry(Guid.Empty)
                        oCountry.ISO = sISO
                        Dim oIbanStructure As DTOIban.Structure = Find(oCountry)
                        If oIbanStructure Is Nothing Then
                            retval = True
                        Else
                            retval = src.Length = oIbanStructure.OverallLength
                        End If
                End Select
            End If
        End If
        Return retval
    End Function


    Shared Function GetCountryISO(sIbanDigits As String) As String
        Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
        If IsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If
        Dim retval As String = ""
        If src.Length >= 2 And Not IsNumeric(src) Then
            retval = src.Substring(0, 2)
        End If
        Return retval
    End Function

    Shared Function GetCountry(sIbanDigits As String) As DTOCountry
        Dim retval As DTOCountry = Nothing
        Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
        If src.Length >= 2 And Not IsNumeric(src) Then
            If IsNumeric(src) And src.Length = 20 Then
                src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
            End If

            retval = New DTOCountry
            retval.ISO = src.Substring(0, 2)
        End If
        Return retval
    End Function

    Shared Function GetBankId(sIbanDigits As String) As String
        Dim retval As String = ""
        Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
        If IsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not IsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure As DTOIban.Structure = Find(sPaisDigits)
            retval = BankDigits(oStructure, src)
        End If
        Return retval
    End Function

    Shared Function GetBank(sIbanDigits As String) As DTOBank
        Dim retval As DTOBank = Nothing
        Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
        If IsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not IsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure As DTOIban.Structure = Find(sPaisDigits)
            If oStructure IsNot Nothing Then
                Dim sBankDigits As String = BankDigits(oStructure, src)
                retval = BankLoader.FromCodi(sPaisDigits, sBankDigits)
            End If
        End If
        Return retval
    End Function

    Shared Function GetBank(oIban As DTOIban) As DTOBank
        Dim retval As DTOBank = Nothing
        IbanLoader.Load(oIban)
        Dim oStructure As DTOIban.Structure = oIban.IbanStructure
        If oStructure IsNot Nothing Then
            Dim BankId As String = oIban.Digits.Substring(oStructure.BankPosition, oStructure.BankLength)
            retval = BankLoader.Find(oStructure.Country, BankId)
        End If
        Return retval
    End Function

    Shared Function GetBankBranch(oIban As DTOIban) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing
        IbanLoader.Load(oIban)
        Dim oStructure As DTOIban.Structure = oIban.ibanStructure
        If oStructure IsNot Nothing Then
            Dim BankId As String = oIban.Digits.Substring(oStructure.BankPosition, oStructure.BankLength)
            Dim BranchId As String = oIban.Digits.Substring(oStructure.BranchPosition, oStructure.BranchLength)
            retval = BankBranchLoader.Find(oStructure.Country, BankId, BranchId)
        End If
        Return retval
    End Function



    Shared Function GetBankBranchId(sIbanDigits As String) As String
        Dim retval As String = ""
        Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
        If IsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not IsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure As DTOIban.Structure = Find(sPaisDigits)
            If oStructure IsNot Nothing Then
                retval = BranchDigits(oStructure, src)
            End If
        End If
        Return retval
    End Function

    Shared Function GetBankBranch(sIbanDigits As String) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing
        If sIbanDigits > "" Then
            Dim src As String = DTOIban.Structure.CleanCcc(sIbanDigits)
            If IsNumeric(src) And src.Length = 20 Then
                src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
            End If

            If src.Length >= 2 And Not IsNumeric(src) Then
                Dim sPaisDigits As String = src.Substring(0, 2)
                Dim oStructure As DTOIban.Structure = Find(sPaisDigits)
                If oStructure IsNot Nothing Then
                    Dim sBankDigits As String = BankDigits(oStructure, src)
                    Dim sBranchDigits As String = BranchDigits(oStructure, src)

                    retval = BankBranchLoader.FromCodi(sPaisDigits, sBankDigits, sBranchDigits)

                End If
            End If
        End If
        Return retval
    End Function


    Shared Function BankDigits(oStructure As DTOIban.Structure, sIBANDigits As String) As String
        Dim iPos As Integer = oStructure.BankPosition
        Dim iLen As Integer = oStructure.BankLength
        Dim retval As String = ""
        If sIBANDigits.Length >= iPos + iLen Then
            retval = sIBANDigits.Substring(iPos, iLen)
        End If
        Return retval
    End Function

    Shared Function BranchDigits(oStructure As DTOIban.Structure, sIBANDigits As String) As String
        Dim iPos As Integer = oStructure.BranchPosition
        Dim iLen As Integer = oStructure.BranchLength
        Dim retval As String = ""
        If sIBANDigits.Length >= iPos + iLen Then
            retval = sIBANDigits.Substring(iPos, iLen)
        End If
        Return retval
    End Function
End Class



Public Class IbanStructures
    Shared Function All() As List(Of DTOIban.Structure)
        Dim retval As List(Of DTOIban.Structure) = IbanStructuresLoader.All
        Return retval
    End Function
End Class

