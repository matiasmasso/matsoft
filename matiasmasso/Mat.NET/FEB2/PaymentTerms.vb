Public Class PaymentTerms
    Shared Function Text(oPaymentTerms As DTOPaymentTerms, ByVal oLang As DTOLang, Optional ByVal sVto As String = "") As String
        Dim retval As String = ""
        If oPaymentTerms IsNot Nothing Then
            If sVto > "" Then
                retval = DTOPaymentTerms.CfpText(oPaymentTerms.Cod, oLang) & " dia " & sVto
            Else
                retval = TextHelper.StringListToMultiline(ToList(oPaymentTerms, oLang, False))
            End If
        End If
        Return retval
    End Function

    Shared Function ToList(oPaymentTerms As DTOPaymentTerms, ByVal oLang As DTOLang, Optional ByVal IncludeBankDetails As Boolean = True) As List(Of String)
        Dim exs As New List(Of Exception)
        Dim oList As New List(Of String)
        If oPaymentTerms IsNot Nothing Then
            Dim sb As New Text.StringBuilder
            If oPaymentTerms.Cod > 0 Then
                sb.Append(DTOPaymentTerms.CfpText(oPaymentTerms.Cod, oLang) & " ")
            End If
            If oPaymentTerms.Months > 0 Then
                sb.Append(String.Format("{0} {1}", oPaymentTerms.Months * 30, oLang.Tradueix("dias", "dies", "days")))
            ElseIf oPaymentTerms.Plazos.Count > 0 Then
                For Each oPlazo In oPaymentTerms.Plazos
                    If oPlazo.Period <> oPaymentTerms.Plazos.First.Period Then sb.Append(", ")
                    sb.Append(DTOPaymentTerms.PlazoText(oPlazo, oLang))
                Next
            End If
            If oPaymentTerms.PaymentDays IsNot Nothing AndAlso oPaymentTerms.PaymentDays.Count > 0 Then
                sb.Append(", " & DTOPaymentTerms.TextDias(oPaymentTerms, oLang))
            End If
            Dim sFpg As String = sb.ToString
            If sFpg > "" Then oList.Add(sFpg)
            If oPaymentTerms.Vacaciones IsNot Nothing Then
                For Each oItm As DTOVacacion In oPaymentTerms.Vacaciones
                    oList.Add(DTOVacacion.Text(oItm, oLang))
                Next
            End If

            If IncludeBankDetails Then
                If oPaymentTerms.Iban IsNot Nothing Then
                    If FEB2.Iban.Load(oPaymentTerms.Iban, exs) Then
                        If oPaymentTerms.Iban.BankBranch IsNot Nothing Then
                            oList.Add(DTOIban.BankNom(oPaymentTerms.Iban))
                        End If
                        oList.Add(DTOIban.Formated(oPaymentTerms.Iban))
                    End If
                End If
                'If mNBanc IsNot Nothing Then
                'If mNBanc.Id <> 0 Then
                'oList.Add(DTOIban.BankNom(mNBanc.Iban))
                'oList.Add(mNBanc.Iban.Branch.Cit & "-" & mNBanc.Iban.Branch.Adr)
                'oList.Add("Iban: " & mNBanc.Iban.Formated)
                'End If
                'End If
            End If
        End If
        Return oList
    End Function

End Class
