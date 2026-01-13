Public Class Finder

    Shared Function FindCta(oPlan As PgcPlan, sSearchKey As String) As PgcCta
        Dim retval As PgcCta = Nothing
        Dim oCtas As PgcCtas = PgcCtas.Find(oPlan, sSearchKey)
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

    Shared Function FindContact(oEmp as DTOEmp, sSearchKey As String) As Contact
        Dim retval As Contact = Nothing
        Dim oContacts As Contacts = MaxiSrvr.Contacts.Search(oEmp, sSearchKey)
        Select Case oContacts.Count
            Case 0
                MsgBox("no s'ha trobat cap contacte per '" & sSearchKey & "'", MsgBoxStyle.Exclamation)
            Case 1
                retval = oContacts(0)
            Case Else
                Dim oFrm As New Frm_Contacts_Select(oContacts)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObject
        End Select
        Return retval
    End Function

    Shared Function FindContact2(oEmp as DTOEmp, sSearchKey As String) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oContacts As List(Of DTOContact) = BLL.BLLContacts.search(oEmp, sSearchKey)
        Select Case oContacts.Count
            Case 0
                MsgBox("no s'ha trobat cap contacte per '" & sSearchKey & "'", MsgBoxStyle.Exclamation)
            Case 1
                retval = oContacts(0)
            Case Else
                Dim oFrm As New Frm_Contacts_Select(oContacts)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObject2
        End Select
        Return retval
    End Function

    Shared Function FindSku(ByVal sSearchKey As String, Optional oMgz As DTOMgz = Nothing, Optional DtFch As Date = Nothing) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oSkus As List(Of DTOProductSku) = BLL.BLLProductSkus.Search(sSearchKey, oMgz, DtFch)
        Select Case oSkus.Count
            Case 0
            Case 1
                retval = oSkus(0)
            Case Else
                Dim oFrm As New Frm_Skus_Select(oSkus)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObject
        End Select
        Return retval
    End Function

    Shared Function FindSku(oMgz As DTOMgz, ByVal sSearchKey As String) As ProductSku
        Dim retval As ProductSku = Nothing
        Dim oSkus As List(Of ProductSku) = BLL_ProductSkus.Search(oMgz, sSearchKey)
        Select Case oSkus.Count
            Case 0
            Case 1
                retval = oSkus(0)
            Case Else
                Dim oFrm As New Frm_Skus_Select(oSkus)
                oFrm.ShowDialog()
                retval = oFrm.SelectedObjectOld
        End Select
        Return retval
    End Function
End Class
