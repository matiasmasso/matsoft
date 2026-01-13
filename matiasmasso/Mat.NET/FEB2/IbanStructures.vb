Public Class IbanStructure
    Inherits _FeblBase

    Shared Async Function Find(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of DTOIban.Structure)
        Return Await Api.Fetch(Of DTOIban.Structure)(exs, "IbanStructure", oCountry.Guid.ToString())
    End Function

    Shared Function FindSync(oCountry As DTOCountry, exs As List(Of Exception)) As DTOIban.Structure
        Return Api.FetchSync(Of DTOIban.Structure)(exs, "IbanStructure", oCountry.Guid.ToString())
    End Function

    Shared Async Function Find(sCountryISO As String, exs As List(Of Exception)) As Task(Of DTOIban.Structure)
        Return Await Api.Fetch(Of DTOIban.Structure)(exs, "IbanStructure/FromCountryIso", sCountryISO)
    End Function

    Shared Function FindSync(sCountryISO As String, exs As List(Of Exception)) As DTOIban.Structure
        Return Api.FetchSync(Of DTOIban.Structure)(exs, "IbanStructure/FromCountryIso", sCountryISO)
    End Function

    Shared Function Load(ByRef oIbanStructure As DTOIban.Structure, exs As List(Of Exception)) As Boolean
        If Not oIbanStructure.IsLoaded And Not oIbanStructure.IsNew Then
            Dim pIbanStructure = Api.FetchSync(Of DTOIban.Structure)(exs, "IbanStructure", oIbanStructure.Country.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIban.Structure)(pIbanStructure, oIbanStructure, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oIbanStructure As DTOIban.Structure, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOIban.Structure)(oIbanStructure, exs, "IbanStructure")
        oIbanStructure.IsNew = False
    End Function


    Shared Async Function Delete(oIbanStructure As DTOIban.Structure, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIban.Structure)(oIbanStructure, exs, "IbanStructure")
    End Function

    Shared Async Function GetBank(sIbanDigits As String, exs As List(Of Exception)) As Task(Of DTOBank)
        Dim retval As DTOBank = Nothing
        Dim src As String = DTOIban.CleanCcc(sIbanDigits)
        If TextHelper.VbIsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not TextHelper.VbIsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure = FEB2.IbanStructure.FindSync(sPaisDigits, exs)
            If oStructure IsNot Nothing Then
                Dim sBankDigits As String = DTOIban.Structure.BankDigits(oStructure, src)
                If Not String.IsNullOrEmpty(sBankDigits) Then
                    retval = Await FEB2.Bank.FromCodi(oStructure.Country, sBankDigits, exs)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function GetBank(oIban As DTOIban, exs As List(Of Exception)) As Task(Of DTOBank)
        Dim retval As DTOBank = Nothing
        If FEB2.Iban.Load(oIban, exs) Then
            Dim oStructure As DTOIban.Structure = oIban.IbanStructure
            If oStructure IsNot Nothing Then
                Dim BankId As String = oIban.Digits.Substring(oStructure.BankPosition, oStructure.BankLength)
                retval = Await FEB2.Bank.FromCodi(oStructure.Country, BankId, exs)
            End If
        End If
        Return retval
    End Function
    Shared Function GetBankId(sIbanDigits As String) As String
        Dim retval As String = ""
        Dim exs As New List(Of Exception)
        Dim src As String = DTOIban.CleanCcc(sIbanDigits)
        If TextHelper.VbIsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not TextHelper.VbIsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure = FEB2.IbanStructure.FindSync(sPaisDigits, exs)
            retval = DTOIban.Structure.BankDigits(oStructure, src)
        End If
        Return retval
    End Function

    Shared Async Function GetBankBranch(oIban As DTOIban, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Dim retval As DTOBankBranch = Nothing
        If FEB2.Iban.Load(oIban, exs) Then
            Dim oStructure As DTOIban.Structure = oIban.IbanStructure
            If oStructure IsNot Nothing Then
                Dim BankId As String = oIban.Digits.Substring(oStructure.BankPosition, oStructure.BankLength)
                Dim BranchId As String = oIban.Digits.Substring(oStructure.BranchPosition, oStructure.BranchLength)
                retval = Await FEB2.BankBranch.Find(oStructure.Country, BankId, BranchId, exs)
            End If
        End If
        Return retval
    End Function

    Shared Async Function GetBankBranch(sIbanDigits As String, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Dim retval As DTOBankBranch = Nothing
        If sIbanDigits > "" Then
            Dim src As String = DTOIban.CleanCcc(sIbanDigits)
            If TextHelper.VbIsNumeric(src) And src.Length = 20 Then
                src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
            End If

            If src.Length >= 2 And Not TextHelper.VbIsNumeric(src) Then
                Dim sPaisDigits As String = src.Substring(0, 2)
                Dim oStructure = FEB2.IbanStructure.FindSync(sPaisDigits, exs)
                If oStructure IsNot Nothing Then
                    Dim sBankDigits As String = DTOIban.Structure.BankDigits(oStructure, src)
                    Dim sBranchDigits As String = DTOIban.Structure.BranchDigits(oStructure, src)

                    retval = Await FEB2.BankBranch.Find(oStructure.Country, sBankDigits, sBranchDigits, exs)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function GetBankBranchId(sIbanDigits As String) As Task(Of String)
        Dim retval As String = ""
        Dim exs As New List(Of Exception)
        Dim src As String = DTOIban.CleanCcc(sIbanDigits)
        If TextHelper.VbIsNumeric(src) And src.Length = 20 Then
            src = DTOIban.Structure.IbanDigitsFromEspCcc(src)
        End If

        If src.Length >= 2 And Not TextHelper.VbIsNumeric(src) Then
            Dim sPaisDigits As String = src.Substring(0, 2)
            Dim oStructure As DTOIban.Structure = Await FEB2.IbanStructure.Find(sPaisDigits, exs)
            If oStructure IsNot Nothing Then
                retval = DTOIban.Structure.BranchDigits(oStructure, src)
            End If
        End If
        Return retval
    End Function

    Shared Function Formatted(oIban As DTOIban) As String
        Dim retval As String
        Dim exs As New List(Of Exception)
        FEB2.Iban.Load(oIban, exs)
        Dim sCleanCcc As String = DTOIban.CleanCcc(oIban.Digits)
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



    Shared Async Function Validate(src As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean

        src = DTOIban.CleanCcc(src)

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
                                s = TextHelper.VbMid(Str, 9 - i%, 1)
                                If s < "0" Or s > "9" Then Return False
                                c = CInt(s)
                                t = t + c * peso(i%)
                            Next i%

                            iCheckDigit1 = (11 - (t Mod 11))
                            If iCheckDigit1 = 10 Then iCheckDigit1 = 1
                            If iCheckDigit1 = 11 Then iCheckDigit1 = 0

                            'calcula el segon digit segons numero de compte
                            Str = src.Substring(14, 10)
                            t = 0
                            For i% = 1 To 10
                                c = CInt(TextHelper.VbMid(Str, 11 - i%, 1))
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
                        Dim oIbanStructure = Await FEB2.IbanStructure.Find(sISO, exs)
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
End Class

Public Class IbanStructures
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOIban.Structure))
        Return Await Api.Fetch(Of List(Of DTOIban.Structure))(exs, "IbanStructures")
    End Function

End Class
