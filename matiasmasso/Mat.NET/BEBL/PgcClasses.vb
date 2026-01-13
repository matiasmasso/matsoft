Public Class PgcClass

    Shared Function Find(oGuid As Guid) As DTOPgcClass
        Dim retval As DTOPgcClass = PgcClassLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPgcClass As DTOPgcClass) As Boolean
        Dim retval As Boolean = PgcClassLoader.Load(oPgcClass)
        Return retval
    End Function

    Shared Function Update(oPgcClass As DTOPgcClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcClassLoader.Update(oPgcClass, exs)
        Return retval
    End Function

    Shared Function Delete(oPgcClass As DTOPgcClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcClassLoader.Delete(oPgcClass, exs)
        Return retval
    End Function


End Class

Public Class PgcClasses

    Shared Function All(Optional oPlan As DTOPgcPlan = Nothing) As List(Of DTOPgcClass)
        If oPlan Is Nothing Then oPlan = DTOApp.Current.PgcPlan
        Dim retval As List(Of DTOPgcClass) = PgcClassesLoader.All(oPlan)
        Return retval
    End Function

    Shared Function Tree(oEmp As DTOEmp, FromYear As Integer, Optional oPlan As DTOPgcPlan = Nothing) As List(Of DTOPgcClass)
        If oPlan Is Nothing Then oPlan = DTOApp.Current.PgcPlan
        Dim oClasses As List(Of DTOPgcClass) = PgcClassesLoader.All(oPlan)
        Dim oContent As List(Of DTOPgcClass) = PgcClassesLoader.All(oEmp, FromYear)
        For Each item In oContent
            Dim oClass = oClasses.Find(Function(x) x.Equals(item))
            If oClass IsNot Nothing Then
                oClass.Ctas = item.Ctas
            End If
        Next
        Dim retval = DTOPgcClass.Tree(oClasses)
        DTOPgcClass.SetLevels(retval)

        'Els saldos llegits de la base de dades son creditors, lo qual es correcte al Passiu i al Compte d'explotació
        'pero a l'actiu hem de plasmar saldos deutors per lo que hem de canviar el signe:
        Dim oActiu As DTOPgcClass = DTOPgcClass.TreeSearch(retval, DTOPgcClass.Cods.aA_Activo)
        DTOPgcClass.ReverseSignOnAssets(oActiu)

        DTOPgcClass.SetResultats(retval, FromYear)
        Return retval
    End Function

End Class

