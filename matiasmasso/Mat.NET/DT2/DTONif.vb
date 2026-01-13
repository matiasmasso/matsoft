Public Class DTONif
    Private Const _ISOpaisLetters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Private Const _AllowedLetters As String = "TRWAGMYFPDXBNJZSQVHLCKE"
    Private Const _AllowedNumbers As String = "0123456789"

    Property Value As String
    Property Country As DTOCountry
    Property Mode As Modes
    Property Type As NifTypes
    Property Dni As String
    Property Letra As String

    Public Enum Modes
        _NotSet
        NIF
        RegistreTributari
        RegistreComercial
        NumeroDeContribuente
    End Enum

    Public Enum Errors
        Ok
        Empty
        WrongLength
        WrongAlfaNum
        MisDecimaltter
        LetterUnvalid
    End Enum

    Public Enum NifTypes
        NotSet
        Unvalid
        Fisica
        Juridica
        Estranger
        EstrangerResident
    End Enum

    Shared Function Factory(ByVal src As String, Optional oCountry As DTOCountry = Nothing, Optional oMode As DTONif.Modes = DTONif.Modes._NotSet) As DTONif
        Dim retval As New DTONif
        With retval
            .Value = DTONif.CleanNif(src)
            .Country = oCountry
            .Mode = oMode
        End With
        Return retval
    End Function

    Shared Function Intl(oContact As DTOContact) As String
        Dim retval As String = ""
        If oContact IsNot Nothing AndAlso oContact.Nif > "" Then
            Dim sNif As String = oContact.Nif
            Dim oCountry = DTOAddress.Country(oContact.Address)
            If DTOCountry.IsEsp(oCountry) And Not sNif.StartsWith("ES") Then
                retval = String.Format("ES{0}", sNif)
            Else
                retval = sNif
            End If
        End If
        Return retval
    End Function

    Shared Function FullText(oContact As DTOContact) As String
        Dim retval As String = String.Format("NIF:{0}", oContact.Nif)
        If DTOAddress.Country(oContact.Address).Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Andorra)) Then
            retval = String.Format("Numero de Registre Tributari (NRT): {0:@-000000-@}", oContact.Nif)
        Else
            Dim sNifLabel As String = oContact.Lang.Tradueix("NIF", "NIF", "VAT", "NIF")
            retval = String.Format("{0}: {1}", sNifLabel, oContact.Nif)
        End If
        Return retval
    End Function

    Shared Function FullText2(oContact As DTOContact) As String
        Dim retval As String = ""
        If DTOAddress.Country(oContact.Address).Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Andorra)) Then
            retval = String.Format("Numero de Comerç: {0}", oContact.Nif2)
        End If
        Return retval
    End Function

    Shared Sub Load(ByRef oNif As DTONif, ByRef oError As DTONif.Errors)
        With oNif

            If .Type = DTONif.NifTypes.NotSet Then
                Dim iLen As Integer = .Value.Length
                If iLen > 0 Then

                    Dim FirstDigit As String = .Value.Substring(0, 1)
                    Dim LastDigit As String = .Value.Substring(iLen - 1)
                    Select Case iLen
                        Case 0
                            oError = DTONif.Errors.Empty
                            .Type = DTONif.NifTypes.Unvalid
                            Exit Sub
                        Case Is < 8
                            oError = DTONif.Errors.WrongLength
                            .Type = DTONif.NifTypes.Unvalid
                            Exit Sub
                        Case 9, 10, 11
                            Dim SecondDigit As String = .Value.Substring(1, 1)
                            If _ISOpaisLetters.Contains(FirstDigit) And _ISOpaisLetters.Contains(SecondDigit) Then
                                If .Value.Substring(0, 2) = "ES" Then
                                    .Value = .Value.Substring(2)
                                Else
                                    .Type = DTONif.NifTypes.Estranger
                                    oError = DTONif.Errors.Ok
                                    Exit Sub
                                End If
                            End If
                        Case Is > 11
                            oError = DTONif.Errors.WrongLength
                            .Type = DTONif.NifTypes.Unvalid
                            Exit Sub
                    End Select


                    Dim FirstDigitIsLetter As Boolean = _AllowedLetters.Contains(FirstDigit)
                    Dim LastDigitIsLetter As Boolean = _AllowedLetters.Contains(LastDigit)
                    If FirstDigitIsLetter Then
                        .Dni = .Value.Substring(1)
                        .Letra = FirstDigit
                        .Type = DTONif.NifTypes.Juridica
                        If CheckCIF(oNif.ToString) Then
                            oError = DTONif.Errors.Ok
                        Else
                            oError = DTONif.Errors.LetterUnvalid
                        End If
                    Else
                        If LastDigitIsLetter Then
                            .Dni = .Value.Substring(0, iLen - 1)
                            If IsNumeric(.Dni) Then
                                Dim iResto As Integer = Val(.Dni) Mod 23
                                .Letra = _AllowedLetters.Substring(iResto, 1)
                                If .Letra = LastDigit Then
                                    .Type = DTONif.NifTypes.Fisica
                                Else
                                    oError = DTONif.Errors.LetterUnvalid
                                    .Type = DTONif.NifTypes.Unvalid
                                End If
                            Else
                                oError = DTONif.Errors.WrongAlfaNum
                                .Type = DTONif.NifTypes.Unvalid
                            End If
                        Else
                            oError = DTONif.Errors.MisDecimaltter
                            .Type = DTONif.NifTypes.Unvalid
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Shared Function CheckCIF(ByVal sSource As String) As Boolean
        Dim retVal As Boolean = False

        'neteja de signes de punctuació
        Dim sCleanSource As String = ""
        Dim sSignesDePuntuacio As String = "-,. /\:;"
        Dim tmpChar As String = ""
        For i As Integer = 0 To sSource.Length - 1
            tmpChar = sSource.Substring(i, 1)
            If Not sSignesDePuntuacio.Contains(tmpChar) Then
                sCleanSource += tmpChar
            End If
        Next

        'filtre de longitud: el NIF ha de tenir 9 xifres
        Dim iLen As Integer = sSource.Length
        If iLen = 9 Then
            'validem la primera lletra
            Dim sValidFirstLetter As String = "ABCDEFHJPQSKLMRUVWX"
            Dim sFirstLetter As String = sCleanSource.Substring(0, 1)
            If sValidFirstLetter.Contains(sFirstLetter) Then
                'validem els estrangers residents
                'canviem la X inicial per un 0 i validem com si fos persona fisica
                Dim BlExtrangerResident As Boolean = sFirstLetter = "X"
                If BlExtrangerResident Then
                    Dim sEquivalentNIF As String = "0" & sCleanSource.Substring(1)
                    retVal = CheckNIF(sEquivalentNIF)
                    'oNif.Type = DTONif.NifTypes.EstrangerResident
                    Return retVal
                    Exit Function
                End If

                'descartem el primer digit (de classificació) i l'ultim (el de control)
                Dim sNucli As String = sCleanSource.Substring(1, 7)
                Dim iDigit(7) As Integer
                For i As Integer = 1 To 7
                    iDigit(i) = CInt(sNucli.Substring(i - 1, 1))
                Next

                'sumem els digits parells
                Dim iCalcParells As Integer = iDigit(2) + iDigit(4) + iDigit(6)

                'multipliquem cada xifra impar per dos, i sumem els digits dels resultats
                Dim s1 As String = Format(iDigit(1) * 2, "00")
                Dim s3 As String = Format(iDigit(3) * 2, "00")
                Dim s5 As String = Format(iDigit(5) * 2, "00")
                Dim s7 As String = Format(iDigit(7) * 2, "00")
                Dim i1 As Integer = CInt(s1.Substring(0, 1)) + CInt(s1.Substring(1, 1))
                Dim i3 As Integer = CInt(s3.Substring(0, 1)) + CInt(s3.Substring(1, 1))
                Dim i5 As Integer = CInt(s5.Substring(0, 1)) + CInt(s5.Substring(1, 1))
                Dim i7 As Integer = CInt(s7.Substring(0, 1)) + CInt(s7.Substring(1, 1))
                Dim iCalcImpars As Integer = i1 + i3 + i5 + i7

                'Sumem els calculs parell e imparell,
                'i trobem la resta de dividir-ho per deu.
                'el digit de control será la diferencia fins a deu (o zero si dona deu)
                Dim iSuma As Integer = iCalcParells + iCalcImpars
                Dim iMod As Integer = iSuma Mod 10
                Dim iDigitControl As Integer = 10 - iMod
                If iDigitControl = 10 Then iDigitControl = 0
                Dim sDigitControl As String = iDigitControl.ToString

                'Les corporacions locals tenen una lletra com a digit de control
                Dim sSourceDigitControl As String = sCleanSource.Substring(8, 1)
                Dim sCorporacions As String = "PS"
                If sCorporacions.Contains(sSourceDigitControl) Then
                    sDigitControl = Chr(iDigitControl + 64)
                End If

                retVal = (sDigitControl = sSourceDigitControl)

            End If

        End If

        Return retVal
    End Function

    Shared Function CheckNIF(ByVal sSource As String) As Boolean
        Dim retVal As Boolean = False
        Dim sCadenaDeReferencia As String = "TRWAGMYFPDXBNJZSQVHLCKE"

        'descarta la lletra final
        Dim iLen As Integer = sSource.Length
        Dim sSourceDigitControl As String = sSource.Substring(iLen - 1, 1)
        Dim sNucli As String = sSource.Substring(0, iLen - 1)

        'descarta no numerics
        Dim tmpChar As String = ""
        For i As Integer = 0 To sNucli.Length - 1
            tmpChar = sSource.Substring(i, 1)
            If Not IsNumeric(tmpChar) Then
                Return retVal
                Exit Function
            End If
        Next

        'troba la resta de la divisió per 23 
        'que será la posició de la lletra dins la cadena de referencia
        Dim iResto As Integer = Val(sNucli) Mod 23
        Dim sDigitControl As String = sCadenaDeReferencia.Substring(iResto, 1)

        retVal = (sDigitControl = sSourceDigitControl)
        Return retVal
    End Function

    Shared ReadOnly Property ValidationResult(oNif As DTONif) As DTONif.Errors
        Get
            Dim retval As DTONif.Errors
            If oNif.Type = DTONif.NifTypes.NotSet Then DTONif.Load(oNif, retval)
            Return retval
        End Get
    End Property

    Shared Function ValidationResultString(oNif As DTONif, ByVal oLang As DTOLang) As String
        Dim s As String = ""
        Select Case ValidationResult(oNif)
            Case DTONif.Errors.Empty
                s = oLang.Tradueix("casilla vacía", "casella buida", "empty textbox")
            Case DTONif.Errors.MisDecimaltter
                s = oLang.Tradueix("falta la letra", "manca la lletra", "missing letter")
            Case DTONif.Errors.WrongAlfaNum
                s = oLang.Tradueix("combinación inválida de letras y numeros", "combinació de lletres i numeros incorrecte", "wrong mix of letters and digits")
            Case DTONif.Errors.WrongLength
                s = oLang.Tradueix("longitud invalida", "longitud invalida", "wrong length")
            Case DTONif.Errors.LetterUnvalid
                s = oLang.Tradueix("letra incorrecta", "lletra incorrecte", "wrong letter")
            Case DTONif.Errors.Ok
                oLang.Tradueix("NIF validado correctamente", "NIF validad correctament", "NIF validated")
            Case Else
                oLang.Tradueix("error desconocido", "error desconegut", "unknown error")
        End Select
        Return s
    End Function


    Public Shadows Function ToString() As String
        Return _Value
    End Function

    Shared Function CleanNif(ByVal sSource As String) As String
        'deixa nomes digits i lletres i passa-les a majuscules
        Dim retval As String = TextHelper.RegexSuppress(sSource, "[^A-Za-z0-9]").ToUpper
        Return retval
    End Function
End Class
