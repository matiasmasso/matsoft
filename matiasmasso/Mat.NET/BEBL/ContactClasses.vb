Public Class ContactClass
    Shared Function Find(oGuid As Guid) As DTOContactClass
        Dim retval As DTOContactClass = ContactClassLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oContactClass As DTOContactClass) As Boolean
        Dim retval As Boolean = ContactClassLoader.Load(oContactClass)
        Return retval
    End Function

    Shared Function Update(oContactClass As DTOContactClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContactClassLoader.Update(oContactClass, exs)
        Return retval
    End Function

    Shared Function Delete(oContactClass As DTOContactClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContactClassLoader.Delete(oContactClass, exs)
        Return retval
    End Function

End Class

Public Class ContactClasses

    Shared Function All() As List(Of DTOContactClass)
        Dim retval As List(Of DTOContactClass) = ContactClassesLoader.All()
        Return retval
    End Function

    Shared Function AllWithChannel() As List(Of DTOContactClass)
        Dim retval As List(Of DTOContactClass) = ContactClassesLoader.All().Where(Function(x) x.DistributionChannel IsNot Nothing).ToList
        Return retval
    End Function
End Class
