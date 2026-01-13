Public Class LeadAreas
    Shared Function Consumers(oEmp As DTOEmp, oLang As DTOLang) As DTOLeadAreas
        Dim retval As DTOLeadAreas = LeadAreasLoader.Consumers(oEmp, oLang)
        Return retval
    End Function
    Shared Function Pro(oEmp As DTOEmp, oLang As DTOLang) As DTOLeadAreas
        Dim retval As DTOLeadAreas = LeadAreasLoader.Pro(oEmp, oLang)
        Return retval
    End Function

End Class
