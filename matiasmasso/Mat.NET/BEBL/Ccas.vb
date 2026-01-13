

Public Class Cca
    Shared Function Find(oGuid As Guid) As DTOCca
        Dim retval As DTOCca = CcaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCca As DTOCca) As Boolean
        Dim retval As Boolean = CcaLoader.Load(oCca)
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, yea As Integer, num As Integer) As DTOCca
        Dim retval As DTOCca = CcaLoader.FromNum(oEmp, yea, num)
        Return retval
    End Function

    Shared Function Pdf(oCca As DTOCca) As Byte()
        Return CcaLoader.Pdf(oCca)
    End Function

    Shared Function Update(oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CcaLoader.Update(oCca, exs)
        Return retval
    End Function

    Shared Function Delete(oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CcaLoader.Delete(oCca, exs)
        Return retval
    End Function

    Shared Function IvaFchUltimaDeclaracio(oEmp As DTOEmp) As Date
        Dim RetVal As Date = CcaLoader.IvaFchUltimaDeclaracio(oEmp)
        Return RetVal
    End Function
End Class

Public Class Ccas

    Shared Function Model(oUser As DTOUser, year As Integer) As List(Of Models.Compact.Cca)
        Return CcasLoader.Model(oUser, year)
    End Function

    Shared Function Headers(oExercici As DTOExercici) As List(Of DTOCca)
        Return CcasLoader.Headers(oExercici)
    End Function

    Shared Function All(oExercici As DTOExercici, Optional OnlyIvaRelateds As Boolean = False) As List(Of DTOCca)
        Return CcasLoader.All(oExercici, , OnlyIvaRelateds)
    End Function

    Shared Function Descuadres(oExercici As DTOExercici) As List(Of DTOCca)
        Return CcasLoader.Descuadres(oExercici)
    End Function

    Shared Function Update(exs As List(Of Exception), oCcas As List(Of DTOCca)) As Boolean
        Dim retval As Boolean = CcasLoader.Update(oCcas, exs)
        Return retval
    End Function

    Shared Function LlibreDiari(oExercici As DTOExercici) As List(Of DTOCca)
        Dim retval = LlibreDiariLoader.All(oExercici)
        Return retval
    End Function

    Shared Function LlibreDiariExcel(oExercici As DTOExercici, oLang As DTOLang) As MatHelper.Excel.Sheet
        Return LlibreDiariLoader.Excel(oExercici, oLang)
    End Function

    Shared Function LlibreMajorExcel(oExercici As DTOExercici, oLang As DTOLang) As MatHelper.Excel.Sheet
        Return LlibreMajorLoader.Excel(oExercici, oLang)
    End Function


End Class
