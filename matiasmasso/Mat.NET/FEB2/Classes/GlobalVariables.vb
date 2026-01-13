Public Module GlobalVariables
    Private _Caches As List(Of Models.ClientCache)


    Public ReadOnly Property Caches As List(Of Models.ClientCache)
        Get
            If _Caches Is Nothing Then _Caches = New List(Of Models.ClientCache)
            Return _Caches
        End Get
    End Property


    Public Function Cache(oEmp As DTOEmp) As Models.ClientCache
        Dim retval = Caches.FirstOrDefault(Function(x) x.Emp.Equals(oEmp))
        If retval Is Nothing Then
            retval = New Models.ClientCache
            retval.Emp = oEmp.Trimmed()
            retval.LastUpdates = Models.ClientCache.LastUpdatesFactory()
            _Caches.Add(retval)
        End If
        Return retval
    End Function


End Module
