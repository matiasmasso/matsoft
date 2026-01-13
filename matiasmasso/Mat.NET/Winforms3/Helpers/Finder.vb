Public Class Finder


    Shared Function FindCta(oAllCtas As List(Of DTOPgcCta), sSearchKey As String) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim oCtas = oAllCtas.Where(Function(x) x.id.StartsWith(sSearchKey)).ToList
        Select Case oCtas.Count
            Case 0
            Case 1
                retval = oCtas(0)
            Case Else
                Dim oFrm As New Frm_Ctas_Select(oCtas)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObject
        End Select
        Return retval
    End Function

    Shared Function FindCtaSync(oPlan As DTOPgcPlan, sSearchKey As String, exs As List(Of Exception)) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        If sSearchKey > "" Then

            Dim oCtas = FEB.PgcCtas.SearchSync(oPlan, sSearchKey, exs)
            Select Case oCtas.Count
                Case 0
                Case 1
                    retval = oCtas(0)
                Case Else
                    Dim oFrm As New Frm_Ctas_Select(oCtas)
                    oFrm.ShowDialog()
                    retval = oFrm.SelectedObject
            End Select
        End If
        Return retval
    End Function

    Shared Function FindContact(exs As List(Of Exception), oUser As DTOUser, sSearchKey As String, Optional SearchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As DTOContact
        Dim retval As DTOContact = Nothing
        If sSearchKey > " " Then
            Dim oContacts = FEB.Contact.SearchSync(exs, oUser, sSearchKey, SearchBy)
            If exs.Count = 0 Then
                Select Case oContacts.Count
                    Case 0
                        MsgBox("no s'ha trobat cap contacte per '" & sSearchKey & "'", MsgBoxStyle.Exclamation)
                    Case 1
                        retval = oContacts(0)
                    Case Else
                        Dim oFrm As New Frm_Contacts_Select(oContacts)
                        oFrm.ShowDialog()
                        retval = oFrm.Contact
                End Select

            End If
        End If
        Return retval
    End Function

    Shared Async Function FindContactAsync(exs As List(Of Exception), oUser As DTOUser, sSearchKey As String, Optional SearchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset, Optional toggleProgressBar As ToggleProgressBarHandler = Nothing) As Task(Of DTOContact)
        Dim retval As DTOContact = Nothing
        If sSearchKey > " " Then
            toggleProgressBar(True)
            Dim oContacts = Await FEB.Contact.Search(exs, oUser, sSearchKey, SearchBy)
            toggleProgressBar(False)
            If exs.Count = 0 Then
                Select Case oContacts.Count
                    Case 0
                        MsgBox("no s'ha trobat cap contacte per '" & sSearchKey & "'", MsgBoxStyle.Exclamation)
                    Case 1
                        retval = oContacts(0)
                    Case Else
                        Dim oFrm As New Frm_Contacts_Select(oContacts)
                        oFrm.ShowDialog()
                        retval = oFrm.Contact
                End Select

            End If
        End If
        Return retval
    End Function

    Shared Async Function FindSku(exs As List(Of Exception), oEmp As DTOEmp, ByVal sSearchKey As String, Optional oMgz As DTOMgz = Nothing, Optional DtFch As Date = Nothing) As Task(Of DTOProductSku)
        Dim retval As DTOProductSku = Nothing
        Dim oSkus = Await FEB.ProductSkus.Search(exs, sSearchKey, oEmp, oMgz, DtFch)
        If exs.Count = 0 Then
            Select Case oSkus.Count
                Case 0
                Case 1
                    retval = oSkus.First
                Case Else
                    Dim oFrm As New Frm_Skus_Select(oSkus)
                    oFrm.ShowDialog()
                    retval = oFrm.SelectedObject
            End Select
        End If
        Return retval
    End Function



    Shared Function FindSkuSync(exs As List(Of Exception), oEmp As DTOEmp, ByVal sSearchKey As String, Optional oMgz As DTOMgz = Nothing, Optional DtFch As Date = Nothing) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oSkus = FEB.ProductSkus.SearchSync(exs, sSearchKey, oEmp, oMgz, DtFch)
        Select Case oSkus.Count
            Case 0
            Case 1
                retval = oSkus.First
            Case Else
                Dim oFrm As New Frm_Skus_Select(oSkus)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObject
        End Select
        Return retval
    End Function


End Class
